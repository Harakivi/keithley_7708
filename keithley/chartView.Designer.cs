namespace keithley
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    partial class chartView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(chartView));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.hideBtn = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.zoomBox = new System.Windows.Forms.GroupBox();
            this.zoomXChkBx = new System.Windows.Forms.CheckBox();
            this.zoomYChkBx = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.zoomBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            resources.ApplyResources(this.chart1, "chart1");
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Name = "chart1";
            this.chart1.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.Chart1_AxisViewChanged);
            // 
            // hideBtn
            // 
            resources.ApplyResources(this.hideBtn, "hideBtn");
            this.hideBtn.Name = "hideBtn";
            this.hideBtn.UseVisualStyleBackColor = true;
            this.hideBtn.Click += new System.EventHandler(this.hideBtn_Click);
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // zoomBox
            // 
            resources.ApplyResources(this.zoomBox, "zoomBox");
            this.zoomBox.Controls.Add(this.zoomXChkBx);
            this.zoomBox.Controls.Add(this.zoomYChkBx);
            this.zoomBox.Name = "zoomBox";
            this.zoomBox.TabStop = false;
            // 
            // zoomXChkBx
            // 
            resources.ApplyResources(this.zoomXChkBx, "zoomXChkBx");
            this.zoomXChkBx.Name = "zoomXChkBx";
            this.zoomXChkBx.UseVisualStyleBackColor = true;
            this.zoomXChkBx.CheckedChanged += new System.EventHandler(this.ZoomXChkBx_CheckedChanged);
            // 
            // zoomYChkBx
            // 
            resources.ApplyResources(this.zoomYChkBx, "zoomYChkBx");
            this.zoomYChkBx.Name = "zoomYChkBx";
            this.zoomYChkBx.UseVisualStyleBackColor = true;
            this.zoomYChkBx.CheckedChanged += new System.EventHandler(this.ZoomYChkBx_CheckedChanged);
            // 
            // chartView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.zoomBox);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.hideBtn);
            this.Controls.Add(this.chart1);
            this.MinimizeBox = false;
            this.Name = "chartView";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Chart_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.zoomBox.ResumeLayout(false);
            this.zoomBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button hideBtn;
        protected System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private GroupBox zoomBox;
        private CheckBox zoomXChkBx;
        private CheckBox zoomYChkBx;
    }
}