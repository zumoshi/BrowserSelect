using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrowserSelect.Properties;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace BrowserSelect
{
    public partial class frm_settings : Form
    {

        public Form1 mainForm;
        private DataTable rules;

        public frm_settings()
        {
            InitializeComponent();
            this.Icon = Icon = Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        public frm_settings(Form mainForm)
        {
            this.mainForm = (Form1)mainForm;
            InitializeComponent();
            this.Icon = Icon = Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

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
            DataGridViewComboBoxColumn c = ((DataGridViewComboBoxColumn)gv_filters.Columns["browser"]);
            List<string> enabled_browsers = new List<string>();

            foreach (Browser b in browsers)
            {
                browser_filter.Items.Add(b, !Settings.Default.HideBrowsers.Contains(b.Identifier));
                c.Items.Add(b.ToString());
                enabled_browsers.Add(b.ToString());
            }
            // add browser select to the list
            c.Items.Add("display BrowserSelect");
            enabled_browsers.Add("display BrowserSelect");
            cmbo_default_browser.DataSource = enabled_browsers;
            if (Settings.Default.DefaultBrowser == null ||
                Settings.Default.DefaultBrowser == "" ||
                !enabled_browsers.Contains(Settings.Default.DefaultBrowser))
                cmbo_default_browser.SelectedItem = "display BrowserSelect";
            else
                cmbo_default_browser.SelectedItem = Settings.Default.DefaultBrowser;

            //populate Rules in the gridview
            if (Settings.Default.Rules != null && Settings.Default.Rules != "")
                load_rules();
            else if (Settings.Default.AutoBrowser != null)
            {
                load_legacy_rules();
                Settings.Default.DefaultBrowser = cmbo_default_browser.SelectedItem.ToString();
                save_rules();
                Settings.Default.Save();
            }

            chk_check_update.Checked = Settings.Default.check_update != "nope";
            chk_launch_settings.Checked = (Boolean)Settings.Default.LaunchToSettings;
        }

        private Boolean check_rules()
        {
            Boolean rules_ok = true;
            for (int row_num = 0; row_num < gv_filters.Rows.Count-1; ++row_num)
            {
                for (int col_num = 0; col_num < gv_filters.Columns.Count; ++col_num)
                {
                    if (gv_filters[col_num, row_num].Value == null || gv_filters[col_num, row_num].Value.ToString() == "")
                    {
                        rules_ok = false;
                        gv_filters[col_num, row_num].Style.BackColor = Color.Tomato;
                    }
                    else
                        gv_filters[col_num, row_num].Style.BackColor = SystemColors.Window;
                }
            }
            return rules_ok;
        }

        /*
         * Cells that are highlighted by check_rules are returned to normal when clicked.
         */
        private void gv_filters_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            ((DataGridView)sender).CurrentCell.Style.BackColor = SystemColors.Window;
        }

        /*
         * Expand combobox when the cells is clicked.
         * Note: Tried this on Enter but it meant cells were expanded in unwanted circumstances (e.g. on Form Load).
         */
        private void gv_filters_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bool validClick = (e.RowIndex != -1 && e.ColumnIndex != -1); //Make sure the clicked row/column is valid.
            var dgv = sender as DataGridView;

            // Check to make sure the cell clicked is the cell containing the combobox 
            if (dgv.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && validClick)
            {
                dgv.BeginEdit(true);
                ((ComboBox)dgv.EditingControl).DroppedDown = true;
            }
        }

        private void save_rules()
        {
            Settings.Default.Rules = JsonConvert.SerializeObject(rules);
        }

        private void load_rules()
        {
            rules = (DataTable)JsonConvert.DeserializeObject(Settings.Default.Rules, (typeof(DataTable)));
            gv_filters.DataSource = rules;
        }

        private void load_legacy_rules()
        {
            rules = new DataTable();
            for (int i = 0; i < gv_filters.Columns.Count; ++i)
            {
                rules.Columns.Add(new DataColumn(gv_filters.Columns[i].DataPropertyName));
            }
            gv_filters.AutoGenerateColumns = false;
            foreach (string rule in Settings.Default.AutoBrowser)
            {
                string[] ss = rule.Split(new[] { "[#!][$~][?_]" }, StringSplitOptions.None);
                string pattern = ss[0];
                string browser = ss[1];
                string comp_type;
                if (pattern == "*")
                    cmbo_default_browser.SelectedItem = browser;
                else
                {
                    int count = 0;
                    foreach (var c in pattern.ToCharArray())
                        if (c == '*')
                            count++;

                    //replace "*." at start of string with "*" to align with legacy code
                    if (pattern.StartsWith("*."))
                        pattern = "*" + pattern.Substring(2, pattern.Length - 2);

                    if (!pattern.Contains("*"))
                        comp_type = "Matches";
                    else if (pattern.StartsWith("*") && count == 1)
                    {
                        comp_type = "Ends With";
                        pattern = pattern.Substring(1, pattern.Length - 1);
                    }
                    else if (pattern.EndsWith("*") && count == 1)
                    {
                        comp_type = "Starts With";
                        pattern = pattern.Substring(0, pattern.Length - 1);
                    }
                    else if (pattern.StartsWith("*") && pattern.EndsWith("*") && count == 2)
                    {
                        comp_type = "Contains";
                        pattern = pattern.Substring(1, pattern.Length - 2);
                    }
                    else
                    {
                        comp_type = "RegEx";
                        pattern = pattern.Replace(@".", @"\.").Replace(@"*", @".*");
                    }
                    rules.Rows.Add(comp_type, "Domain", pattern, browser);
                }
            }
            gv_filters.DataSource = rules;
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
                Settings.Default.HideBrowsers.Remove(((Browser)browser_filter.Items[e.Index]).Identifier);
            }
            else
            {
                Settings.Default.HideBrowsers.Add(((Browser)browser_filter.Items[e.Index]).Identifier);
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
            if (check_rules())
            {
                save_rules();
                Settings.Default.DefaultBrowser = cmbo_default_browser.SelectedItem.ToString();
                Settings.Default.Save();
                //Enabled property of apply button is used as a flag for unsaved changes
                btn_apply.Enabled = false;
                btn_cancel.Text = "Close";
            }
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
            btn_cancel.Text = "Cancel";
        }

        private void frm_settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            //close the help window (if it was open)
            _frmHelp?.Close();
        }

        private void btn_check_update_Click(object sender, EventArgs e)
        {
            var btn = ((Button)sender);
            var uc = new UpdateChecker();
            // color the button to indicate request, disable it to prevent multiple instances
            btn.BackColor = Color.Blue;
            btn.Enabled = false;
            // run inside a Task to prevent freezing the UI
            Task.Factory.StartNew(() => uc.check()).ContinueWith(x =>
            {
                try
                {
                    if (uc.Checked)
                    {
                        if (uc.Updated)
                            MessageBox.Show(String.Format(
                                "New Update Available!\nCurrent Version: {1}\nLast Version: {0}" +
                                "\nto Update download and install the new version from project's github.",
                                uc.LVer, uc.CVer));
                        else
                            MessageBox.Show("You are running the lastest version.");
                    }
                    else
                        MessageBox.Show("Unable to check for updates.\nPlease make sure you are connected to internet.");
                    btn.UseVisualStyleBackColor = true;
                    btn.Enabled = true;
                }
                catch (Exception ex) {
                    Debug.WriteLine(ex);
                }
                return x;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void chk_check_update_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.check_update = (((CheckBox)sender).Checked) ? "0" : "nope";
            Settings.Default.Save();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            List<Browser> browsers = BrowserFinder.find(true);
            var c = ((DataGridViewComboBoxColumn)gv_filters.Columns["browser"]);
            c.Items.Clear();
            browser_filter.Items.Clear();
            foreach (Browser b in browsers)
            {
                browser_filter.Items.Add(b, !Settings.Default.HideBrowsers.Contains(b.Identifier));
                c.Items.Add(b.ToString());
            }
            // add browser select to the list
            c.Items.Add("display BrowserSelect");

            if (mainForm != null)
                this.mainForm.updateBrowsers();
            else
                browsers = BrowserFinder.find().Where(b => !Settings.Default.HideBrowsers.Contains(b.Identifier)).ToList();
        }

        private void gv_filters_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // to prevent System.ArgumentException: DataGridViewComboBoxCell value is not valid MessageBoxes
        }

         private void btn_expandurls_Click(object sender, EventArgs e)
        {
            (new frm_settings_urlexpander()).ShowDialog();
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            DataGridView dgv = gv_filters;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == 0)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataRow selectedRow = rules.Rows[rowIndex];
                DataRow row = rules.NewRow();
                for (int i = 0; i < gv_filters.Columns.Count; ++i)
                    row[i] = selectedRow[i];
                rules.Rows.Remove(selectedRow);
                rules.Rows.InsertAt(row, rowIndex - 1);
                dgv.ClearSelection();
                dgv.Rows[rowIndex - 1].Cells[colIndex].Selected = true;
                dgv.CurrentCell = dgv[colIndex, rowIndex - 1];
                //set the unsaved changes flag to true
                btn_apply.Enabled = true;
                btn_cancel.Text = "Cancel";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void btn_down_Click(object sender, EventArgs e)
        {
            DataGridView dgv = gv_filters;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == totalRows - 1)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataRow selectedRow = rules.Rows[rowIndex];
                DataRow row = rules.NewRow();
                for (int i = 0; i < gv_filters.Columns.Count; ++i)
                    row[i] = selectedRow[i];
                rules.Rows.Remove(selectedRow);
                rules.Rows.InsertAt(row, rowIndex + 1);
                dgv.ClearSelection();
                dgv.Rows[rowIndex + 1].Cells[colIndex].Selected = true;
                dgv.CurrentCell = dgv[colIndex, rowIndex + 1];
                //set the unsaved changes flag to true
                btn_apply.Enabled = true;
                btn_cancel.Text = "Cancel";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void chk_launch_settings_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.LaunchToSettings = ((CheckBox)sender).Checked;
            Settings.Default.Save();
        }
    }
}
