using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BrowserSelect {
    public partial class frm_About : Form {
        public frm_About() {
            InitializeComponent();
        }

        private void frm_About_Load(object sender, EventArgs e) {
            pictureBox1.Image = IconExtractor.fromFile(Application.ExecutablePath).ToBitmap();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            System.Diagnostics.Process.Start("Mailto:me@bor691.ir");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            System.Diagnostics.Process.Start(((LinkLabel)sender).Text);
        }

        private void frm_About_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
