using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrowserSelect.Properties;
using System.Web;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using System.Data;

namespace BrowserSelect
{
    static class Program
    {
        public static string url = "";
        public static HttpWebRequest webRequestThread = null;
        public static bool uriExpanderThreadStop = false;
        public static (string name, string domain)[] defaultUriExpander = new(string name, string domain)[]
            {
                ("Outlook safe links", "safelinks.protection.outlook.com")//,
                //("Test1", "test.com"),
                //("Test2", "test2.com")
            };

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // to prevent loss of settings when on update
            if (Settings.Default.UpdateSettings)
            {
                Settings.Default.Upgrade();
                Settings.Default.UpdateSettings = false;
                Settings.Default.last_version = "nope";
                // to prevent nullreference in case settings file did not exist
                if (Settings.Default.HideBrowsers == null)
                    Settings.Default.HideBrowsers = new StringCollection();
                if (Settings.Default.AutoBrowser == null)
                    Settings.Default.AutoBrowser = new StringCollection();
                Settings.Default.Save();
            }
            // check for update
            if (Settings.Default.check_update != "nope" &&
                DateTime.Now.Subtract(time(Settings.Default.check_update)).TotalDays > 7)
            {
                var uc = new UpdateChecker();
                Task.Factory.StartNew(() => uc.check());
            }
            //load URL Shortners
            string[] defultUrlShortners = new string[] {
                "adf.ly",
                "bit.do",
                "bit.ly",
                "goo.gl",
                "ht.ly",
                "is.gd",
                "ity.im",
                "lnk.co",
                "ow.ly",
                "q.gs",
                "rb.gy",
                "rotf.lol",
                "t.co",
                "tiny.one",
                "tinyurl.com"
            };
            if (Settings.Default.URLShortners == null)
            {
                StringCollection url_shortners = new StringCollection();
                url_shortners.AddRange(defultUrlShortners);
                Settings.Default.URLShortners = url_shortners;
                Settings.Default.Save();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //checking if a url is being opened or app is ran from start menu (without arguments)
            if (args.Length > 0)
            {
                //check to see if auto select rules match
                url = args[0];
                //add http:// to url if it is missing a protocol
                var uri = new UriBuilder(url).Uri;
                uri = UriExpander(uri);
                if (Settings.Default.ExpandUrl != null && Settings.Default.ExpandUrl != "Never")
                    uri = UriFollowRedirects(uri);
                url = uri.AbsoluteUri;

                //if we loaded the browser finish execution here...
                if (load_browser(uri))
                    return;
            }

            // display main form
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            if (url != "")
                Application.Run(new Form1());
            else
                Application.Run(new frm_settings());
        }

        private static Boolean load_browser(Uri uri)
        {
            if (Settings.Default.Rules != null && Settings.Default.Rules != "")
            {
                DataTable rules = (DataTable)JsonConvert.DeserializeObject(Settings.Default.Rules, (typeof(DataTable)));
                foreach (DataRow rule in rules.Rows)
                {
                    Boolean rule_match = false;
                    string match_type = (string)rule["Type"];
                    string match = (string)rule["Match"];
                    string pattern = (string)rule["Pattern"];

                    string test_uri = "";
                    if (match == "Domain")
                        test_uri = uri.Host;
                    else if (match == "URL Path")
                        test_uri = uri.PathAndQuery;
                    else if (match == "Full URL")
                        test_uri = uri.AbsoluteUri;

                    switch (match_type)
                    {
                        case "Ends With":
                            if (test_uri.EndsWith(pattern, StringComparison.OrdinalIgnoreCase))
                                rule_match = true;
                            break;
                        case "Starts With":
                            if (test_uri.StartsWith(pattern, StringComparison.OrdinalIgnoreCase))
                                rule_match = true;
                            break;
                        case "Contains":
                            if (test_uri.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) >= 0)
                                rule_match = true;
                            break;
                        case "Matches":
                            if (test_uri.Equals(pattern, StringComparison.OrdinalIgnoreCase))
                                rule_match = true;
                            break;
                        case "RegEx":
                            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                            if (regex.IsMatch(test_uri))
                                rule_match = true;
                            break;
                    }

                    if (rule_match)
                    {
                        System.Diagnostics.Debug.WriteLine(test_uri + " " + match_type + " " + pattern);
                        string browser = (string)rule["Browser"];
                        if (browser != "display BrowserSelect")
                            Form1.open_url((Browser)browser);
                        return true;
                    }
                }
            }
            if (Settings.Default.DefaultBrowser != null &&
                Settings.Default.DefaultBrowser != "" &&
                Settings.Default.DefaultBrowser != "display BrowserSelect")
            {
                Form1.open_url((Browser)Settings.Default.DefaultBrowser);
                return true;
            }
            return false;
        }

