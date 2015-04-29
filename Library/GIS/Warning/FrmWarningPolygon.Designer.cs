namespace GIS.Warning
{
    partial class FrmWarningPolygon
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
            this.btnup = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.checkAll = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.progressBarControl2 = new DevExpress.XtraEditors.ProgressBarControl();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnup
            // 
            this.btnup.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnup.Location = new System.Drawing.Point(12, 310);
            this.btnup.Name = "btnup";
            this.btnup.Size = new System.Drawing.Size(75, 23);
            this.btnup.TabIndex = 1;
            this.btnup.Text = "生   成";
            this.btnup.UseVisualStyleBackColor = true;
            this.btnup.Click += new System.EventHandler(this.btnup_Click);
            // 
            // btnclose
            // 
            this.btnclose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnclose.Location = new System.Drawing.Point(150, 310);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(75, 23);
            this.btnclose.TabIndex = 2;
            this.btnclose.Text = "退   出";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // checkAll
            // 
            this.checkAll.AutoSize = true;
            this.checkAll.Location = new System.Drawing.Point(190, 4);
            this.checkAll.Name = "checkAll";
            this.checkAll.Size = new System.Drawing.Size(48, 16);
            this.checkAll.TabIndex = 3;
            this.checkAll.Text = "全选";
            this.checkAll.UseVisualStyleBackColor = true;
            this.checkAll.CheckedChanged += new System.EventHandler(this.checkAll_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "选择要生成预警图的地质构造";
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Location = new System.Drawing.Point(3, 23);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(231, 222);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Location = new System.Drawing.Point(3, 253);
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
            this.progressBarControl1.Size = new System.Drawing.Size(231, 18);
            this.progressBarControl1.TabIndex = 19;
            // 
            // progressBarControl2
            // 
            this.progressBarControl2.Location = new System.Drawing.Point(3, 284);
            this.progressBarControl2.Name = "progressBarControl2";
            this.progressBarControl2.Properties.Appearance.BackColor = System.Drawing.SystemColors.Highlight;
            this.progressBarControl2.Properties.Appearance.BackColor2 = System.Drawing.Color.Lime;
            this.progressBarControl2.Properties.Appearance.ForeColor = System.Drawing.SystemColors.Highlight;
            this.progressBarControl2.Properties.Appearance.ForeColor2 = System.Drawing.Color.Lime;
            this.progressBarControl2.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Lime;
            this.progressBarControl2.Properties.AppearanceDisabled.BackColor2 = System.Drawing.Color.Lime;
            this.progressBarControl2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Lime;
            this.progressBarControl2.Properties.AppearanceDisabled.ForeColor2 = System.Drawing.Color.Lime;
            this.progressBarControl2.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.progressBarControl2.Size = new System.Drawing.Size(231, 18);
            this.progressBarControl2.TabIndex = 20;
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(3, 251);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(231, 22);
            this.progressBar2.TabIndex = 21;
            this.progressBar2.Visible = false;
            // 
            // FrmWarningPolygon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 340);
            this.Controls.Add(this.progressBarControl1);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBarControl2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkAll);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.btnup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmWarningPolygon";
            this.Text = "区域预警图";
            this.Load += new System.EventHandler(this.FrmWarningPolygon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl2.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnup;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.CheckBox checkAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView1;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl2;
        private System.Windows.Forms.ProgressBar progressBar2;
    }
}