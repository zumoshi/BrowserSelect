using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using SHDocVw;

using BrowserSelect.Properties;

namespace BrowserSelect
{
    //=============================================================================================================
    class UrlProcessor
    //=============================================================================================================
    {
        //-------------------------------------------------------------------------------------------------------------
        public void OpenUrl(BrowserModel browser, bool incognito = false)
        //-------------------------------------------------------------------------------------------------------------
        {
            var args = new List<string>();
            if (!string.IsNullOrEmpty(browser.additionalArgs))
                args.Add(browser.additionalArgs);
            if (incognito)
                args.Add(browser.privateArg);
            if (browser.exec.ToLower().EndsWith("brave.exe"))
                args.Add("--");
            args.Add(BrowserSelectApp.url.Replace("\"", "%22"));

            if (browser.exec.EndsWith("iexplore.exe") && !incognito)
            {
                // IE tends to open in a new window instead of a new tab
                // code borrowed from http://stackoverflow.com/a/3713470/1461004
                bool found = false;
                ShellWindows iExplorerInstances = new ShellWindows();
                foreach (InternetExplorer iExplorer in iExplorerInstances)
                {
                    if (iExplorer.Name.EndsWith("Internet Explorer"))
                    {
                        iExplorer.Navigate(BrowserSelectApp.url, 0x800);
                        // for issue #10 (bring IE to focus after opening link)
                        ForegroundAgent.RestoreWindow(iExplorer.HWND);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Process.Start(browser.exec, args2Str(args));
                }
            }
            else
            {
                Process.Start(browser.exec, args2Str(args));
            }

            Application.Exit();
        }

        //-------------------------------------------------------------------------------------------------------------
        public bool ProcessUrl(Uri uri)
        //-------------------------------------------------------------------------------------------------------------
        {
            List<RuleModel> rules = new List<RuleModel>();
            foreach (var ruleSetting in Settings.Default.Rules)
                rules.Add(ruleSetting);
            //rules.Sort();

            foreach (RuleModel rule in rules)
            {
                //MessageBox.Show(uri.Host + "\n\n" + rule.Type + "\n" + rule.Pattern + "\n" + rule.Browser);

                if (rule.Type.ToUpper() == "URL")
                {
                    // matching the domain to pattern
                    if (DoesURLMatchRule(uri.Host, rule.Pattern))
                    {
                        //todo: handle the case if browser is not found (e.g. imported settings or uninstalled browser)
                        OpenUrl((BrowserModel)rule.Browser);
                        return true;
                    }
                }
                else if (rule.Type == "Process")
                {
                    Process process = ParentProcessUtilities.GetParentProcess();

                    // matching the calling process to pattern
                    if (DoesProcessMatchRule(process.ProcessName, rule.Pattern))
                    {
                        //todo: handle the case if browser is not found (e.g. imported settings or uninstalled browser)
                        OpenUrl((BrowserModel)rule.Browser);
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid rule type: " + rule.Pattern);
                }
            }

            //no matches, used the default browser (if configured)
            if (!String.IsNullOrEmpty(Settings.Default.DefaultBrowser) && Settings.Default.DefaultBrowser != "<choose with BrowserSelect>")
            {
                //MessageBox.Show("Opening with: " + Settings.Default.DefaultBrowser);
                OpenUrl((BrowserModel)Settings.Default.DefaultBrowser);
                return true;
            }

            return false;
        }

        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Checks if a wildcard string matches a domain pattern
        /// taken from http://madskristensen.net/post/wildcard-search-for-domains-in-c
        /// </summary>
        public bool DoesURLMatchRule(string url, string pattern)
        //-------------------------------------------------------------------------------------------------------------
        {
            if (pattern.Contains("*"))
            {
                string checkDomain = pattern;
                if (checkDomain.StartsWith("*."))
                {
                    checkDomain = "*" + checkDomain.Substring(2, checkDomain.Length - 2);
                }

                return DoesWildcardMatch(url, checkDomain);
            }
            else
            {
                return pattern.Equals(url, StringComparison.OrdinalIgnoreCase);
            }
        }

        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Checks if a process string matches a pattern
        /// </summary>
        public bool DoesProcessMatchRule(string process, string pattern)
        //-------------------------------------------------------------------------------------------------------------
        {
            if (pattern == "*")
            {
                return true;
            }
            else if (pattern.Contains("*"))
            {
                return process.ToUpper().Contains(pattern.ToUpper());
            }
            else
            {
                return (process.ToUpper() == pattern.ToUpper());
            }
        }

        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Performs a wildcard (*) search on any string.
        /// </summary>
        public bool DoesWildcardMatch(string originalString, string searchString)
        //-------------------------------------------------------------------------------------------------------------
        {
            if (!searchString.StartsWith("*"))
            {
                int stop = searchString.IndexOf('*');
                if (!originalString.StartsWith(searchString.Substring(0, stop)))
                    return false;
            }
            if (!searchString.EndsWith("*"))
            {
                int start = searchString.LastIndexOf('*') + 1;
                if (!originalString.EndsWith(searchString.Substring(start, searchString.Length - start)))
                    return false;
            }

            Regex regex = new Regex(searchString.Replace(@".", @"\.").Replace(@"*", @".*"));
            return regex.IsMatch(originalString);
        }

        //-------------------------------------------------------------------------------------------------------------
        private string args2Str(List<string> args)
        //-------------------------------------------------------------------------------------------------------------
        {
            return args2Str(args.ToArray());
        }

        //-------------------------------------------------------------------------------------------------------------
        private string args2Str(string[] args)
        //-------------------------------------------------------------------------------------------------------------
        {
            return string.Join(" ", args.Select(EncodeParameterArgument));
        }

        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Encodes an argument for passing into a program
        /// taken from : http://stackoverflow.com/a/12364234/1461004
        /// </summary>
        /// <param name="original">The value that should be received by the program</param>
        /// <returns>The value which needs to be passed to the program for the original value 
        /// to come through</returns>
        private string EncodeParameterArgument(string original)
        //-------------------------------------------------------------------------------------------------------------
        {
            if (string.IsNullOrEmpty(original))
                return original;

            string value = Regex.Replace(original, @"(\\*)" + "\"", @"$1\$0");
            value = Regex.Replace(value, @"^(.*\s.*?)(\\*)$", "\"$1$2$2\"");
            return value;
        }
    }
}
