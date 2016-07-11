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
            // add browserUC objects to the form
            int i = 0;
            foreach (var browser in browsers)
            {
                var buc = new BrowserUC(browser, i);
                buc.Left = 128 * i++;
                buc.Click += browser_click;
                this.Controls.Add(buc);
            }
            // resize the form
            this.Width = i * 128 + 20 + 20;
            this.KeyPreview = true;
            this.Text = Program.url;
            // set the form icon from .exe file icon
            this.Icon = IconExtractor.fromFile(Application.ExecutablePath);
            // add vertical buttons to right of form
            add_button("About", show_about, 0);
            add_button("Settings", show_setting, 1);
            // put the domain name in Always open X in... checkbox
            rule_url = new Uri(Program.url).Host;
            var url_parts = rule_url.Split(new[] { '.' });
            if (url_parts.Length > 2 && url_parts[url_parts.Length - 2].Length > 3)
                rule_url = url_parts[url_parts.Length - 2] + url_parts[url_parts.Length - 1];
            rule_url = "*." + rule_url;

            //didn't add this after all... (it was a checkbox for issue #9)
            //chk_addRule.Text = String.Format(chk_addRule.Text, rule_url);
            //toolTip1.SetToolTip(chk_addRule,String.Format(toolTip1.GetToolTip(chk_addRule), rule_url));

            center_me();
        }

        private void show_setting(object sender, EventArgs e)
        {
            new frm_settings().ShowDialog();
        }

        private void show_about(object sender, EventArgs e)
        {
            new frm_About().ShowDialog();
        }

        private List<VButton> vbtn = new List<VButton>();
        private void add_button(string text, EventHandler evt, int index)
        {
            var btn = new VButton();
            btn.Text = text;
            btn.Anchor = AnchorStyles.Right;
            btn.Width = 20;
            btn.Height = 75;
            btn.Top = index * 80;
            //btn.Left = this.Width - 35;
            btn.Left = btn_help.Right - btn.Width;
            Controls.Add(btn);
            btn.Click += evt;

            vbtn.Add(btn);
        }
        
        private void browser_click(object sender, EventArgs e)
        {
            BrowserUC uc;
            if (sender is BrowserUC)
                uc = (BrowserUC)sender;
            else if (((Control)sender).Parent is BrowserUC)
                uc = (BrowserUC)((Control)sender).Parent;
            else
                throw new Exception("this should not happen");

            if (uc.Always)
                add_rule(uc.browser);

            open_url(uc.browser);
        }

        private string rule_url;
        public void add_rule(Browser b)
        {
            Settings.Default.AutoBrowser.Add((new AutoMatchRule()
            {
                Pattern = rule_url,
                Browser = b.name
            }).ToString());
            Settings.Default.Save();
        }

        public static void open_url(Browser b)
        {
            if (b.exec == "edge")
            {
                //edge is a universal app , which means we can't just run it like other browsers
                Process.Start("microsoft-edge:" + Program.url
                    .Replace(" ", "%20")
                    .Replace("\"", "%22"));
            }
            else if (b.exec.EndsWith("iexplore.exe"))
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
                        found = true;
                        break;
                    }
                }
                if (!found)
                    Process.Start(b.exec, "\"" + Program.url.Replace("\"", "%22") + "\"");
            }
            else
                Process.Start(b.exec, "\"" + Program.url.Replace("\"", "%22") + "\"");
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
            var wa = Screen.PrimaryScreen.WorkingArea;
            var left = wa.Width / 2 + wa.Left - Width / 2;
            var top = wa.Height / 2 + wa.Top - Height / 2;

            this.Location = new Point(left, top);
        }

        private void btn_help_Click(object sender, EventArgs e)
        {
            (new frm_help_main()).ShowDialog();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            
        }
    }
}
