namespace LibCommonForm
{
    partial class SelectTunnelSimple
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbxTunnel = new System.Windows.Forms.ComboBox();
            this.btnChooseTunnel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbxTunnel
            // 
            this.cbxTunnel.DisplayMember = "TunnelName";
            this.cbxTunnel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTunnel.FormattingEnabled = true;
            this.cbxTunnel.Location = new System.Drawing.Point(3, 9);
            this.cbxTunnel.Name = "cbxTunnel";
            this.cbxTunnel.Size = new System.Drawing.Size(119, 20);
            this.cbxTunnel.TabIndex = 1;
            this.cbxTunnel.ValueMember = "TunnelId";
            this.cbxTunnel.SelectedIndexChanged += new System.EventHandler(this.cbxTunnel_SelectedIndexChanged);
            this.cbxTunnel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbxTunnel_KeyDown);
            // 
            // btnChooseTunnel
            // 
            this.btnChooseTunnel.Location = new System.Drawing.Point(128, 7);
            this.btnChooseTunnel.Name = "btnChooseTunnel";
            this.btnChooseTunnel.Size = new System.Drawing.Size(75, 23);
            this.btnChooseTunnel.TabIndex = 2;
            this.btnChooseTunnel.Text = "选择巷道";
            this.btnChooseTunnel.UseVisualStyleBackColor = true;
            this.btnChooseTunnel.Click += new System.EventHandler(this.btnChooseTunnel_Click);
            // 
            // SelectTunnelSimple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnChooseTunnel);
            this.Controls.Add(this.cbxTunnel);
            this.Name = "SelectTunnelSimple";
            this.Size = new System.Drawing.Size(219, 38);
            this.Load += new System.EventHandler(this.SelectTunnelSimple_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox cbxTunnel;
        private System.Windows.Forms.Button btnChooseTunnel;
    }
}
