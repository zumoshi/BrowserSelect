namespace BrowserSelect {
    partial class BrowserUC {
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
            this.icon = new System.Windows.Forms.PictureBox();
            this.name = new System.Windows.Forms.Label();
            this.shortcuts = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).BeginInit();
            this.SuspendLayout();
            // 
            // icon
            // 
            this.icon.Location = new System.Drawing.Point(0, 0);
            this.icon.Name = "icon";
            this.icon.Size = new System.Drawing.Size(128, 128);
            this.icon.TabIndex = 0;
            this.icon.TabStop = false;
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(3, 131);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(122, 18);
            this.name.TabIndex = 1;
            this.name.Text = "label1";
            this.name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // shortcuts
            // 
            this.shortcuts.Location = new System.Drawing.Point(3, 147);
            this.shortcuts.Name = "shortcuts";
            this.shortcuts.Size = new System.Drawing.Size(122, 13);
            this.shortcuts.TabIndex = 2;
            this.shortcuts.Text = "label1";
            this.shortcuts.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // BrowserUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.shortcuts);
            this.Controls.Add(this.name);
            this.Controls.Add(this.icon);
            this.Name = "BrowserUC";
            this.Size = new System.Drawing.Size(128, 160);
            ((System.ComponentModel.ISupportInitialize)(this.icon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox icon;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label shortcuts;
    }
}
