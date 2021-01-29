namespace BrowserSelect {
    partial class SettingsView {
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
            System.Windows.Forms.Button CancelButton;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsView));
            this.SetDefaultButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BrowserList = new System.Windows.Forms.CheckedListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DownButton = new System.Windows.Forms.Button();
            this.UpButton = new System.Windows.Forms.Button();
            this.DefaultBrowserList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.LinkLabel();
            this.SaveButton = new System.Windows.Forms.Button();
            this.RulesGrid = new System.Windows.Forms.DataGridView();
            this.type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.pattern = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.browser = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.RefreshButton = new System.Windows.Forms.Button();
            CancelButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RulesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            CancelButton.Location = new System.Drawing.Point(445, 431);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new System.Drawing.Size(75, 23);
            CancelButton.TabIndex = 8;
            CancelButton.Text = "Cancel";
            CancelButton.UseMnemonic = false;
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SetDefaultButton
            // 
            this.SetDefaultButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SetDefaultButton.Location = new System.Drawing.Point(6, 63);
            this.SetDefaultButton.Name = "SetDefaultButton";
            this.SetDefaultButton.Size = new System.Drawing.Size(202, 23);
            this.SetDefaultButton.TabIndex = 0;
            this.SetDefaultButton.Text = "Set as &OS Default Browser";
            this.SetDefaultButton.UseVisualStyleBackColor = true;
            this.SetDefaultButton.Click += new System.EventHandler(this.SetDefaultButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 45);
            this.label1.TabIndex = 1;
            this.label1.Text = "BrowserSelect must be set as default browser for it to function correctly.  This " +
    "button will set it as the default browser.";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.BrowserList);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 325);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Available Browsers";
            // 
            // BrowserList
            // 
            this.BrowserList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.BrowserList.FormattingEnabled = true;
            this.BrowserList.Location = new System.Drawing.Point(3, 16);
            this.BrowserList.Name = "BrowserList";
            this.BrowserList.Size = new System.Drawing.Size(214, 304);
            this.BrowserList.TabIndex = 0;
            this.BrowserList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.BrowserList_ItemCheck);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.SetDefaultButton);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(15, 373);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(214, 99);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.DownButton);
            this.groupBox3.Controls.Add(this.UpButton);
            this.groupBox3.Controls.Add(this.DefaultBrowserList);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(CancelButton);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.SaveButton);
            this.groupBox3.Controls.Add(this.RulesGrid);
            this.groupBox3.Location = new System.Drawing.Point(235, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(607, 460);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rules";
            // 
            // DownButton
            // 
            this.DownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DownButton.Location = new System.Drawing.Point(86, 408);
            this.DownButton.Name = "DownButton";
            this.DownButton.Size = new System.Drawing.Size(75, 23);
            this.DownButton.TabIndex = 12;
            this.DownButton.Text = "&Down";
            this.DownButton.UseVisualStyleBackColor = true;
            this.DownButton.Click += new System.EventHandler(this.DownButton_Click);
            // 
            // UpButton
            // 
            this.UpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UpButton.Location = new System.Drawing.Point(5, 408);
            this.UpButton.Name = "UpButton";
            this.UpButton.Size = new System.Drawing.Size(75, 23);
            this.UpButton.TabIndex = 11;
            this.UpButton.Text = "&Up";
            this.UpButton.UseVisualStyleBackColor = true;
            this.UpButton.Click += new System.EventHandler(this.UpButton_Click);
            // 
            // DefaultBrowserList
            // 
            this.DefaultBrowserList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DefaultBrowserList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DefaultBrowserList.FormattingEnabled = true;
            this.DefaultBrowserList.Location = new System.Drawing.Point(220, 431);
            this.DefaultBrowserList.Name = "DefaultBrowserList";
            this.DefaultBrowserList.Size = new System.Drawing.Size(219, 21);
            this.DefaultBrowserList.TabIndex = 10;
            this.DefaultBrowserList.SelectedIndexChanged += new System.EventHandler(this.DefaultBrowserList_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 436);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(209, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Default Browser (when no rule is matched):";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.LinkArea = new System.Windows.Forms.LinkArea(212, 235);
            this.label2.Location = new System.Drawing.Point(3, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(601, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Using this section you can define rules for which browser to use based on URL or " +
    "Process pattern matches.";
            this.label2.UseCompatibleTextRendering = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.SaveButton.Enabled = false;
            this.SaveButton.Location = new System.Drawing.Point(526, 431);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 5;
            this.SaveButton.Text = "&Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // RulesGrid
            // 
            this.RulesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RulesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RulesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.type,
            this.pattern,
            this.browser});
            this.RulesGrid.Location = new System.Drawing.Point(6, 34);
            this.RulesGrid.Name = "RulesGrid";
            this.RulesGrid.Size = new System.Drawing.Size(595, 367);
            this.RulesGrid.TabIndex = 1;
            this.RulesGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.RulesGrid_CellBeginEdit);
            this.RulesGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RulesGrid_CellClick);
            this.RulesGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.RulesGrid_DataError);
            this.RulesGrid.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.RulesGrid_CellBeginEdit);
            this.RulesGrid.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.RulesGrid_CellBeginEdit);
            // 
            // type
            // 
            this.type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.type.DataPropertyName = "Type";
            this.type.HeaderText = "Type";
            this.type.Name = "type";
            this.type.Width = 39;
            // 
            // pattern
            // 
            this.pattern.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.pattern.DataPropertyName = "Pattern";
            this.pattern.HeaderText = "Pattern";
            this.pattern.Name = "pattern";
            this.pattern.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.pattern.Width = 250;
            // 
            // browser
            // 
            this.browser.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.browser.DataPropertyName = "Browser";
            this.browser.HeaderText = "Browser";
            this.browser.Name = "browser";
            this.browser.Width = 51;
            // 
            // RefreshButton
            // 
            this.RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RefreshButton.Location = new System.Drawing.Point(12, 340);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(217, 23);
            this.RefreshButton.TabIndex = 2;
            this.RefreshButton.Text = "&Refresh Browser List";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // SettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = CancelButton;
            this.ClientSize = new System.Drawing.Size(854, 481);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(870, 420);
            this.Name = "SettingsView";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsView_FormClosing);
            this.Load += new System.EventHandler(this.SettingsView_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RulesGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SetDefaultButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox BrowserList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView RulesGrid;
        private System.Windows.Forms.LinkLabel label2;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.DataGridViewComboBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn pattern;
        private System.Windows.Forms.DataGridViewComboBoxColumn browser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox DefaultBrowserList;
        private System.Windows.Forms.Button DownButton;
        private System.Windows.Forms.Button UpButton;
    }
}