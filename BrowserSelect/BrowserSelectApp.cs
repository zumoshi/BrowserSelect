using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Windows.Forms;

using BrowserSelect.Properties;

namespace BrowserSelect
{
    //=============================================================================================================
    class BrowserSelectApp
    //=============================================================================================================
    {
        public static string url { get; set; } = "https://www.google.com";
        public static bool launchWithUrl { get; set; } = false;

        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        //-------------------------------------------------------------------------------------------------------------
        {
            BrowserSelectSetup.RegisterAsBrowser();
            ConfigureUriParser();

            // to prevent loss of settings when updating to new version
            if (Settings.Default.Version == 0)
            {
                Settings.Default.Upgrade();
                Settings.Default.Version = 2;
                // to prevent null reference in case settings file did not exist
                if (Settings.Default.HiddenBrowsers == null)
                    Settings.Default.HiddenBrowsers = new StringCollection();
                if (Settings.Default.Rules == null)
                    Settings.Default.Rules = new StringCollection();
                Settings.Default.Save();
            }

            //checking if a url is being opened or app is run from start menu (without arguments)
            if (args.Length > 0)
            {
                //check to see if auto select rules match
                url = args[0];
                //normalize the url
                Uri uri = new UriBuilder(url).Uri;
                url = uri.AbsoluteUri;
                launchWithUrl = true;

                UrlProcessor processor = new UrlProcessor();
                if (processor.ProcessUrl(uri))
                    return;
            }

            // display main view
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BrowserSelectView());
        }

        //-------------------------------------------------------------------------------------------------------------
        // https://stackoverflow.com/a/7202560/1461004
        private static void ConfigureUriParser()
        //-------------------------------------------------------------------------------------------------------------
        {
            var getSyntaxMethod = typeof(UriParser).GetMethod("GetSyntax", BindingFlags.Static | BindingFlags.NonPublic);
            if (getSyntaxMethod == null)
            {
                throw new MissingMethodException("UriParser", "GetSyntax");
            }

            var uriParser = getSyntaxMethod.Invoke(null, new object[] { "http" });

            var setUpdatableFlagsMethod = uriParser.GetType().GetMethod("SetUpdatableFlags", BindingFlags.Instance | BindingFlags.NonPublic);
            if (setUpdatableFlagsMethod == null)
            {
                throw new MissingMethodException("UriParser", "SetUpdatableFlags");
            }
            setUpdatableFlagsMethod.Invoke(uriParser, new object[] { 0 });
        }
    }
}
