using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BrowserSelect
{
    public partial class ButtonsUC : UserControl
    {

        public Form callingForm;
        public ButtonsUC(Form callingForm)
        {
            this.callingForm = callingForm;
            InitializeComponent();
            add_button("About", show_about, 0);
            add_button("Settings", show_setting, 1);

            // http://www.telerik.com/blogs/winforms-scaling-at-large-dpi-settings-is-it-even-possible-
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        }

        private void show_setting(object sender, EventArgs e)
        {
            new frm_settings(this.callingForm).ShowDialog();
        }

        private void show_about(object sender, EventArgs e)
        {
            new frm_About().ShowDialog();
        }

        private List<VButton> vbtn = new List<VButton>();
        private void add_button(string text, EventHandler evt, int index)
        {
            // code for vertical buttons on the right, they are custom controls
            // without support for form designer, so we initiate them in code
            var btn = new VButton();
            btn.Text = text;
            btn.Anchor = AnchorStyles.Right;
            btn.Width = 20;
            btn.Height = 75;
            btn.Top = index * 80;
            //btn.Left = this.Width - 35;
            //btn.Left = btn_help.Right - btn.Width;
            btn.Left = 5;
            Controls.Add(btn);
            btn.Click += evt;

            vbtn.Add(btn);
        }
    }
}
