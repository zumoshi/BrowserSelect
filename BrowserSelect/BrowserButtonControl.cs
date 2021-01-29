using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrowserSelect
{
    //=============================================================================================================
    public partial class BrowserButtonControl : UserControl
    //=============================================================================================================
    {
        public BrowserModel browser;
        public bool createRule { get; set; } = false;

        //-------------------------------------------------------------------------------------------------------------
        public BrowserButtonControl(BrowserModel browser, int index)
        //-------------------------------------------------------------------------------------------------------------
        {
            InitializeComponent();

            this.browser = browser;

            BrowserLabel.Text = browser.name;
            ShortcutLabel.Text = "( " + Convert.ToString(index + 1) + "," + String.Join(",", browser.shortcuts) + " )";
            ShortcutLabel.ForeColor = Color.FromKnownColor(KnownColor.GrayText);
            BrowserIcon.Image = browser.string2Icon();//.ToBitmap();
            BrowserIcon.SizeMode = PictureBoxSizeMode.Zoom;
        }

        //-------------------------------------------------------------------------------------------------------------
        public new event EventHandler Click
        //-------------------------------------------------------------------------------------------------------------
        {
            add
            {
                base.Click += value;
                foreach (Control control in Controls)
                {
                    control.Click += value;
                }
            }
            remove
            {
                base.Click -= value;
                foreach (Control control in Controls)
                {
                    control.Click -= value;
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------
        private void RuleButton_Click(object sender, EventArgs e)
        //-------------------------------------------------------------------------------------------------------------
        {
            createRule = true;
        }
    }
}
