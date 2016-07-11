using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using BrowserSelect.Properties;
using Microsoft.Win32;

namespace BrowserSelect
{
    public partial class frm_settings : Form
    {
        public frm_settings()
        {
            InitializeComponent();
        }

        private List<AutoMatchRule> rules = new List<AutoMatchRule>();
        private void frm_settings_Load(object sender, EventArgs e)
        {
            //check if browser select is the default browser or not
            //to disable/enable "set Browser select as default" button
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice"))
            {
                var default_browser = key?.GetValue("ProgId");

                //disable the set default if already default
                if (default_browser != null && (string)default_browser == "bselectURL")
                    btn_setdefault.Enabled = false;
            }

            //populate list of browsers for Rule List ComboBox
            var browsers = BrowserFinder.find();
            var c = ((DataGridViewComboBoxColumn)gv_filters.Columns["browser"]);

            foreach (Browser b in browsers)
            {
                browser_filter.Items.Add(b, !Settings.Default.HideBrowsers.Contains(b.exec));
                c.Items.Add(b.ToString());
            }
            // add browser select to the list
            c.Items.Add("display BrowserSelect");

            //populate Rules in the gridview
            foreach (var rule in Settings.Default.AutoBrowser)
                rules.Add(rule);
            var bs = new BindingSource();
            bs.DataSource = rules;
            gv_filters.DataSource = bs;
        }

        private void btn_setdefault_Click(object sender, EventArgs e)
        {
            //set browser select as default in registry

            //http
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(
                    @"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice"))
            {
                key.SetValue("ProgId", "bselectURL");
            }
            //https
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(
                    @"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\https\UserChoice"))
            {
                key.SetValue("ProgId", "bselectURL");
            }

            btn_setdefault.Enabled = false;
        }

        private void browser_filter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //Save changes to the BrowserFilter List
            if (e.NewValue == CheckState.Checked)
            {
                Settings.Default.HideBrowsers.Remove(((Browser)browser_filter.Items[e.Index]).exec);
            }
            else
            {
                Settings.Default.HideBrowsers.Add(((Browser)browser_filter.Items[e.Index]).exec);
            }
            Settings.Default.Save();
        }

        private void frm_settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            // alert user of unsaved changes
            if (btn_apply.Enabled)
            {
                var window = MessageBox.Show("You have unsaved changes, are you sure you want to close without saving ?",
                    "Unsaved Changes", MessageBoxButtons.YesNo);
                if (window == DialogResult.No) e.Cancel = true;
                else e.Cancel = false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // open RuleList help
            Process.Start("https://github.com/zumoshi/BrowserSelect/blob/master/help/filters.md");
        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            //save rules

            //clear rules (instead of checking for changes we just overwrite the whole ruleset)
            Settings.Default.AutoBrowser.Clear();
            foreach (var rule in rules)
            {
                //check if rule has both pattern and browser defined
                if (rule.valid())
                    //add it to rule list
                    Settings.Default.AutoBrowser.Add(rule.ToString());
                else
                {
                    //ignore rule if both pattern and browser is empty otherwise inform user of missing part
                    var err = rule.error();
                    if (err.Length > 0)
                    {
                        MessageBox.Show("Invalid Rule: " + err);
                    }
                }

            }
            //save rules
            Settings.Default.Save();
            //Enabled property of apply button is used as a flag for unsaved changes
            btn_apply.Enabled = false;
        }

        private frm_help_rules _frmHelp;
        private void button1_Click(object sender, EventArgs e)
        {
            if (_frmHelp != null)
            {   //help window is open
                _frmHelp.Focus();
            }
            else
            {   //its not open...
                _frmHelp = new frm_help_rules();
                _frmHelp.FormClosed += (o, ev) => _frmHelp = null;
                _frmHelp.Show();
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            //close the window without saving or warning about unsaved changes
            btn_apply.Enabled = false;
            Close();
        }

        private void gv_filters_CellBeginEdit(object sender, EventArgs e)
        {
            //set the unsaved changes flag to true
            btn_apply.Enabled = true;
        }

        private void frm_settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            //close the help window (if it was open)
            _frmHelp?.Close();
        }
    }
    class AutoMatchRule
    {
        public string Pattern { get; set; }
        public string Browser { get; set; }

        public static implicit operator AutoMatchRule(System.String s)
        {
            var ss = s.Split(new[] { "[#!][$~][?_]" }, StringSplitOptions.None);
            return new AutoMatchRule()
            {
                Pattern = ss[0],
                Browser = ss[1]
            };
        }

        public override string ToString()
        {
            return Pattern + "[#!][$~][?_]" + Browser;
        }

        public string error()
        {
            if (!string.IsNullOrEmpty(Pattern))
                return string.Format("You forgot to select a Browser for '{0}' rule.", Pattern);
            else if (!string.IsNullOrEmpty(Browser))
                return "one of your rules has an Empty pattern. please refer to Help for more information.";
            else
                return "";
        }

        public bool valid()
        {
            try //because they may be null
            {
                return Browser.Length > 0 && Pattern.Length > 0;
            }
            catch
            {
                return false;
            }

        }
    }
}
