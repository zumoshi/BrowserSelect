using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrowserSelect
{
    public partial class frm_settings_urlexpander : Form
    {
        public frm_settings_urlexpander()
        {
            InitializeComponent();
        }

        private void frm_settings_urlexpander_Load(object sender, EventArgs e)
        {
            cmbo_expand_url.DataSource = (new string[] { "Never", "URL shortners", "Follow all redirects" });
            if (Properties.Settings.Default.ExpandUrl == null || Properties.Settings.Default.ExpandUrl == "")
                cmbo_expand_url.SelectedItem = "Never";
            else
                cmbo_expand_url.SelectedItem = Properties.Settings.Default.ExpandUrl;
            gv_url_shortners.Enabled = ((string)cmbo_expand_url.SelectedItem == "URL shortners");

            if (Properties.Settings.Default.URLShortners != null)
            {
                foreach (String url in Properties.Settings.Default.URLShortners)
                    gv_url_shortners.Rows.Add(url);
            }
            chk_box_url_expanders.DataSource = Program.defaultUriExpander.Select(ele => ele.name).ToList();
            for (int i = 0; i < chk_box_url_expanders.Items.Count; i++)
            {
                StringCollection url_processors;
                if (Properties.Settings.Default.URLProcessors == null)
                    url_processors = new StringCollection();
                else
                    url_processors = Properties.Settings.Default.URLProcessors;
                chk_box_url_expanders.SetItemChecked(i, url_processors.Contains(chk_box_url_expanders.Items[i].ToString()));
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.URLShortners != null)
                Properties.Settings.Default.URLShortners.Clear();
            StringCollection url_shortners = new StringCollection();
            foreach (DataGridViewRow row in gv_url_shortners.Rows)
            {
                if (row.Cells[0].Value != null)
                    url_shortners.Add(row.Cells[0].Value.ToString());
            }
            Properties.Settings.Default.URLShortners = url_shortners;
            Properties.Settings.Default.ExpandUrl = (string)((ComboBox)cmbo_expand_url).SelectedItem;
            StringCollection url_processors = new StringCollection();
            for (int i = 0; i < chk_box_url_expanders.Items.Count; i++)
            {
                if (chk_box_url_expanders.GetItemChecked(i))
                    url_processors.Add(chk_box_url_expanders.Items[i].ToString());
            }
            Properties.Settings.Default.URLProcessors = url_processors;
            Properties.Settings.Default.Save();
            Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbo_expand_url_SelectedIndexChanged(object sender, EventArgs e)
        {
            gv_url_shortners.Enabled = ((string)cmbo_expand_url.SelectedItem == "URL shortners");
        }

        private void DataGridView_EnabledChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (!dgv.Enabled)
            {
                dgv.DefaultCellStyle.BackColor = SystemColors.Control;
                dgv.DefaultCellStyle.ForeColor = SystemColors.GrayText;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.GrayText;
                dgv.CurrentCell = null;
                dgv.ReadOnly = true;
                dgv.EnableHeadersVisualStyles = false;
            }
            else
            {
                dgv.DefaultCellStyle.BackColor = SystemColors.Window;
                dgv.DefaultCellStyle.ForeColor = SystemColors.ControlText;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Window;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                dgv.ReadOnly = false;
                dgv.EnableHeadersVisualStyles = true;
            }
        }
    }
}
