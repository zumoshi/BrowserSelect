using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BrowserSelect
{
    public class Browser
    {
        public string name;
        public string exec;
        public Icon icon;

        public string private_arg
        {
            get
            {
                var file = exec.Split(new[] { '/', '\\' }).Last().ToLower();
                if (file.Contains("chrome") || file.Contains("chromium"))
                    return "-incognito";
                if (file.Contains("opera"))
                    return "-newprivatetab";
                if (file.Contains("iexplore"))
                    return "-private";
                if (file.Contains("edge"))
                    return "-private";
                return "-private-window";  // FF
            }
        }

        public List<char> shortcuts
        {
            get
            {
                return name.Split(new[] { ' ' }).Select(x => x.Substring(0, 1).ToLower()[0]).ToList();
            }
        }
        public override string ToString()
        {
            return name;
        }
        public static implicit operator Browser(string s)
        {
            return BrowserFinder.find().First(b => b.name == s);
        }
    }
    static class BrowserFinder
    {
        public static List<Browser> find()
        {
            List<Browser> browsers = new List<Browser>();
            //special case , firefox+firefox developer both installed
            //(only works if firefox installed in default directory)
            var ff_path = Path.Combine(
                Program.ProgramFilesx86(),
                @"Mozilla Firefox\firefox.exe");
            if (File.Exists(ff_path))
                browsers.Add(new Browser()
                {
                    name = "FireFox",
                    exec = ff_path,
                    icon = IconExtractor.fromFile(ff_path)
                });
            //special case , Edge
            var edge_path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                @"SystemApps\Microsoft.MicrosoftEdge_8wekyb3d8bbwe\MicrosoftEdge.exe");
            if (File.Exists(edge_path))
                browsers.Add(new Browser()
                {
                    name = "Edge",
                    // #34
                    exec = "shell:AppsFolder\\Microsoft.MicrosoftEdge_8wekyb3d8bbwe!MicrosoftEdge",
                    icon = IconExtractor.fromFile(edge_path)
                });

            //gather browsers from registry
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
                browsers.AddRange(find(hklm));
            using (RegistryKey hkcu = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32))
                browsers.AddRange(find(hkcu));

            //remove myself
            browsers = browsers.Where(x => Path.GetFileName(x.exec).ToLower() !=
                 Path.GetFileName(Application.ExecutablePath).ToLower()).ToList();
            //remove duplicates
            browsers = browsers.GroupBy(browser => browser.exec)
                .Select(group => group.First()).ToList();

            return browsers;
        }
        private static List<Browser> find(RegistryKey hklm)
        {
            List<Browser> browsers = new List<Browser>();
            // startmenu internet key
            RegistryKey smi = hklm.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
            if (smi != null)
                foreach (var browser in smi.GetSubKeyNames())
                {
                    try
                    {
                        var key = smi.OpenSubKey(browser);
                        var name = (string)key.GetValue(null);
                        var cmd = key.OpenSubKey("shell").OpenSubKey("open").OpenSubKey("command");
                        var exec = (string)cmd.GetValue(null);

                        // by this point if registry is missing keys we are alreay out of here
                        // because of the try catch, but there are still things that could go wrong

                        //0. check if it can handle the http protocol
                        var capabilities = key.OpenSubKey("Capabilities");
                            // IE does not have the capabilities subkey...
                            // so assume that the app can handle http if it doesn't
                            // advertise it's capablities
                        if(capabilities != null)
                            if ((string)capabilities.OpenSubKey("URLAssociations").GetValue("http") == null)
                                continue;
                        //1. check if path is not empty
                        if (string.IsNullOrWhiteSpace(exec))
                            continue;

                        //1.1. remove possible "%1" from the end
                        exec = exec.Replace("\"%1\"", "");
                        //1.2. remove possible quotes around address
                        exec = exec.Trim("\"".ToCharArray());
                        //2. check if path is valid
                        if (!File.Exists(exec))
                            continue;
                        //3. check if name is valid
                        if (string.IsNullOrWhiteSpace(name))
                            name = Path.GetFileNameWithoutExtension(exec);

                        browsers.Add(new Browser()
                        {
                            name = name,
                            exec = exec,
                            icon = IconExtractor.fromFile(exec)
                        });
                    }
                    catch (NullReferenceException)
                    {
                    } // incomplete registry record for browser, ignore it
                    catch (Exception ex)
                    {
                        // todo: log errors
                    }
                }
            return browsers;
        }
    }
}
