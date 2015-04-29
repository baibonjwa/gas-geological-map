namespace sys3
{
    partial class ProspectingLineInfoEntering
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
            this.txtProspectingLineName = new System.Windows.Forms.TextBox();
            this.lblProspectingLineName = new System.Windows.Forms.Label();
            this.lblExposePoints = new System.Windows.Forms.Label();
            this.lstProspectingBoreholeAll = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDeltete = new System.Windows.Forms.Button();
            this.lstProspectingBoreholeSelected = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.上移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtProspectingLineName
            // 
            this.txtProspectingLineName.Location = new System.Drawing.Point(95, 13);
            this.txtProspectingLineName.MaxLength = 15;
            this.txtProspectingLineName.Name = "txtProspectingLineName";
            this.txtProspectingLineName.Size = new System.Drawing.Size(134, 21);
            this.txtProspectingLineName.TabIndex = 1;
            // 
            // lblProspectingLineName
            // 
            this.lblProspectingLineName.AutoSize = true;
            this.lblProspectingLineName.Location = new System.Drawing.Point(23, 16);
            this.lblProspectingLineName.Name = "lblProspectingLineName";
            this.lblProspectingLineName.Size = new System.Drawing.Size(77, 12);
            this.lblProspectingLineName.TabIndex = 0;
            this.lblProspectingLineName.Text = "勘探线名称：";
            // 
            // lblExposePoints
            // 
            this.lblExposePoints.AutoSize = true;
            this.lblExposePoints.Location = new System.Drawing.Point(12, 50);
            this.lblExposePoints.Name = "lblExposePoints";
            this.lblExposePoints.Size = new System.Drawing.Size(89, 12);
            this.lblExposePoints.TabIndex = 3;
            this.lblExposePoints.Text = "勘探钻孔选择：";
            // 
            // lstProspectingBoreholeAll
            // 
            this.lstProspectingBoreholeAll.FormattingEnabled = true;
            this.lstProspectingBoreholeAll.ItemHeight = 12;
            this.lstProspectingBoreholeAll.Location = new System.Drawing.Point(95, 50);
            this.lstProspectingBoreholeAll.Name = "lstProspectingBoreholeAll";
            this.lstProspectingBoreholeAll.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstProspectingBoreholeAll.Size = new System.Drawing.Size(135, 244);
            this.lstProspectingBoreholeAll.TabIndex = 4;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(236, 103);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = ">>";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDeltete
            // 
            this.btnDeltete.Location = new System.Drawing.Point(236, 162);
            this.btnDeltete.Name = "btnDeltete";
            this.btnDeltete.Size = new System.Drawing.Size(75, 23);
            this.btnDeltete.TabIndex = 6;
            this.btnDeltete.Text = "<<";
            this.btnDeltete.UseVisualStyleBackColor = true;
            this.btnDeltete.Click += new System.EventHandler(this.btnDeltete_Click);
            // 
            // lstProspectingBoreholeSelected
            // 
            this.lstProspectingBoreholeSelected.ContextMenuStrip = this.contextMenuStrip1;
            this.lstProspectingBoreholeSelected.FormattingEnabled = true;
            this.lstProspectingBoreholeSelected.ItemHeight = 12;
            this.lstProspectingBoreholeSelected.Location = new System.Drawing.Point(317, 50);
            this.lstProspectingBoreholeSelected.Name = "lstProspectingBoreholeSelected";
            this.lstProspectingBoreholeSelected.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstProspectingBoreholeSelected.Size = new System.Drawing.Size(135, 244);
            this.lstProspectingBoreholeSelected.TabIndex = 7;
            this.lstProspectingBoreholeSelected.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstProspectingBoreholeSelected_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.上移ToolStripMenuItem,
            this.下移ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 48);
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
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(382, 320);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSubmit.Location = new System.Drawing.Point(301, 320);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 9;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(235, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(458, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "*";
            // 
            // ProspectingLineInfoEntering
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(478, 357);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnDeltete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstProspectingBoreholeSelected);
            this.Controls.Add(this.lstProspectingBoreholeAll);
            this.Controls.Add(this.lblExposePoints);
            this.Controls.Add(this.txtProspectingLineName);
            this.Controls.Add(this.lblProspectingLineName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProspectingLineInfoEntering";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "勘探线数据录入界面";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtProspectingLineName;
        private System.Windows.Forms.Label lblProspectingLineName;
        private System.Windows.Forms.Label lblExposePoints;
        private System.Windows.Forms.ListBox lstProspectingBoreholeAll;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDeltete;
        private System.Windows.Forms.ListBox lstProspectingBoreholeSelected;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 上移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下移ToolStripMenuItem;
    }
}