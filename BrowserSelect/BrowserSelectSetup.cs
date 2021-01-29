using System.Reflection;

using Microsoft.Win32;

namespace BrowserSelect
{
    //=============================================================================================================
    static class BrowserSelectSetup
    //=============================================================================================================
    {
        //-------------------------------------------------------------------------------------------------------------
        public static void RegisterAsBrowser()
        //-------------------------------------------------------------------------------------------------------------
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string exe = asm.Location;

            /*
                [HKEY_CURRENT_USER\SOFTWARE\Clients\StartMenuInternet\BrowserSelect.exe]
                @="Browser Select"
            */
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(
                    @"SOFTWARE\Clients\StartMenuInternet\BrowserSelect.exe"))
            {
                key.SetValue("", "Browser Select");
            }

            /*
                [HKEY_CURRENT_USER\SOFTWARE\Clients\StartMenuInternet\BrowserSelect.exe\Capabilities]
                "ApplicationDescription"="Select which browser to open links with"
                "ApplicationIcon"="D:\\DEV\\GitHub\\BrowserSelect\\BrowserSelect\\bin\\Debug\\BrowserSelect.exe,0"
                "ApplicationName"="BrowserSelect"
            */
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(
                    @"SOFTWARE\Clients\StartMenuInternet\BrowserSelect.exe\Capabilities"))
            {
                string value = exe + ",0";
                key.SetValue("ApplicationName", "BrowserSelect");
                key.SetValue("ApplicationDescription", "Select which browser to open links with");
                key.SetValue("ApplicationIcon", value);
            }

            /*
                [HKEY_CURRENT_USER\SOFTWARE\Clients\StartMenuInternet\BrowserSelect.exe\Capabilities\StartMenu]
                "StartMenuInternet"="BrowserSelect.exe"
            */
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(
                    @"SOFTWARE\Clients\StartMenuInternet\BrowserSelect.exe\Capabilities\StartMenu"))
            {
                key.SetValue("StartMenuInternet", "BrowserSelect.exe");
            }

            /*
                [HKEY_CURRENT_USER\SOFTWARE\Clients\StartMenuInternet\BrowserSelect.exe\Capabilities\URLAssociations]
                "http"="BrowserSelectURL"
                "https"="BrowserSelectURL"
            */
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(
                    @"SOFTWARE\Clients\StartMenuInternet\BrowserSelect.exe\Capabilities\URLAssociations"))
            {
                key.SetValue("http", "BrowserSelectURL");
                key.SetValue("https", "BrowserSelectURL");
            }

            /*
                [HKEY_CURRENT_USER\SOFTWARE\Clients\StartMenuInternet\BrowserSelect.exe\DefaultIcon]
                @="D:\\DEV\\GitHub\\BrowserSelect\\BrowserSelect\\bin\\Debug\\BrowserSelect.exe,0"
            */
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(
                    @"SOFTWARE\Clients\StartMenuInternet\BrowserSelect.exe\DefaultIcon"))
            {
                string value = exe + ",0";
                key.SetValue("", value);
            }

            /*
                [HKEY_CURRENT_USER\SOFTWARE\Clients\StartMenuInternet\BrowserSelect.exe\shell]
                [HKEY_CURRENT_USER\SOFTWARE\Clients\StartMenuInternet\BrowserSelect.exe\shell\open]
                [HKEY_CURRENT_USER\SOFTWARE\Clients\StartMenuInternet\BrowserSelect.exe\shell\open\command]
                @="\"D:\\DEV\\GitHub\\BrowserSelect\\BrowserSelect\\bin\\Debug\\BrowserSelect.exe\""
            */
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(
                    @"SOFTWARE\Clients\StartMenuInternet\BrowserSelect.exe\shell\open\command"))
            {
                key.SetValue("", exe);
            }

            /*
                [HKEY_CURRENT_USER\SOFTWARE\RegisteredApplications]
                "BrowserSelect"="Software\\Clients\\StartMenuInternet\\BrowserSelect.exe\\Capabilities"
            */
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(
                    @"SOFTWARE\RegisteredApplications"))
            {
                key.SetValue("BrowserSelect", @"Software\Clients\StartMenuInternet\BrowserSelect.exe\Capabilities");
            }

            /*
                [HKEY_CURRENT_USER\SOFTWARE\Classes\BrowserSelectURL]
                @="BrowserSelect Url"
            */
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(
                    @"SOFTWARE\Classes\BrowserSelectURL"))
            {
                key.SetValue("", "BrowserSelect Url");
            }

            /*
                [HKEY_CURRENT_USER\SOFTWARE\Classes\BrowserSelectURL\shell]
                [HKEY_CURRENT_USER\SOFTWARE\Classes\BrowserSelectURL\shell\open]
                [HKEY_CURRENT_USER\SOFTWARE\Classes\BrowserSelectURL\shell\open\command]
                @="\"D:\\DEV\\GitHub\\BrowserSelect\\BrowserSelect\\bin\\Debug\\BrowserSelect.exe\" \"%1\""
            */
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(
                    @"SOFTWARE\Classes\BrowserSelectURL\shell\open\command"))
            {
                string value = "\"" + exe + "\"" + " " + "\"" + "%1" + "\"";
                key.SetValue("", value);
            }
        }
    }
}