        // from : http://stackoverflow.com/a/250400/1461004
        public static double time()
        {
            return time(DateTime.Now);
        }
        public static DateTime time(string unixTimeStamp)
        {
            return time(double.Parse(unixTimeStamp));
        }
        public static DateTime time(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        public static double time(DateTime dateTime)
        {
            return (TimeZoneInfo.ConvertTimeToUtc(dateTime) -
                   new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }


        public static string Args2Str(List<string> args)
        {
            return Args2Str(args.ToArray());
        }
        public static string Args2Str(string[] args)
        {
            return string.Join(" ", args.Select(Program.EncodeParameterArgument));
        }

        /// <summary>
        /// Encodes an argument for passing into a program
        /// taken from : http://stackoverflow.com/a/12364234/1461004
        /// </summary>
        /// <param name="original">The value that should be received by the program</param>
        /// <returns>The value which needs to be passed to the program for the original value 
        /// to come through</returns>
        public static string EncodeParameterArgument(string original)
        {
            if (string.IsNullOrEmpty(original))
                return original;
            string value = Regex.Replace(original, @"(\\*)" + "\"", @"$1\$0");
            value = Regex.Replace(value, @"^(.*\s.*?)(\\*)$", "\"$1$2$2\"");
            return value;
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

        private static Uri UriExpander(Uri uri)
        {
            List<string> enabled_url_expanders = new List<string>();
            if (Settings.Default.URLProcessors != null)
            {
                foreach ((string name, string domain) in defaultUriExpander)
                {
                    if (Settings.Default.URLProcessors.Contains(name))
                    {
                        enabled_url_expanders.Add(domain);
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine("URLExpander: " + uri.Host);
            if (uri.Host.EndsWith("safelinks.protection.outlook.com") &&
                enabled_url_expanders.Contains("safelinks.protection.outlook.com"))
            {
                var queryDict = HttpUtility.ParseQueryString(uri.Query);
                if (queryDict != null && queryDict.Get("url") != null)
                {
                    uri = new UriBuilder(HttpUtility.UrlDecode(queryDict.Get("url"))).Uri;
                }
            }

            return uri;
        }

        private static Uri UriFollowRedirects(Uri uri, int num_redirects = 0)
        {
            int max_redirects = 20;
            if (num_redirects >= max_redirects)
            {
                return uri;
            }
            System.Diagnostics.Debug.WriteLine("Url " + num_redirects + " " + uri.Host);
            StringCollection url_shortners = Settings.Default.URLShortners;
            Form SplashScreen = null;
            if (!Program.uriExpanderThreadStop &&
                (url_shortners.Contains(uri.Host) || Settings.Default.ExpandUrl == "Follow all redirects"))
            {
                //Thread.Sleep(2000);
                if (num_redirects == 0)
                {
                    SplashScreen = new frm_SplashScreen();
                    var splashThread = new Thread(new ThreadStart(() => Application.Run(SplashScreen)));
                    splashThread.Start();
                }
                HttpWebResponse response = MyWebRequest(uri);
                if (response != null)
                {
                    if ((int)response.StatusCode > 299 && (int)response.StatusCode < 400)
                    {
                        uri = UriFollowRedirects(new UriBuilder(response.Headers["Location"]).Uri, (num_redirects + 1));
                    }
                    else
                    {
                        uri = response.ResponseUri;
                    }
                }
            }

            if (num_redirects == 0)
            {
                if (SplashScreen != null && !SplashScreen.Disposing && !SplashScreen.IsDisposed)
                    try
                    {
                        Program.uriExpanderThreadStop = true;
                        SplashScreen.Invoke(new Action(() => SplashScreen.Close()));
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
            }
            return uri;
        }

        private static HttpWebResponse MyWebRequest(Uri uri)
        {
            //Support TLS1.2 - updated .Net framework - no longer needed
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | (SecurityProtocolType)768 | (SecurityProtocolType)3072 | SecurityProtocolType.Ssl3; //SecurityProtocolType.Tls12;
            var webRequest = (HttpWebRequest)WebRequest.Create(uri.AbsoluteUri);
            // Set timeout - needs to be high enough for HTTP request to succeed on slow network connections,
            // but fast enough not to slow down BrowserSelect startup too much.
            // 2 seconds seems about right
            webRequest.Timeout = 2000;
            //webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv: 85.0) Gecko/20100101 Firefox/85.0";
            webRequest.AllowAutoRedirect = false;
            HttpWebResponse response = null;
            try
            {
                var ar = webRequest.BeginGetResponse(null, null);
                Program.webRequestThread = webRequest;
                ThreadPool.RegisterWaitForSingleObject(ar.AsyncWaitHandle, new WaitOrTimerCallback(TimeoutCallback), webRequest, webRequest.Timeout, true);
                response = (HttpWebResponse)webRequest.EndGetResponse(ar);
                response.Close();
            }
            catch (WebException ex)
            {
                // We are mostly catch up webRequest.Abort() or webRequest errors here (e.g. untrusted certificates)
                // No action required.
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return response;
        }

        // Abort the request if the timer fires.
        private static void TimeoutCallback(object state, bool timedOut)
        {
            if (timedOut)
            {
                HttpWebRequest request = state as HttpWebRequest;
                if (request != null)
                {
                    System.Diagnostics.Debug.WriteLine("Timed out, aborting HTTP request...");
                    request.Abort();
                }
            }
        }
    }
}
