using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BrowserSelect.Properties;
using Microsoft.Win32;

namespace BrowserSelect {
    public partial class frm_settings : Form {
        public frm_settings() {
            InitializeComponent();
        }

        private List<AutoMatchRule> rules= new List<AutoMatchRule>();
        private void frm_settings_Load(object sender, EventArgs e) {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice")) {
                var default_browser = key.GetValue("ProgId");

                //disable the set default if already default
                if (default_browser != null && (string)default_browser == "bselectURL")
                    btn_setdefault.Enabled = false;
            }

            var browsers = BrowserFinder.find();
            var c = ((DataGridViewComboBoxColumn)gv_filters.Columns["browser"]);
            //c.ValueType = typeof(Browser);
            
            foreach (Browser b in browsers)
            {
                browser_filter.Items.Add(b,!Settings.Default.HideBrowsers.Contains(b.exec));
                c.Items.Add(b.ToString());
            }

            foreach(var rule in Settings.Default.AutoBrowser)
                rules.Add(rule);
            var bs = new BindingSource();
            bs.DataSource = rules;
            gv_filters.DataSource = bs;
        }

        private void btn_setdefault_Click(object sender, EventArgs e) {
            //http
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(
                    @"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice")) {
                key.SetValue("ProgId", "bselectURL");
            }
            //https
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(
                    @"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\https\UserChoice")) {
                key.SetValue("ProgId", "bselectURL");
            }

            btn_setdefault.Enabled = false;
        }

        private void browser_filter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                Settings.Default.HideBrowsers.Remove(((Browser) browser_filter.Items[e.Index]).exec);
            }
            else
            {
                Settings.Default.HideBrowsers.Add(((Browser)browser_filter.Items[e.Index]).exec);
            }
            Settings.Default.Save();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frm_settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.AutoBrowser.Clear();
            foreach (var rule in rules)
            {
                if(rule.valid())
                    Settings.Default.AutoBrowser.Add(rule.ToString());
            }
            Settings.Default.Save();
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
            return Pattern+ "[#!][$~][?_]"+Browser;
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
