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
            Icon = Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        private void frm_SplashScreen_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!Program.uriExpanderThreadStop)
            {
                System.Diagnostics.Debug.WriteLine("FormCosing, Abort HTTPWebrequest...");
                Program.uriExpanderThreadStop = true;
                Program.webRequestThread.Abort();
            }
            e.Cancel = false;
        }
    }
}
