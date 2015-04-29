namespace sys3
{
    partial class CollapsePillarsManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CollapsePillarsManagement));
            this.tsBtnExit = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsBtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsBtnDel = new System.Windows.Forms.ToolStripButton();
            this.tsBtnModify = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnPrint = new System.Windows.Forms.ToolStripButton();
            this.tsBtnExport = new System.Windows.Forms.ToolStripButton();
            this.tsBtnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnMap = new System.Windows.Forms.ToolStripButton();
            this.gcCollapsePillars = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.controlNavigator1 = new DevExpress.XtraEditors.ControlNavigator();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCollapsePillars)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tsBtnExit
            // 
            this.tsBtnExit.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnExit.Image")));
            this.tsBtnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnExit.Name = "tsBtnExit";
            this.tsBtnExit.Size = new System.Drawing.Size(52, 21);
            this.tsBtnExit.Text = "退出";
            this.tsBtnExit.Click += new System.EventHandler(this.tsBtnExit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 593);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(938, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsBtnRefresh
            // 
            this.tsBtnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnRefresh.Image")));
            this.tsBtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnRefresh.Name = "tsBtnRefresh";
            this.tsBtnRefresh.Size = new System.Drawing.Size(52, 21);
            this.tsBtnRefresh.Text = "刷新";
            this.tsBtnRefresh.Click += new System.EventHandler(this.tsBtnRefresh_Click);
            // 
            // tsBtnDel
            // 
            this.tsBtnDel.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnDel.Image")));
            this.tsBtnDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnDel.Name = "tsBtnDel";
            this.tsBtnDel.Size = new System.Drawing.Size(52, 21);
            this.tsBtnDel.Text = "删除";
            this.tsBtnDel.Click += new System.EventHandler(this.tsBtnDel_Click);
            // 
            // tsBtnModify
            // 
            this.tsBtnModify.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnModify.Image")));
            this.tsBtnModify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnModify.Name = "tsBtnModify";
            this.tsBtnModify.Size = new System.Drawing.Size(52, 21);
            this.tsBtnModify.Text = "修改";
            this.tsBtnModify.Click += new System.EventHandler(this.tsBtnModify_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnPrint,
            this.tsBtnExport,
            this.tsBtnAdd,
            this.tsBtnModify,
            this.tsBtnDel,
            this.tsBtnRefresh,
            this.btnMap,
            this.tsBtnExit});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(938, 24);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.TabStop = true;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsBtnPrint
            // 
            this.tsBtnPrint.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnPrint.Image")));
            this.tsBtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnPrint.Name = "tsBtnPrint";
            this.tsBtnPrint.Size = new System.Drawing.Size(52, 21);
            this.tsBtnPrint.Text = "打印";
            this.tsBtnPrint.Click += new System.EventHandler(this.tsBtnPrint_Click);
            // 
            // tsBtnExport
            // 
            this.tsBtnExport.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnExport.Image")));
            this.tsBtnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnExport.Name = "tsBtnExport";
            this.tsBtnExport.Size = new System.Drawing.Size(52, 21);
            this.tsBtnExport.Text = "导出";
            this.tsBtnExport.Click += new System.EventHandler(this.tsBtnExport_Click);
            // 
            // tsBtnAdd
            // 
            this.tsBtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnAdd.Image")));
            this.tsBtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnAdd.Name = "tsBtnAdd";
            this.tsBtnAdd.Size = new System.Drawing.Size(52, 21);
            this.tsBtnAdd.Text = "添加";
            this.tsBtnAdd.Click += new System.EventHandler(this.tsBtnAdd_Click);
            // 
            // btnMap
            // 
            this.btnMap.Image = ((System.Drawing.Image)(resources.GetObject("btnMap.Image")));
            this.btnMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMap.Name = "btnMap";
            this.btnMap.Size = new System.Drawing.Size(52, 21);
            this.btnMap.Text = "图显";
            this.btnMap.Click += new System.EventHandler(this.btnMap_Click);
            // 
            // gcCollapsePillars
            // 
            this.gcCollapsePillars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcCollapsePillars.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcCollapsePillars.Location = new System.Drawing.Point(10, 23);
            this.gcCollapsePillars.MainView = this.gridView1;
            this.gcCollapsePillars.Name = "gcCollapsePillars";
            this.gcCollapsePillars.Size = new System.Drawing.Size(917, 544);
            this.gcCollapsePillars.TabIndex = 7;
            this.gcCollapsePillars.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridView1.GridControl = this.gcCollapsePillars;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "陷落柱名称";
            this.gridColumn1.FieldName = "CollapsePillarsName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "描述";
            this.gridColumn2.FieldName = "Discribe";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // controlNavigator1
            // 
            this.controlNavigator1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.controlNavigator1.Location = new System.Drawing.Point(10, 573);
            this.controlNavigator1.Name = "controlNavigator1";
            this.controlNavigator1.NavigatableControl = this.gcCollapsePillars;
            this.controlNavigator1.Size = new System.Drawing.Size(267, 21);
            this.controlNavigator1.TabIndex = 86;
            this.controlNavigator1.Text = "controlNavigator1";
            this.controlNavigator1.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Center;
            this.controlNavigator1.TextStringFormat = "记录 {0} / {1}";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Excel 2007|*.xlsx|Excel 2003兼容|*.xls|PDF文档|*.pdf|TXT文档|*.txt";
            // 
            // CollapsePillarsManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 615);
            this.Controls.Add(this.controlNavigator1);
            this.Controls.Add(this.gcCollapsePillars);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CollapsePillarsManagement";
            this.Text = "陷落柱管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CollapsePillarsManagement_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCollapsePillars)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton tsBtnExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnRefresh;
        private System.Windows.Forms.ToolStripButton tsBtnDel;
        private System.Windows.Forms.ToolStripButton tsBtnModify;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnPrint;
        private System.Windows.Forms.ToolStripButton tsBtnExport;
        private System.Windows.Forms.ToolStripButton tsBtnAdd;
        private System.Windows.Forms.ToolStripButton btnMap;
        private DevExpress.XtraGrid.GridControl gcCollapsePillars;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.ControlNavigator controlNavigator1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}