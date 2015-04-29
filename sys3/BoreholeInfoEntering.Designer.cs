namespace sys3
{
    partial class BoreholeInfoEntering
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblBoreholeNumber = new System.Windows.Forms.Label();
            this.txtBoreholeNumber = new System.Windows.Forms.TextBox();
            this.lblGroundElevation = new System.Windows.Forms.Label();
            this.txtGroundElevation = new System.Windows.Forms.TextBox();
            this.lblCoalSeamsTexture = new System.Windows.Forms.Label();
            this.gvCoalSeamsTexture = new System.Windows.Forms.DataGridView();
            this.LITHOLOGY = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.FLOOR_ELEVATION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.THICKNESS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COAL_SEAMS_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COORDINATE_X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COORDINATE_Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COORDINATE_Z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.插入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.下移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblCoordinate = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnQD = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCoordinateX = new System.Windows.Forms.TextBox();
            this.txtCoordinateZ = new System.Windows.Forms.TextBox();
            this.lblCoordinateX = new System.Windows.Forms.Label();
            this.txtCoordinateY = new System.Windows.Forms.TextBox();
            this.lblCoordinateY = new System.Windows.Forms.Label();
            this.lblCoordinateZ = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnReadMultTxt = new System.Windows.Forms.Button();
            this.btnReadTxt = new System.Windows.Forms.Button();
            this.上移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblSuccessed = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.pbCount = new System.Windows.Forms.ProgressBar();
            this.btnDetails = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvCoalSeamsTexture)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblBoreholeNumber
            // 
            this.lblBoreholeNumber.AutoSize = true;
            this.lblBoreholeNumber.Location = new System.Drawing.Point(12, 12);
            this.lblBoreholeNumber.Name = "lblBoreholeNumber";
            this.lblBoreholeNumber.Size = new System.Drawing.Size(65, 12);
            this.lblBoreholeNumber.TabIndex = 0;
            this.lblBoreholeNumber.Text = "孔    号：";
            // 
            // txtBoreholeNumber
            // 
            this.txtBoreholeNumber.BackColor = System.Drawing.SystemColors.Window;
            this.txtBoreholeNumber.Location = new System.Drawing.Point(72, 9);
            this.txtBoreholeNumber.MaxLength = 15;
            this.txtBoreholeNumber.Name = "txtBoreholeNumber";
            this.txtBoreholeNumber.Size = new System.Drawing.Size(100, 21);
            this.txtBoreholeNumber.TabIndex = 1;
            // 
            // lblGroundElevation
            // 
            this.lblGroundElevation.AutoSize = true;
            this.lblGroundElevation.Location = new System.Drawing.Point(12, 40);
            this.lblGroundElevation.Name = "lblGroundElevation";
            this.lblGroundElevation.Size = new System.Drawing.Size(65, 12);
            this.lblGroundElevation.TabIndex = 3;
            this.lblGroundElevation.Text = "地面标高：";
            // 
            // txtGroundElevation
            // 
            this.txtGroundElevation.Location = new System.Drawing.Point(72, 37);
            this.txtGroundElevation.MaxLength = 15;
            this.txtGroundElevation.Name = "txtGroundElevation";
            this.txtGroundElevation.Size = new System.Drawing.Size(100, 21);
            this.txtGroundElevation.TabIndex = 4;
            // 
            // lblCoalSeamsTexture
            // 
            this.lblCoalSeamsTexture.AutoSize = true;
            this.lblCoalSeamsTexture.Location = new System.Drawing.Point(12, 108);
            this.lblCoalSeamsTexture.Name = "lblCoalSeamsTexture";
            this.lblCoalSeamsTexture.Size = new System.Drawing.Size(65, 12);
            this.lblCoalSeamsTexture.TabIndex = 8;
            this.lblCoalSeamsTexture.Text = "煤层结构：";
            // 
            // gvCoalSeamsTexture
            // 
            this.gvCoalSeamsTexture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvCoalSeamsTexture.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvCoalSeamsTexture.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvCoalSeamsTexture.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LITHOLOGY,
            this.FLOOR_ELEVATION,
            this.THICKNESS,
            this.COAL_SEAMS_NAME,
            this.COORDINATE_X,
            this.COORDINATE_Y,
            this.COORDINATE_Z,
            this.delete});
            this.gvCoalSeamsTexture.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvCoalSeamsTexture.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvCoalSeamsTexture.Location = new System.Drawing.Point(72, 108);
            this.gvCoalSeamsTexture.Name = "gvCoalSeamsTexture";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvCoalSeamsTexture.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvCoalSeamsTexture.RowTemplate.Height = 23;
            this.gvCoalSeamsTexture.Size = new System.Drawing.Size(791, 284);
            this.gvCoalSeamsTexture.TabIndex = 9;
            this.gvCoalSeamsTexture.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvCoalSeamsTexture_CellContentClick);
            this.gvCoalSeamsTexture.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvCoalSeamsTexture_CellEndEdit);
            this.gvCoalSeamsTexture.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvCoalSeamsTexture_CellMouseDown);
            this.gvCoalSeamsTexture.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.gvCoalSeamsTexture_RowPostPaint);
            this.gvCoalSeamsTexture.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvCoalSeamsTexture_RowValidated);
            // 
            // LITHOLOGY
            // 
            this.LITHOLOGY.HeaderText = "岩性";
            this.LITHOLOGY.Name = "LITHOLOGY";
            this.LITHOLOGY.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LITHOLOGY.ToolTipText = "岩性";
            // 
            // FLOOR_ELEVATION
            // 
            this.FLOOR_ELEVATION.HeaderText = "底板标高";
            this.FLOOR_ELEVATION.MaxInputLength = 15;
            this.FLOOR_ELEVATION.Name = "FLOOR_ELEVATION";
            this.FLOOR_ELEVATION.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FLOOR_ELEVATION.ToolTipText = "底板标高";
            // 
            // THICKNESS
            // 
            this.THICKNESS.HeaderText = "厚度";
            this.THICKNESS.MaxInputLength = 15;
            this.THICKNESS.Name = "THICKNESS";
            this.THICKNESS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.THICKNESS.ToolTipText = "厚度";
            // 
            // COAL_SEAMS_NAME
            // 
            this.COAL_SEAMS_NAME.HeaderText = "煤层名称";
            this.COAL_SEAMS_NAME.MaxInputLength = 15;
            this.COAL_SEAMS_NAME.Name = "COAL_SEAMS_NAME";
            this.COAL_SEAMS_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // COORDINATE_X
            // 
            this.COORDINATE_X.HeaderText = "坐标X";
            this.COORDINATE_X.Name = "COORDINATE_X";
            this.COORDINATE_X.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // COORDINATE_Y
            // 
            this.COORDINATE_Y.HeaderText = "坐标Y";
            this.COORDINATE_Y.Name = "COORDINATE_Y";
            this.COORDINATE_Y.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // COORDINATE_Z
            // 
            this.COORDINATE_Z.HeaderText = "坐标Z";
            this.COORDINATE_Z.Name = "COORDINATE_Z";
            this.COORDINATE_Z.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // delete
            // 
            this.delete.HeaderText = "";
            this.delete.Name = "delete";
            this.delete.Text = "删除";
            this.delete.UseColumnTextForButtonValue = true;
            this.delete.Width = 50;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.插入ToolStripMenuItem,
            this.toolStripSeparator2,
            this.上移ToolStripMenuItem,
            this.下移ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 76);
            // 
            // 插入ToolStripMenuItem
            // 
            this.插入ToolStripMenuItem.Name = "插入ToolStripMenuItem";
            this.插入ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.插入ToolStripMenuItem.Text = "插入";
            this.插入ToolStripMenuItem.Click += new System.EventHandler(this.插入ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // 下移ToolStripMenuItem
            // 
            this.下移ToolStripMenuItem.Name = "下移ToolStripMenuItem";
            this.下移ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.下移ToolStripMenuItem.Text = "下移";
            this.下移ToolStripMenuItem.Click += new System.EventHandler(this.下移ToolStripMenuItem_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSubmit.Location = new System.Drawing.Point(707, 416);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 11;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(788, 416);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblCoordinate
            // 
            this.lblCoordinate.AutoSize = true;
            this.lblCoordinate.Location = new System.Drawing.Point(12, 74);
            this.lblCoordinate.Name = "lblCoordinate";
            this.lblCoordinate.Size = new System.Drawing.Size(65, 12);
            this.lblCoordinate.TabIndex = 6;
            this.lblCoordinate.Text = "孔口坐标：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnQD);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCoordinateX);
            this.groupBox1.Controls.Add(this.txtCoordinateZ);
            this.groupBox1.Controls.Add(this.lblCoordinateX);
            this.groupBox1.Controls.Add(this.txtCoordinateY);
            this.groupBox1.Controls.Add(this.lblCoordinateY);
            this.groupBox1.Controls.Add(this.lblCoordinateZ);
            this.groupBox1.Location = new System.Drawing.Point(72, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(753, 43);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // btnQD
            // 
            this.btnQD.Location = new System.Drawing.Point(378, 14);
            this.btnQD.Name = "btnQD";
            this.btnQD.Size = new System.Drawing.Size(98, 23);
            this.btnQD.TabIndex = 75;
            this.btnQD.Text = "拾取孔口坐标";
            this.btnQD.UseVisualStyleBackColor = true;
            this.btnQD.Click += new System.EventHandler(this.btnQD_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(347, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 26;
            this.label5.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(229, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 25;
            this.label4.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(110, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 24;
            this.label3.Text = "*";
            // 
            // txtCoordinateX
            // 
            this.txtCoordinateX.Location = new System.Drawing.Point(49, 14);
            this.txtCoordinateX.MaxLength = 15;
            this.txtCoordinateX.Name = "txtCoordinateX";
            this.txtCoordinateX.Size = new System.Drawing.Size(59, 21);
            this.txtCoordinateX.TabIndex = 1;
            // 
            // txtCoordinateZ
            // 
            this.txtCoordinateZ.Location = new System.Drawing.Point(286, 14);
            this.txtCoordinateZ.MaxLength = 15;
            this.txtCoordinateZ.Name = "txtCoordinateZ";
            this.txtCoordinateZ.Size = new System.Drawing.Size(59, 21);
            this.txtCoordinateZ.TabIndex = 5;
            this.txtCoordinateZ.Text = "0";
            // 
            // lblCoordinateX
            // 
            this.lblCoordinateX.AutoSize = true;
            this.lblCoordinateX.Location = new System.Drawing.Point(6, 17);
            this.lblCoordinateX.Name = "lblCoordinateX";
            this.lblCoordinateX.Size = new System.Drawing.Size(47, 12);
            this.lblCoordinateX.TabIndex = 0;
            this.lblCoordinateX.Text = "坐标X：";
            // 
            // txtCoordinateY
            // 
            this.txtCoordinateY.Location = new System.Drawing.Point(168, 14);
            this.txtCoordinateY.MaxLength = 15;
            this.txtCoordinateY.Name = "txtCoordinateY";
            this.txtCoordinateY.Size = new System.Drawing.Size(59, 21);
            this.txtCoordinateY.TabIndex = 3;
            // 
            // lblCoordinateY
            // 
            this.lblCoordinateY.AutoSize = true;
            this.lblCoordinateY.Location = new System.Drawing.Point(125, 17);
            this.lblCoordinateY.Name = "lblCoordinateY";
            this.lblCoordinateY.Size = new System.Drawing.Size(47, 12);
            this.lblCoordinateY.TabIndex = 2;
            this.lblCoordinateY.Text = "坐标Y：";
            // 
            // lblCoordinateZ
            // 
            this.lblCoordinateZ.AutoSize = true;
            this.lblCoordinateZ.Location = new System.Drawing.Point(248, 17);
            this.lblCoordinateZ.Name = "lblCoordinateZ";
            this.lblCoordinateZ.Size = new System.Drawing.Size(47, 12);
            this.lblCoordinateZ.TabIndex = 4;
            this.lblCoordinateZ.Text = "坐标Z：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(174, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(174, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(869, 225);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(273, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 25;
            this.label7.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(148, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 26;
            this.label8.Text = "*";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(349, 113);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 27;
            this.label9.Text = "*";
            // 
            // btnReadMultTxt
            // 
            this.btnReadMultTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadMultTxt.Location = new System.Drawing.Point(595, 416);
            this.btnReadMultTxt.Name = "btnReadMultTxt";
            this.btnReadMultTxt.Size = new System.Drawing.Size(106, 23);
            this.btnReadMultTxt.TabIndex = 28;
            this.btnReadMultTxt.Text = "批量读取txt文件";
            this.btnReadMultTxt.UseVisualStyleBackColor = true;
            this.btnReadMultTxt.Click += new System.EventHandler(this.btnReadMultTxt_Click);
            // 
            // btnReadTxt
            // 
            this.btnReadTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadTxt.Location = new System.Drawing.Point(504, 416);
            this.btnReadTxt.Name = "btnReadTxt";
            this.btnReadTxt.Size = new System.Drawing.Size(85, 23);
            this.btnReadTxt.TabIndex = 29;
            this.btnReadTxt.Text = "读取txt文件";
            this.btnReadTxt.UseVisualStyleBackColor = true;
            this.btnReadTxt.Click += new System.EventHandler(this.btnReadTxt_Click);
            // 
            // 上移ToolStripMenuItem
            // 
            this.上移ToolStripMenuItem.Name = "上移ToolStripMenuItem";
            this.上移ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.上移ToolStripMenuItem.Text = "上移";
            this.上移ToolStripMenuItem.Click += new System.EventHandler(this.上移ToolStripMenuItem_Click);
            // 
            // lblSuccessed
            // 
            this.lblSuccessed.AutoSize = true;
            this.lblSuccessed.Location = new System.Drawing.Point(354, 421);
            this.lblSuccessed.Name = "lblSuccessed";
            this.lblSuccessed.Size = new System.Drawing.Size(11, 12);
            this.lblSuccessed.TabIndex = 45;
            this.lblSuccessed.Text = "0";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(410, 421);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(11, 12);
            this.lblError.TabIndex = 46;
            this.lblError.Text = "0";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(287, 421);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(11, 12);
            this.lblTotal.TabIndex = 47;
            this.lblTotal.Text = "0";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(264, 421);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 16);
            this.label10.TabIndex = 42;
            this.label10.Text = "共:";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(376, 421);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 16);
            this.label11.TabIndex = 43;
            this.label11.Text = "错误:";
            // 
            // lbl2
            // 
            this.lbl2.Location = new System.Drawing.Point(309, 421);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(56, 16);
            this.lbl2.TabIndex = 44;
            this.lbl2.Text = "已导入:";
            // 
            // pbCount
            // 
            this.pbCount.Location = new System.Drawing.Point(72, 416);
            this.pbCount.Name = "pbCount";
            this.pbCount.Size = new System.Drawing.Size(186, 23);
            this.pbCount.TabIndex = 41;
            // 
            // btnDetails
            // 
            this.btnDetails.Enabled = false;
            this.btnDetails.Location = new System.Drawing.Point(427, 416);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(56, 23);
            this.btnDetails.TabIndex = 48;
            this.btnDetails.Text = "详细";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // BoreholeInfoEntering
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(880, 451);
            this.Controls.Add(this.btnDetails);
            this.Controls.Add(this.lblSuccessed);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.pbCount);
            this.Controls.Add(this.btnReadTxt);
            this.Controls.Add(this.btnReadMultTxt);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.gvCoalSeamsTexture);
            this.Controls.Add(this.lblCoalSeamsTexture);
            this.Controls.Add(this.txtGroundElevation);
            this.Controls.Add(this.txtBoreholeNumber);
            this.Controls.Add(this.lblGroundElevation);
            this.Controls.Add(this.lblBoreholeNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BoreholeInfoEntering";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "勘探钻孔数据录入";
            ((System.ComponentModel.ISupportInitialize)(this.gvCoalSeamsTexture)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBoreholeNumber;
        private System.Windows.Forms.TextBox txtBoreholeNumber;
        private System.Windows.Forms.Label lblGroundElevation;
        private System.Windows.Forms.TextBox txtGroundElevation;
        private System.Windows.Forms.Label lblCoalSeamsTexture;
        private System.Windows.Forms.DataGridView gvCoalSeamsTexture;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblCoordinate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCoordinateX;
        private System.Windows.Forms.TextBox txtCoordinateZ;
        private System.Windows.Forms.Label lblCoordinateX;
        private System.Windows.Forms.TextBox txtCoordinateY;
        private System.Windows.Forms.Label lblCoordinateY;
        private System.Windows.Forms.Label lblCoordinateZ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 插入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.DataGridViewComboBoxColumn LITHOLOGY;
        private System.Windows.Forms.DataGridViewTextBoxColumn FLOOR_ELEVATION;
        private System.Windows.Forms.DataGridViewTextBoxColumn THICKNESS;
        private System.Windows.Forms.DataGridViewTextBoxColumn COAL_SEAMS_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn COORDINATE_X;
        private System.Windows.Forms.DataGridViewTextBoxColumn COORDINATE_Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn COORDINATE_Z;
        private System.Windows.Forms.DataGridViewButtonColumn delete;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnQD;
        private System.Windows.Forms.Button btnReadMultTxt;
        private System.Windows.Forms.Button btnReadTxt;
        private System.Windows.Forms.ToolStripMenuItem 上移ToolStripMenuItem;
        private System.Windows.Forms.Label lblSuccessed;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.ProgressBar pbCount;
        private System.Windows.Forms.Button btnDetails;
    }
}