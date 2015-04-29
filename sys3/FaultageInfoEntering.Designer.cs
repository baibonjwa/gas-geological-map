namespace sys3
{
    partial class FaultageInfoEntering
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
            this.lblFaultageName = new System.Windows.Forms.Label();
            this.txtFaultageName = new System.Windows.Forms.TextBox();
            this.lblGap = new System.Windows.Forms.Label();
            this.txtGap = new System.Windows.Forms.TextBox();
            this.lblAngle = new System.Windows.Forms.Label();
            this.txtAngle = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.rbtnFrontFaultage = new System.Windows.Forms.RadioButton();
            this.rbtnOppositeFaultage = new System.Windows.Forms.RadioButton();
            this.lblSeparation = new System.Windows.Forms.Label();
            this.txtSeparation = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtCoordinateZ = new System.Windows.Forms.TextBox();
            this.txtCoordinateY = new System.Windows.Forms.TextBox();
            this.txtCoordinateX = new System.Windows.Forms.TextBox();
            this.lblCoordinateZ = new System.Windows.Forms.Label();
            this.lblCoordinateY = new System.Windows.Forms.Label();
            this.lblCoordinateX = new System.Windows.Forms.Label();
            this.gbCoordinate = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTrend = new System.Windows.Forms.Label();
            this.txtTrend = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlType = new System.Windows.Forms.Panel();
            this.gbImage = new System.Windows.Forms.GroupBox();
            this.btnDetails = new System.Windows.Forms.Button();
            this.pbCount = new System.Windows.Forms.ProgressBar();
            this.lblSuccessed = new System.Windows.Forms.Label();
            this.btnMultTxt = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.btnReadTxt = new System.Windows.Forms.Button();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.gbCoordinate.SuspendLayout();
            this.pnlType.SuspendLayout();
            this.gbImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFaultageName
            // 
            this.lblFaultageName.AutoSize = true;
            this.lblFaultageName.Location = new System.Drawing.Point(13, 13);
            this.lblFaultageName.Name = "lblFaultageName";
            this.lblFaultageName.Size = new System.Drawing.Size(65, 12);
            this.lblFaultageName.TabIndex = 0;
            this.lblFaultageName.Text = "断层名称：";
            // 
            // txtFaultageName
            // 
            this.txtFaultageName.Location = new System.Drawing.Point(72, 10);
            this.txtFaultageName.MaxLength = 15;
            this.txtFaultageName.Name = "txtFaultageName";
            this.txtFaultageName.Size = new System.Drawing.Size(134, 21);
            this.txtFaultageName.TabIndex = 1;
            // 
            // lblGap
            // 
            this.lblGap.AutoSize = true;
            this.lblGap.Location = new System.Drawing.Point(13, 40);
            this.lblGap.Name = "lblGap";
            this.lblGap.Size = new System.Drawing.Size(65, 12);
            this.lblGap.TabIndex = 2;
            this.lblGap.Text = "落    差：";
            // 
            // txtGap
            // 
            this.txtGap.Location = new System.Drawing.Point(72, 37);
            this.txtGap.MaxLength = 15;
            this.txtGap.Name = "txtGap";
            this.txtGap.Size = new System.Drawing.Size(134, 21);
            this.txtGap.TabIndex = 3;
            // 
            // lblAngle
            // 
            this.lblAngle.AutoSize = true;
            this.lblAngle.Location = new System.Drawing.Point(13, 132);
            this.lblAngle.Name = "lblAngle";
            this.lblAngle.Size = new System.Drawing.Size(65, 12);
            this.lblAngle.TabIndex = 4;
            this.lblAngle.Text = "走    向：";
            // 
            // txtAngle
            // 
            this.txtAngle.Location = new System.Drawing.Point(72, 64);
            this.txtAngle.MaxLength = 15;
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new System.Drawing.Size(134, 21);
            this.txtAngle.TabIndex = 5;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(12, 102);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(65, 12);
            this.lblType.TabIndex = 6;
            this.lblType.Text = "类    型：";
            // 
            // rbtnFrontFaultage
            // 
            this.rbtnFrontFaultage.AutoSize = true;
            this.rbtnFrontFaultage.Checked = true;
            this.rbtnFrontFaultage.Location = new System.Drawing.Point(4, 7);
            this.rbtnFrontFaultage.Name = "rbtnFrontFaultage";
            this.rbtnFrontFaultage.Size = new System.Drawing.Size(59, 16);
            this.rbtnFrontFaultage.TabIndex = 0;
            this.rbtnFrontFaultage.TabStop = true;
            this.rbtnFrontFaultage.Text = "正断层";
            this.rbtnFrontFaultage.UseVisualStyleBackColor = true;
            // 
            // rbtnOppositeFaultage
            // 
            this.rbtnOppositeFaultage.AutoSize = true;
            this.rbtnOppositeFaultage.Location = new System.Drawing.Point(62, 7);
            this.rbtnOppositeFaultage.Name = "rbtnOppositeFaultage";
            this.rbtnOppositeFaultage.Size = new System.Drawing.Size(59, 16);
            this.rbtnOppositeFaultage.TabIndex = 1;
            this.rbtnOppositeFaultage.TabStop = true;
            this.rbtnOppositeFaultage.Text = "逆断层";
            this.rbtnOppositeFaultage.UseVisualStyleBackColor = true;
            // 
            // lblSeparation
            // 
            this.lblSeparation.AutoSize = true;
            this.lblSeparation.Location = new System.Drawing.Point(14, 159);
            this.lblSeparation.Name = "lblSeparation";
            this.lblSeparation.Size = new System.Drawing.Size(65, 12);
            this.lblSeparation.TabIndex = 11;
            this.lblSeparation.Text = "断    距：";
            // 
            // txtSeparation
            // 
            this.txtSeparation.Location = new System.Drawing.Point(73, 156);
            this.txtSeparation.MaxLength = 15;
            this.txtSeparation.Name = "txtSeparation";
            this.txtSeparation.Size = new System.Drawing.Size(134, 21);
            this.txtSeparation.TabIndex = 12;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(131, 224);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSubmit.Location = new System.Drawing.Point(50, 224);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 16;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtCoordinateZ
            // 
            this.txtCoordinateZ.Location = new System.Drawing.Point(58, 67);
            this.txtCoordinateZ.MaxLength = 15;
            this.txtCoordinateZ.Name = "txtCoordinateZ";
            this.txtCoordinateZ.Size = new System.Drawing.Size(100, 21);
            this.txtCoordinateZ.TabIndex = 7;
            this.txtCoordinateZ.Text = "0";
            // 
            // txtCoordinateY
            // 
            this.txtCoordinateY.Location = new System.Drawing.Point(58, 40);
            this.txtCoordinateY.MaxLength = 15;
            this.txtCoordinateY.Name = "txtCoordinateY";
            this.txtCoordinateY.Size = new System.Drawing.Size(100, 21);
            this.txtCoordinateY.TabIndex = 4;
            // 
            // txtCoordinateX
            // 
            this.txtCoordinateX.Location = new System.Drawing.Point(58, 13);
            this.txtCoordinateX.MaxLength = 15;
            this.txtCoordinateX.Name = "txtCoordinateX";
            this.txtCoordinateX.Size = new System.Drawing.Size(100, 21);
            this.txtCoordinateX.TabIndex = 1;
            // 
            // lblCoordinateZ
            // 
            this.lblCoordinateZ.AutoSize = true;
            this.lblCoordinateZ.Location = new System.Drawing.Point(15, 70);
            this.lblCoordinateZ.Name = "lblCoordinateZ";
            this.lblCoordinateZ.Size = new System.Drawing.Size(47, 12);
            this.lblCoordinateZ.TabIndex = 6;
            this.lblCoordinateZ.Text = "坐标Z：";
            // 
            // lblCoordinateY
            // 
            this.lblCoordinateY.AutoSize = true;
            this.lblCoordinateY.Location = new System.Drawing.Point(15, 43);
            this.lblCoordinateY.Name = "lblCoordinateY";
            this.lblCoordinateY.Size = new System.Drawing.Size(47, 12);
            this.lblCoordinateY.TabIndex = 3;
            this.lblCoordinateY.Text = "坐标Y：";
            // 
            // lblCoordinateX
            // 
            this.lblCoordinateX.AutoSize = true;
            this.lblCoordinateX.Location = new System.Drawing.Point(15, 16);
            this.lblCoordinateX.Name = "lblCoordinateX";
            this.lblCoordinateX.Size = new System.Drawing.Size(47, 12);
            this.lblCoordinateX.TabIndex = 0;
            this.lblCoordinateX.Text = "坐标X：";
            // 
            // gbCoordinate
            // 
            this.gbCoordinate.Controls.Add(this.button1);
            this.gbCoordinate.Controls.Add(this.label9);
            this.gbCoordinate.Controls.Add(this.label8);
            this.gbCoordinate.Controls.Add(this.label7);
            this.gbCoordinate.Controls.Add(this.txtCoordinateX);
            this.gbCoordinate.Controls.Add(this.txtCoordinateZ);
            this.gbCoordinate.Controls.Add(this.lblCoordinateX);
            this.gbCoordinate.Controls.Add(this.txtCoordinateY);
            this.gbCoordinate.Controls.Add(this.lblCoordinateY);
            this.gbCoordinate.Controls.Add(this.lblCoordinateZ);
            this.gbCoordinate.Location = new System.Drawing.Point(226, 3);
            this.gbCoordinate.Name = "gbCoordinate";
            this.gbCoordinate.Size = new System.Drawing.Size(189, 120);
            this.gbCoordinate.TabIndex = 14;
            this.gbCoordinate.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(58, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 23);
            this.button1.TabIndex = 76;
            this.button1.Text = "拾取坐标";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(164, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(164, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(164, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "*";
            // 
            // lblTrend
            // 
            this.lblTrend.AutoSize = true;
            this.lblTrend.Location = new System.Drawing.Point(12, 67);
            this.lblTrend.Name = "lblTrend";
            this.lblTrend.Size = new System.Drawing.Size(65, 12);
            this.lblTrend.TabIndex = 8;
            this.lblTrend.Text = "倾    角：";
            // 
            // txtTrend
            // 
            this.txtTrend.Location = new System.Drawing.Point(72, 129);
            this.txtTrend.MaxLength = 15;
            this.txtTrend.Name = "txtTrend";
            this.txtTrend.Size = new System.Drawing.Size(134, 21);
            this.txtTrend.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(209, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(209, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(209, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(210, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(210, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "*";
            // 
            // pnlType
            // 
            this.pnlType.Controls.Add(this.rbtnOppositeFaultage);
            this.pnlType.Controls.Add(this.rbtnFrontFaultage);
            this.pnlType.Location = new System.Drawing.Point(71, 93);
            this.pnlType.Name = "pnlType";
            this.pnlType.Size = new System.Drawing.Size(134, 28);
            this.pnlType.TabIndex = 7;
            // 
            // gbImage
            // 
            this.gbImage.Controls.Add(this.btnDetails);
            this.gbImage.Controls.Add(this.pbCount);
            this.gbImage.Controls.Add(this.lblSuccessed);
            this.gbImage.Controls.Add(this.btnMultTxt);
            this.gbImage.Controls.Add(this.lblError);
            this.gbImage.Controls.Add(this.btnReadTxt);
            this.gbImage.Controls.Add(this.lbl2);
            this.gbImage.Controls.Add(this.lblTotal);
            this.gbImage.Controls.Add(this.label12);
            this.gbImage.Controls.Add(this.label11);
            this.gbImage.Location = new System.Drawing.Point(228, 129);
            this.gbImage.Name = "gbImage";
            this.gbImage.Size = new System.Drawing.Size(187, 118);
            this.gbImage.TabIndex = 15;
            this.gbImage.TabStop = false;
            // 
            // btnDetails
            // 
            this.btnDetails.Enabled = false;
            this.btnDetails.Location = new System.Drawing.Point(14, 89);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(94, 23);
            this.btnDetails.TabIndex = 48;
            this.btnDetails.Text = "错误详细信息";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // pbCount
            // 
            this.pbCount.Location = new System.Drawing.Point(14, 41);
            this.pbCount.Name = "pbCount";
            this.pbCount.Size = new System.Drawing.Size(158, 23);
            this.pbCount.TabIndex = 22;
            // 
            // lblSuccessed
            // 
            this.lblSuccessed.AutoSize = true;
            this.lblSuccessed.Location = new System.Drawing.Point(105, 70);
            this.lblSuccessed.Name = "lblSuccessed";
            this.lblSuccessed.Size = new System.Drawing.Size(11, 12);
            this.lblSuccessed.TabIndex = 45;
            this.lblSuccessed.Text = "0";
            // 
            // btnMultTxt
            // 
            this.btnMultTxt.Location = new System.Drawing.Point(93, 15);
            this.btnMultTxt.Name = "btnMultTxt";
            this.btnMultTxt.Size = new System.Drawing.Size(80, 20);
            this.btnMultTxt.TabIndex = 21;
            this.btnMultTxt.Text = "多文件导入";
            this.btnMultTxt.UseVisualStyleBackColor = true;
            this.btnMultTxt.Click += new System.EventHandler(this.btnMultTxt_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(157, 70);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(11, 12);
            this.lblError.TabIndex = 46;
            this.lblError.Text = "0";
            // 
            // btnReadTxt
            // 
            this.btnReadTxt.Location = new System.Drawing.Point(15, 15);
            this.btnReadTxt.Name = "btnReadTxt";
            this.btnReadTxt.Size = new System.Drawing.Size(81, 20);
            this.btnReadTxt.TabIndex = 21;
            this.btnReadTxt.Text = "单文件导入";
            this.btnReadTxt.UseVisualStyleBackColor = true;
            this.btnReadTxt.Click += new System.EventHandler(this.btnReadTxt_Click);
            // 
            // lbl2
            // 
            this.lbl2.Location = new System.Drawing.Point(57, 70);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(63, 16);
            this.lbl2.TabIndex = 44;
            this.lbl2.Text = "已导入:";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(37, 70);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(11, 12);
            this.lblTotal.TabIndex = 47;
            this.lblTotal.Text = "0";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(122, 70);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 16);
            this.label12.TabIndex = 43;
            this.label12.Text = "错误:";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(13, 70);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 16);
            this.label11.TabIndex = 42;
            this.label11.Text = "共:";
            // 
            // txtLength
            // 
            this.txtLength.BackColor = System.Drawing.SystemColors.Window;
            this.txtLength.Location = new System.Drawing.Point(73, 185);
            this.txtLength.MaxLength = 15;
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(134, 21);
            this.txtLength.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "长    度：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(210, 188);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 20;
            this.label10.Text = "*";
            // 
            // FaultageInfoEntering
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(427, 259);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtLength);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gbImage);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbCoordinate);
            this.Controls.Add(this.pnlType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtSeparation);
            this.Controls.Add(this.txtTrend);
            this.Controls.Add(this.txtAngle);
            this.Controls.Add(this.txtGap);
            this.Controls.Add(this.lblSeparation);
            this.Controls.Add(this.lblTrend);
            this.Controls.Add(this.txtFaultageName);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblAngle);
            this.Controls.Add(this.lblGap);
            this.Controls.Add(this.lblFaultageName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FaultageInfoEntering";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "揭露断层数据录入";
            this.gbCoordinate.ResumeLayout(false);
            this.gbCoordinate.PerformLayout();
            this.pnlType.ResumeLayout(false);
            this.pnlType.PerformLayout();
            this.gbImage.ResumeLayout(false);
            this.gbImage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFaultageName;
        private System.Windows.Forms.TextBox txtFaultageName;
        private System.Windows.Forms.Label lblGap;
        private System.Windows.Forms.TextBox txtGap;
        private System.Windows.Forms.Label lblAngle;
        private System.Windows.Forms.TextBox txtAngle;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.RadioButton rbtnFrontFaultage;
        private System.Windows.Forms.RadioButton rbtnOppositeFaultage;
        private System.Windows.Forms.Label lblSeparation;
        private System.Windows.Forms.TextBox txtSeparation;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox txtCoordinateZ;
        private System.Windows.Forms.TextBox txtCoordinateY;
        private System.Windows.Forms.TextBox txtCoordinateX;
        private System.Windows.Forms.Label lblCoordinateZ;
        private System.Windows.Forms.Label lblCoordinateY;
        private System.Windows.Forms.Label lblCoordinateX;
        private System.Windows.Forms.GroupBox gbCoordinate;
        private System.Windows.Forms.Label lblTrend;
        private System.Windows.Forms.TextBox txtTrend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnlType;
        private System.Windows.Forms.GroupBox gbImage;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnReadTxt;
        private System.Windows.Forms.ProgressBar pbCount;
        private System.Windows.Forms.Button btnMultTxt;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.Label lblSuccessed;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
    }
}