namespace sys3
{
    partial class WireInfoEntering
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
            this.lblWireName = new System.Windows.Forms.Label();
            this.txtWireName = new System.Windows.Forms.TextBox();
            this.dgrdvWire = new System.Windows.Forms.DataGridView();
            this.txtWirePointID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCoordinateX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCoordinateY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCoordinateZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDistanceFromTheLeft = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDistanceFromTheRight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDistanceFromTop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDistanceFromBottom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblWireLevel = new System.Windows.Forms.Label();
            this.lblMeasureDate = new System.Windows.Forms.Label();
            this.lblVobserver = new System.Windows.Forms.Label();
            this.txtWireLevel = new System.Windows.Forms.TextBox();
            this.dtpMeasureDate = new System.Windows.Forms.DateTimePicker();
            this.dtpCountDate = new System.Windows.Forms.DateTimePicker();
            this.lblCounter = new System.Windows.Forms.Label();
            this.lblCountDate = new System.Windows.Forms.Label();
            this.dtpCheckDate = new System.Windows.Forms.DateTimePicker();
            this.lblChecker = new System.Windows.Forms.Label();
            this.lblCheckDate = new System.Windows.Forms.Label();
            this.cboVobserver = new System.Windows.Forms.ComboBox();
            this.cboCounter = new System.Windows.Forms.ComboBox();
            this.cboChecker = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnTxt = new System.Windows.Forms.Button();
            this.btnMultTxt = new System.Windows.Forms.Button();
            this.pbCount = new System.Windows.Forms.ProgressBar();
            this.lbl2 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.lblSuccessed = new System.Windows.Forms.Label();
            this.btnDetails = new System.Windows.Forms.Button();
            this.selectTunnelUserControl1 = new LibCommonForm.SelectTunnelUserControl();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvWire)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWireName
            // 
            this.lblWireName.AutoSize = true;
            this.lblWireName.Location = new System.Drawing.Point(19, 203);
            this.lblWireName.Name = "lblWireName";
            this.lblWireName.Size = new System.Drawing.Size(65, 12);
            this.lblWireName.TabIndex = 1;
            this.lblWireName.Text = "导线名称：";
            // 
            // txtWireName
            // 
            this.txtWireName.Location = new System.Drawing.Point(90, 199);
            this.txtWireName.Name = "txtWireName";
            this.txtWireName.Size = new System.Drawing.Size(119, 21);
            this.txtWireName.TabIndex = 2;
            // 
            // dgrdvWire
            // 
            this.dgrdvWire.AllowDrop = true;
            this.dgrdvWire.AllowUserToOrderColumns = true;
            this.dgrdvWire.AllowUserToResizeRows = false;
            this.dgrdvWire.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrdvWire.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdvWire.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.txtWirePointID,
            this.txtCoordinateX,
            this.txtCoordinateY,
            this.txtCoordinateZ,
            this.txtDistanceFromTheLeft,
            this.txtDistanceFromTheRight,
            this.txtDistanceFromTop,
            this.txtDistanceFromBottom});
            this.dgrdvWire.Location = new System.Drawing.Point(11, 264);
            this.dgrdvWire.MultiSelect = false;
            this.dgrdvWire.Name = "dgrdvWire";
            this.dgrdvWire.RowTemplate.Height = 23;
            this.dgrdvWire.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrdvWire.Size = new System.Drawing.Size(765, 219);
            this.dgrdvWire.TabIndex = 17;
            this.dgrdvWire.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvWire_RowEnter);
            this.dgrdvWire.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgrdvWire_RowPostPaint);
            // 
            // txtWirePointID
            // 
            this.txtWirePointID.HeaderText = "导线点编号";
            this.txtWirePointID.MaxInputLength = 15;
            this.txtWirePointID.Name = "txtWirePointID";
            this.txtWirePointID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.txtWirePointID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.txtWirePointID.Width = 90;
            // 
            // txtCoordinateX
            // 
            this.txtCoordinateX.HeaderText = "坐标X";
            this.txtCoordinateX.Name = "txtCoordinateX";
            this.txtCoordinateX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.txtCoordinateX.Width = 90;
            // 
            // txtCoordinateY
            // 
            this.txtCoordinateY.HeaderText = "坐标Y";
            this.txtCoordinateY.Name = "txtCoordinateY";
            this.txtCoordinateY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.txtCoordinateY.Width = 90;
            // 
            // txtCoordinateZ
            // 
            this.txtCoordinateZ.HeaderText = "坐标Z";
            this.txtCoordinateZ.Name = "txtCoordinateZ";
            this.txtCoordinateZ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.txtCoordinateZ.Width = 90;
            // 
            // txtDistanceFromTheLeft
            // 
            this.txtDistanceFromTheLeft.HeaderText = "距左帮距离";
            this.txtDistanceFromTheLeft.Name = "txtDistanceFromTheLeft";
            this.txtDistanceFromTheLeft.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.txtDistanceFromTheLeft.Width = 90;
            // 
            // txtDistanceFromTheRight
            // 
            this.txtDistanceFromTheRight.HeaderText = "距右帮距离";
            this.txtDistanceFromTheRight.Name = "txtDistanceFromTheRight";
            this.txtDistanceFromTheRight.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.txtDistanceFromTheRight.Width = 90;
            // 
            // txtDistanceFromTop
            // 
            this.txtDistanceFromTop.HeaderText = "距顶板距离";
            this.txtDistanceFromTop.Name = "txtDistanceFromTop";
            this.txtDistanceFromTop.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.txtDistanceFromTop.Width = 90;
            // 
            // txtDistanceFromBottom
            // 
            this.txtDistanceFromBottom.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.txtDistanceFromBottom.HeaderText = "距底板距离";
            this.txtDistanceFromBottom.Name = "txtDistanceFromBottom";
            this.txtDistanceFromBottom.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(620, 489);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 24;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(701, 489);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblWireLevel
            // 
            this.lblWireLevel.AutoSize = true;
            this.lblWireLevel.Location = new System.Drawing.Point(19, 233);
            this.lblWireLevel.Name = "lblWireLevel";
            this.lblWireLevel.Size = new System.Drawing.Size(65, 12);
            this.lblWireLevel.TabIndex = 3;
            this.lblWireLevel.Text = "导线级别：";
            // 
            // lblMeasureDate
            // 
            this.lblMeasureDate.AutoSize = true;
            this.lblMeasureDate.Location = new System.Drawing.Point(240, 234);
            this.lblMeasureDate.Name = "lblMeasureDate";
            this.lblMeasureDate.Size = new System.Drawing.Size(65, 12);
            this.lblMeasureDate.TabIndex = 7;
            this.lblMeasureDate.Text = "测量日期：";
            // 
            // lblVobserver
            // 
            this.lblVobserver.AutoSize = true;
            this.lblVobserver.Location = new System.Drawing.Point(240, 205);
            this.lblVobserver.Name = "lblVobserver";
            this.lblVobserver.Size = new System.Drawing.Size(65, 12);
            this.lblVobserver.TabIndex = 5;
            this.lblVobserver.Text = "观 测 者：";
            // 
            // txtWireLevel
            // 
            this.txtWireLevel.Location = new System.Drawing.Point(90, 230);
            this.txtWireLevel.Name = "txtWireLevel";
            this.txtWireLevel.Size = new System.Drawing.Size(119, 21);
            this.txtWireLevel.TabIndex = 4;
            // 
            // dtpMeasureDate
            // 
            this.dtpMeasureDate.CustomFormat = "yyyy/MM/dd";
            this.dtpMeasureDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMeasureDate.Location = new System.Drawing.Point(311, 230);
            this.dtpMeasureDate.Name = "dtpMeasureDate";
            this.dtpMeasureDate.Size = new System.Drawing.Size(118, 21);
            this.dtpMeasureDate.TabIndex = 8;
            this.dtpMeasureDate.Value = new System.DateTime(2013, 12, 2, 9, 30, 14, 0);
            // 
            // dtpCountDate
            // 
            this.dtpCountDate.CustomFormat = "yyyy/MM/dd";
            this.dtpCountDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCountDate.Location = new System.Drawing.Point(526, 230);
            this.dtpCountDate.Name = "dtpCountDate";
            this.dtpCountDate.Size = new System.Drawing.Size(118, 21);
            this.dtpCountDate.TabIndex = 12;
            this.dtpCountDate.Value = new System.DateTime(2013, 12, 2, 9, 30, 14, 0);
            // 
            // lblCounter
            // 
            this.lblCounter.AutoSize = true;
            this.lblCounter.Location = new System.Drawing.Point(455, 205);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(65, 12);
            this.lblCounter.TabIndex = 9;
            this.lblCounter.Text = "计 算 者：";
            // 
            // lblCountDate
            // 
            this.lblCountDate.AutoSize = true;
            this.lblCountDate.Location = new System.Drawing.Point(455, 234);
            this.lblCountDate.Name = "lblCountDate";
            this.lblCountDate.Size = new System.Drawing.Size(65, 12);
            this.lblCountDate.TabIndex = 11;
            this.lblCountDate.Text = "计算日期：";
            // 
            // dtpCheckDate
            // 
            this.dtpCheckDate.CustomFormat = "yyyy/MM/dd";
            this.dtpCheckDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckDate.Location = new System.Drawing.Point(748, 228);
            this.dtpCheckDate.Name = "dtpCheckDate";
            this.dtpCheckDate.Size = new System.Drawing.Size(118, 21);
            this.dtpCheckDate.TabIndex = 16;
            this.dtpCheckDate.Value = new System.DateTime(2013, 12, 2, 9, 30, 14, 0);
            // 
            // lblChecker
            // 
            this.lblChecker.AutoSize = true;
            this.lblChecker.Location = new System.Drawing.Point(674, 204);
            this.lblChecker.Name = "lblChecker";
            this.lblChecker.Size = new System.Drawing.Size(65, 12);
            this.lblChecker.TabIndex = 13;
            this.lblChecker.Text = "校 核 者：";
            // 
            // lblCheckDate
            // 
            this.lblCheckDate.AutoSize = true;
            this.lblCheckDate.Location = new System.Drawing.Point(674, 232);
            this.lblCheckDate.Name = "lblCheckDate";
            this.lblCheckDate.Size = new System.Drawing.Size(65, 12);
            this.lblCheckDate.TabIndex = 15;
            this.lblCheckDate.Text = "校核日期：";
            // 
            // cboVobserver
            // 
            this.cboVobserver.FormattingEnabled = true;
            this.cboVobserver.Location = new System.Drawing.Point(311, 201);
            this.cboVobserver.Name = "cboVobserver";
            this.cboVobserver.Size = new System.Drawing.Size(120, 20);
            this.cboVobserver.TabIndex = 6;
            // 
            // cboCounter
            // 
            this.cboCounter.FormattingEnabled = true;
            this.cboCounter.Location = new System.Drawing.Point(526, 201);
            this.cboCounter.Name = "cboCounter";
            this.cboCounter.Size = new System.Drawing.Size(118, 20);
            this.cboCounter.TabIndex = 10;
            // 
            // cboChecker
            // 
            this.cboChecker.FormattingEnabled = true;
            this.cboChecker.Location = new System.Drawing.Point(748, 200);
            this.cboChecker.Name = "cboChecker";
            this.cboChecker.Size = new System.Drawing.Size(118, 20);
            this.cboChecker.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(611, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 33;
            this.label5.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(215, 205);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 32;
            this.label1.Text = "*";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(799, 265);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 18;
            this.btnAdd.Text = "插入";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveUp.Enabled = false;
            this.btnMoveUp.Location = new System.Drawing.Point(799, 342);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(75, 23);
            this.btnMoveUp.TabIndex = 22;
            this.btnMoveUp.Text = "上移";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDel.Enabled = false;
            this.btnDel.Location = new System.Drawing.Point(799, 303);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 21;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveDown.Enabled = false;
            this.btnMoveDown.Location = new System.Drawing.Point(799, 381);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(75, 23);
            this.btnMoveDown.TabIndex = 23;
            this.btnMoveDown.Text = "下移";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnTxt
            // 
            this.btnTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTxt.Location = new System.Drawing.Point(526, 489);
            this.btnTxt.Name = "btnTxt";
            this.btnTxt.Size = new System.Drawing.Size(88, 23);
            this.btnTxt.TabIndex = 34;
            this.btnTxt.Text = "单文件导入";
            this.btnTxt.UseVisualStyleBackColor = true;
            this.btnTxt.Click += new System.EventHandler(this.btnTXT_Click);
            // 
            // btnMultTxt
            // 
            this.btnMultTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMultTxt.Location = new System.Drawing.Point(426, 489);
            this.btnMultTxt.Name = "btnMultTxt";
            this.btnMultTxt.Size = new System.Drawing.Size(94, 23);
            this.btnMultTxt.TabIndex = 37;
            this.btnMultTxt.Text = "多文件导入";
            this.btnMultTxt.UseVisualStyleBackColor = true;
            this.btnMultTxt.Click += new System.EventHandler(this.btnMultTxt_Click);
            // 
            // pbCount
            // 
            this.pbCount.Location = new System.Drawing.Point(11, 489);
            this.pbCount.Name = "pbCount";
            this.pbCount.Size = new System.Drawing.Size(186, 23);
            this.pbCount.TabIndex = 38;
            // 
            // lbl2
            // 
            this.lbl2.Location = new System.Drawing.Point(248, 494);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(56, 16);
            this.lbl2.TabIndex = 39;
            this.lbl2.Text = "已导入:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(203, 494);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 39;
            this.label2.Text = "共:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(315, 494);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 39;
            this.label3.Text = "错误:";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(226, 494);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(11, 12);
            this.lblTotal.TabIndex = 40;
            this.lblTotal.Text = "0";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(349, 494);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(11, 12);
            this.lblError.TabIndex = 40;
            this.lblError.Text = "0";
            // 
            // lblSuccessed
            // 
            this.lblSuccessed.AutoSize = true;
            this.lblSuccessed.Location = new System.Drawing.Point(293, 494);
            this.lblSuccessed.Name = "lblSuccessed";
            this.lblSuccessed.Size = new System.Drawing.Size(11, 12);
            this.lblSuccessed.TabIndex = 40;
            this.lblSuccessed.Text = "0";
            // 
            // btnDetails
            // 
            this.btnDetails.Enabled = false;
            this.btnDetails.Location = new System.Drawing.Point(372, 489);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(48, 23);
            this.btnDetails.TabIndex = 41;
            this.btnDetails.Text = "详细";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // selectTunnelUserControl1
            // 
            this.selectTunnelUserControl1.Location = new System.Drawing.Point(12, 10);
            this.selectTunnelUserControl1.Name = "selectTunnelUserControl1";
            this.selectTunnelUserControl1.SelectedTunnel = null;
            this.selectTunnelUserControl1.Size = new System.Drawing.Size(583, 187);
            this.selectTunnelUserControl1.TabIndex = 36;
            // 
            // WireInfoEntering
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(886, 524);
            this.Controls.Add(this.btnDetails);
            this.Controls.Add(this.lblSuccessed);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.pbCount);
            this.Controls.Add(this.btnMultTxt);
            this.Controls.Add(this.btnTxt);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cboCounter);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblChecker);
            this.Controls.Add(this.cboVobserver);
            this.Controls.Add(this.lblCounter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblVobserver);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cboChecker);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dtpCheckDate);
            this.Controls.Add(this.dgrdvWire);
            this.Controls.Add(this.lblCheckDate);
            this.Controls.Add(this.txtWireLevel);
            this.Controls.Add(this.lblCountDate);
            this.Controls.Add(this.dtpCountDate);
            this.Controls.Add(this.lblWireLevel);
            this.Controls.Add(this.dtpMeasureDate);
            this.Controls.Add(this.lblWireName);
            this.Controls.Add(this.lblMeasureDate);
            this.Controls.Add(this.txtWireName);
            this.Controls.Add(this.selectTunnelUserControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WireInfoEntering";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "导线信息";
            this.Load += new System.EventHandler(this.WireInfoEntering_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvWire)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWireName;
        private System.Windows.Forms.TextBox txtWireName;
        private System.Windows.Forms.DataGridView dgrdvWire;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblWireLevel;
        private System.Windows.Forms.Label lblMeasureDate;
        private System.Windows.Forms.Label lblVobserver;
        private System.Windows.Forms.TextBox txtWireLevel;
        private System.Windows.Forms.DateTimePicker dtpMeasureDate;
        private System.Windows.Forms.DateTimePicker dtpCountDate;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.Label lblCountDate;
        private System.Windows.Forms.DateTimePicker dtpCheckDate;
        private System.Windows.Forms.Label lblChecker;
        private System.Windows.Forms.Label lblCheckDate;
        private LibCommonForm.SelectTunnelUserControl selectTunnelUserControl1;
        private System.Windows.Forms.ComboBox cboVobserver;
        private System.Windows.Forms.ComboBox cboCounter;
        private System.Windows.Forms.ComboBox cboChecker;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnTxt;
        private System.Windows.Forms.Button btnMultTxt;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtWirePointID;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtCoordinateX;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtCoordinateY;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtCoordinateZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtDistanceFromTheLeft;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtDistanceFromTheRight;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtDistanceFromTop;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtDistanceFromBottom;
        private System.Windows.Forms.ProgressBar pbCount;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblSuccessed;
        private System.Windows.Forms.Button btnDetails;
    }
}