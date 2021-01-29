namespace BrowserSelect {
    partial class BrowserSelectView {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserSelectView));
            this.tooltipUrl = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // BrowserSelectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(124, 181);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BrowserSelectView";
            this.Text = "Browser Select";
            this.Load += new System.EventHandler(this.BrowserSelectView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BrowserSelectView_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BrowserSelectView_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip tooltipUrl;
    }
}

