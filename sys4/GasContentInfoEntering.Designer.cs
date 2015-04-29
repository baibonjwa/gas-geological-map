namespace sys4
{
    partial class GasContentInfoEntering
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.gbTunnel = new System.Windows.Forms.GroupBox();
            this.gbGasContentInfo = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpMeasureDateTime = new System.Windows.Forms.DateTimePicker();
            this.lblMeasureDateTime = new System.Windows.Forms.Label();
            this.txtGasContentValue = new System.Windows.Forms.TextBox();
            this.txtDepth = new System.Windows.Forms.TextBox();
            this.txtCoordinateZ = new System.Windows.Forms.TextBox();
            this.txtCoordinateY = new System.Windows.Forms.TextBox();
            this.txtCoordinateX = new System.Windows.Forms.TextBox();
            this.lblGasContentValue = new System.Windows.Forms.Label();
            this.lblDepth = new System.Windows.Forms.Label();
            this.lblCoordinateZ = new System.Windows.Forms.Label();
            this.lblCoordinateY = new System.Windows.Forms.Label();
            this.lblCoordinateX = new System.Windows.Forms.Label();
            this.lblCoalSeams = new System.Windows.Forms.Label();
            this.btnAddCoalSeams = new System.Windows.Forms.Button();
            this.gbCoalSeams = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboCoalSeams = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.selectTunnelSimple1 = new LibCommonForm.SelectTunnelSimple();
            this.gbTunnel.SuspendLayout();
            this.gbGasContentInfo.SuspendLayout();
            this.gbCoalSeams.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(523, 256);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSubmit.Location = new System.Drawing.Point(442, 256);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // gbTunnel
            // 
            this.gbTunnel.Controls.Add(this.label7);
            this.gbTunnel.Controls.Add(this.selectTunnelSimple1);
            this.gbTunnel.Location = new System.Drawing.Point(12, 12);
            this.gbTunnel.Name = "gbTunnel";
            this.gbTunnel.Size = new System.Drawing.Size(583, 68);
            this.gbTunnel.TabIndex = 0;
            this.gbTunnel.TabStop = false;
            this.gbTunnel.Text = "所在巷道";
            // 
            // gbGasContentInfo
            // 
            this.gbGasContentInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbGasContentInfo.Controls.Add(this.label5);
            this.gbGasContentInfo.Controls.Add(this.label4);
            this.gbGasContentInfo.Controls.Add(this.label3);
            this.gbGasContentInfo.Controls.Add(this.label2);
            this.gbGasContentInfo.Controls.Add(this.label1);
            this.gbGasContentInfo.Controls.Add(this.dtpMeasureDateTime);
            this.gbGasContentInfo.Controls.Add(this.lblMeasureDateTime);
            this.gbGasContentInfo.Controls.Add(this.txtGasContentValue);
            this.gbGasContentInfo.Controls.Add(this.txtDepth);
            this.gbGasContentInfo.Controls.Add(this.txtCoordinateZ);
            this.gbGasContentInfo.Controls.Add(this.txtCoordinateY);
            this.gbGasContentInfo.Controls.Add(this.txtCoordinateX);
            this.gbGasContentInfo.Controls.Add(this.lblGasContentValue);
            this.gbGasContentInfo.Controls.Add(this.lblDepth);
            this.gbGasContentInfo.Controls.Add(this.lblCoordinateZ);
            this.gbGasContentInfo.Controls.Add(this.lblCoordinateY);
            this.gbGasContentInfo.Controls.Add(this.lblCoordinateX);
            this.gbGasContentInfo.Location = new System.Drawing.Point(12, 135);
            this.gbGasContentInfo.Name = "gbGasContentInfo";
            this.gbGasContentInfo.Size = new System.Drawing.Size(583, 102);
            this.gbGasContentInfo.TabIndex = 3;
            this.gbGasContentInfo.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(191, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(191, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(545, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(361, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(191, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "*";
            // 
            // dtpMeasureDateTime
            // 
            this.dtpMeasureDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMeasureDateTime.Location = new System.Drawing.Point(273, 72);
            this.dtpMeasureDateTime.Name = "dtpMeasureDateTime";
            this.dtpMeasureDateTime.Size = new System.Drawing.Size(160, 21);
            this.dtpMeasureDateTime.TabIndex = 16;
            // 
            // lblMeasureDateTime
            // 
            this.lblMeasureDateTime.AutoSize = true;
            this.lblMeasureDateTime.Location = new System.Drawing.Point(212, 75);
            this.lblMeasureDateTime.Name = "lblMeasureDateTime";
            this.lblMeasureDateTime.Size = new System.Drawing.Size(65, 12);
            this.lblMeasureDateTime.TabIndex = 15;
            this.lblMeasureDateTime.Text = "测定时间：";
            // 
            // txtGasContentValue
            // 
            this.txtGasContentValue.Location = new System.Drawing.Point(85, 75);
            this.txtGasContentValue.MaxLength = 15;
            this.txtGasContentValue.Name = "txtGasContentValue";
            this.txtGasContentValue.Size = new System.Drawing.Size(100, 21);
            this.txtGasContentValue.TabIndex = 13;
            // 
            // txtDepth
            // 
            this.txtDepth.Location = new System.Drawing.Point(85, 44);
            this.txtDepth.MaxLength = 15;
            this.txtDepth.Name = "txtDepth";
            this.txtDepth.Size = new System.Drawing.Size(100, 21);
            this.txtDepth.TabIndex = 10;
            // 
            // txtCoordinateZ
            // 
            this.txtCoordinateZ.Location = new System.Drawing.Point(439, 14);
            this.txtCoordinateZ.MaxLength = 15;
            this.txtCoordinateZ.Name = "txtCoordinateZ";
            this.txtCoordinateZ.Size = new System.Drawing.Size(100, 21);
            this.txtCoordinateZ.TabIndex = 7;
            // 
            // txtCoordinateY
            // 
            this.txtCoordinateY.Location = new System.Drawing.Point(255, 14);
            this.txtCoordinateY.MaxLength = 15;
            this.txtCoordinateY.Name = "txtCoordinateY";
            this.txtCoordinateY.Size = new System.Drawing.Size(100, 21);
            this.txtCoordinateY.TabIndex = 4;
            // 
            // txtCoordinateX
            // 
            this.txtCoordinateX.Location = new System.Drawing.Point(85, 14);
            this.txtCoordinateX.MaxLength = 15;
            this.txtCoordinateX.Name = "txtCoordinateX";
            this.txtCoordinateX.Size = new System.Drawing.Size(100, 21);
            this.txtCoordinateX.TabIndex = 1;
            // 
            // lblGasContentValue
            // 
            this.lblGasContentValue.AutoSize = true;
            this.lblGasContentValue.Location = new System.Drawing.Point(12, 78);
            this.lblGasContentValue.Name = "lblGasContentValue";
            this.lblGasContentValue.Size = new System.Drawing.Size(77, 12);
            this.lblGasContentValue.TabIndex = 12;
            this.lblGasContentValue.Text = "瓦斯含量值：";
            // 
            // lblDepth
            // 
            this.lblDepth.AutoSize = true;
            this.lblDepth.Location = new System.Drawing.Point(48, 47);
            this.lblDepth.Name = "lblDepth";
            this.lblDepth.Size = new System.Drawing.Size(41, 12);
            this.lblDepth.TabIndex = 9;
            this.lblDepth.Text = "埋深：";
            // 
            // lblCoordinateZ
            // 
            this.lblCoordinateZ.AutoSize = true;
            this.lblCoordinateZ.Location = new System.Drawing.Point(378, 17);
            this.lblCoordinateZ.Name = "lblCoordinateZ";
            this.lblCoordinateZ.Size = new System.Drawing.Size(65, 12);
            this.lblCoordinateZ.TabIndex = 6;
            this.lblCoordinateZ.Text = "测点标高：";
            // 
            // lblCoordinateY
            // 
            this.lblCoordinateY.AutoSize = true;
            this.lblCoordinateY.Location = new System.Drawing.Point(212, 17);
            this.lblCoordinateY.Name = "lblCoordinateY";
            this.lblCoordinateY.Size = new System.Drawing.Size(47, 12);
            this.lblCoordinateY.TabIndex = 3;
            this.lblCoordinateY.Text = "坐标Y：";
            // 
            // lblCoordinateX
            // 
            this.lblCoordinateX.AutoSize = true;
            this.lblCoordinateX.Location = new System.Drawing.Point(42, 17);
            this.lblCoordinateX.Name = "lblCoordinateX";
            this.lblCoordinateX.Size = new System.Drawing.Size(47, 12);
            this.lblCoordinateX.TabIndex = 0;
            this.lblCoordinateX.Text = "坐标X：";
            // 
            // lblCoalSeams
            // 
            this.lblCoalSeams.AutoSize = true;
            this.lblCoalSeams.Location = new System.Drawing.Point(24, 18);
            this.lblCoalSeams.Name = "lblCoalSeams";
            this.lblCoalSeams.Size = new System.Drawing.Size(65, 12);
            this.lblCoalSeams.TabIndex = 0;
            this.lblCoalSeams.Text = "所在煤层：";
            // 
            // btnAddCoalSeams
            // 
            this.btnAddCoalSeams.Location = new System.Drawing.Point(212, 15);
            this.btnAddCoalSeams.Name = "btnAddCoalSeams";
            this.btnAddCoalSeams.Size = new System.Drawing.Size(32, 20);
            this.btnAddCoalSeams.TabIndex = 3;
            this.btnAddCoalSeams.Text = "+";
            this.btnAddCoalSeams.UseVisualStyleBackColor = true;
            this.btnAddCoalSeams.Click += new System.EventHandler(this.btnAddCoalSeams_Click);
            // 
            // gbCoalSeams
            // 
            this.gbCoalSeams.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbCoalSeams.Controls.Add(this.label6);
            this.gbCoalSeams.Controls.Add(this.cboCoalSeams);
            this.gbCoalSeams.Controls.Add(this.lblCoalSeams);
            this.gbCoalSeams.Controls.Add(this.btnAddCoalSeams);
            this.gbCoalSeams.Location = new System.Drawing.Point(12, 86);
            this.gbCoalSeams.Name = "gbCoalSeams";
            this.gbCoalSeams.Size = new System.Drawing.Size(585, 43);
            this.gbCoalSeams.TabIndex = 2;
            this.gbCoalSeams.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(195, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "*";
            // 
            // cboCoalSeams
            // 
            this.cboCoalSeams.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCoalSeams.FormattingEnabled = true;
            this.cboCoalSeams.Location = new System.Drawing.Point(85, 15);
            this.cboCoalSeams.Name = "cboCoalSeams";
            this.cboCoalSeams.Size = new System.Drawing.Size(104, 20);
            this.cboCoalSeams.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(222, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "*";
            // 
            // selectTunnelSimple1
            // 
            this.selectTunnelSimple1.Location = new System.Drawing.Point(14, 20);
            this.selectTunnelSimple1.Name = "selectTunnelSimple1";
            this.selectTunnelSimple1.Size = new System.Drawing.Size(219, 38);
            this.selectTunnelSimple1.TabIndex = 0;
            // 
            // GasContentInfoEntering
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(610, 291);
            this.Controls.Add(this.gbCoalSeams);
            this.Controls.Add(this.gbGasContentInfo);
            this.Controls.Add(this.gbTunnel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GasContentInfoEntering";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "瓦斯含量数据录入";
            this.Load += new System.EventHandler(this.GasContentInfoEntering_Load);
            this.gbTunnel.ResumeLayout(false);
            this.gbTunnel.PerformLayout();
            this.gbGasContentInfo.ResumeLayout(false);
            this.gbGasContentInfo.PerformLayout();
            this.gbCoalSeams.ResumeLayout(false);
            this.gbCoalSeams.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.GroupBox gbTunnel;
        private System.Windows.Forms.GroupBox gbGasContentInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpMeasureDateTime;
        private System.Windows.Forms.Label lblMeasureDateTime;
        private System.Windows.Forms.TextBox txtGasContentValue;
        private System.Windows.Forms.TextBox txtDepth;
        private System.Windows.Forms.TextBox txtCoordinateZ;
        private System.Windows.Forms.TextBox txtCoordinateY;
        private System.Windows.Forms.TextBox txtCoordinateX;
        private System.Windows.Forms.Label lblGasContentValue;
        private System.Windows.Forms.Label lblDepth;
        private System.Windows.Forms.Label lblCoordinateZ;
        private System.Windows.Forms.Label lblCoordinateY;
        private System.Windows.Forms.Label lblCoordinateX;
        private System.Windows.Forms.Button btnAddCoalSeams;
        private System.Windows.Forms.Label lblCoalSeams;
        private System.Windows.Forms.GroupBox gbCoalSeams;
        private System.Windows.Forms.ComboBox cboCoalSeams;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private LibCommonForm.SelectTunnelSimple selectTunnelSimple1;
    }
}