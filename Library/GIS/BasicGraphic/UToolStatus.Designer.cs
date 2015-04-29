namespace GIS.BasicGraphic
{
    partial class UToolStatus
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.statusBarXY = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolLabelBLC = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ckBtnQX = new DevExpress.XtraEditors.CheckButton();
            this.ckBtnBX = new DevExpress.XtraEditors.CheckButton();
            this.ckBtnZD = new DevExpress.XtraEditors.CheckButton();
            this.ckBtnZheD = new DevExpress.XtraEditors.CheckButton();
            this.ckBtnJD = new DevExpress.XtraEditors.CheckButton();
            this.ckBtnDD = new DevExpress.XtraEditors.CheckButton();
            this.ckBtnDian = new DevExpress.XtraEditors.CheckButton();
            this.ckBtnOpenBZ = new DevExpress.XtraEditors.CheckButton();
            this.cbBoxCKBLC = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbroate = new System.Windows.Forms.Label();
            this.txtRoate = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBarXY
            // 
            this.statusBarXY.Location = new System.Drawing.Point(4, 3);
            this.statusBarXY.Name = "statusBarXY";
            this.statusBarXY.Size = new System.Drawing.Size(240, 18);
            this.statusBarXY.TabIndex = 0;
            this.statusBarXY.Text = "当前坐标：3940418.93，513946.2 米";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(224, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "比例尺：";
            // 
            // toolLabelBLC
            // 
            this.toolLabelBLC.FormattingEnabled = true;
            this.toolLabelBLC.Items.AddRange(new object[] {
            "1:100",
            "1:250",
            "1:500",
            "1:1000",
            "1:2500",
            "1:5000",
            "1:10000",
            "1:25000",
            "1:50000",
            "1:100000",
            "1:250000",
            "1:500000"});
            this.toolLabelBLC.Location = new System.Drawing.Point(274, 0);
            this.toolLabelBLC.Name = "toolLabelBLC";
            this.toolLabelBLC.Size = new System.Drawing.Size(83, 20);
            this.toolLabelBLC.TabIndex = 3;
            this.toolLabelBLC.SelectedIndexChanged += new System.EventHandler(this.toolLabelBLC_SelectedIndexChanged);
            this.toolLabelBLC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolLabelBLC_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ckBtnQX);
            this.panel1.Controls.Add(this.ckBtnBX);
            this.panel1.Controls.Add(this.ckBtnZD);
            this.panel1.Controls.Add(this.ckBtnZheD);
            this.panel1.Controls.Add(this.ckBtnJD);
            this.panel1.Controls.Add(this.ckBtnDD);
            this.panel1.Controls.Add(this.ckBtnDian);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(428, -1);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(148, 24);
            this.panel1.TabIndex = 11;
            // 
            // ckBtnQX
            // 
            this.ckBtnQX.AllowFocus = false;
            this.ckBtnQX.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.ckBtnQX.Image = global::GIS.Properties.Resources.qiexian;
            this.ckBtnQX.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.ckBtnQX.Location = new System.Drawing.Point(43, 3);
            this.ckBtnQX.Margin = new System.Windows.Forms.Padding(0);
            this.ckBtnQX.Name = "ckBtnQX";
            this.ckBtnQX.Size = new System.Drawing.Size(19, 17);
            this.ckBtnQX.TabIndex = 6;
            this.ckBtnQX.Tag = "64";
            this.ckBtnQX.ToolTip = "捕捉切线";
            this.ckBtnQX.CheckedChanged += new System.EventHandler(this.ckBtnZD_CheckedChanged);
            // 
            // ckBtnBX
            // 
            this.ckBtnBX.AllowFocus = false;
            this.ckBtnBX.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.ckBtnBX.Image = global::GIS.Properties.Resources.bianxian;
            this.ckBtnBX.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.ckBtnBX.Location = new System.Drawing.Point(118, 3);
            this.ckBtnBX.Margin = new System.Windows.Forms.Padding(0);
            this.ckBtnBX.Name = "ckBtnBX";
            this.ckBtnBX.Size = new System.Drawing.Size(19, 17);
            this.ckBtnBX.TabIndex = 10;
            this.ckBtnBX.Tag = "8";
            this.ckBtnBX.ToolTip = "捕捉边线";
            this.ckBtnBX.CheckedChanged += new System.EventHandler(this.ckBtnZD_CheckedChanged);
            // 
            // ckBtnZD
            // 
            this.ckBtnZD.AllowFocus = false;
            this.ckBtnZD.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.ckBtnZD.Image = global::GIS.Properties.Resources.zhongdian;
            this.ckBtnZD.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.ckBtnZD.Location = new System.Drawing.Point(5, 3);
            this.ckBtnZD.Margin = new System.Windows.Forms.Padding(0);
            this.ckBtnZD.Name = "ckBtnZD";
            this.ckBtnZD.Size = new System.Drawing.Size(19, 17);
            this.ckBtnZD.TabIndex = 4;
            this.ckBtnZD.Tag = "16";
            this.ckBtnZD.ToolTip = "捕捉中点";
            this.ckBtnZD.CheckedChanged += new System.EventHandler(this.ckBtnZD_CheckedChanged);
            // 
            // ckBtnZheD
            // 
            this.ckBtnZheD.AllowFocus = false;
            this.ckBtnZheD.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.ckBtnZheD.Image = global::GIS.Properties.Resources.zhedian;
            this.ckBtnZheD.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.ckBtnZheD.Location = new System.Drawing.Point(100, 3);
            this.ckBtnZheD.Margin = new System.Windows.Forms.Padding(0);
            this.ckBtnZheD.Name = "ckBtnZheD";
            this.ckBtnZheD.Size = new System.Drawing.Size(19, 17);
            this.ckBtnZheD.TabIndex = 9;
            this.ckBtnZheD.Tag = "4";
            this.ckBtnZheD.ToolTip = "捕捉折点";
            this.ckBtnZheD.CheckedChanged += new System.EventHandler(this.ckBtnZD_CheckedChanged);
            // 
            // ckBtnJD
            // 
            this.ckBtnJD.AllowFocus = false;
            this.ckBtnJD.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.ckBtnJD.Image = global::GIS.Properties.Resources.jiaodian;
            this.ckBtnJD.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.ckBtnJD.Location = new System.Drawing.Point(24, 3);
            this.ckBtnJD.Margin = new System.Windows.Forms.Padding(0);
            this.ckBtnJD.Name = "ckBtnJD";
            this.ckBtnJD.Size = new System.Drawing.Size(19, 17);
            this.ckBtnJD.TabIndex = 5;
            this.ckBtnJD.Tag = "32";
            this.ckBtnJD.ToolTip = "捕捉交点";
            this.ckBtnJD.CheckedChanged += new System.EventHandler(this.ckBtnZD_CheckedChanged);
            // 
            // ckBtnDD
            // 
            this.ckBtnDD.AllowFocus = false;
            this.ckBtnDD.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.ckBtnDD.Image = global::GIS.Properties.Resources.duandian;
            this.ckBtnDD.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.ckBtnDD.Location = new System.Drawing.Point(81, 3);
            this.ckBtnDD.Margin = new System.Windows.Forms.Padding(0);
            this.ckBtnDD.Name = "ckBtnDD";
            this.ckBtnDD.Size = new System.Drawing.Size(19, 17);
            this.ckBtnDD.TabIndex = 8;
            this.ckBtnDD.Tag = "2";
            this.ckBtnDD.ToolTip = "捕捉端点";
            this.ckBtnDD.CheckedChanged += new System.EventHandler(this.ckBtnZD_CheckedChanged);
            // 
            // ckBtnDian
            // 
            this.ckBtnDian.AllowFocus = false;
            this.ckBtnDian.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.ckBtnDian.Image = global::GIS.Properties.Resources.dian;
            this.ckBtnDian.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.ckBtnDian.Location = new System.Drawing.Point(62, 3);
            this.ckBtnDian.Margin = new System.Windows.Forms.Padding(0);
            this.ckBtnDian.Name = "ckBtnDian";
            this.ckBtnDian.Size = new System.Drawing.Size(19, 17);
            this.ckBtnDian.TabIndex = 7;
            this.ckBtnDian.Tag = "1";
            this.ckBtnDian.ToolTip = "捕捉点";
            this.ckBtnDian.CheckedChanged += new System.EventHandler(this.ckBtnZD_CheckedChanged);
            // 
            // ckBtnOpenBZ
            // 
            this.ckBtnOpenBZ.AllowFocus = false;
            this.ckBtnOpenBZ.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.ckBtnOpenBZ.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.ckBtnOpenBZ.Location = new System.Drawing.Point(370, 0);
            this.ckBtnOpenBZ.Margin = new System.Windows.Forms.Padding(0);
            this.ckBtnOpenBZ.Name = "ckBtnOpenBZ";
            this.ckBtnOpenBZ.Size = new System.Drawing.Size(60, 20);
            this.ckBtnOpenBZ.TabIndex = 12;
            this.ckBtnOpenBZ.Text = "捕捉关闭";
            this.ckBtnOpenBZ.CheckedChanged += new System.EventHandler(this.ckBtnOpenBZ_CheckedChanged);
            // 
            // cbBoxCKBLC
            // 
            this.cbBoxCKBLC.FormattingEnabled = true;
            this.cbBoxCKBLC.Items.AddRange(new object[] {
            "无",
            "1:50",
            "1:100",
            "1:250",
            "1:500",
            "1:1000",
            "1:2500",
            "1:5000",
            "1:10000",
            "1:25000",
            "1:50000"});
            this.cbBoxCKBLC.Location = new System.Drawing.Point(650, 1);
            this.cbBoxCKBLC.Name = "cbBoxCKBLC";
            this.cbBoxCKBLC.Size = new System.Drawing.Size(83, 20);
            this.cbBoxCKBLC.TabIndex = 14;
            this.cbBoxCKBLC.Text = "无";
            this.cbBoxCKBLC.SelectedIndexChanged += new System.EventHandler(this.cbBoxCKBLC_SelectedIndexChanged);
            this.cbBoxCKBLC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbBoxCKBLC_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(577, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "参考比例尺：";
            // 
            // lbroate
            // 
            this.lbroate.AutoSize = true;
            this.lbroate.Location = new System.Drawing.Point(740, 5);
            this.lbroate.Name = "lbroate";
            this.lbroate.Size = new System.Drawing.Size(65, 12);
            this.lbroate.TabIndex = 15;
            this.lbroate.Text = "图框角度：";
            // 
            // txtRoate
            // 
            this.txtRoate.Location = new System.Drawing.Point(801, 1);
            this.txtRoate.Name = "txtRoate";
            this.txtRoate.Size = new System.Drawing.Size(48, 21);
            this.txtRoate.TabIndex = 16;
            this.txtRoate.Text = "0";
            this.txtRoate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRoate_KeyDown);
            // 
            // UToolStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtRoate);
            this.Controls.Add(this.lbroate);
            this.Controls.Add(this.cbBoxCKBLC);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ckBtnOpenBZ);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolLabelBLC);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusBarXY);
            this.MaximumSize = new System.Drawing.Size(0, 22);
            this.MinimumSize = new System.Drawing.Size(0, 22);
            this.Name = "UToolStatus";
            this.Size = new System.Drawing.Size(1087, 22);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label statusBarXY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox toolLabelBLC;
        private DevExpress.XtraEditors.CheckButton ckBtnZD;
        private DevExpress.XtraEditors.CheckButton ckBtnJD;
        private DevExpress.XtraEditors.CheckButton ckBtnQX;
        private DevExpress.XtraEditors.CheckButton ckBtnDian;
        private DevExpress.XtraEditors.CheckButton ckBtnDD;
        private DevExpress.XtraEditors.CheckButton ckBtnZheD;
        private DevExpress.XtraEditors.CheckButton ckBtnBX;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.CheckButton ckBtnOpenBZ;
        private System.Windows.Forms.ComboBox cbBoxCKBLC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbroate;
        private System.Windows.Forms.TextBox txtRoate;

    }
}
