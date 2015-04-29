namespace GIS
{
    partial class LayersListControl
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
            this.lblLayers = new System.Windows.Forms.Label();
            this.cmbLayersList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblLayers
            // 
            this.lblLayers.AutoSize = true;
            this.lblLayers.Location = new System.Drawing.Point(3, 7);
            this.lblLayers.Name = "lblLayers";
            this.lblLayers.Size = new System.Drawing.Size(41, 12);
            this.lblLayers.TabIndex = 0;
            this.lblLayers.Text = "图层：";
            // 
            // cmbLayersList
            // 
            this.cmbLayersList.FormattingEnabled = true;
            this.cmbLayersList.Location = new System.Drawing.Point(41, 3);
            this.cmbLayersList.Name = "cmbLayersList";
            this.cmbLayersList.Size = new System.Drawing.Size(152, 20);
            this.cmbLayersList.TabIndex = 1;
            this.cmbLayersList.SelectedIndexChanged += new System.EventHandler(this.cmbLayersList_SelectedIndexChanged);
            // 
            // LayersListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbLayersList);
            this.Controls.Add(this.lblLayers);
            this.Name = "LayersListControl";
            this.Size = new System.Drawing.Size(202, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLayers;
        private System.Windows.Forms.ComboBox cmbLayersList;
    }
}
