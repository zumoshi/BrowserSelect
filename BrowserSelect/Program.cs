using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BrowserSelect {
    static class Program {
        public static string url = "http://google.com/";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            if (args.Length > 0)
                url = args[0];

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
