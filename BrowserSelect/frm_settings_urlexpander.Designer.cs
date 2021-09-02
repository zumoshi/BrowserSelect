
namespace BrowserSelect
{
    partial class frm_settings_urlexpander
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gv_url_shortners = new System.Windows.Forms.DataGridView();
            this.Domain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chk_box_url_expanders = new System.Windows.Forms.CheckedListBox();
            this.cmbo_expand_url = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gv_url_shortners)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gv_url_shortners
            // 
            this.gv_url_shortners.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gv_url_shortners.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_url_shortners.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Domain});
            this.gv_url_shortners.Location = new System.Drawing.Point(6, 19);
            this.gv_url_shortners.Name = "gv_url_shortners";
            this.gv_url_shortners.RowHeadersWidth = 30;
            this.gv_url_shortners.Size = new System.Drawing.Size(366, 177);
            this.gv_url_shortners.TabIndex = 0;
            this.gv_url_shortners.EnabledChanged += new System.EventHandler(this.DataGridView_EnabledChanged);
            // 
            // Domain
            // 
            this.Domain.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Domain.DataPropertyName = "Domain";
            this.Domain.HeaderText = "Domain";
            this.Domain.MinimumWidth = 10;
            this.Domain.Name = "Domain";
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.Location = new System.Drawing.Point(313, 285);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ok.Location = new System.Drawing.Point(232, 285);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 2;
            this.btn_ok.Text = "&OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.gv_url_shortners);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 202);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "URL Shortners";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chk_box_url_expanders);
            this.groupBox2.Location = new System.Drawing.Point(10, 215);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(378, 64);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Custom URL Processors";
            // 
            // chk_box_url_expanders
            // 
            this.chk_box_url_expanders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_box_url_expanders.CheckOnClick = true;
            this.chk_box_url_expanders.FormattingEnabled = true;
            this.chk_box_url_expanders.Location = new System.Drawing.Point(6, 18);
            this.chk_box_url_expanders.Name = "chk_box_url_expanders";
            this.chk_box_url_expanders.Size = new System.Drawing.Size(366, 34);
            this.chk_box_url_expanders.TabIndex = 1;
            // 
            // cmbo_expand_url
            // 
            this.cmbo_expand_url.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbo_expand_url.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbo_expand_url.FormattingEnabled = true;
            this.cmbo_expand_url.Location = new System.Drawing.Point(92, 286);
            this.cmbo_expand_url.Name = "cmbo_expand_url";
            this.cmbo_expand_url.Size = new System.Drawing.Size(130, 21);
            this.cmbo_expand_url.TabIndex = 5;
            this.cmbo_expand_url.SelectedIndexChanged += new System.EventHandler(this.cmbo_expand_url_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 290);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Expander URLs:";
            // 
            // frm_settings_urlexpander
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 314);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbo_expand_url);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frm_settings_urlexpander";
            this.Text = "URL Expander Config";
            this.Load += new System.EventHandler(this.frm_settings_urlexpander_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gv_url_shortners)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gv_url_shortners;
        private System.Windows.Forms.DataGridViewTextBoxColumn Domain;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox chk_box_url_expanders;
        private System.Windows.Forms.ComboBox cmbo_expand_url;
        private System.Windows.Forms.Label label1;
    }
}