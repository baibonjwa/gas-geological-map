namespace LibCommonForm
{
    partial class TunnelInfoEntering
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
            this.cboLithology = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboSupportPattern = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtTunnelName = new System.Windows.Forms.TextBox();
            this.lblTunnelName = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.txtDesignLength = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.cboCoalOrStone = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtDesignArea = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboTunnelType = new System.Windows.Forms.ComboBox();
            this.selectWorkingFaceControl1 = new LibCommonForm.SelectWorkingFaceControl();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboLithology
            // 
            this.cboLithology.FormattingEnabled = true;
            this.cboLithology.Location = new System.Drawing.Point(72, 274);
            this.cboLithology.Name = "cboLithology";
            this.cboLithology.Size = new System.Drawing.Size(154, 20);
            this.cboLithology.TabIndex = 9;
            this.cboLithology.SelectedIndexChanged += new System.EventHandler(this.cboLithology_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 277);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "围岩类型";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 224);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "支护方式";
            // 
            // cboSupportPattern
            // 
            this.cboSupportPattern.FormattingEnabled = true;
            this.cboSupportPattern.Items.AddRange(new object[] {
            "简单支护",
            "U型钢支护",
            "背板支护",
            "工字钢支护",
            "锚杆支护",
            "锚喷支护",
            "锚网支护",
            "木材支护",
            "砌碹支护支架",
            "液压支架",
            "单体支架",
            "木垛支架",
            "无支护"});
            this.cboSupportPattern.Location = new System.Drawing.Point(72, 221);
            this.cboSupportPattern.Name = "cboSupportPattern";
            this.cboSupportPattern.Size = new System.Drawing.Size(154, 20);
            this.cboSupportPattern.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(376, 309);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(295, 308);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 12;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtTunnelName
            // 
            this.txtTunnelName.Location = new System.Drawing.Point(72, 194);
            this.txtTunnelName.Name = "txtTunnelName";
            this.txtTunnelName.Size = new System.Drawing.Size(153, 21);
            this.txtTunnelName.TabIndex = 1;
            // 
            // lblTunnelName
            // 
            this.lblTunnelName.AutoSize = true;
            this.lblTunnelName.Location = new System.Drawing.Point(13, 197);
            this.lblTunnelName.Name = "lblTunnelName";
            this.lblTunnelName.Size = new System.Drawing.Size(53, 12);
            this.lblTunnelName.TabIndex = 0;
            this.lblTunnelName.Text = "巷道名称";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(236, 197);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 12);
            this.label24.TabIndex = 2;
            this.label24.Text = "设计长度";
            // 
            // txtDesignLength
            // 
            this.txtDesignLength.Location = new System.Drawing.Point(295, 194);
            this.txtDesignLength.Name = "txtDesignLength";
            this.txtDesignLength.Size = new System.Drawing.Size(106, 21);
            this.txtDesignLength.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.selectWorkingFaceControl1);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(461, 188);
            this.panel2.TabIndex = 30;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(236, 251);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(53, 12);
            this.label25.TabIndex = 10;
            this.label25.Text = "煤巷岩巷";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(13, 251);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(53, 12);
            this.label29.TabIndex = 10;
            this.label29.Text = "巷道类型";
            // 
            // cboCoalOrStone
            // 
            this.cboCoalOrStone.FormattingEnabled = true;
            this.cboCoalOrStone.Items.AddRange(new object[] {
            "煤巷",
            "岩巷"});
            this.cboCoalOrStone.Location = new System.Drawing.Point(295, 248);
            this.cboCoalOrStone.Name = "cboCoalOrStone";
            this.cboCoalOrStone.Size = new System.Drawing.Size(154, 20);
            this.cboCoalOrStone.TabIndex = 11;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(236, 224);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(53, 12);
            this.label27.TabIndex = 39;
            this.label27.Text = "设计面积";
            // 
            // txtDesignArea
            // 
            this.txtDesignArea.Location = new System.Drawing.Point(295, 221);
            this.txtDesignArea.Name = "txtDesignArea";
            this.txtDesignArea.Size = new System.Drawing.Size(106, 21);
            this.txtDesignArea.TabIndex = 40;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(407, 224);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(41, 12);
            this.label28.TabIndex = 41;
            this.label28.Text = "平方米";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(407, 197);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(17, 12);
            this.label30.TabIndex = 43;
            this.label30.Text = "米";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label30);
            this.panel1.Controls.Add(this.label28);
            this.panel1.Controls.Add(this.txtDesignArea);
            this.panel1.Controls.Add(this.label27);
            this.panel1.Controls.Add(this.cboTunnelType);
            this.panel1.Controls.Add(this.cboCoalOrStone);
            this.panel1.Controls.Add(this.label29);
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.txtDesignLength);
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.lblTunnelName);
            this.panel1.Controls.Add(this.txtTunnelName);
            this.panel1.Controls.Add(this.btnSubmit);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.cboSupportPattern);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboLithology);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(461, 336);
            this.panel1.TabIndex = 0;
            // 
            // cboTunnelType
            // 
            this.cboTunnelType.FormattingEnabled = true;
            this.cboTunnelType.Location = new System.Drawing.Point(72, 248);
            this.cboTunnelType.Name = "cboTunnelType";
            this.cboTunnelType.Size = new System.Drawing.Size(154, 20);
            this.cboTunnelType.TabIndex = 11;
            // 
            // selectWorkingFaceControl1
            // 
            this.selectWorkingFaceControl1.Location = new System.Drawing.Point(3, 0);
            this.selectWorkingFaceControl1.Name = "selectWorkingFaceControl1";
            this.selectWorkingFaceControl1.SelectedWorkingFace = null;
            this.selectWorkingFaceControl1.Size = new System.Drawing.Size(461, 188);
            this.selectWorkingFaceControl1.TabIndex = 0;
            // 
            // TunnelInfoEntering
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(461, 336);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.IsMdiContainer = true;
            this.Name = "TunnelInfoEntering";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "巷道信息";
            this.Load += new System.EventHandler(this.TunnelInfoEntering_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboLithology;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboSupportPattern;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox txtTunnelName;
        private System.Windows.Forms.Label lblTunnelName;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtDesignLength;
        private System.Windows.Forms.Panel panel2;
        private SelectWorkingFaceControl selectWorkingFaceControl1;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox cboCoalOrStone;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtDesignArea;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboTunnelType;

    }
}