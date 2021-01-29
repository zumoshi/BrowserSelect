using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using Microsoft.Win32;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using BrowserSelect.Properties;

namespace BrowserSelect
{
    //=============================================================================================================
    class BrowserFinder
    //=============================================================================================================
    {
        //-------------------------------------------------------------------------------------------------------------
        public List<BrowserModel> FindBrowsers(bool update = false)
        //-------------------------------------------------------------------------------------------------------------
        {
            List<BrowserModel> browsers = new List<BrowserModel>();
            if (Settings.Default.CachedBrowserList != "" && !update)
            {
                browsers = JsonConvert.DeserializeObject<List<BrowserModel>>(Settings.Default.CachedBrowserList);
            }
            else
            {
                //special case Edge (Classic)
                var edge_path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                    @"SystemApps\Microsoft.MicrosoftEdge_8wekyb3d8bbwe\MicrosoftEdge.exe");
                if (File.Exists(edge_path))
                    browsers.Add(new BrowserModel()
                    {
                        name = "Edge (Classic)",
                        // #34
                        exec = "shell:AppsFolder\\Microsoft.MicrosoftEdge_8wekyb3d8bbwe!MicrosoftEdge",
                        icon = icon2String(IconExtractor.fromFile(edge_path))
                    });

                //gather browsers from registry
                using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
                    browsers.AddRange(FindRegisteredBrowsers(hklm));
                using (RegistryKey hkcu = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32))
                    browsers.AddRange(FindRegisteredBrowsers(hkcu));

                //remove BrowserSelect app itself
                browsers = browsers.Where(x => Path.GetFileName(x.exec).ToLower() !=
                     Path.GetFileName(Application.ExecutablePath).ToLower()).ToList();

                //remove duplicates
                browsers = browsers.GroupBy(browser => browser.exec)
                    .Select(group => group.First()).ToList();

                List<BrowserModel> browsersToAdd = new List<BrowserModel>();
                List<BrowserModel> browsersToRemove = new List<BrowserModel>();
                //check for Edge (Chromium) profiles
                foreach (BrowserModel browser in browsers)
                {
                    if (browser.name.Contains("Microsoft Edge"))
                        if (AddChromiumProfiles(browsersToAdd, browser))
                            browsersToRemove.Add(browser);
                }

                //check for Chrome Profiles
                foreach (BrowserModel browser in browsers)
                {
                    if (browser.name.Contains("Google Chrome"))
                        if (AddChromiumProfiles(browsersToAdd, browser))
                            browsersToRemove.Add(browser);
                }

                if (browsersToAdd.Count > 0)
                {
                    browsers.AddRange(browsersToAdd);
                }

                if (browsersToRemove.Count > 0)
                {
                    foreach (BrowserModel browser in browsersToRemove)
                        browsers.Remove(browser);
                }

                //sort
                browsers.Sort();

                System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(browsers));
                Settings.Default.CachedBrowserList = JsonConvert.SerializeObject(browsers);
                Settings.Default.Save();
            }

            return browsers;
        }

        //-------------------------------------------------------------------------------------------------------------
        private bool AddChromiumProfiles(List<BrowserModel> browsers, BrowserModel browser)
        //-------------------------------------------------------------------------------------------------------------
        {
            string vendorDataFolder = browser.exec.Replace(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\", ""); //bad assumption?
            vendorDataFolder = vendorDataFolder.Replace(Path.GetFileName(browser.exec), "");
            vendorDataFolder = vendorDataFolder.Replace("\\Application", ""); //cleaner way to do this?

            string chromiumUserDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), vendorDataFolder);
            chromiumUserDataFolder += "User Data";
            List<string> chromiumProfiles = FindChromiumProfiles(chromiumUserDataFolder, browser.profileIcon);

//          if (chromiumProfiles.Count > 1)
            {
                //add the Chromium instances and remove the default one
                foreach (string profile in chromiumProfiles)
                {
                    browsers.Add(new BrowserModel()
                    {
                        name = browser.name + " (" + GetChromiumProfileName(chromiumUserDataFolder + "\\" + profile) + ")",
                        exec = browser.exec,
                        icon = icon2String(IconExtractor.fromFile(chromiumUserDataFolder + "\\" + profile + "\\" + browser.profileIcon)),
                        additionalArgs = String.Format("--profile-directory={0}", profile)
                    });
                }

                return true;
            }

//          return false;
        }

        //-------------------------------------------------------------------------------------------------------------
        private string GetChromiumProfileName(string fullProfilePath)
        //-------------------------------------------------------------------------------------------------------------
        {
            dynamic profilePreferences = JObject.Parse(File.ReadAllText(fullProfilePath + @"\Preferences"));
            return profilePreferences.profile.name;
        }

        //-------------------------------------------------------------------------------------------------------------
        private List<string> FindChromiumProfiles(string chromiumUserDataDir, string iconFilename)
        //-------------------------------------------------------------------------------------------------------------
        {
            List<string> profiles = new List<string>();
            var profileDirs = Directory.GetFiles(chromiumUserDataDir, iconFilename, SearchOption.AllDirectories);
            foreach (var profile in profileDirs)
            {
                if (!profile.Contains("Snapshots")) //new Edge Chromium feature to backup profiles across major versions
                {
                    string folder = Path.GetDirectoryName(profile);
                    profiles.Add(folder.Substring(chromiumUserDataDir.Length + 1));
                }
            }
            return profiles;
        }

        //-------------------------------------------------------------------------------------------------------------
        private List<BrowserModel> FindRegisteredBrowsers(RegistryKey hklm)
        //-------------------------------------------------------------------------------------------------------------
        {
            List<BrowserModel> browsers = new List<BrowserModel>();
            RegistryKey key = hklm.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
            if (key != null)
            {
                foreach (var browser in key.GetSubKeyNames())
                {
                    try
                    {
                        var subKey = key.OpenSubKey(browser);
                        var name = (string)subKey.GetValue(null);
                        var cmd = subKey.OpenSubKey("shell").OpenSubKey("open").OpenSubKey("command");
                        var exec = (string)cmd.GetValue(null);

                        // by this point if registry is missing keys we are already out of here
                        // because of the try catch, but there are still things that could go wrong

                        //0. check if it can handle the http protocol
                        var capabilities = subKey.OpenSubKey("Capabilities");
                        // IE does not have the capabilities subkey...
                        // so assume that the app can handle http if it doesn't
                        // advertise it's capabilities
                        if (capabilities != null)
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

                        browsers.Add(new BrowserModel()
                        {
                            name = name,
                            exec = exec,
                            icon = icon2String(IconExtractor.fromFile(exec))
                        });
                    }
                    catch (NullReferenceException)
                    {
                        // incomplete registry record for browser, ignore it
                    }
                    catch (Exception /*ex*/)
                    {
                        // todo: log errors
                    }
                }
            }

            return browsers;
        }

        //-------------------------------------------------------------------------------------------------------------
        public string icon2String(Icon myIcon)
        //-------------------------------------------------------------------------------------------------------------
        {
            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                myIcon.ToBitmap().Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                bytes = ms.ToArray();
            }

            string iconString = Convert.ToBase64String(bytes);
            return iconString;
        }
    }
}
