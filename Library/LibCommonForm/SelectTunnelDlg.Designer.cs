namespace LibCommonForm
{
    partial class SelectTunnelDlg
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
            this.gbTunnel = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.selectTunnelUserControl1 = new LibCommonForm.SelectTunnelUserControl();
            this.tMINEINFOBindingSource = new System.Windows.Forms.BindingSource();
            this.gbTunnel.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbTunnel
            // 
            this.gbTunnel.Controls.Add(this.selectTunnelUserControl1);
            this.gbTunnel.Location = new System.Drawing.Point(16, 6);
            this.gbTunnel.Name = "gbTunnel";
            this.gbTunnel.Size = new System.Drawing.Size(680, 238);
            this.gbTunnel.TabIndex = 10;
            this.gbTunnel.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(605, 259);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 27);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSubmit.Location = new System.Drawing.Point(512, 259);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(87, 27);
            this.btnSubmit.TabIndex = 13;
            this.btnSubmit.Text = "确定";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // selectTunnelUserControl1
            // 
            this.selectTunnelUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectTunnelUserControl1.Location = new System.Drawing.Point(3, 18);
            this.selectTunnelUserControl1.Name = "selectTunnelUserControl1";
            this.selectTunnelUserControl1.Size = new System.Drawing.Size(674, 217);
            this.selectTunnelUserControl1.TabIndex = 0;
            // 
            // SelectTunnelDlg
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(728, 309);
            this.Controls.Add(this.gbTunnel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectTunnelDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择巷道";
            this.gbTunnel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbTunnel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private LibCommonForm.SelectTunnelUserControl selectTunnelUserControl1;
        private System.Windows.Forms.BindingSource tMINEINFOBindingSource;


    }
}