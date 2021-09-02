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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_settings));
            this.btn_setdefault = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.browser_filter = new System.Windows.Forms.CheckedListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_expandurls = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btn_apply = new System.Windows.Forms.Button();
            this.gv_filters = new System.Windows.Forms.DataGridView();
            this.pattern = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.browser = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chk_check_update = new System.Windows.Forms.CheckBox();
            this.btn_check_update = new System.Windows.Forms.Button();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_filters)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_setdefault
            // 
            this.btn_setdefault.Location = new System.Drawing.Point(6, 47);
            this.btn_setdefault.Name = "btn_setdefault";
            this.btn_setdefault.Size = new System.Drawing.Size(135, 31);
            this.btn_setdefault.TabIndex = 2;
            this.btn_setdefault.Text = "Set as Default Browser";
            this.btn_setdefault.UseVisualStyleBackColor = true;
            this.btn_setdefault.Click += new System.EventHandler(this.btn_setdefault_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "BrowserSelect must be the default browser.";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 316);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 56);
            this.label2.TabIndex = 2;
            this.label2.Text = "if you have feature requests,bug reports or suggestions please submit an issue on" +
    " the project\'s Github.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.browser_filter);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 125);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Browsers";
            // 
            // browser_filter
            // 
            this.browser_filter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browser_filter.FormattingEnabled = true;
            this.browser_filter.Location = new System.Drawing.Point(3, 16);
            this.browser_filter.Name = "browser_filter";
            this.browser_filter.Size = new System.Drawing.Size(144, 106);
            this.browser_filter.TabIndex = 1;
            this.browser_filter.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.browser_filter_ItemCheck);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_expandurls);
            this.groupBox2.Controls.Add(this.btn_setdefault);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(15, 143);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(147, 123);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // btn_expandurls
            // 
            this.btn_expandurls.Location = new System.Drawing.Point(6, 84);
            this.btn_expandurls.Name = "btn_expandurls";
            this.btn_expandurls.Size = new System.Drawing.Size(135, 31);
            this.btn_expandurls.TabIndex = 3;
            this.btn_expandurls.Text = "URL Expander Config";
            this.btn_expandurls.UseVisualStyleBackColor = true;
            this.btn_expandurls.Click += new System.EventHandler(this.btn_expandurls_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btn_cancel);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.linkLabel1);
            this.groupBox3.Controls.Add(this.btn_apply);
            this.groupBox3.Controls.Add(this.gv_filters);
            this.groupBox3.Location = new System.Drawing.Point(168, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(444, 360);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Auto Select Filters";
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(282, 331);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 8;
            this.btn_cancel.Text = "Close";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(6, 331);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Help";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(212, 235);
            this.linkLabel1.Location = new System.Drawing.Point(3, 16);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(438, 40);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = resources.GetString("linkLabel1.Text");
            this.linkLabel1.UseCompatibleTextRendering = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btn_apply
            // 
            this.btn_apply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_apply.Enabled = false;
            this.btn_apply.Location = new System.Drawing.Point(363, 331);
            this.btn_apply.Name = "btn_apply";
            this.btn_apply.Size = new System.Drawing.Size(75, 23);
            this.btn_apply.TabIndex = 9;
            this.btn_apply.Text = "Apply";
            this.btn_apply.UseVisualStyleBackColor = true;
            this.btn_apply.Click += new System.EventHandler(this.btn_apply_Click);
            // 
            // gv_filters
            // 
            this.gv_filters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gv_filters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_filters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pattern,
            this.browser});
            this.gv_filters.Location = new System.Drawing.Point(6, 59);
            this.gv_filters.Name = "gv_filters";
            this.gv_filters.RowHeadersWidth = 30;
            this.gv_filters.Size = new System.Drawing.Size(432, 266);
            this.gv_filters.TabIndex = 6;
            this.gv_filters.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gv_filters_CellBeginEdit);
            this.gv_filters.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gv_filters_DataError);
            this.gv_filters.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.gv_filters_CellBeginEdit);
            this.gv_filters.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.gv_filters_CellBeginEdit);
            // 
            // pattern
            // 
            this.pattern.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.pattern.DataPropertyName = "Pattern";
            this.pattern.HeaderText = "Domain Pattern";
            this.pattern.MinimumWidth = 10;
            this.pattern.Name = "pattern";
            this.pattern.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // browser
            // 
            this.browser.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.browser.DataPropertyName = "Browser";
            this.browser.HeaderText = "Browser";
            this.browser.MinimumWidth = 10;
            this.browser.Name = "browser";
            this.browser.Width = 51;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chk_check_update);
            this.groupBox4.Controls.Add(this.btn_check_update);
            this.groupBox4.Location = new System.Drawing.Point(12, 272);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(150, 41);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Update checker";
            // 
            // chk_check_update
            // 
            this.chk_check_update.AutoSize = true;
            this.chk_check_update.Location = new System.Drawing.Point(12, 16);
            this.chk_check_update.Name = "chk_check_update";
            this.chk_check_update.Size = new System.Drawing.Size(58, 17);
            this.chk_check_update.TabIndex = 4;
            this.chk_check_update.Text = "enable";
            this.chk_check_update.UseVisualStyleBackColor = true;
            this.chk_check_update.CheckedChanged += new System.EventHandler(this.chk_check_update_CheckedChanged);
            // 
            // btn_check_update
            // 
            this.btn_check_update.Location = new System.Drawing.Point(69, 12);
            this.btn_check_update.Name = "btn_check_update";
            this.btn_check_update.Size = new System.Drawing.Size(75, 23);
            this.btn_check_update.TabIndex = 5;
            this.btn_check_update.Text = "check now";
            this.btn_check_update.UseVisualStyleBackColor = true;
            this.btn_check_update.Click += new System.EventHandler(this.btn_check_update_Click);
            // 
            // btn_refresh
            // 
            this.btn_refresh.Location = new System.Drawing.Point(104, 7);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(59, 19);
            this.btn_refresh.TabIndex = 0;
            this.btn_refresh.Text = "Refresh";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // frm_settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(624, 381);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(353, 399);
            this.Name = "frm_settings";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_settings_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frm_settings_FormClosed);
            this.Load += new System.EventHandler(this.frm_settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gv_filters)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_setdefault;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox browser_filter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView gv_filters;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button btn_apply;
        private System.Windows.Forms.DataGridViewTextBoxColumn pattern;
        private System.Windows.Forms.DataGridViewComboBoxColumn browser;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chk_check_update;
        private System.Windows.Forms.Button btn_check_update;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.Button btn_expandurls;
    }
}