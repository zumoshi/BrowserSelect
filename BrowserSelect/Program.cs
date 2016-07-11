using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BrowserSelect.Properties;

namespace BrowserSelect
{
    static class Program
    {
        public static string url = "http://google.com/";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // to prevent nullreference in case settings file does not exist
            if (Settings.Default.HideBrowsers == null)
                Settings.Default.HideBrowsers = new StringCollection();
            if (Settings.Default.AutoBrowser == null)
                Settings.Default.AutoBrowser = new StringCollection();

            //checking if a url is being opened or app is ran from start menu (without arguments)
            if (args.Length > 0)
            {
                //check to see if auto select rules match
                url = args[0];
                foreach (var sr in Settings.Default.AutoBrowser.Cast<string>()
                    // maybe i should use a better way to split the pattern and browser name ?
                    .Select(x=>x.Split(new[] { "[#!][$~][?_]" }, StringSplitOptions.None))
                    // to make sure * doesn't match when non-* rules exist.
                    .OrderBy(x=> ((x[0].Contains("*"))?1:0) + (x[0]=="*"?1:0)))
                {
                    var pattern = sr[0];
                    var browser = sr[1];

                    // matching the domain to pattern
                    if (DoesDomainMatchPattern(new Uri(url).Host, pattern))
                    {
                        // ignore the display browser select entry to prevent app running itself
                        if (browser != "display BrowserSelect")
                        {
                            Form1.open_url((Browser)browser);
                            return;
                        }
                        else
                        {
                            // simply break the loop to let the app display selection dialogue
                            break;
                        }
                    }
                }
            }

            // display main form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        // http://stackoverflow.com/a/194223/1461004
        public static string ProgramFilesx86()
        {
            if (8 == IntPtr.Size
                || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }


        /// <summary>
        /// Checks if a wildcard string matches a domain
        /// taken from http://madskristensen.net/post/wildcard-search-for-domains-in-c
        /// </summary>
        public static bool DoesDomainMatchPattern(string domain, string domainToCheck)
        {
            if (domainToCheck.Contains("*"))
            {
                string checkDomain = domainToCheck;
                if (checkDomain.StartsWith("*."))
                    checkDomain = "*" + checkDomain.Substring(2, checkDomain.Length - 2);
                return DoesWildcardMatch(domain, checkDomain);
            }
            else
            {
                return domainToCheck.Equals(domain, StringComparison.OrdinalIgnoreCase);
            }
        }
        /// <summary>
        /// Performs a wildcard (*) search on any string.
        /// </summary>
        public static bool DoesWildcardMatch(string originalString, string searchString)
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

    }
}
