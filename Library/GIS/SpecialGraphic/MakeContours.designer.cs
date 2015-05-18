namespace GIS.SpecialGraphic
{
    partial class MakeContours
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
            this.CB_InterpolationMethod = new System.Windows.Forms.ComboBox();
            this.TB_DocumentPath = new System.Windows.Forms.TextBox();
            this.FileInput = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_Interval = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BT_Ok = new System.Windows.Forms.Button();
            this.BT_Cancle = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CB_searchRadiusProp = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.CB_semiVariogramProp = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CB_SplineType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CB_ZFiled = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioBtnLSD = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.radioBtnKJ = new System.Windows.Forms.RadioButton();
            this.panelLSD = new System.Windows.Forms.Panel();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioBtndata = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbshengc = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelLSD.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // CB_InterpolationMethod
            // 
            this.CB_InterpolationMethod.BackColor = System.Drawing.Color.White;
            this.CB_InterpolationMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_InterpolationMethod.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CB_InterpolationMethod.Items.AddRange(new object[] {
            "样条函数插值法",
            "自然邻域插值法",
            "克里格插值法",
            "反距离权重插值法",
            "趋势面插值法"});
            this.CB_InterpolationMethod.Location = new System.Drawing.Point(74, 7);
            this.CB_InterpolationMethod.Name = "CB_InterpolationMethod";
            this.CB_InterpolationMethod.Size = new System.Drawing.Size(157, 20);
            this.CB_InterpolationMethod.TabIndex = 0;
            this.CB_InterpolationMethod.SelectedIndexChanged += new System.EventHandler(this.CB_InterpolationMethod_SelectedIndexChanged);
            // 
            // TB_DocumentPath
            // 
            this.TB_DocumentPath.Location = new System.Drawing.Point(78, 8);
            this.TB_DocumentPath.Name = "TB_DocumentPath";
            this.TB_DocumentPath.Size = new System.Drawing.Size(102, 21);
            this.TB_DocumentPath.TabIndex = 3;
            // 
            // FileInput
            // 
            this.FileInput.Location = new System.Drawing.Point(195, 8);
            this.FileInput.Name = "FileInput";
            this.FileInput.Size = new System.Drawing.Size(46, 23);
            this.FileInput.TabIndex = 4;
            this.FileInput.Text = "打开";
            this.FileInput.UseVisualStyleBackColor = true;
            this.FileInput.Click += new System.EventHandler(this.FileInput_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "方法选择:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "离散点路径:";
            // 
            // TB_Interval
            // 
            this.TB_Interval.Location = new System.Drawing.Point(81, 76);
            this.TB_Interval.Name = "TB_Interval";
            this.TB_Interval.Size = new System.Drawing.Size(102, 21);
            this.TB_Interval.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "等 高 距:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(191, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "单位(米)";
            // 
            // BT_Ok
            // 
            this.BT_Ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_Ok.Location = new System.Drawing.Point(14, 6);
            this.BT_Ok.Name = "BT_Ok";
            this.BT_Ok.Size = new System.Drawing.Size(73, 33);
            this.BT_Ok.TabIndex = 11;
            this.BT_Ok.Text = "生成";
            this.BT_Ok.UseVisualStyleBackColor = true;
            this.BT_Ok.Click += new System.EventHandler(this.BT_Ok_Click);
            // 
            // BT_Cancle
            // 
            this.BT_Cancle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_Cancle.Location = new System.Drawing.Point(159, 6);
            this.BT_Cancle.Name = "BT_Cancle";
            this.BT_Cancle.Size = new System.Drawing.Size(73, 33);
            this.BT_Cancle.TabIndex = 12;
            this.BT_Cancle.Text = "取消";
            this.BT_Cancle.UseVisualStyleBackColor = true;
            this.BT_Cancle.Click += new System.EventHandler(this.BT_Cancle_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CB_searchRadiusProp);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.CB_semiVariogramProp);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(5, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 84);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "克里格插值法参数设置";
            this.groupBox1.Visible = false;
            // 
            // CB_searchRadiusProp
            // 
            this.CB_searchRadiusProp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_searchRadiusProp.FormattingEnabled = true;
            this.CB_searchRadiusProp.Items.AddRange(new object[] {
            "固定式",
            "可变式"});
            this.CB_searchRadiusProp.Location = new System.Drawing.Point(95, 54);
            this.CB_searchRadiusProp.Name = "CB_searchRadiusProp";
            this.CB_searchRadiusProp.Size = new System.Drawing.Size(121, 20);
            this.CB_searchRadiusProp.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "搜索半径类型:";
            // 
            // CB_semiVariogramProp
            // 
            this.CB_semiVariogramProp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_semiVariogramProp.FormattingEnabled = true;
            this.CB_semiVariogramProp.Items.AddRange(new object[] {
            "球模型",
            "圆模型",
            "指数模型",
            "高斯模型",
            "线模型",
            "一次线性漂移模型",
            "二次线性漂移模型"});
            this.CB_semiVariogramProp.Location = new System.Drawing.Point(95, 27);
            this.CB_semiVariogramProp.Name = "CB_semiVariogramProp";
            this.CB_semiVariogramProp.Size = new System.Drawing.Size(121, 20);
            this.CB_semiVariogramProp.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "半方差模型:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CB_SplineType);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(5, 33);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(239, 59);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "样条函数插值法参数设置";
            // 
            // CB_SplineType
            // 
            this.CB_SplineType.AutoCompleteCustomSource.AddRange(new string[] {
            "正规化",
            "张力化"});
            this.CB_SplineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_SplineType.FormattingEnabled = true;
            this.CB_SplineType.Items.AddRange(new object[] {
            "正规化",
            "张力化"});
            this.CB_SplineType.Location = new System.Drawing.Point(95, 27);
            this.CB_SplineType.Name = "CB_SplineType";
            this.CB_SplineType.Size = new System.Drawing.Size(121, 20);
            this.CB_SplineType.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "样条线类型:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CB_ZFiled);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(5, 197);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(242, 59);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "趋势面插值法参数设置";
            this.groupBox3.Visible = false;
            // 
            // CB_ZFiled
            // 
            this.CB_ZFiled.AutoCompleteCustomSource.AddRange(new string[] {
            "正规化",
            "张力化"});
            this.CB_ZFiled.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_ZFiled.FormattingEnabled = true;
            this.CB_ZFiled.Items.AddRange(new object[] {
            "Shape.Z"});
            this.CB_ZFiled.Location = new System.Drawing.Point(95, 27);
            this.CB_ZFiled.Name = "CB_ZFiled";
            this.CB_ZFiled.Size = new System.Drawing.Size(121, 20);
            this.CB_ZFiled.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "高度值:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panelLSD);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioBtndata);
            this.panel1.Controls.Add(this.TB_Interval);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(8, 119);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(253, 144);
            this.panel1.TabIndex = 16;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.radioBtnLSD);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.radioBtnKJ);
            this.panel3.Location = new System.Drawing.Point(3, 99);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(247, 32);
            this.panel3.TabIndex = 20;
            // 
            // radioBtnLSD
            // 
            this.radioBtnLSD.AutoSize = true;
            this.radioBtnLSD.Checked = true;
            this.radioBtnLSD.Location = new System.Drawing.Point(155, 8);
            this.radioBtnLSD.Name = "radioBtnLSD";
            this.radioBtnLSD.Size = new System.Drawing.Size(83, 16);
            this.radioBtnLSD.TabIndex = 16;
            this.radioBtnLSD.TabStop = true;
            this.radioBtnLSD.Text = "离散点边界";
            this.radioBtnLSD.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 14;
            this.label11.Text = "范    围:";
            // 
            // radioBtnKJ
            // 
            this.radioBtnKJ.AutoSize = true;
            this.radioBtnKJ.Location = new System.Drawing.Point(79, 8);
            this.radioBtnKJ.Name = "radioBtnKJ";
            this.radioBtnKJ.Size = new System.Drawing.Size(47, 16);
            this.radioBtnKJ.TabIndex = 15;
            this.radioBtnKJ.Text = "矿界";
            this.radioBtnKJ.UseVisualStyleBackColor = true;
            // 
            // panelLSD
            // 
            this.panelLSD.Controls.Add(this.TB_DocumentPath);
            this.panelLSD.Controls.Add(this.FileInput);
            this.panelLSD.Controls.Add(this.label2);
            this.panelLSD.Enabled = false;
            this.panelLSD.Location = new System.Drawing.Point(3, 32);
            this.panelLSD.Name = "panelLSD";
            this.panelLSD.Size = new System.Drawing.Size(244, 37);
            this.panelLSD.TabIndex = 19;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(138, 9);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(107, 16);
            this.radioButton2.TabIndex = 18;
            this.radioButton2.Text = "离散点文本文件";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioBtndata
            // 
            this.radioBtndata.AutoSize = true;
            this.radioBtndata.Checked = true;
            this.radioBtndata.Location = new System.Drawing.Point(10, 9);
            this.radioBtndata.Name = "radioBtndata";
            this.radioBtndata.Size = new System.Drawing.Size(119, 16);
            this.radioBtndata.TabIndex = 17;
            this.radioBtndata.TabStop = true;
            this.radioBtndata.Text = "数据库读取离散点";
            this.radioBtndata.UseVisualStyleBackColor = true;
            this.radioBtndata.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.CB_InterpolationMethod);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Location = new System.Drawing.Point(8, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(253, 101);
            this.panel2.TabIndex = 17;
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBarControl1.Location = new System.Drawing.Point(0, 336);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Properties.Appearance.BackColor = System.Drawing.Color.Lime;
            this.progressBarControl1.Properties.Appearance.BackColor2 = System.Drawing.Color.Lime;
            this.progressBarControl1.Properties.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.progressBarControl1.Properties.Appearance.ForeColor2 = System.Drawing.Color.Lime;
            this.progressBarControl1.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Lime;
            this.progressBarControl1.Properties.AppearanceDisabled.BackColor2 = System.Drawing.Color.Lime;
            this.progressBarControl1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Lime;
            this.progressBarControl1.Properties.AppearanceDisabled.ForeColor2 = System.Drawing.Color.Lime;
            this.progressBarControl1.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.progressBarControl1.Size = new System.Drawing.Size(271, 18);
            this.progressBarControl1.TabIndex = 18;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.BT_Ok);
            this.panel4.Controls.Add(this.BT_Cancle);
            this.panel4.Location = new System.Drawing.Point(8, 266);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(253, 43);
            this.panel4.TabIndex = 19;
            // 
            // lbshengc
            // 
            this.lbshengc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbshengc.AutoSize = true;
            this.lbshengc.Location = new System.Drawing.Point(4, 314);
            this.lbshengc.Name = "lbshengc";
            this.lbshengc.Size = new System.Drawing.Size(173, 12);
            this.lbshengc.TabIndex = 20;
            this.lbshengc.Text = "正在生成中，请耐心等待......";
            this.lbshengc.Visible = false;
            // 
            // MakeContours
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 354);
            this.Controls.Add(this.lbshengc);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.progressBarControl1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MakeContours";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "离散点插值生成等值线";
            this.Load += new System.EventHandler(this.MakeContours_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panelLSD.ResumeLayout(false);
            this.panelLSD.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CB_InterpolationMethod;
        private System.Windows.Forms.TextBox TB_DocumentPath;
        private System.Windows.Forms.Button FileInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_Interval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BT_Ok;
        private System.Windows.Forms.Button BT_Cancle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox CB_searchRadiusProp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox CB_semiVariogramProp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox CB_SplineType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox CB_ZFiled;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton radioBtnLSD;
        private System.Windows.Forms.RadioButton radioBtnKJ;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioBtndata;
        private System.Windows.Forms.Panel panelLSD;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lbshengc;
    }
}