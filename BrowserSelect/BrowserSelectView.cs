using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using BrowserSelect.Properties;

namespace BrowserSelect
{
    //=============================================================================================================
    public partial class BrowserSelectView : Form
    //=============================================================================================================
    {
        List<BrowserModel> browsers;
        //private ButtonsControl buttons;
        private RulesEngine.AmbiguousRule autoRule;

        //-------------------------------------------------------------------------------------------------------------
        public BrowserSelectView()
        //-------------------------------------------------------------------------------------------------------------
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------
        public void RefreshBrowserListUI()
        //-------------------------------------------------------------------------------------------------------------
        {
            SuspendLayout();

            BrowserFinder finder = new BrowserFinder();
            browsers = finder.FindBrowsers().Where(b => !Settings.Default.HiddenBrowsers.Contains(b.Identifier)).ToList();

            // remove existing browser buttons
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                Control control = Controls[i];
                if (control is BrowserButtonControl)
                    Controls.RemoveAt(i);
            }

            int index = 0;
            int totalWidth = 0;
            // add a button for each enabled browser to the form
            foreach (var browser in browsers)
            {
                var control = new BrowserButtonControl(browser, index);
                control.Left =  control.Width * index++;
                totalWidth += control.Width;
                control.Click += BrowserButton_Click;
                Controls.Add(control);
                tooltipUrl.SetToolTip(control, BrowserSelectApp.url);
            }

            ResumeLayout();

            // position Settings button at right edge of form
            Control settingsControl = this.Controls["SettingsButtonControl"];
            if (settingsControl != null)
                settingsControl.Left = totalWidth;

            CenterWindow();
        }

        //-------------------------------------------------------------------------------------------------------------
        private void BrowserSelectView_Load(object sender, EventArgs e)
        //-------------------------------------------------------------------------------------------------------------
        {
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Icon = IconExtractor.fromFile(Application.ExecutablePath); // set the form icon from .exe file icon
            KeyPreview = true;
            MinimizeBox = false;
            MaximizeBox = false;
            Text = BrowserSelectApp.launchWithUrl ? BrowserSelectApp.url : "Browser Select";

            // create a wildcard rule for this domain (create rule button)
            RulesEngine engine = new RulesEngine();
            autoRule = engine.GenerateRule(BrowserSelectApp.url);

            // add Settings button
            var settingsButtonControl = new SettingsButtonControl();
            Controls.Add(settingsButtonControl);
            tooltipUrl.SetToolTip(settingsButtonControl, BrowserSelectApp.url);

            RefreshBrowserListUI();
        }

        //-------------------------------------------------------------------------------------------------------------
        private void BrowserButton_Click(object sender, EventArgs e)
        //-------------------------------------------------------------------------------------------------------------
        {
            // callback for click event inside the browserControls
            BrowserButtonControl button;
            if (sender is BrowserButtonControl)
            {
                button = (BrowserButtonControl)sender;
            }
            else if (((Control)sender).Parent is BrowserButtonControl)
            {
                button = (BrowserButtonControl)((Control)sender).Parent;
            }
            else
            {
                throw new Exception("Invalid item was clicked!");
            }

            UrlProcessor processor = new UrlProcessor();

            // check if create rule button was clicked
            if (button.createRule)
            {
                ShowAddRuleMenu(button.browser);
            }
            else if ((ModifierKeys & Keys.Shift) != 0 || (ModifierKeys & Keys.Alt) != 0)    // open in incognito
            {
                processor.OpenUrl(button.browser, true);
            }
            else
            {
                processor.OpenUrl(button.browser);
            }
        }

        //-------------------------------------------------------------------------------------------------------------
        public void ShowAddRuleMenu(BrowserModel browser)
        //-------------------------------------------------------------------------------------------------------------
        {
            // check if desired pattern is ambiguous
            if (autoRule.mode == 3)
            {
                RulesEngine engine = new RulesEngine();

                // generate a context menu with tld_rule and second_rule as options
                // and call the overloaded AddRule with pattern as second arg on click
                ContextMenu menu = new ContextMenu((new[] { autoRule.firstRule, autoRule.secondRule })
                    .Select(x => new MenuItem(x, (s, e) => engine.AddRule("URL", x, browser))).ToArray());

                // display the context menu at mouse position
                menu.Show(this, PointToClient(Cursor.Position));
            }
            else if (autoRule.mode == 0)
            {
                // in case ambiguousness of pattern was not determined, should not happen
                MessageBox.Show(String.Format("Unable to generate rule from this url:\n" +
                                              "{0}",
                                              BrowserSelectApp.url));
            }
            else
            {
                // in case pattern was not ambiguous, just set the pattern as rule and open the url
                var pattern = (autoRule.mode == 1) ? autoRule.firstRule : autoRule.secondRule;
                RulesEngine engine = new RulesEngine();
                engine.AddRule("URL", pattern, browser);
            }
        }


        //-------------------------------------------------------------------------------------------------------------
        private void BrowserSelectView_KeyPress(object sender, KeyPressEventArgs e)
        //-------------------------------------------------------------------------------------------------------------
        {
            int i = 1;
            foreach (var browser in browsers)
            {
                if (browser.shortcuts.Contains(e.KeyChar) || e.KeyChar == (Convert.ToString(i++))[0])
                {
                    UrlProcessor processor = new UrlProcessor();
                    processor.OpenUrl(browser);
                    return;
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------
        private void BrowserSelectView_KeyDown(object sender, KeyEventArgs e)
        //-------------------------------------------------------------------------------------------------------------
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }

        //-------------------------------------------------------------------------------------------------------------
        private void CenterWindow()
        //-------------------------------------------------------------------------------------------------------------
        {
            var wa = Screen.FromPoint(new Point(Cursor.Position.X, Cursor.Position.Y)).WorkingArea;
            var left = wa.Width / 2 + wa.Left - Width / 2;
            var top = wa.Height / 2 + wa.Top - Height / 2;

            this.Location = new Point(left, top);
            this.Activate();
        }
    }
}
