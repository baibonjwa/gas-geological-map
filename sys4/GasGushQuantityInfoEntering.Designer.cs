namespace sys4
{
    partial class GasGushQuantityInfoEntering
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
            this.dtpStopeDate = new System.Windows.Forms.DateTimePicker();
            this.lblStopeDate = new System.Windows.Forms.Label();
            this.txtRelativeGasGushQuantity = new System.Windows.Forms.TextBox();
            this.lblRelativeGasGushQuantity = new System.Windows.Forms.Label();
            this.lblAbsoluteGasGushQuantity = new System.Windows.Forms.Label();
            this.lblCoordinateZ = new System.Windows.Forms.Label();
            this.lblCoordinateY = new System.Windows.Forms.Label();
            this.lblCoordinateX = new System.Windows.Forms.Label();
            this.lblWorkingFaceDayOutput = new System.Windows.Forms.Label();
            this.txtWorkingFaceDayOutput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.gbTunnel = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAbsoluteGasGushQuantity = new System.Windows.Forms.TextBox();
            this.txtCoordinateZ = new System.Windows.Forms.TextBox();
            this.txtCoordinateY = new System.Windows.Forms.TextBox();
            this.txtCoordinateX = new System.Windows.Forms.TextBox();
            this.gbCoalSeams = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboCoalSeams = new System.Windows.Forms.ComboBox();
            this.lblCoalSeams = new System.Windows.Forms.Label();
            this.btnAddCoalSeams = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.selectTunnelSimple1 = new LibCommonForm.SelectTunnelSimple();
            this.gbTunnel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbCoalSeams.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(523, 247);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSubmit.Location = new System.Drawing.Point(442, 247);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // dtpStopeDate
            // 
            this.dtpStopeDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStopeDate.Location = new System.Drawing.Point(352, 74);
            this.dtpStopeDate.Name = "dtpStopeDate";
            this.dtpStopeDate.Size = new System.Drawing.Size(116, 21);
            this.dtpStopeDate.TabIndex = 19;
            // 
            // lblStopeDate
            // 
            this.lblStopeDate.AutoSize = true;
            this.lblStopeDate.Location = new System.Drawing.Point(291, 80);
            this.lblStopeDate.Name = "lblStopeDate";
            this.lblStopeDate.Size = new System.Drawing.Size(65, 12);
            this.lblStopeDate.TabIndex = 18;
            this.lblStopeDate.Text = "回采年月：";
            // 
            // txtRelativeGasGushQuantity
            // 
            this.txtRelativeGasGushQuantity.Location = new System.Drawing.Point(352, 45);
            this.txtRelativeGasGushQuantity.MaxLength = 15;
            this.txtRelativeGasGushQuantity.Name = "txtRelativeGasGushQuantity";
            this.txtRelativeGasGushQuantity.Size = new System.Drawing.Size(116, 21);
            this.txtRelativeGasGushQuantity.TabIndex = 13;
            // 
            // lblRelativeGasGushQuantity
            // 
            this.lblRelativeGasGushQuantity.AutoSize = true;
            this.lblRelativeGasGushQuantity.Location = new System.Drawing.Point(255, 48);
            this.lblRelativeGasGushQuantity.Name = "lblRelativeGasGushQuantity";
            this.lblRelativeGasGushQuantity.Size = new System.Drawing.Size(101, 12);
            this.lblRelativeGasGushQuantity.TabIndex = 12;
            this.lblRelativeGasGushQuantity.Text = "相对瓦斯涌出量：";
            // 
            // lblAbsoluteGasGushQuantity
            // 
            this.lblAbsoluteGasGushQuantity.AutoSize = true;
            this.lblAbsoluteGasGushQuantity.Location = new System.Drawing.Point(15, 48);
            this.lblAbsoluteGasGushQuantity.Name = "lblAbsoluteGasGushQuantity";
            this.lblAbsoluteGasGushQuantity.Size = new System.Drawing.Size(101, 12);
            this.lblAbsoluteGasGushQuantity.TabIndex = 9;
            this.lblAbsoluteGasGushQuantity.Text = "绝对瓦斯涌出量：";
            // 
            // lblCoordinateZ
            // 
            this.lblCoordinateZ.AutoSize = true;
            this.lblCoordinateZ.Location = new System.Drawing.Point(379, 17);
            this.lblCoordinateZ.Name = "lblCoordinateZ";
            this.lblCoordinateZ.Size = new System.Drawing.Size(47, 12);
            this.lblCoordinateZ.TabIndex = 6;
            this.lblCoordinateZ.Text = "坐标Z：";
            // 
            // lblCoordinateY
            // 
            this.lblCoordinateY.AutoSize = true;
            this.lblCoordinateY.Location = new System.Drawing.Point(197, 17);
            this.lblCoordinateY.Name = "lblCoordinateY";
            this.lblCoordinateY.Size = new System.Drawing.Size(47, 12);
            this.lblCoordinateY.TabIndex = 3;
            this.lblCoordinateY.Text = "坐标Y：";
            // 
            // lblCoordinateX
            // 
            this.lblCoordinateX.AutoSize = true;
            this.lblCoordinateX.Location = new System.Drawing.Point(15, 17);
            this.lblCoordinateX.Name = "lblCoordinateX";
            this.lblCoordinateX.Size = new System.Drawing.Size(47, 12);
            this.lblCoordinateX.TabIndex = 0;
            this.lblCoordinateX.Text = "坐标X：";
            // 
            // lblWorkingFaceDayOutput
            // 
            this.lblWorkingFaceDayOutput.AutoSize = true;
            this.lblWorkingFaceDayOutput.Location = new System.Drawing.Point(27, 80);
            this.lblWorkingFaceDayOutput.Name = "lblWorkingFaceDayOutput";
            this.lblWorkingFaceDayOutput.Size = new System.Drawing.Size(89, 12);
            this.lblWorkingFaceDayOutput.TabIndex = 15;
            this.lblWorkingFaceDayOutput.Text = "工作面日产量：";
            // 
            // txtWorkingFaceDayOutput
            // 
            this.txtWorkingFaceDayOutput.Location = new System.Drawing.Point(112, 77);
            this.txtWorkingFaceDayOutput.MaxLength = 15;
            this.txtWorkingFaceDayOutput.Name = "txtWorkingFaceDayOutput";
            this.txtWorkingFaceDayOutput.Size = new System.Drawing.Size(116, 21);
            this.txtWorkingFaceDayOutput.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(180, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(362, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "*";
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(235, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(474, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(234, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "*";
            // 
            // gbTunnel
            // 
            this.gbTunnel.Controls.Add(this.label8);
            this.gbTunnel.Controls.Add(this.selectTunnelSimple1);
            this.gbTunnel.Location = new System.Drawing.Point(12, 12);
            this.gbTunnel.Name = "gbTunnel";
            this.gbTunnel.Size = new System.Drawing.Size(583, 62);
            this.gbTunnel.TabIndex = 0;
            this.gbTunnel.TabStop = false;
            this.gbTunnel.Text = "所在巷道";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.txtAbsoluteGasGushQuantity);
            this.groupBox1.Controls.Add(this.txtCoordinateZ);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtCoordinateY);
            this.groupBox1.Controls.Add(this.dtpStopeDate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblStopeDate);
            this.groupBox1.Controls.Add(this.txtCoordinateX);
            this.groupBox1.Controls.Add(this.lblCoordinateX);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtWorkingFaceDayOutput);
            this.groupBox1.Controls.Add(this.lblCoordinateY);
            this.groupBox1.Controls.Add(this.lblWorkingFaceDayOutput);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtRelativeGasGushQuantity);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblCoordinateZ);
            this.groupBox1.Controls.Add(this.lblRelativeGasGushQuantity);
            this.groupBox1.Controls.Add(this.lblAbsoluteGasGushQuantity);
            this.groupBox1.Location = new System.Drawing.Point(12, 123);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(583, 104);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // txtAbsoluteGasGushQuantity
            // 
            this.txtAbsoluteGasGushQuantity.Location = new System.Drawing.Point(113, 45);
            this.txtAbsoluteGasGushQuantity.MaxLength = 15;
            this.txtAbsoluteGasGushQuantity.Name = "txtAbsoluteGasGushQuantity";
            this.txtAbsoluteGasGushQuantity.Size = new System.Drawing.Size(116, 21);
            this.txtAbsoluteGasGushQuantity.TabIndex = 10;
            // 
            // txtCoordinateZ
            // 
            this.txtCoordinateZ.Location = new System.Drawing.Point(423, 14);
            this.txtCoordinateZ.MaxLength = 15;
            this.txtCoordinateZ.Name = "txtCoordinateZ";
            this.txtCoordinateZ.Size = new System.Drawing.Size(116, 21);
            this.txtCoordinateZ.TabIndex = 7;
            // 
            // txtCoordinateY
            // 
            this.txtCoordinateY.Location = new System.Drawing.Point(240, 14);
            this.txtCoordinateY.MaxLength = 15;
            this.txtCoordinateY.Name = "txtCoordinateY";
            this.txtCoordinateY.Size = new System.Drawing.Size(116, 21);
            this.txtCoordinateY.TabIndex = 4;
            // 
            // txtCoordinateX
            // 
            this.txtCoordinateX.Location = new System.Drawing.Point(58, 14);
            this.txtCoordinateX.MaxLength = 15;
            this.txtCoordinateX.Name = "txtCoordinateX";
            this.txtCoordinateX.Size = new System.Drawing.Size(116, 21);
            this.txtCoordinateX.TabIndex = 1;
            // 
            // gbCoalSeams
            // 
            this.gbCoalSeams.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbCoalSeams.Controls.Add(this.label7);
            this.gbCoalSeams.Controls.Add(this.cboCoalSeams);
            this.gbCoalSeams.Controls.Add(this.lblCoalSeams);
            this.gbCoalSeams.Controls.Add(this.btnAddCoalSeams);
            this.gbCoalSeams.Location = new System.Drawing.Point(12, 78);
            this.gbCoalSeams.Name = "gbCoalSeams";
            this.gbCoalSeams.Size = new System.Drawing.Size(585, 43);
            this.gbCoalSeams.TabIndex = 1;
            this.gbCoalSeams.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(186, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "*";
            // 
            // cboCoalSeams
            // 
            this.cboCoalSeams.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCoalSeams.FormattingEnabled = true;
            this.cboCoalSeams.Location = new System.Drawing.Point(76, 14);
            this.cboCoalSeams.Name = "cboCoalSeams";
            this.cboCoalSeams.Size = new System.Drawing.Size(104, 20);
            this.cboCoalSeams.TabIndex = 1;
            // 
            // lblCoalSeams
            // 
            this.lblCoalSeams.AutoSize = true;
            this.lblCoalSeams.Location = new System.Drawing.Point(15, 17);
            this.lblCoalSeams.Name = "lblCoalSeams";
            this.lblCoalSeams.Size = new System.Drawing.Size(65, 12);
            this.lblCoalSeams.TabIndex = 0;
            this.lblCoalSeams.Text = "所在煤层：";
            // 
            // btnAddCoalSeams
            // 
            this.btnAddCoalSeams.Location = new System.Drawing.Point(203, 14);
            this.btnAddCoalSeams.Name = "btnAddCoalSeams";
            this.btnAddCoalSeams.Size = new System.Drawing.Size(32, 20);
            this.btnAddCoalSeams.TabIndex = 3;
            this.btnAddCoalSeams.Text = "+";
            this.btnAddCoalSeams.UseVisualStyleBackColor = true;
            this.btnAddCoalSeams.Click += new System.EventHandler(this.btnAddCoalSeams_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(224, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 9;
            this.label8.Text = "*";
            // 
            // selectTunnelSimple1
            // 
            this.selectTunnelSimple1.Location = new System.Drawing.Point(16, 17);
            this.selectTunnelSimple1.Name = "selectTunnelSimple1";
            this.selectTunnelSimple1.Size = new System.Drawing.Size(219, 38);
            this.selectTunnelSimple1.TabIndex = 0;
            // 
            // GasGushQuantityInfoEntering
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(610, 282);
            this.Controls.Add(this.gbCoalSeams);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbTunnel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GasGushQuantityInfoEntering";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "瓦斯涌出量数据录入";
            this.Load += new System.EventHandler(this.GasGushQuantityInfoEntering_Load);
            this.gbTunnel.ResumeLayout(false);
            this.gbTunnel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbCoalSeams.ResumeLayout(false);
            this.gbCoalSeams.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.DateTimePicker dtpStopeDate;
        private System.Windows.Forms.Label lblStopeDate;
        private System.Windows.Forms.TextBox txtRelativeGasGushQuantity;
        private System.Windows.Forms.Label lblRelativeGasGushQuantity;
        private System.Windows.Forms.Label lblAbsoluteGasGushQuantity;
        private System.Windows.Forms.Label lblCoordinateZ;
        private System.Windows.Forms.Label lblCoordinateY;
        private System.Windows.Forms.Label lblCoordinateX;
        private System.Windows.Forms.Label lblWorkingFaceDayOutput;
        private System.Windows.Forms.TextBox txtWorkingFaceDayOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox gbTunnel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtAbsoluteGasGushQuantity;
        private System.Windows.Forms.TextBox txtCoordinateZ;
        private System.Windows.Forms.TextBox txtCoordinateY;
        private System.Windows.Forms.TextBox txtCoordinateX;
        private System.Windows.Forms.GroupBox gbCoalSeams;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboCoalSeams;
        private System.Windows.Forms.Label lblCoalSeams;
        private System.Windows.Forms.Button btnAddCoalSeams;
        private System.Windows.Forms.Label label8;
        private LibCommonForm.SelectTunnelSimple selectTunnelSimple1;
    }
}