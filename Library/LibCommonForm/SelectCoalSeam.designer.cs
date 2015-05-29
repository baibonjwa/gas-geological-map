namespace LibLoginForm
{
    partial class SelectCoalSeam
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <params name="disposing">true if managed resources should be disposed; otherwise, false.</params>
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
            this.btnSelect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboCoalSeam = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(238, 28);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "选择";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "请选择煤层：";
            // 
            // cboCoalSeam
            // 
            this.cboCoalSeam.DisplayMember = "name";
            this.cboCoalSeam.FormattingEnabled = true;
            this.cboCoalSeam.Location = new System.Drawing.Point(95, 30);
            this.cboCoalSeam.Name = "cboCoalSeam";
            this.cboCoalSeam.Size = new System.Drawing.Size(121, 20);
            this.cboCoalSeam.TabIndex = 2;
            // 
            // SelectCoalSeam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 81);
            this.Controls.Add(this.cboCoalSeam);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelect);
            this.Name = "SelectCoalSeam";
            this.Text = "选择煤层";
            this.Load += new System.EventHandler(this.SelectCoalSeam_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboCoalSeam;
    }
}