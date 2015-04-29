namespace GIS.SpecialGraphic
{
    partial class FrmNewXZZ
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgrdvZhzzt = new System.Windows.Forms.DataGridView();
            this.houdu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DropDownZZ = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btnDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.插入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.剪切ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.上移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtX = new System.Windows.Forms.TextBox();
            this.lblCollapsePillarsName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBlc = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnQD = new System.Windows.Forms.Button();
            this.txtDBBG = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtAngle = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btndel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvZhzzt)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(87, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 34;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(240, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 35;
            this.label2.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(190, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 33;
            this.label6.Text = "*";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(294, 372);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgrdvZhzzt
            // 
            this.dgrdvZhzzt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrdvZhzzt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdvZhzzt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.houdu,
            this.DropDownZZ,
            this.btnDelete});
            this.dgrdvZhzzt.ContextMenuStrip = this.contextMenuStrip1;
            this.dgrdvZhzzt.Location = new System.Drawing.Point(6, 106);
            this.dgrdvZhzzt.MultiSelect = false;
            this.dgrdvZhzzt.Name = "dgrdvZhzzt";
            this.dgrdvZhzzt.RowTemplate.Height = 23;
            this.dgrdvZhzzt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrdvZhzzt.Size = new System.Drawing.Size(368, 260);
            this.dgrdvZhzzt.TabIndex = 28;
            this.dgrdvZhzzt.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvZhzzt_CellContentClick);
            this.dgrdvZhzzt.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgrdvZhzzt_CellMouseDown);
            this.dgrdvZhzzt.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgrdvZhzzt_RowPostPaint);
            // 
            // houdu
            // 
            this.houdu.HeaderText = "厚度";
            this.houdu.MaxInputLength = 18;
            this.houdu.Name = "houdu";
            this.houdu.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.houdu.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.houdu.Width = 150;
            // 
            // DropDownZZ
            // 
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.DropDownZZ.DefaultCellStyle = dataGridViewCellStyle3;
            this.DropDownZZ.FillWeight = 162.963F;
            this.DropDownZZ.HeaderText = "柱状";
            this.DropDownZZ.Items.AddRange(new object[] {
            "煤层",
            "岩层"});
            this.DropDownZZ.Name = "DropDownZZ";
            // 
            // btnDelete
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnDelete.DefaultCellStyle = dataGridViewCellStyle4;
            this.btnDelete.HeaderText = "";
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ReadOnly = true;
            this.btnDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseColumnTextForButtonValue = true;
            this.btnDelete.Width = 75;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.插入ToolStripMenuItem,
            this.toolStripSeparator1,
            this.复制ToolStripMenuItem,
            this.剪切ToolStripMenuItem,
            this.粘贴ToolStripMenuItem,
            this.toolStripSeparator2,
            this.上移ToolStripMenuItem,
            this.下移ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 148);
            // 
            // 插入ToolStripMenuItem
            // 
            this.插入ToolStripMenuItem.Name = "插入ToolStripMenuItem";
            this.插入ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.插入ToolStripMenuItem.Text = "插入";
            this.插入ToolStripMenuItem.Click += new System.EventHandler(this.插入ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(97, 6);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.复制ToolStripMenuItem.Text = "复制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // 剪切ToolStripMenuItem
            // 
            this.剪切ToolStripMenuItem.Name = "剪切ToolStripMenuItem";
            this.剪切ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.剪切ToolStripMenuItem.Text = "剪切";
            this.剪切ToolStripMenuItem.Click += new System.EventHandler(this.剪切ToolStripMenuItem_Click);
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.粘贴ToolStripMenuItem.Text = "粘贴";
            this.粘贴ToolStripMenuItem.Visible = false;
            this.粘贴ToolStripMenuItem.Click += new System.EventHandler(this.粘贴ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(97, 6);
            // 
            // 上移ToolStripMenuItem
            // 
            this.上移ToolStripMenuItem.Name = "上移ToolStripMenuItem";
            this.上移ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.上移ToolStripMenuItem.Text = "上移";
            this.上移ToolStripMenuItem.Click += new System.EventHandler(this.上移ToolStripMenuItem_Click);
            // 
            // 下移ToolStripMenuItem
            // 
            this.下移ToolStripMenuItem.Name = "下移ToolStripMenuItem";
            this.下移ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.下移ToolStripMenuItem.Text = "下移";
            this.下移ToolStripMenuItem.Click += new System.EventHandler(this.下移ToolStripMenuItem_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(200, 372);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 31;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(88, 8);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(97, 21);
            this.txtX.TabIndex = 26;
            // 
            // lblCollapsePillarsName
            // 
            this.lblCollapsePillarsName.AutoSize = true;
            this.lblCollapsePillarsName.Location = new System.Drawing.Point(10, 11);
            this.lblCollapsePillarsName.Name = "lblCollapsePillarsName";
            this.lblCollapsePillarsName.Size = new System.Drawing.Size(77, 12);
            this.lblCollapsePillarsName.TabIndex = 25;
            this.lblCollapsePillarsName.Text = "图例位置 X：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 37;
            this.label4.Text = "比例尺：";
            // 
            // txtBlc
            // 
            this.txtBlc.Location = new System.Drawing.Point(59, 40);
            this.txtBlc.Name = "txtBlc";
            this.txtBlc.Size = new System.Drawing.Size(39, 21);
            this.txtBlc.TabIndex = 38;
            this.txtBlc.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(237, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 39;
            this.label5.Text = "米";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(101, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 40;
            this.label7.Text = "*";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(242, 8);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(105, 21);
            this.txtY.TabIndex = 44;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(220, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 43;
            this.label3.Text = "Y：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(353, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 45;
            this.label8.Text = "*";
            // 
            // btnQD
            // 
            this.btnQD.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQD.Location = new System.Drawing.Point(239, 77);
            this.btnQD.Name = "btnQD";
            this.btnQD.Size = new System.Drawing.Size(135, 23);
            this.btnQD.TabIndex = 75;
            this.btnQD.Text = "点击地图拾取图例坐标";
            this.btnQD.UseVisualStyleBackColor = true;
            this.btnQD.Click += new System.EventHandler(this.btnQD_Click);
            // 
            // txtDBBG
            // 
            this.txtDBBG.Location = new System.Drawing.Point(171, 41);
            this.txtDBBG.Name = "txtDBBG";
            this.txtDBBG.Size = new System.Drawing.Size(63, 21);
            this.txtDBBG.TabIndex = 77;
            this.txtDBBG.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(112, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 76;
            this.label10.Text = "底板标高：";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(7, 77);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(145, 23);
            this.btnUpdate.TabIndex = 79;
            this.btnUpdate.Text = "双击地图修改现有小柱状";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtAngle
            // 
            this.txtAngle.Location = new System.Drawing.Point(308, 42);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new System.Drawing.Size(39, 21);
            this.txtAngle.TabIndex = 81;
            this.txtAngle.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(265, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 80;
            this.label9.Text = "角度：";
            // 
            // btndel
            // 
            this.btndel.Location = new System.Drawing.Point(166, 77);
            this.btndel.Name = "btndel";
            this.btndel.Size = new System.Drawing.Size(60, 23);
            this.btndel.TabIndex = 82;
            this.btndel.Text = "删除";
            this.btndel.UseVisualStyleBackColor = true;
            this.btndel.Click += new System.EventHandler(this.btndel_Click);
            // 
            // FrmNewXZZ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 397);
            this.Controls.Add(this.btndel);
            this.Controls.Add(this.txtAngle);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtDBBG);
            this.Controls.Add(this.txtBlc);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnQD);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dgrdvZhzzt);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.lblCollapsePillarsName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmNewXZZ";
            this.Text = "新建小柱状";
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvZhzzt)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lblCollapsePillarsName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 插入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 剪切ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 上移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下移ToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnQD;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridViewTextBoxColumn houdu;
        private System.Windows.Forms.DataGridViewComboBoxColumn DropDownZZ;
        private System.Windows.Forms.DataGridViewButtonColumn btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        public System.Windows.Forms.DataGridView dgrdvZhzzt;
        public System.Windows.Forms.TextBox txtX;
        public System.Windows.Forms.TextBox txtBlc;
        public System.Windows.Forms.TextBox txtY;
        public System.Windows.Forms.TextBox txtDBBG;
        public System.Windows.Forms.TextBox txtAngle;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btndel;
    }
}