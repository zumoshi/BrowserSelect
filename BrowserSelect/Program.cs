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

namespace BrowserSelect
{
    static class Program
    {
        public static string url = "";
        public static HttpWebRequest webRequestThread = null;
        public static bool uriExpanderThreadStop = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            // fix #28
            LeaveDotsAndSlashesEscaped();
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
                uri = UriFollowRedirects(uri);
                url = uri.AbsoluteUri;

                foreach (var sr in Settings.Default.AutoBrowser.Cast<string>()
                    // maybe i should use a better way to split the pattern and browser name ?
                    .Select(x => x.Split(new[] { "[#!][$~][?_]" }, StringSplitOptions.None))
                    // to make sure * doesn't match when non-* rules exist.
                    .OrderBy(x => ((x[0].Contains("*")) ? 1 : 0) + (x[0] == "*" ? 1 : 0)))
                {
                    var pattern = sr[0];
                    var browser = sr[1];

                    // matching the domain to pattern
                    if (DoesDomainMatchPattern(uri.Host, pattern))
                    {
                        // ignore the display browser select entry to prevent app running itself
                        if (browser != "display BrowserSelect")
                        {
                            //todo: handle the case if browser is not found (e.g. imported settings or uninstalled browser)
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
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            if (url != "")
                Application.Run(new Form1());
            else
                Application.Run(new frm_settings());
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

        // https://stackoverflow.com/a/7202560/1461004
        private static void LeaveDotsAndSlashesEscaped()
        {
            var getSyntaxMethod =
                typeof(UriParser).GetMethod("GetSyntax", BindingFlags.Static | BindingFlags.NonPublic);
            if (getSyntaxMethod == null)
            {
                throw new MissingMethodException("UriParser", "GetSyntax");
            }

            var uriParser = getSyntaxMethod.Invoke(null, new object[] { "http" });

            var setUpdatableFlagsMethod =
                uriParser.GetType().GetMethod("SetUpdatableFlags", BindingFlags.Instance | BindingFlags.NonPublic);
            if (setUpdatableFlagsMethod == null)
            {
                throw new MissingMethodException("UriParser", "SetUpdatableFlags");
            }

            setUpdatableFlagsMethod.Invoke(uriParser, new object[] { 0 });
        }

        private static Uri UriExpander(Uri uri)
        {
            //TODO - this array should have URL expanders enabled/disabled via settings screen
            string[] enabled_url_expanders = {
                "safelinks.protection.outlook.com"
            };

            if (Settings.Default.expand_url != "None" &&
                enabled_url_expanders.Contains(uri.Host))
            {
                if (uri.Host.EndsWith("safelinks.protection.outlook.com"))
                {
                    var queryDict = HttpUtility.ParseQueryString(uri.Query);
                    if (queryDict != null && queryDict.Get("url") != null)
                    {
                        uri = new UriBuilder(HttpUtility.UrlDecode(queryDict.Get("url"))).Uri;
                    }
                }
            }

            return uri;
        }
        private static Uri UriFollowRedirects(Uri uri, int num_redirects = 0)
        {
            int max_redirects = 20;
            switch (Settings.Default.expand_url)
            {
                case "None":
                    max_redirects = 0;
                    break;
                case "First Redirect":
                    max_redirects = 1;
                    break;
            }
            if (num_redirects >= max_redirects)
            {
                return uri;
            }

            //TODO - This should be a user configurable list
            string[] url_shortners = {
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
                "tinyurl.com",
                "httpstat.us",
                "browserselect.voxtor.ir",
                "myts3.ir"
            };

            Form SplashScreen = null;
            if (!Program.uriExpanderThreadStop &&
                url_shortners.Contains(uri.Host))
            {
                //Console.WriteLine("num_redirects: " + num_redirects);
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
                    Console.WriteLine("Url " + num_redirects + " " + uri.Host);
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
                    catch
                    {

                    }
            }
            return uri;
        }

        private static HttpWebResponse MyWebRequest(Uri uri)
        {
            //Support TLS1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | (SecurityProtocolType)768 | (SecurityProtocolType)3072 | SecurityProtocolType.Ssl3; //SecurityProtocolType.Tls12;
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
            }
            catch (Exception ex)
            {
                // We are mostly catch up webRequest.Abort() or webRequest errors here (e.g. untrusted certificates)
                // No action required.
                Console.WriteLine(ex);
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
                    Console.WriteLine("Timed out, aborting HTTP request...");
                    request.Abort();
                }
            }
        }
    }
}
