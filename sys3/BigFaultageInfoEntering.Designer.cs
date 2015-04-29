namespace sys3
{
    partial class BigFaultageInfoEntering
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
            this.tbFaultageName = new System.Windows.Forms.TextBox();
            this.lblFaultageName = new System.Windows.Forms.Label();
            this.pnlType = new System.Windows.Forms.Panel();
            this.rbtnOppositeFaultage = new System.Windows.Forms.RadioButton();
            this.rbtnFrontFaultage = new System.Windows.Forms.RadioButton();
            this.lblType = new System.Windows.Forms.Label();
            this.lblExposePoints = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbGap = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbAngle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgrdvUp = new System.Windows.Forms.DataGridView();
            this.coordinateX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coordinateY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coordinateZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dgrdvDown = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.tbTrend = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnReadTxt = new System.Windows.Forms.Button();
            this.btnReadMultTxt = new System.Windows.Forms.Button();
            this.btnDetails = new System.Windows.Forms.Button();
            this.lblSuccessed = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.pbCount = new System.Windows.Forms.ProgressBar();
            this.pnlType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvDown)).BeginInit();
            this.SuspendLayout();
            // 
            // tbFaultageName
            // 
            this.tbFaultageName.Location = new System.Drawing.Point(83, 11);
            this.tbFaultageName.MaxLength = 15;
            this.tbFaultageName.Name = "tbFaultageName";
            this.tbFaultageName.Size = new System.Drawing.Size(134, 21);
            this.tbFaultageName.TabIndex = 1;
            // 
            // lblFaultageName
            // 
            this.lblFaultageName.AutoSize = true;
            this.lblFaultageName.Location = new System.Drawing.Point(12, 14);
            this.lblFaultageName.Name = "lblFaultageName";
            this.lblFaultageName.Size = new System.Drawing.Size(65, 12);
            this.lblFaultageName.TabIndex = 0;
            this.lblFaultageName.Text = "断层名称：";
            // 
            // pnlType
            // 
            this.pnlType.Controls.Add(this.rbtnOppositeFaultage);
            this.pnlType.Controls.Add(this.rbtnFrontFaultage);
            this.pnlType.Location = new System.Drawing.Point(83, 38);
            this.pnlType.Name = "pnlType";
            this.pnlType.Size = new System.Drawing.Size(134, 28);
            this.pnlType.TabIndex = 4;
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
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(12, 48);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(65, 12);
            this.lblType.TabIndex = 3;
            this.lblType.Text = "类    型：";
            // 
            // lblExposePoints
            // 
            this.lblExposePoints.AutoSize = true;
            this.lblExposePoints.Location = new System.Drawing.Point(245, 48);
            this.lblExposePoints.Name = "lblExposePoints";
            this.lblExposePoints.Size = new System.Drawing.Size(65, 12);
            this.lblExposePoints.TabIndex = 5;
            this.lblExposePoints.Text = "倾    角：";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(819, 382);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSubmit.Location = new System.Drawing.Point(738, 382);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 11;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(223, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "*";
            // 
            // tbGap
            // 
            this.tbGap.Location = new System.Drawing.Point(311, 11);
            this.tbGap.MaxLength = 15;
            this.tbGap.Name = "tbGap";
            this.tbGap.Size = new System.Drawing.Size(134, 21);
            this.tbGap.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(245, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "落    差：";
            // 
            // tbAngle
            // 
            this.tbAngle.Location = new System.Drawing.Point(311, 45);
            this.tbAngle.MaxLength = 15;
            this.tbAngle.Name = "tbAngle";
            this.tbAngle.Size = new System.Drawing.Size(134, 21);
            this.tbAngle.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "上    盘：";
            // 
            // dgrdvUp
            // 
            this.dgrdvUp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdvUp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.coordinateX,
            this.coordinateY,
            this.coordinateZ,
            this.btnDelete});
            this.dgrdvUp.Location = new System.Drawing.Point(83, 91);
            this.dgrdvUp.Name = "dgrdvUp";
            this.dgrdvUp.RowTemplate.Height = 23;
            this.dgrdvUp.Size = new System.Drawing.Size(362, 266);
            this.dgrdvUp.TabIndex = 17;
            // 
            // coordinateX
            // 
            this.coordinateX.HeaderText = "坐标X";
            this.coordinateX.MaxInputLength = 18;
            this.coordinateX.Name = "coordinateX";
            this.coordinateX.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.coordinateX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.coordinateX.Width = 85;
            // 
            // coordinateY
            // 
            this.coordinateY.HeaderText = "坐标Y";
            this.coordinateY.MaxInputLength = 18;
            this.coordinateY.Name = "coordinateY";
            this.coordinateY.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.coordinateY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.coordinateY.Width = 85;
            // 
            // coordinateZ
            // 
            this.coordinateZ.FillWeight = 162.963F;
            this.coordinateZ.HeaderText = "坐标Z";
            this.coordinateZ.MaxInputLength = 18;
            this.coordinateZ.Name = "coordinateZ";
            this.coordinateZ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.coordinateZ.Width = 85;
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.btnDelete.FillWeight = 37.03703F;
            this.btnDelete.HeaderText = "";
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Text = "删除";
            this.btnDelete.UseColumnTextForButtonValue = true;
            // 
            // dgrdvDown
            // 
            this.dgrdvDown.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdvDown.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewButtonColumn1});
            this.dgrdvDown.Location = new System.Drawing.Point(533, 91);
            this.dgrdvDown.Name = "dgrdvDown";
            this.dgrdvDown.RowTemplate.Height = 23;
            this.dgrdvDown.Size = new System.Drawing.Size(362, 266);
            this.dgrdvDown.TabIndex = 19;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "坐标X";
            this.dataGridViewTextBoxColumn1.MaxInputLength = 18;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 85;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "坐标Y";
            this.dataGridViewTextBoxColumn2.MaxInputLength = 18;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 85;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 162.963F;
            this.dataGridViewTextBoxColumn3.HeaderText = "坐标Z";
            this.dataGridViewTextBoxColumn3.MaxInputLength = 18;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 85;
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewButtonColumn1.FillWeight = 37.03703F;
            this.dataGridViewButtonColumn1.HeaderText = "";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.Text = "删除";
            this.dataGridViewButtonColumn1.UseColumnTextForButtonValue = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(462, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "下    盘：";
            // 
            // tbTrend
            // 
            this.tbTrend.Location = new System.Drawing.Point(533, 11);
            this.tbTrend.MaxLength = 15;
            this.tbTrend.Name = "tbTrend";
            this.tbTrend.Size = new System.Drawing.Size(134, 21);
            this.tbTrend.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(462, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "走    向：";
            // 
            // btnReadTxt
            // 
            this.btnReadTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadTxt.Location = new System.Drawing.Point(657, 382);
            this.btnReadTxt.Name = "btnReadTxt";
            this.btnReadTxt.Size = new System.Drawing.Size(75, 23);
            this.btnReadTxt.TabIndex = 22;
            this.btnReadTxt.Text = "读取txt";
            this.btnReadTxt.UseVisualStyleBackColor = true;
            this.btnReadTxt.Click += new System.EventHandler(this.btnReadTxt_Click);
            // 
            // btnReadMultTxt
            // 
            this.btnReadMultTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadMultTxt.Location = new System.Drawing.Point(563, 382);
            this.btnReadMultTxt.Name = "btnReadMultTxt";
            this.btnReadMultTxt.Size = new System.Drawing.Size(88, 23);
            this.btnReadMultTxt.TabIndex = 23;
            this.btnReadMultTxt.Text = "批量读取txt";
            this.btnReadMultTxt.UseVisualStyleBackColor = true;
            this.btnReadMultTxt.Click += new System.EventHandler(this.btnReadMultTxt_Click);
            // 
            // btnDetails
            // 
            this.btnDetails.Enabled = false;
            this.btnDetails.Location = new System.Drawing.Point(438, 382);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(56, 23);
            this.btnDetails.TabIndex = 56;
            this.btnDetails.Text = "详细";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // lblSuccessed
            // 
            this.lblSuccessed.AutoSize = true;
            this.lblSuccessed.Location = new System.Drawing.Point(365, 387);
            this.lblSuccessed.Name = "lblSuccessed";
            this.lblSuccessed.Size = new System.Drawing.Size(11, 12);
            this.lblSuccessed.TabIndex = 53;
            this.lblSuccessed.Text = "0";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(421, 387);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(11, 12);
            this.lblError.TabIndex = 54;
            this.lblError.Text = "0";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(298, 387);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(11, 12);
            this.lblTotal.TabIndex = 55;
            this.lblTotal.Text = "0";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(275, 387);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 16);
            this.label10.TabIndex = 50;
            this.label10.Text = "共:";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(387, 387);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 16);
            this.label11.TabIndex = 51;
            this.label11.Text = "错误:";
            // 
            // lbl2
            // 
            this.lbl2.Location = new System.Drawing.Point(320, 387);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(56, 16);
            this.lbl2.TabIndex = 52;
            this.lbl2.Text = "已导入:";
            // 
            // pbCount
            // 
            this.pbCount.Location = new System.Drawing.Point(83, 382);
            this.pbCount.Name = "pbCount";
            this.pbCount.Size = new System.Drawing.Size(186, 23);
            this.pbCount.TabIndex = 49;
            // 
            // BigFaultageInfoEntering
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(927, 417);
            this.Controls.Add(this.btnDetails);
            this.Controls.Add(this.lblSuccessed);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.pbCount);
            this.Controls.Add(this.btnReadMultTxt);
            this.Controls.Add(this.btnReadTxt);
            this.Controls.Add(this.tbTrend);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgrdvDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgrdvUp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbAngle);
            this.Controls.Add(this.tbGap);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.lblExposePoints);
            this.Controls.Add(this.pnlType);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.tbFaultageName);
            this.Controls.Add(this.lblFaultageName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BigFaultageInfoEntering";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "推断断层数据录入";
            this.pnlType.ResumeLayout(false);
            this.pnlType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFaultageName;
        private System.Windows.Forms.Label lblFaultageName;
        private System.Windows.Forms.Panel pnlType;
        private System.Windows.Forms.RadioButton rbtnOppositeFaultage;
        private System.Windows.Forms.RadioButton rbtnFrontFaultage;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblExposePoints;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbGap;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbAngle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgrdvUp;
        private System.Windows.Forms.DataGridViewTextBoxColumn coordinateX;
        private System.Windows.Forms.DataGridViewTextBoxColumn coordinateY;
        private System.Windows.Forms.DataGridViewTextBoxColumn coordinateZ;
        private System.Windows.Forms.DataGridViewButtonColumn btnDelete;
        private System.Windows.Forms.DataGridView dgrdvDown;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbTrend;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnReadTxt;
        private System.Windows.Forms.Button btnReadMultTxt;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.Label lblSuccessed;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.ProgressBar pbCount;
    }
}