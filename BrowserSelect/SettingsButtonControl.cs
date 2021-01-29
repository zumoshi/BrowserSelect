using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrowserSelect
{
    //=============================================================================================================
    public partial class SettingsButtonControl : UserControl
    //=============================================================================================================
    {
        //-------------------------------------------------------------------------------------------------------------
        public SettingsButtonControl()
        //-------------------------------------------------------------------------------------------------------------
        {
            InitializeComponent();

            var verticalButton = new VerticalButton();
            verticalButton.Text = "Settings";
            verticalButton.Anchor = AnchorStyles.Right;
            verticalButton.Size = new Size(25, 75);
            verticalButton.Top = 75/2;
            verticalButton.Left = 0;
            verticalButton.Click += SettingsButton_Click;
            Controls.Add(verticalButton);

            // http://www.telerik.com/blogs/winforms-scaling-at-large-dpi-settings-is-it-even-possible-
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
        }

        //-------------------------------------------------------------------------------------------------------------
        private void SettingsButton_Click(object sender, EventArgs e)
        //-------------------------------------------------------------------------------------------------------------
        {
            new SettingsView(ParentForm).ShowDialog();
        }
    }
}
