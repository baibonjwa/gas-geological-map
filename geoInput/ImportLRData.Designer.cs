namespace _3.GeologyMeasure
{
    partial class ImportLRData
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CancleConnect = new System.Windows.Forms.Button();
            this.BTConnect = new System.Windows.Forms.Button();
            this.TBDataTableName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TBPassWord = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TBUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.字段 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.值 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CancleConnect);
            this.groupBox1.Controls.Add(this.BTConnect);
            this.groupBox1.Controls.Add(this.TBDataTableName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.TBPassWord);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TBUserName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 190);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "龙软数据库";
            // 
            // CancleConnect
            // 
            this.CancleConnect.Location = new System.Drawing.Point(129, 147);
            this.CancleConnect.Name = "CancleConnect";
            this.CancleConnect.Size = new System.Drawing.Size(75, 28);
            this.CancleConnect.TabIndex = 7;
            this.CancleConnect.Text = "取消连接";
            this.CancleConnect.UseVisualStyleBackColor = true;
            this.CancleConnect.Click += new System.EventHandler(this.CancleConnect_Click);
            // 
            // BTConnect
            // 
            this.BTConnect.Location = new System.Drawing.Point(18, 147);
            this.BTConnect.Name = "BTConnect";
            this.BTConnect.Size = new System.Drawing.Size(75, 28);
            this.BTConnect.TabIndex = 6;
            this.BTConnect.Text = "连 接";
            this.BTConnect.UseVisualStyleBackColor = true;
            this.BTConnect.Click += new System.EventHandler(this.BTConnect_Click);
            // 
            // TBDataTableName
            // 
            this.TBDataTableName.Location = new System.Drawing.Point(99, 101);
            this.TBDataTableName.Name = "TBDataTableName";
            this.TBDataTableName.Size = new System.Drawing.Size(105, 21);
            this.TBDataTableName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "数据表格名称：";
            // 
            // TBPassWord
            // 
            this.TBPassWord.Location = new System.Drawing.Point(83, 61);
            this.TBPassWord.Name = "TBPassWord";
            this.TBPassWord.Size = new System.Drawing.Size(121, 21);
            this.TBPassWord.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "密  码：";
            // 
            // TBUserName
            // 
            this.TBUserName.Location = new System.Drawing.Point(83, 21);
            this.TBUserName.Name = "TBUserName";
            this.TBUserName.Size = new System.Drawing.Size(121, 21);
            this.TBUserName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名：";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.字段,
            this.值});
            this.dataGridView1.Location = new System.Drawing.Point(6, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(315, 164);
            this.dataGridView1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(247, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(327, 190);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据预览";
            // 
            // 字段
            // 
            this.字段.HeaderText = "字段";
            this.字段.Name = "字段";
            // 
            // 值
            // 
            this.值.HeaderText = "值";
            this.值.Name = "值";
            // 
            // ImportLRData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 200);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ImportLRData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "从龙软数据库导入导线数据";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TBDataTableName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TBPassWord;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TBUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CancleConnect;
        private System.Windows.Forms.Button BTConnect;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 字段;
        private System.Windows.Forms.DataGridViewTextBoxColumn 值;
    }
}