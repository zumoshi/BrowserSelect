namespace BrowserSelect {
    partial class BrowserButtonControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.BrowserIcon = new System.Windows.Forms.PictureBox();
            this.BrowserLabel = new System.Windows.Forms.Label();
            this.ShortcutLabel = new System.Windows.Forms.Label();
            this.CreateRuleButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BrowserIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // BrowserIcon
            // 
            this.BrowserIcon.Location = new System.Drawing.Point(0, 0);
            this.BrowserIcon.Name = "BrowserIcon";
            this.BrowserIcon.Size = new System.Drawing.Size(128, 128);
            this.BrowserIcon.TabIndex = 0;
            this.BrowserIcon.TabStop = false;
            // 
            // BrowserLabel
            // 
            this.BrowserLabel.Location = new System.Drawing.Point(3, 131);
            this.BrowserLabel.Name = "BrowserLabel";
            this.BrowserLabel.Size = new System.Drawing.Size(122, 18);
            this.BrowserLabel.TabIndex = 1;
            this.BrowserLabel.Text = "<browser>";
            this.BrowserLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ShortcutLabel
            // 
            this.ShortcutLabel.Location = new System.Drawing.Point(3, 147);
            this.ShortcutLabel.Name = "ShortcutLabel";
            this.ShortcutLabel.Size = new System.Drawing.Size(122, 13);
            this.ShortcutLabel.TabIndex = 2;
            this.ShortcutLabel.Text = "<shortcuts>";
            this.ShortcutLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // CreateRuleButton
            // 
            this.CreateRuleButton.Location = new System.Drawing.Point(0, 161);
            this.CreateRuleButton.Name = "CreateRuleButton";
            this.CreateRuleButton.Size = new System.Drawing.Size(125, 20);
            this.CreateRuleButton.TabIndex = 3;
            this.CreateRuleButton.Text = "Create Rule";
            this.CreateRuleButton.UseVisualStyleBackColor = true;
            this.CreateRuleButton.Click += new System.EventHandler(this.RuleButton_Click);
            // 
            // BrowserButtonControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.CreateRuleButton);
            this.Controls.Add(this.ShortcutLabel);
            this.Controls.Add(this.BrowserLabel);
            this.Controls.Add(this.BrowserIcon);
            this.Name = "BrowserButtonControl";
            this.Size = new System.Drawing.Size(130, 185);
            ((System.ComponentModel.ISupportInitialize)(this.BrowserIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox BrowserIcon;
        private System.Windows.Forms.Label BrowserLabel;
        private System.Windows.Forms.Label ShortcutLabel;
        private System.Windows.Forms.Button CreateRuleButton;
    }
}
