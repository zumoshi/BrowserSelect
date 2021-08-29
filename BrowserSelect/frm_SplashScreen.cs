using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BrowserSelect
{
    public partial class frm_SplashScreen : Form
    {
        public frm_SplashScreen()
        {
            InitializeComponent();
        }

        private void frm_SplashScreen_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!Program.uriExpanderThreadStop)
            {
                System.Console.WriteLine("FormCosing, Abort HTTPWebrequest...");
                Program.uriExpanderThreadStop = true;
                Program.webRequestThread.Abort();
            }
            e.Cancel = false;
        }
    }
}
