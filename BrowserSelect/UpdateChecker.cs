using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using BrowserSelect.Properties;

namespace BrowserSelect
{
    class UpdateChecker
    {
        public String CVer => current_version;
        public String LVer => last_version;
        public Boolean Checked => init;
        public Boolean Updated => new_version();
        
        private string current_version = "x";
        private string last_version = "x";
        private bool init = false;

        public void check()
        {
            if (new_version())
                Settings.Default.last_version = last_version;
            if (Settings.Default.check_update != "nope")
                Settings.Default.check_update = Program.time().ToString();
            Settings.Default.Save();
        }
        string get_last_version()
        {
            // request to releases/latest redirects the user to /releases/tag.
            // since tag is the version number we can get latest version from Location header
            // and make a HEAD request instead of get to save bandwidth
            var req = (HttpWebRequest)WebRequest.Create("https://github.com/zumoshi/BrowserSelect/releases/latest");
            // make webrequest use tls 1.2 instead of ssl3 (#43)
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            req.Method = "HEAD";
            req.AllowAutoRedirect = false;
            using (var res = req.GetResponse())
            {
                return res.Headers["Location"].Split('/').Last();
            }
        }

        void get_versions()
        {
            try
            {
                last_version = get_last_version();
                current_version = ((Func<String, String>)((x) => x.Substring(0, x.Length - 2)))(Application.ProductVersion);
                init = true;
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        bool new_version()
        {
            if (!init)
                get_versions();
            return last_version != current_version;
        }
    }
}
