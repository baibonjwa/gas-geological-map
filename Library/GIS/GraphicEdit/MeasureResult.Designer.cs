namespace GIS
{
    partial class MeasureResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MeasureResult));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiDistanceUnit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDisKilometers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDisMeters = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDisDeci = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDisCenti = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDisMilli = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDisMiles = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDisNauticalMiles = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDisYards = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDisFeet = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDisInches = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDisDecimalDegrees = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAreaUnit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAreaKilo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAreaMeters = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAreaMiles = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAreaFeet = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAreaYards = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAreaHectares = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAreaAcres = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnClear = new System.Windows.Forms.ToolStripButton();
            this.txtResultInfo = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.tsbtnClear});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(272, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDistanceUnit,
            this.tsmiAreaUnit});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(45, 22);
            this.toolStripDropDownButton1.Text = "单位";
            // 
            // tsmiDistanceUnit
            // 
            this.tsmiDistanceUnit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDisKilometers,
            this.tsmiDisMeters,
            this.tsmiDisDeci,
            this.tsmiDisCenti,
            this.tsmiDisMilli,
            this.tsmiDisMiles,
            this.tsmiDisNauticalMiles,
            this.tsmiDisYards,
            this.tsmiDisFeet,
            this.tsmiDisInches,
            this.tsmiDisDecimalDegrees});
            this.tsmiDistanceUnit.Name = "tsmiDistanceUnit";
            this.tsmiDistanceUnit.Size = new System.Drawing.Size(152, 22);
            this.tsmiDistanceUnit.Text = "距离";
            // 
            // tsmiDisKilometers
            // 
            this.tsmiDisKilometers.Name = "tsmiDisKilometers";
            this.tsmiDisKilometers.Size = new System.Drawing.Size(152, 22);
            this.tsmiDisKilometers.Tag = "Kilometers";
            this.tsmiDisKilometers.Text = "千米";
            this.tsmiDisKilometers.Click += new System.EventHandler(this.tsmiDisKilometers_Click);
            // 
            // tsmiDisMeters
            // 
            this.tsmiDisMeters.Name = "tsmiDisMeters";
            this.tsmiDisMeters.Size = new System.Drawing.Size(152, 22);
            this.tsmiDisMeters.Tag = "Meters";
            this.tsmiDisMeters.Text = "米";
            this.tsmiDisMeters.Click += new System.EventHandler(this.tsmiDisKilometers_Click);
            // 
            // tsmiDisDeci
            // 
            this.tsmiDisDeci.Name = "tsmiDisDeci";
            this.tsmiDisDeci.Size = new System.Drawing.Size(152, 22);
            this.tsmiDisDeci.Tag = "Decimeters";
            this.tsmiDisDeci.Text = "分米";
            this.tsmiDisDeci.Visible = false;
            this.tsmiDisDeci.Click += new System.EventHandler(this.tsmiDisKilometers_Click);
            // 
            // tsmiDisCenti
            // 
            this.tsmiDisCenti.Name = "tsmiDisCenti";
            this.tsmiDisCenti.Size = new System.Drawing.Size(152, 22);
            this.tsmiDisCenti.Tag = "Centimeters";
            this.tsmiDisCenti.Text = "厘米";
            this.tsmiDisCenti.Visible = false;
            this.tsmiDisCenti.Click += new System.EventHandler(this.tsmiDisKilometers_Click);
            // 
            // tsmiDisMilli
            // 
            this.tsmiDisMilli.Name = "tsmiDisMilli";
            this.tsmiDisMilli.Size = new System.Drawing.Size(152, 22);
            this.tsmiDisMilli.Tag = "Millimeters";
            this.tsmiDisMilli.Text = "毫米";
            this.tsmiDisMilli.Visible = false;
            this.tsmiDisMilli.Click += new System.EventHandler(this.tsmiDisKilometers_Click);
            // 
            // tsmiDisMiles
            // 
            this.tsmiDisMiles.Name = "tsmiDisMiles";
            this.tsmiDisMiles.Size = new System.Drawing.Size(152, 22);
            this.tsmiDisMiles.Tag = "Miles";
            this.tsmiDisMiles.Text = "英里";
            this.tsmiDisMiles.Visible = false;
            this.tsmiDisMiles.Click += new System.EventHandler(this.tsmiDisKilometers_Click);
            // 
            // tsmiDisNauticalMiles
            // 
            this.tsmiDisNauticalMiles.Name = "tsmiDisNauticalMiles";
            this.tsmiDisNauticalMiles.Size = new System.Drawing.Size(152, 22);
            this.tsmiDisNauticalMiles.Tag = "NauticalMiles";
            this.tsmiDisNauticalMiles.Text = "海里";
            this.tsmiDisNauticalMiles.Visible = false;
            this.tsmiDisNauticalMiles.Click += new System.EventHandler(this.tsmiDisKilometers_Click);
            // 
            // tsmiDisYards
            // 
            this.tsmiDisYards.Name = "tsmiDisYards";
            this.tsmiDisYards.Size = new System.Drawing.Size(152, 22);
            this.tsmiDisYards.Tag = "Yards";
            this.tsmiDisYards.Text = "码";
            this.tsmiDisYards.Visible = false;
            this.tsmiDisYards.Click += new System.EventHandler(this.tsmiDisKilometers_Click);
            // 
            // tsmiDisFeet
            // 
            this.tsmiDisFeet.Name = "tsmiDisFeet";
            this.tsmiDisFeet.Size = new System.Drawing.Size(152, 22);
            this.tsmiDisFeet.Tag = "Feet";
            this.tsmiDisFeet.Text = "英尺";
            this.tsmiDisFeet.Visible = false;
            this.tsmiDisFeet.Click += new System.EventHandler(this.tsmiDisKilometers_Click);
            // 
            // tsmiDisInches
            // 
            this.tsmiDisInches.Name = "tsmiDisInches";
            this.tsmiDisInches.Size = new System.Drawing.Size(152, 22);
            this.tsmiDisInches.Tag = "Inches";
            this.tsmiDisInches.Text = "英寸";
            this.tsmiDisInches.Visible = false;
            this.tsmiDisInches.Click += new System.EventHandler(this.tsmiDisKilometers_Click);
            // 
            // tsmiDisDecimalDegrees
            // 
            this.tsmiDisDecimalDegrees.Name = "tsmiDisDecimalDegrees";
            this.tsmiDisDecimalDegrees.Size = new System.Drawing.Size(152, 22);
            this.tsmiDisDecimalDegrees.Tag = "DecimalDegrees";
            this.tsmiDisDecimalDegrees.Text = "度";
            this.tsmiDisDecimalDegrees.Visible = false;
            this.tsmiDisDecimalDegrees.Click += new System.EventHandler(this.tsmiDisKilometers_Click);
            // 
            // tsmiAreaUnit
            // 
            this.tsmiAreaUnit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAreaKilo,
            this.tsmiAreaMeters,
            this.tsmiAreaMiles,
            this.tsmiAreaFeet,
            this.tsmiAreaYards,
            this.tsmiAreaHectares,
            this.tsmiAreaAcres});
            this.tsmiAreaUnit.Name = "tsmiAreaUnit";
            this.tsmiAreaUnit.Size = new System.Drawing.Size(152, 22);
            this.tsmiAreaUnit.Text = "面积";
            // 
            // tsmiAreaKilo
            // 
            this.tsmiAreaKilo.Name = "tsmiAreaKilo";
            this.tsmiAreaKilo.Size = new System.Drawing.Size(152, 22);
            this.tsmiAreaKilo.Tag = "Kilometers";
            this.tsmiAreaKilo.Text = "千米";
            this.tsmiAreaKilo.Click += new System.EventHandler(this.tsmiAreaKilo_Click);
            // 
            // tsmiAreaMeters
            // 
            this.tsmiAreaMeters.Name = "tsmiAreaMeters";
            this.tsmiAreaMeters.Size = new System.Drawing.Size(152, 22);
            this.tsmiAreaMeters.Tag = "Meters";
            this.tsmiAreaMeters.Text = "米";
            this.tsmiAreaMeters.Click += new System.EventHandler(this.tsmiAreaKilo_Click);
            // 
            // tsmiAreaMiles
            // 
            this.tsmiAreaMiles.Name = "tsmiAreaMiles";
            this.tsmiAreaMiles.Size = new System.Drawing.Size(152, 22);
            this.tsmiAreaMiles.Tag = "Miles";
            this.tsmiAreaMiles.Text = "英里";
            this.tsmiAreaMiles.Visible = false;
            this.tsmiAreaMiles.Click += new System.EventHandler(this.tsmiAreaKilo_Click);
            // 
            // tsmiAreaFeet
            // 
            this.tsmiAreaFeet.Name = "tsmiAreaFeet";
            this.tsmiAreaFeet.Size = new System.Drawing.Size(152, 22);
            this.tsmiAreaFeet.Tag = "Feet";
            this.tsmiAreaFeet.Text = "英尺";
            this.tsmiAreaFeet.Visible = false;
            this.tsmiAreaFeet.Click += new System.EventHandler(this.tsmiAreaKilo_Click);
            // 
            // tsmiAreaYards
            // 
            this.tsmiAreaYards.Name = "tsmiAreaYards";
            this.tsmiAreaYards.Size = new System.Drawing.Size(152, 22);
            this.tsmiAreaYards.Tag = "Yards";
            this.tsmiAreaYards.Text = "码";
            this.tsmiAreaYards.Visible = false;
            this.tsmiAreaYards.Click += new System.EventHandler(this.tsmiAreaKilo_Click);
            // 
            // tsmiAreaHectares
            // 
            this.tsmiAreaHectares.Name = "tsmiAreaHectares";
            this.tsmiAreaHectares.Size = new System.Drawing.Size(152, 22);
            this.tsmiAreaHectares.Tag = "Hectares";
            this.tsmiAreaHectares.Text = "公顷";
            this.tsmiAreaHectares.Click += new System.EventHandler(this.tsmiAreaKilo_Click);
            // 
            // tsmiAreaAcres
            // 
            this.tsmiAreaAcres.Name = "tsmiAreaAcres";
            this.tsmiAreaAcres.Size = new System.Drawing.Size(152, 22);
            this.tsmiAreaAcres.Tag = "Acres";
            this.tsmiAreaAcres.Text = "英亩";
            this.tsmiAreaAcres.Visible = false;
            this.tsmiAreaAcres.Click += new System.EventHandler(this.tsmiAreaKilo_Click);
            // 
            // tsbtnClear
            // 
            this.tsbtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnClear.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnClear.Image")));
            this.tsbtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClear.Name = "tsbtnClear";
            this.tsbtnClear.Size = new System.Drawing.Size(36, 22);
            this.tsbtnClear.Text = "清除";
            this.tsbtnClear.Click += new System.EventHandler(this.tsbtnClear_Click);
            // 
            // txtResultInfo
            // 
            this.txtResultInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResultInfo.Location = new System.Drawing.Point(0, 25);
            this.txtResultInfo.Multiline = true;
            this.txtResultInfo.Name = "txtResultInfo";
            this.txtResultInfo.Size = new System.Drawing.Size(272, 114);
            this.txtResultInfo.TabIndex = 1;
            // 
            // MeasureResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 139);
            this.Controls.Add(this.txtResultInfo);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MeasureResult";
            this.ShowIcon = false;
            this.Text = "测量";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MeasureResult_FormClosing);
            this.Load += new System.EventHandler(this.MeasureResult_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TextBox txtResultInfo;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisKilometers;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisMeters;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisDeci;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisCenti;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisMilli;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisMiles;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisNauticalMiles;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisYards;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisFeet;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisInches;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisDecimalDegrees;
        private System.Windows.Forms.ToolStripButton tsbtnClear;
        private System.Windows.Forms.ToolStripMenuItem tsmiAreaKilo;
        private System.Windows.Forms.ToolStripMenuItem tsmiAreaMeters;
        private System.Windows.Forms.ToolStripMenuItem tsmiAreaMiles;
        private System.Windows.Forms.ToolStripMenuItem tsmiAreaFeet;
        private System.Windows.Forms.ToolStripMenuItem tsmiAreaYards;
        private System.Windows.Forms.ToolStripMenuItem tsmiAreaHectares;
        private System.Windows.Forms.ToolStripMenuItem tsmiAreaAcres;
        public System.Windows.Forms.ToolStripMenuItem tsmiDistanceUnit;
        public System.Windows.Forms.ToolStripMenuItem tsmiAreaUnit;
    }
}