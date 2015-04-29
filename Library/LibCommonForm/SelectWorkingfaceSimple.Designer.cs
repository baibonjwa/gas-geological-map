namespace LibCommonForm
{
    partial class SelectWorkingfaceSimple
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbxWorkingface = new System.Windows.Forms.ComboBox();
            this.btnChooseWorkingface = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbxWorkingface
            // 
            this.cbxWorkingface.DisplayMember = "WorkingFaceName";
            this.cbxWorkingface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxWorkingface.FormattingEnabled = true;
            this.cbxWorkingface.Location = new System.Drawing.Point(3, 9);
            this.cbxWorkingface.Name = "cbxWorkingface";
            this.cbxWorkingface.Size = new System.Drawing.Size(119, 20);
            this.cbxWorkingface.TabIndex = 1;
            this.cbxWorkingface.ValueMember = "WorkingFaceId";
            this.cbxWorkingface.SelectedIndexChanged += new System.EventHandler(this.cbxTunnel_SelectedIndexChanged);
            this.cbxWorkingface.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbxTunnel_KeyDown);
            // 
            // btnChooseWorkingface
            // 
            this.btnChooseWorkingface.Location = new System.Drawing.Point(128, 7);
            this.btnChooseWorkingface.Name = "btnChooseWorkingface";
            this.btnChooseWorkingface.Size = new System.Drawing.Size(75, 23);
            this.btnChooseWorkingface.TabIndex = 2;
            this.btnChooseWorkingface.Text = "选择工作面";
            this.btnChooseWorkingface.UseVisualStyleBackColor = true;
            this.btnChooseWorkingface.Click += new System.EventHandler(this.btnChooseTunnel_Click);
            // 
            // SelectWorkingfaceSimple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnChooseWorkingface);
            this.Controls.Add(this.cbxWorkingface);
            this.Name = "SelectWorkingfaceSimple";
            this.Size = new System.Drawing.Size(219, 38);
            this.Load += new System.EventHandler(this.SelectTunnelSimple_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox cbxWorkingface;
        private System.Windows.Forms.Button btnChooseWorkingface;
    }
}
