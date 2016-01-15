namespace BrowserSelect {
    partial class frm_settings {
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
            this.btn_setdefault = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_setdefault
            // 
            this.btn_setdefault.Location = new System.Drawing.Point(12, 104);
            this.btn_setdefault.Name = "btn_setdefault";
            this.btn_setdefault.Size = new System.Drawing.Size(137, 23);
            this.btn_setdefault.TabIndex = 0;
            this.btn_setdefault.Text = "Set as Default Browser";
            this.btn_setdefault.UseVisualStyleBackColor = true;
            this.btn_setdefault.Click += new System.EventHandler(this.btn_setdefault_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 92);
            this.label1.TabIndex = 1;
            this.label1.Text = "This is for windows 7 , windows 8 and newer detect this program automatically and" +
    " prompt for default browser on first url opened after installation.";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 80);
            this.label2.TabIndex = 2;
            this.label2.Text = "If you think there should be any other options here that is missing please submit" +
    " an issue on the github page and i\'ll see if i can add it.";
            // 
            // frm_settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(161, 222);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_setdefault);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frm_settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.frm_settings_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_setdefault;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}