namespace _3.GeologyMeasure
{
    partial class DrawTunnel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblTunnelName = new System.Windows.Forms.Label();
            this.lstTunnelName = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblWorkingFaceName = new System.Windows.Forms.Label();
            this.lstWorkingFaceName = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMiningAreaName = new System.Windows.Forms.Label();
            this.lstMiningAreaName = new System.Windows.Forms.ListBox();
            this.lblHorizontalName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMineName = new System.Windows.Forms.Label();
            this.lstHorizontalName = new System.Windows.Forms.ListBox();
            this.lstMineName = new System.Windows.Forms.ListBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(729, 433);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(648, 433);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 13;
            this.btnSubmit.Text = "确定";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(15, 170);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(133, 102);
            this.groupBox1.TabIndex = 58;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "巷道类型";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(21, 69);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(59, 16);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "新巷道";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(21, 31);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(59, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "老巷道";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.radioButton4);
            this.groupBox2.Location = new System.Drawing.Point(179, 170);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(133, 102);
            this.groupBox2.TabIndex = 59;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "绘制方式";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(21, 69);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(59, 16);
            this.radioButton3.TabIndex = 1;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "全自动";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(21, 31);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(59, 16);
            this.radioButton4.TabIndex = 0;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "交互式";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(343, 170);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(133, 102);
            this.groupBox3.TabIndex = 60;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "巷道样式";
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(12, 278);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(792, 118);
            this.groupBox4.TabIndex = 61;
            this.groupBox4.TabStop = false;
            // 
            // lblTunnelName
            // 
            this.lblTunnelName.AutoSize = true;
            this.lblTunnelName.Location = new System.Drawing.Point(667, 9);
            this.lblTunnelName.Name = "lblTunnelName";
            this.lblTunnelName.Size = new System.Drawing.Size(53, 12);
            this.lblTunnelName.TabIndex = 85;
            this.lblTunnelName.Text = "巷道名称";
            // 
            // lstTunnelName
            // 
            this.lstTunnelName.FormattingEnabled = true;
            this.lstTunnelName.ItemHeight = 12;
            this.lstTunnelName.Location = new System.Drawing.Point(669, 24);
            this.lstTunnelName.Name = "lstTunnelName";
            this.lstTunnelName.Size = new System.Drawing.Size(135, 124);
            this.lstTunnelName.TabIndex = 84;
            this.lstTunnelName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstTunnelName_MouseUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(646, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 83;
            this.label4.Text = ">>";
            // 
            // lblWorkingFaceName
            // 
            this.lblWorkingFaceName.AutoSize = true;
            this.lblWorkingFaceName.Location = new System.Drawing.Point(503, 9);
            this.lblWorkingFaceName.Name = "lblWorkingFaceName";
            this.lblWorkingFaceName.Size = new System.Drawing.Size(65, 12);
            this.lblWorkingFaceName.TabIndex = 82;
            this.lblWorkingFaceName.Text = "工作面名称";
            // 
            // lstWorkingFaceName
            // 
            this.lstWorkingFaceName.FormattingEnabled = true;
            this.lstWorkingFaceName.ItemHeight = 12;
            this.lstWorkingFaceName.Location = new System.Drawing.Point(505, 24);
            this.lstWorkingFaceName.Name = "lstWorkingFaceName";
            this.lstWorkingFaceName.Size = new System.Drawing.Size(135, 124);
            this.lstWorkingFaceName.TabIndex = 81;
            this.lstWorkingFaceName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstWorkingFaceName_MouseUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(482, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 80;
            this.label3.Text = ">>";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(318, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 79;
            this.label2.Text = ">>";
            // 
            // lblMiningAreaName
            // 
            this.lblMiningAreaName.AutoSize = true;
            this.lblMiningAreaName.Location = new System.Drawing.Point(339, 9);
            this.lblMiningAreaName.Name = "lblMiningAreaName";
            this.lblMiningAreaName.Size = new System.Drawing.Size(53, 12);
            this.lblMiningAreaName.TabIndex = 78;
            this.lblMiningAreaName.Text = "采区名称";
            // 
            // lstMiningAreaName
            // 
            this.lstMiningAreaName.FormattingEnabled = true;
            this.lstMiningAreaName.ItemHeight = 12;
            this.lstMiningAreaName.Location = new System.Drawing.Point(341, 24);
            this.lstMiningAreaName.Name = "lstMiningAreaName";
            this.lstMiningAreaName.Size = new System.Drawing.Size(135, 124);
            this.lstMiningAreaName.TabIndex = 77;
            this.lstMiningAreaName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstMiningAreaName_MouseUp);
            // 
            // lblHorizontalName
            // 
            this.lblHorizontalName.AutoSize = true;
            this.lblHorizontalName.Location = new System.Drawing.Point(175, 9);
            this.lblHorizontalName.Name = "lblHorizontalName";
            this.lblHorizontalName.Size = new System.Drawing.Size(53, 12);
            this.lblHorizontalName.TabIndex = 76;
            this.lblHorizontalName.Text = "水平名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(154, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 74;
            this.label1.Text = ">>";
            // 
            // lblMineName
            // 
            this.lblMineName.AutoSize = true;
            this.lblMineName.Location = new System.Drawing.Point(13, 9);
            this.lblMineName.Name = "lblMineName";
            this.lblMineName.Size = new System.Drawing.Size(53, 12);
            this.lblMineName.TabIndex = 75;
            this.lblMineName.Text = "矿井名称";
            // 
            // lstHorizontalName
            // 
            this.lstHorizontalName.FormattingEnabled = true;
            this.lstHorizontalName.ItemHeight = 12;
            this.lstHorizontalName.Location = new System.Drawing.Point(177, 24);
            this.lstHorizontalName.Name = "lstHorizontalName";
            this.lstHorizontalName.Size = new System.Drawing.Size(135, 124);
            this.lstHorizontalName.TabIndex = 73;
            this.lstHorizontalName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstHorizontalName_MouseUp);
            // 
            // lstMineName
            // 
            this.lstMineName.FormattingEnabled = true;
            this.lstMineName.ItemHeight = 12;
            this.lstMineName.Location = new System.Drawing.Point(13, 24);
            this.lstMineName.Name = "lstMineName";
            this.lstMineName.Size = new System.Drawing.Size(135, 124);
            this.lstMineName.TabIndex = 72;
            this.lstMineName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstMineName_MouseUp);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMessage.Location = new System.Drawing.Point(544, 181);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 14);
            this.lblMessage.TabIndex = 86;
            // 
            // DrawTunnel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 468);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblTunnelName);
            this.Controls.Add(this.lstTunnelName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblWorkingFaceName);
            this.Controls.Add(this.lstWorkingFaceName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblMiningAreaName);
            this.Controls.Add(this.lstMiningAreaName);
            this.Controls.Add(this.lblHorizontalName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMineName);
            this.Controls.Add(this.lstHorizontalName);
            this.Controls.Add(this.lstMineName);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Name = "DrawTunnel";
            this.Text = "绘制巷道";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblTunnelName;
        private System.Windows.Forms.ListBox lstTunnelName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblWorkingFaceName;
        private System.Windows.Forms.ListBox lstWorkingFaceName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMiningAreaName;
        private System.Windows.Forms.ListBox lstMiningAreaName;
        private System.Windows.Forms.Label lblHorizontalName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMineName;
        private System.Windows.Forms.ListBox lstHorizontalName;
        private System.Windows.Forms.ListBox lstMineName;
        private System.Windows.Forms.Label lblMessage;
    }
}