using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BrowserSelect.Properties;
using SHDocVw;

namespace BrowserSelect
{
    public partial class Form1 : Form
    {
        // get the list of Borwsers from registry and remove the ones unchecked from settings


        List<Browser> browsers = BrowserFinder.find().Where(b => !Settings.Default.HideBrowsers.Contains(b.exec)).ToList();


        public Form1()
        {
            
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int width = 128;
            // add browserUC objects to the form
            int i = 0;
            foreach (var browser in browsers)
            {
                var buc = new BrowserUC(browser, i);
                width = buc.Width;  // buc.Width = 128*dpi Scale
                buc.Left = width * i++;
                buc.Click += browser_click;
                this.Controls.Add(buc);
            }
            // resize the form
            //this.Width = i * 128 + 20 + 20;
            this.AutoSize = true;
            this.KeyPreview = true;
            this.Text = Program.url;
            // set the form icon from .exe file icon
            this.Icon = IconExtractor.fromFile(Application.ExecutablePath);
            // add vertical buttons to right of form
            this.Controls.Add(new ButtonsUC() { Left = i * width });

            // create a wildcard rule for this domain (always button)
            _alwaysRule = generate_rule(Program.url);

            // check for new version
            if (Settings.Default.last_version != "nope")
            {
                btn_help.BackgroundImage = Resources.update_available;
                btn_help.Click -= btn_help_Click;
                btn_help.Click += btn_update_click;
            }
            center_me();
        }

        // struct used to store patterns created for Always button
        public struct AmbiguousRule
        {
            public string tld_rule;
            public string second_rule;
            public int mode;
        }

        private AmbiguousRule _alwaysRule;
        private ContextMenu _alwaysAsk;
        public void add_rule(Browser b)
        {
            // check if desired pattern is ambiguous
            if (_alwaysRule.mode == 3)
            {
                // generate a context menu with tld_rule and second_rule as options
                // and call the overloaded add_rule with pattern as second arg on click
                _alwaysAsk = new ContextMenu((new[] { _alwaysRule.tld_rule, _alwaysRule.second_rule })
                    .Select(x => new MenuItem(x, (s, e) => add_rule(b, x))).ToArray());
                // display the context menu at mouse position
                _alwaysAsk.Show(this, PointToClient(Cursor.Position));
            }
            else if (_alwaysRule.mode == 0)
            {
                // in case ambiguousness of pattern was not determined, should not happen
                MessageBox.Show(String.Format("Error while generating pattern from url." +
                    " Please include the following url in your bug report:\n{0}",
                    Program.url));
            }
            else
            {
                // in case pattern was not ambiguous, just set the pattern as rule and open the url
                var pat = (_alwaysRule.mode == 1) ? _alwaysRule.tld_rule : _alwaysRule.second_rule;
                add_rule(b, pat);
            }
        }

        public void add_rule(Browser b, string pattern)
        {
            save_rule(pattern, b);
            open_url(b);
        }

        private void save_rule(string pattern, Browser b)
        {
            // save a rule and save app settings
            Settings.Default.AutoBrowser.Add((new AutoMatchRule()
            {
                Pattern = pattern,
                Browser = b.name
            }).ToString());
            Settings.Default.Save();
        }

