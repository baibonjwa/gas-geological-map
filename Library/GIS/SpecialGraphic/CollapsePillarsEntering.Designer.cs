namespace GIS.SpecialGraphic
{
    partial class CollapsePillarsEntering
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
            this.txtDescribe = new System.Windows.Forms.TextBox();
            this.lblDiscribe = new System.Windows.Forms.Label();
            this.dgrdvCoordinate = new System.Windows.Forms.DataGridView();
            this.coordinateX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coordinateY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coordinateZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblCoordinate = new System.Windows.Forms.Label();
            this.txtCollapsePillarsName = new System.Windows.Forms.TextBox();
            this.lblCollapsePillarsName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.radioBtnX = new System.Windows.Forms.RadioButton();
            this.radioBtnS = new System.Windows.Forms.RadioButton();
            this.btnMultImport = new System.Windows.Forms.Button();
            this.btnDetails = new System.Windows.Forms.Button();
            this.lblSuccessed = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pbCount = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvCoordinate)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(370, 386);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(289, 386);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtDescribe
            // 
            this.txtDescribe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescribe.Location = new System.Drawing.Point(84, 299);
            this.txtDescribe.Multiline = true;
            this.txtDescribe.Name = "txtDescribe";
            this.txtDescribe.Size = new System.Drawing.Size(362, 52);
            this.txtDescribe.TabIndex = 5;
            // 
            // lblDiscribe
            // 
            this.lblDiscribe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDiscribe.AutoSize = true;
            this.lblDiscribe.Location = new System.Drawing.Point(12, 302);
            this.lblDiscribe.Name = "lblDiscribe";
            this.lblDiscribe.Size = new System.Drawing.Size(77, 12);
            this.lblDiscribe.TabIndex = 4;
            this.lblDiscribe.Text = "描      述：";
            // 
            // dgrdvCoordinate
            // 
            this.dgrdvCoordinate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrdvCoordinate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdvCoordinate.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.coordinateX,
            this.coordinateY,
            this.coordinateZ,
            this.btnDelete});
            this.dgrdvCoordinate.Location = new System.Drawing.Point(83, 33);
            this.dgrdvCoordinate.Name = "dgrdvCoordinate";
            this.dgrdvCoordinate.RowTemplate.Height = 23;
            this.dgrdvCoordinate.Size = new System.Drawing.Size(362, 260);
            this.dgrdvCoordinate.TabIndex = 3;
            this.dgrdvCoordinate.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvCoordinate_CellContentClick);
            this.dgrdvCoordinate.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgrdvCoordinate_RowPostPaint);
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
            // lblCoordinate
            // 
            this.lblCoordinate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCoordinate.AutoSize = true;
            this.lblCoordinate.Location = new System.Drawing.Point(12, 58);
            this.lblCoordinate.Name = "lblCoordinate";
            this.lblCoordinate.Size = new System.Drawing.Size(77, 12);
            this.lblCoordinate.TabIndex = 2;
            this.lblCoordinate.Text = "关键点坐标：";
            // 
            // txtCollapsePillarsName
            // 
            this.txtCollapsePillarsName.Location = new System.Drawing.Point(83, 6);
            this.txtCollapsePillarsName.Name = "txtCollapsePillarsName";
            this.txtCollapsePillarsName.Size = new System.Drawing.Size(100, 21);
            this.txtCollapsePillarsName.TabIndex = 1;
            // 
            // lblCollapsePillarsName
            // 
            this.lblCollapsePillarsName.AutoSize = true;
            this.lblCollapsePillarsName.Location = new System.Drawing.Point(12, 9);
            this.lblCollapsePillarsName.Name = "lblCollapsePillarsName";
            this.lblCollapsePillarsName.Size = new System.Drawing.Size(77, 12);
            this.lblCollapsePillarsName.TabIndex = 0;
            this.lblCollapsePillarsName.Text = "陷落柱名称：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(189, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "*";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(168, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(255, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "*";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(338, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "*";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(371, 6);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 21;
            this.btnImport.Text = "导入关键点";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // radioBtnX
            // 
            this.radioBtnX.AutoSize = true;
            this.radioBtnX.Checked = true;
            this.radioBtnX.Location = new System.Drawing.Point(208, 9);
            this.radioBtnX.Name = "radioBtnX";
            this.radioBtnX.Size = new System.Drawing.Size(47, 16);
            this.radioBtnX.TabIndex = 23;
            this.radioBtnX.TabStop = true;
            this.radioBtnX.Tag = "0";
            this.radioBtnX.Text = "虚线";
            this.radioBtnX.UseVisualStyleBackColor = true;
            // 
            // radioBtnS
            // 
            this.radioBtnS.AutoSize = true;
            this.radioBtnS.Location = new System.Drawing.Point(272, 9);
            this.radioBtnS.Name = "radioBtnS";
            this.radioBtnS.Size = new System.Drawing.Size(47, 16);
            this.radioBtnS.TabIndex = 24;
            this.radioBtnS.Tag = "1";
            this.radioBtnS.Text = "实线";
            this.radioBtnS.UseVisualStyleBackColor = true;
            // 
            // btnMultImport
            // 
            this.btnMultImport.Location = new System.Drawing.Point(208, 386);
            this.btnMultImport.Name = "btnMultImport";
            this.btnMultImport.Size = new System.Drawing.Size(75, 23);
            this.btnMultImport.TabIndex = 31;
            this.btnMultImport.Text = "批量导入";
            this.btnMultImport.UseVisualStyleBackColor = true;
            this.btnMultImport.Click += new System.EventHandler(this.btnMultImport_Click);
            // 
            // btnDetails
            // 
            this.btnDetails.Enabled = false;
            this.btnDetails.Location = new System.Drawing.Point(408, 359);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(37, 23);
            this.btnDetails.TabIndex = 56;
            this.btnDetails.Text = "详细";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // lblSuccessed
            // 
            this.lblSuccessed.AutoSize = true;
            this.lblSuccessed.Location = new System.Drawing.Point(341, 364);
            this.lblSuccessed.Name = "lblSuccessed";
            this.lblSuccessed.Size = new System.Drawing.Size(11, 12);
            this.lblSuccessed.TabIndex = 53;
            this.lblSuccessed.Text = "0";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(393, 364);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(11, 12);
            this.lblError.TabIndex = 54;
            this.lblError.Text = "0";
            // 
            // lbl2
            // 
            this.lbl2.Location = new System.Drawing.Point(293, 364);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(63, 16);
            this.lbl2.TabIndex = 52;
            this.lbl2.Text = "已导入:";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(273, 364);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(11, 12);
            this.lblTotal.TabIndex = 55;
            this.lblTotal.Text = "0";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(358, 364);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 16);
            this.label12.TabIndex = 51;
            this.label12.Text = "错误:";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(249, 364);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 16);
            this.label11.TabIndex = 50;
            this.label11.Text = "共:";
            // 
            // pbCount
            // 
            this.pbCount.Location = new System.Drawing.Point(83, 359);
            this.pbCount.Name = "pbCount";
            this.pbCount.Size = new System.Drawing.Size(158, 23);
            this.pbCount.TabIndex = 49;
            // 
            // CollapsePillarsEntering
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(457, 415);
            this.Controls.Add(this.btnDetails);
            this.Controls.Add(this.pbCount);
            this.Controls.Add(this.lblSuccessed);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnMultImport);
            this.Controls.Add(this.radioBtnS);
            this.Controls.Add(this.radioBtnX);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgrdvCoordinate);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtDescribe);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.txtCollapsePillarsName);
            this.Controls.Add(this.lblCollapsePillarsName);
            this.Controls.Add(this.lblDiscribe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CollapsePillarsEntering";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "陷落柱";
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvCoordinate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox txtDescribe;
        private System.Windows.Forms.Label lblDiscribe;
        private System.Windows.Forms.Label lblCoordinate;
        private System.Windows.Forms.TextBox txtCollapsePillarsName;
        private System.Windows.Forms.Label lblCollapsePillarsName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn coordinateX;
        private System.Windows.Forms.DataGridViewTextBoxColumn coordinateY;
        private System.Windows.Forms.DataGridViewTextBoxColumn coordinateZ;
        private System.Windows.Forms.DataGridViewButtonColumn btnDelete;
        private System.Windows.Forms.DataGridView dgrdvCoordinate;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.RadioButton radioBtnX;
        private System.Windows.Forms.RadioButton radioBtnS;
        private System.Windows.Forms.Button btnMultImport;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.Label lblSuccessed;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ProgressBar pbCount;
    }
}