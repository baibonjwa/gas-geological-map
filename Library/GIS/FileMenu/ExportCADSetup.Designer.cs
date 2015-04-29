namespace GIS.FileMenu
{
    partial class ExportCADSetup
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
            this.cklstCurLayers = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnSelectedPath = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbOutputType = new System.Windows.Forms.ComboBox();
            this.tbOutputFile = new System.Windows.Forms.TextBox();
            this.cbAppendToExisting = new System.Windows.Forms.CheckBox();
            this.cbIgnoreFileNames = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cklstCurLayers
            // 
            this.cklstCurLayers.CheckOnClick = true;
            this.cklstCurLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cklstCurLayers.FormattingEnabled = true;
            this.cklstCurLayers.Location = new System.Drawing.Point(3, 17);
            this.cklstCurLayers.Name = "cklstCurLayers";
            this.cklstCurLayers.Size = new System.Drawing.Size(384, 385);
            this.cklstCurLayers.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.cklstCurLayers);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 405);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "当前图层";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(67, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(90, 16);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "全选/全不选";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 551);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(396, 41);
            this.panel1.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.Location = new System.Drawing.Point(235, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取  消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.Location = new System.Drawing.Point(95, 9);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确  定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnSelectedPath
            // 
            this.btnSelectedPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelectedPath.Location = new System.Drawing.Point(351, 498);
            this.btnSelectedPath.Name = "btnSelectedPath";
            this.btnSelectedPath.Size = new System.Drawing.Size(42, 22);
            this.btnSelectedPath.TabIndex = 9;
            this.btnSelectedPath.Text = "浏览";
            this.btnSelectedPath.UseVisualStyleBackColor = true;
            this.btnSelectedPath.Click += new System.EventHandler(this.btnSelectedPath_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 467);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(342, 28);
            this.label2.TabIndex = 8;
            this.label2.Text = "输出文件";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbOutputType
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.cbOutputType, 2);
            this.cbOutputType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbOutputType.FormattingEnabled = true;
            this.cbOutputType.Location = new System.Drawing.Point(3, 442);
            this.cbOutputType.Name = "cbOutputType";
            this.cbOutputType.Size = new System.Drawing.Size(390, 20);
            this.cbOutputType.TabIndex = 7;
            // 
            // tbOutputFile
            // 
            this.tbOutputFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbOutputFile.Location = new System.Drawing.Point(3, 498);
            this.tbOutputFile.Name = "tbOutputFile";
            this.tbOutputFile.Size = new System.Drawing.Size(342, 21);
            this.tbOutputFile.TabIndex = 6;
            // 
            // cbAppendToExisting
            // 
            this.cbAppendToExisting.AutoSize = true;
            this.cbAppendToExisting.Checked = true;
            this.cbAppendToExisting.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAppendToExisting.Location = new System.Drawing.Point(3, 526);
            this.cbAppendToExisting.Name = "cbAppendToExisting";
            this.cbAppendToExisting.Size = new System.Drawing.Size(144, 1);
            this.cbAppendToExisting.TabIndex = 5;
            this.cbAppendToExisting.Text = "追加到现有文件(可选)";
            this.cbAppendToExisting.UseVisualStyleBackColor = true;
            // 
            // cbIgnoreFileNames
            // 
            this.cbIgnoreFileNames.AutoSize = true;
            this.cbIgnoreFileNames.Checked = true;
            this.cbIgnoreFileNames.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIgnoreFileNames.Location = new System.Drawing.Point(3, 526);
            this.cbIgnoreFileNames.Name = "cbIgnoreFileNames";
            this.cbIgnoreFileNames.Size = new System.Drawing.Size(144, 1);
            this.cbIgnoreFileNames.TabIndex = 4;
            this.cbIgnoreFileNames.Text = "忽略表中的路径(可选)";
            this.cbIgnoreFileNames.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 411);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(342, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "输出类型";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.Controls.Add(this.cbOutputType, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.progressBar1, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbIgnoreFileNames, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbAppendToExisting, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.tbOutputFile, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnSelectedPath, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(396, 551);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // progressBar1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.progressBar1, 2);
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(3, 526);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(390, 22);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 10;
            // 
            // ExportCADSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 592);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Name = "ExportCADSetup";
            this.Text = "导出为CAD";
            this.Load += new System.EventHandler(this.ExportCADSetup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox cklstCurLayers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnSelectedPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbOutputType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbIgnoreFileNames;
        private System.Windows.Forms.CheckBox cbAppendToExisting;
        private System.Windows.Forms.TextBox tbOutputFile;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}