        public static AmbiguousRule generate_rule(string url)
        {
            /*
            to solve issue #13
            there are a lot of second level domains, e.g. domain.info.au, domain.vic.au, ...
            so we check these rules:
            if url has only two parts (e.g. x.tld or www.x.tld) choose *.x.tld
            else if url has 3 parts or more(e.g. y.x.tld) and y!=www:
                check the following rules: (x = second part after tld)
                    1.(x is part of domain)
                        if len(x) > 4: assume that x is not part of extension, and choose  *.x.tld
                    2.(x is part of extension)
                        if len(x) <=2 (e.g. y.id.au) than choose *.y.x.tld
                        if x is in exceptions (com,net,org,edu,gov,asn.sch) choose *.y.x.tld
                            because many TLD's have second level domains on these, e.g. chap.sch.ir
                        if count(parts)==4 and first part is www: e.g. www.news.com.au, choose *.y.x.tld
                if none of the rules apply, the case is ambiguous, display both options in a context menu.
                    e.g. sealake.vic.au or something.fun.ir
            else if url has only one part (#27):
                add *.x
            */

            // needed variables
            var domain = new Uri(url).Host;
            var parts = domain.Split('.');
            var count = parts.Length;
            var tld = parts.Last();
            var x = "";
            var y = "";
            try
            {
                x = parts[count - 2]; //second-level
                y = parts[count - 3]; //third-level
            }
            catch (IndexOutOfRangeException) { } // in case domain did not have 3 parts.. (e.g. localhost, google.com)

            // creating the patterns
            var rule_tld = String.Format("*.{0}.{1}", x, tld);
            var rule_second = String.Format("*.{0}.{1}.{2}", y, x, tld);
            var mode = 0; // 0 = error, 1=use rule_tld (*.x.tld), 2=use rule_second (*.y.x.tld), 3=ambiguous

            // this conditions are based on the long comment above
            if (count == 2 || (count == 3 && y == "www"))
                mode = 1;
            else if (count >= 3)
            {
                if (x.Length > 4)
                    mode = 1;
                else if (
                    (x.Length <= 2) ||
                    ((new[] { "com", "net", "org", "edu", "gov", "asn", "sch" }).Contains(x)) ||
                    (count == 4 && parts[0] == "www")
                    )
                    mode = 2;
                else
                    mode = 3;
            }
            else if (count == 1)
            {
                mode = 1;
                rule_tld = "*." + tld;
            }

            return new AmbiguousRule()
            {
                tld_rule = rule_tld,
                second_rule = rule_second,
                mode = mode
            };
        }

        private void browser_click(object sender, EventArgs e)
        {
            // callback for click event inside the browserControls
            BrowserUC uc;
            if (sender is BrowserUC)
                uc = (BrowserUC)sender;
            else if (((Control)sender).Parent is BrowserUC)
                uc = (BrowserUC)((Control)sender).Parent;
            else
                throw new Exception("this should not happen");

            // check if Always was clicked
            if (uc.Always)
                add_rule(uc.browser);
            else if ((ModifierKeys & Keys.Shift) != 0 || (ModifierKeys & Keys.Alt) != 0)    // open in incognito
                open_url(uc.browser, true);
            else
                open_url(uc.browser);
        }

        public static void open_url(Browser b, bool incognito = false)
        {
            var args = new List<string>();
            if (!string.IsNullOrEmpty(b.additionalArgs))
                args.Add(b.additionalArgs);
            if (incognito)
                args.Add(b.private_arg);
            if (b.exec.ToLower().EndsWith("brave.exe"))
                args.Add("--");
            args.Add(Program.url.Replace("\"", "%22"));

            if (b.exec.EndsWith("iexplore.exe") && !incognito)
            {
                // IE tends to open in a new window instead of a new tab
                // code borrowed from http://stackoverflow.com/a/3713470/1461004
                bool found = false;
                ShellWindows iExplorerInstances = new ShellWindows();
                foreach (InternetExplorer iExplorer in iExplorerInstances)
                {
                    if (iExplorer.Name.EndsWith("Internet Explorer"))
                    {
                        iExplorer.Navigate(Program.url, 0x800);
                        // for issue #10 (bring IE to focus after opening link)
                        ForegroundAgent.RestoreWindow(iExplorer.HWND);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Process.Start(b.exec, Program.Args2Str(args));
                }
            }
            else
            {
                Process.Start(b.exec, Program.Args2Str(args));
            }
            Application.Exit();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int i = 1;
            foreach (var browser in browsers)
            {
                if (browser.shortcuts.Contains(e.KeyChar) || e.KeyChar == (Convert.ToString(i++))[0])
                {
                    open_url(browser);
                    return;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }

        private void center_me()
        {
            var wa = Screen.FromPoint(new Point(Cursor.Position.X, Cursor.Position.Y)).WorkingArea;
            var left = wa.Width / 2 + wa.Left - Width / 2;
            var top = wa.Height / 2 + wa.Top - Height / 2;

            this.Location = new Point(left, top);
            this.Activate();
        }

        private void btn_help_Click(object sender, EventArgs e)
        {
            (new frm_help_main()).ShowDialog();
        }

        void btn_update_click(object sender, EventArgs e)
        {
            var lv = Settings.Default.last_version;
            var cv = Application.ProductVersion;
            cv = cv.Remove(cv.Length - 2);
            MessageBox.Show(String.Format(
                "New Update Available!\nCurrent Version: {1}\nLast Version: {0}" +
                "\nto Update download and install the new version from project's github.",
                lv, cv));
        }
    }
}
