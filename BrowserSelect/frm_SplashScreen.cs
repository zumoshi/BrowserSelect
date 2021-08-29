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
                System.Console.WriteLine("1 Cancel loading...");
                Program.uriExpanderThreadStop = true;
                System.Console.WriteLine("2 Cancel loading...");
                Program.webRequestThread.Abort();
                System.Console.WriteLine("3 Cancel loading...");
            }
            e.Cancel = false;
        }
    }
}
