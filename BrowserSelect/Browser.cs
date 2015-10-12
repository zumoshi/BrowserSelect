using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BrowserSelect {
    public class Browser {
        public string name;
        public string exec;
        public Icon icon;

        public List<char> shortcuts { get { return name.Split(new[] { ' ' }).Select(x=>x.Substring(0, 1).ToLower()[0]).ToList(); } }
    }
    static class BrowserFinder {
        public static List<Browser> find() {
            List<Browser> browsers = new List<Browser>();
            //gather browsers from registry
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
                browsers.AddRange(find(hklm));
            using (RegistryKey hkcu = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32))
                browsers.AddRange(find(hkcu));
            //remove myself
            browsers=browsers.Where(x => Path.GetFileName(x.exec).ToLower() !=
                Path.GetFileName(Application.ExecutablePath).ToLower()).ToList();
            //remove duplicates
            browsers = browsers.GroupBy(browser => browser.exec)
                .Select(group => group.First()).ToList();

            return browsers;
        }
        private static List<Browser> find(RegistryKey hklm) {
            List<Browser> browsers = new List<Browser>();
            RegistryKey webClientsRootKey = hklm.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
            if (webClientsRootKey != null)
                foreach (var subKeyName in webClientsRootKey.GetSubKeyNames())
                    if (webClientsRootKey.OpenSubKey(subKeyName) != null)
                        if (webClientsRootKey.OpenSubKey(subKeyName).OpenSubKey("shell") != null)
                            if (webClientsRootKey.OpenSubKey(subKeyName).OpenSubKey("shell").OpenSubKey("open") != null)
                                if (webClientsRootKey.OpenSubKey(subKeyName).OpenSubKey("shell").OpenSubKey("open").OpenSubKey("command") != null) {
                                    string commandLineUri = (string)webClientsRootKey.OpenSubKey(subKeyName).OpenSubKey("shell").OpenSubKey("open").OpenSubKey("command").GetValue(null);
                                    if (string.IsNullOrEmpty(commandLineUri))
                                        continue;
                                    commandLineUri = commandLineUri.Trim("\"".ToCharArray());
                                    browsers.Add(new Browser() {
                                        name = (string)webClientsRootKey.OpenSubKey(subKeyName).GetValue(null),
                                        exec = commandLineUri,
                                        //icon = Icon.ExtractAssociatedIcon(commandLineUri)
                                        icon = IconExtractor.fromFile(commandLineUri)
                                    });
                                }
            return browsers;
        }
    }
}
