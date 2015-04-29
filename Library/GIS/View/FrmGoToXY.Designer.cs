namespace GIS.View
{
    partial class FrmGoToXY
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tooltemPan = new System.Windows.Forms.ToolStripMenuItem();
            this.toolItemZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(33, 29);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(127, 21);
            this.txtX.TabIndex = 1;
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(189, 29);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(124, 21);
            this.txtY.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(166, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Y:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tooltemPan,
            this.toolItemZoom});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(325, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tooltemPan
            // 
            this.tooltemPan.Image = global::GIS.Properties.Resources.PanTool16;
            this.tooltemPan.Name = "tooltemPan";
            this.tooltemPan.Size = new System.Drawing.Size(72, 21);
            this.tooltemPan.Text = "平移至";
            this.tooltemPan.Click += new System.EventHandler(this.tooltemPan_Click);
            // 
            // toolItemZoom
            // 
            this.toolItemZoom.Image = global::GIS.Properties.Resources.ZoomInTool16;
            this.toolItemZoom.Name = "toolItemZoom";
            this.toolItemZoom.Size = new System.Drawing.Size(72, 21);
            this.toolItemZoom.Text = "缩放至";
            this.toolItemZoom.Click += new System.EventHandler(this.toolItemZoom_Click);
            // 
            // FrmGoToXY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 60);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmGoToXY";
            this.ShowInTaskbar = false;
            this.Text = "转到XY";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tooltemPan;
        private System.Windows.Forms.ToolStripMenuItem toolItemZoom;
    }
}