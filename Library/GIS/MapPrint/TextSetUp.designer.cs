namespace GIS.MapPrint
{
    partial class TextSetUp
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.lblExample = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCalcel = new System.Windows.Forms.Button();
            this.btnConform = new System.Windows.Forms.Button();
            this.btnFontSet = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtContent);
            this.groupBox2.Location = new System.Drawing.Point(12, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(305, 104);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "内容";
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(6, 15);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContent.Size = new System.Drawing.Size(293, 84);
            this.txtContent.TabIndex = 3;
            // 
            // lblExample
            // 
            this.lblExample.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExample.AutoSize = true;
            this.lblExample.Location = new System.Drawing.Point(64, 45);
            this.lblExample.Name = "lblExample";
            this.lblExample.Size = new System.Drawing.Size(29, 12);
            this.lblExample.TabIndex = 0;
            this.lblExample.Text = "文字";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblExample);
            this.groupBox1.Location = new System.Drawing.Point(331, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 104);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "示例";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(420, 118);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 29;
            this.btnApply.Text = "应用";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCalcel
            // 
            this.btnCalcel.Location = new System.Drawing.Point(331, 118);
            this.btnCalcel.Name = "btnCalcel";
            this.btnCalcel.Size = new System.Drawing.Size(75, 23);
            this.btnCalcel.TabIndex = 27;
            this.btnCalcel.Text = "取消";
            this.btnCalcel.UseVisualStyleBackColor = true;
            this.btnCalcel.Click += new System.EventHandler(this.btnCalcel_Click);
            // 
            // btnConform
            // 
            this.btnConform.Location = new System.Drawing.Point(242, 118);
            this.btnConform.Name = "btnConform";
            this.btnConform.Size = new System.Drawing.Size(75, 23);
            this.btnConform.TabIndex = 28;
            this.btnConform.Text = "确定";
            this.btnConform.UseVisualStyleBackColor = true;
            this.btnConform.Click += new System.EventHandler(this.btnConform_Click);
            // 
            // btnFontSet
            // 
            this.btnFontSet.Location = new System.Drawing.Point(153, 118);
            this.btnFontSet.Name = "btnFontSet";
            this.btnFontSet.Size = new System.Drawing.Size(75, 23);
            this.btnFontSet.TabIndex = 26;
            this.btnFontSet.Text = "样式设置";
            this.btnFontSet.UseVisualStyleBackColor = true;
            this.btnFontSet.Click += new System.EventHandler(this.btnFontSet_Click);
            // 
            // TextSetUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 153);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCalcel);
            this.Controls.Add(this.btnConform);
            this.Controls.Add(this.btnFontSet);
            this.Name = "TextSetUp";
            this.Text = "文字设置";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Label lblExample;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCalcel;
        private System.Windows.Forms.Button btnConform;
        private System.Windows.Forms.Button btnFontSet;
    }
}