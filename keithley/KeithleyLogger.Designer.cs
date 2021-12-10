namespace keithley
{
    partial class KeithleyLogger
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeithleyLogger));
            this.SetupBtn = new System.Windows.Forms.Button();
            this.hoursNumeric = new System.Windows.Forms.NumericUpDown();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openApp = new System.Windows.Forms.ToolStripMenuItem();
            this.showChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerEnbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeApp = new System.Windows.Forms.ToolStripMenuItem();
            this.createTimerBtn = new System.Windows.Forms.Button();
            this.openSession = new System.Windows.Forms.Button();
            this.sessionOpt = new System.Windows.Forms.GroupBox();
            this.loggerGroup = new System.Windows.Forms.GroupBox();
            this.minTimeVal = new System.Windows.Forms.Label();
            this.minTimeLabl = new System.Windows.Forms.Label();
            this.showChart = new System.Windows.Forms.Button();
            this.secLabel = new System.Windows.Forms.Label();
            this.secondsNumeric = new System.Windows.Forms.NumericUpDown();
            this.minLabel = new System.Windows.Forms.Label();
            this.minutesNumeric = new System.Windows.Forms.NumericUpDown();
            this.hoursLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeFileDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.русскийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.hoursNumeric)).BeginInit();
            this.notifyMenuStrip.SuspendLayout();
            this.sessionOpt.SuspendLayout();
            this.loggerGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.secondsNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minutesNumeric)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SetupBtn
            // 
            resources.ApplyResources(this.SetupBtn, "SetupBtn");
            this.SetupBtn.Name = "SetupBtn";
            this.SetupBtn.UseVisualStyleBackColor = true;
            this.SetupBtn.Click += new System.EventHandler(this.SetupBtn_Click);
            // 
            // hoursNumeric
            // 
            resources.ApplyResources(this.hoursNumeric, "hoursNumeric");
            this.hoursNumeric.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.hoursNumeric.Name = "hoursNumeric";
            this.hoursNumeric.ValueChanged += new System.EventHandler(this.HoursNumeric_ValueChanged);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.notifyMenuStrip;
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // notifyMenuStrip
            // 
            this.notifyMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openApp,
            this.showChartToolStripMenuItem,
            this.timerEnbToolStripMenuItem,
            this.closeApp});
            this.notifyMenuStrip.Name = "notifyMenuStrip";
            resources.ApplyResources(this.notifyMenuStrip, "notifyMenuStrip");
            // 
            // openApp
            // 
            this.openApp.Name = "openApp";
            resources.ApplyResources(this.openApp, "openApp");
            this.openApp.Click += new System.EventHandler(this.OpenApp_Click);
            // 
            // showChartToolStripMenuItem
            // 
            this.showChartToolStripMenuItem.Name = "showChartToolStripMenuItem";
            resources.ApplyResources(this.showChartToolStripMenuItem, "showChartToolStripMenuItem");
            this.showChartToolStripMenuItem.Click += new System.EventHandler(this.ShowChartToolStripMenuItem_Click);
            // 
            // timerEnbToolStripMenuItem
            // 
            this.timerEnbToolStripMenuItem.Name = "timerEnbToolStripMenuItem";
            resources.ApplyResources(this.timerEnbToolStripMenuItem, "timerEnbToolStripMenuItem");
            this.timerEnbToolStripMenuItem.Click += new System.EventHandler(this.timerEnbToolStripMenuItem_Click);
            // 
            // closeApp
            // 
            this.closeApp.Name = "closeApp";
            resources.ApplyResources(this.closeApp, "closeApp");
            this.closeApp.Click += new System.EventHandler(this.CloseApp_Click);
            // 
            // createTimerBtn
            // 
            resources.ApplyResources(this.createTimerBtn, "createTimerBtn");
            this.createTimerBtn.Name = "createTimerBtn";
            this.createTimerBtn.UseVisualStyleBackColor = true;
            this.createTimerBtn.Click += new System.EventHandler(this.createTimerButt_Click);
            // 
            // openSession
            // 
            resources.ApplyResources(this.openSession, "openSession");
            this.openSession.Name = "openSession";
            this.openSession.UseVisualStyleBackColor = true;
            this.openSession.Click += new System.EventHandler(this.openSession_Click);
            // 
            // sessionOpt
            // 
            this.sessionOpt.BackColor = System.Drawing.SystemColors.Control;
            this.sessionOpt.Controls.Add(this.openSession);
            resources.ApplyResources(this.sessionOpt, "sessionOpt");
            this.sessionOpt.Name = "sessionOpt";
            this.sessionOpt.TabStop = false;
            // 
            // loggerGroup
            // 
            this.loggerGroup.Controls.Add(this.minTimeVal);
            this.loggerGroup.Controls.Add(this.minTimeLabl);
            this.loggerGroup.Controls.Add(this.showChart);
            this.loggerGroup.Controls.Add(this.secLabel);
            this.loggerGroup.Controls.Add(this.secondsNumeric);
            this.loggerGroup.Controls.Add(this.minLabel);
            this.loggerGroup.Controls.Add(this.minutesNumeric);
            this.loggerGroup.Controls.Add(this.SetupBtn);
            this.loggerGroup.Controls.Add(this.hoursLabel);
            this.loggerGroup.Controls.Add(this.createTimerBtn);
            this.loggerGroup.Controls.Add(this.hoursNumeric);
            resources.ApplyResources(this.loggerGroup, "loggerGroup");
            this.loggerGroup.Name = "loggerGroup";
            this.loggerGroup.TabStop = false;
            // 
            // minTimeVal
            // 
            resources.ApplyResources(this.minTimeVal, "minTimeVal");
            this.minTimeVal.Name = "minTimeVal";
            // 
            // minTimeLabl
            // 
            resources.ApplyResources(this.minTimeLabl, "minTimeLabl");
            this.minTimeLabl.Name = "minTimeLabl";
            // 
            // showChart
            // 
            resources.ApplyResources(this.showChart, "showChart");
            this.showChart.Name = "showChart";
            this.showChart.UseVisualStyleBackColor = true;
            this.showChart.Click += new System.EventHandler(this.ShowChart_Click);
            // 
            // secLabel
            // 
            resources.ApplyResources(this.secLabel, "secLabel");
            this.secLabel.Name = "secLabel";
            // 
            // secondsNumeric
            // 
            resources.ApplyResources(this.secondsNumeric, "secondsNumeric");
            this.secondsNumeric.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.secondsNumeric.Name = "secondsNumeric";
            this.secondsNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.secondsNumeric.ValueChanged += new System.EventHandler(this.SecondsNumeric_ValueChanged);
            // 
            // minLabel
            // 
            resources.ApplyResources(this.minLabel, "minLabel");
            this.minLabel.Name = "minLabel";
            // 
            // minutesNumeric
            // 
            resources.ApplyResources(this.minutesNumeric, "minutesNumeric");
            this.minutesNumeric.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.minutesNumeric.Name = "minutesNumeric";
            this.minutesNumeric.ValueChanged += new System.EventHandler(this.MinutesNumeric_ValueChanged);
            // 
            // hoursLabel
            // 
            resources.ApplyResources(this.hoursLabel, "hoursLabel");
            this.hoursLabel.Name = "hoursLabel";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openChartToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // openChartToolStripMenuItem
            // 
            this.openChartToolStripMenuItem.Name = "openChartToolStripMenuItem";
            resources.ApplyResources(this.openChartToolStripMenuItem, "openChartToolStripMenuItem");
            this.openChartToolStripMenuItem.Click += new System.EventHandler(this.OpenChartToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeFileDirectoryToolStripMenuItem,
            this.languageToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
            // 
            // changeFileDirectoryToolStripMenuItem
            // 
            this.changeFileDirectoryToolStripMenuItem.Name = "changeFileDirectoryToolStripMenuItem";
            resources.ApplyResources(this.changeFileDirectoryToolStripMenuItem, "changeFileDirectoryToolStripMenuItem");
            this.changeFileDirectoryToolStripMenuItem.Click += new System.EventHandler(this.ChangeFileDirectoryToolStripMenuItem_Click);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.русскийToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            resources.ApplyResources(this.languageToolStripMenuItem, "languageToolStripMenuItem");
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            resources.ApplyResources(this.englishToolStripMenuItem, "englishToolStripMenuItem");
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // русскийToolStripMenuItem
            // 
            this.русскийToolStripMenuItem.Name = "русскийToolStripMenuItem";
            resources.ApplyResources(this.русскийToolStripMenuItem, "русскийToolStripMenuItem");
            this.русскийToolStripMenuItem.Click += new System.EventHandler(this.русскийToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // KeithleyLogger
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.loggerGroup);
            this.Controls.Add(this.sessionOpt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KeithleyLogger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KeithleyLogger_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KeithleyLogger_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.hoursNumeric)).EndInit();
            this.notifyMenuStrip.ResumeLayout(false);
            this.sessionOpt.ResumeLayout(false);
            this.loggerGroup.ResumeLayout(false);
            this.loggerGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.secondsNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minutesNumeric)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button SetupBtn;
        private System.Windows.Forms.NumericUpDown hoursNumeric;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button createTimerBtn;
        private System.Windows.Forms.Button openSession;
        private System.Windows.Forms.ContextMenuStrip notifyMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openApp;
        private System.Windows.Forms.ToolStripMenuItem closeApp;
        private System.Windows.Forms.GroupBox sessionOpt;
        private System.Windows.Forms.GroupBox loggerGroup;
        private System.Windows.Forms.Label secLabel;
        private System.Windows.Forms.NumericUpDown secondsNumeric;
        private System.Windows.Forms.Label minLabel;
        private System.Windows.Forms.NumericUpDown minutesNumeric;
        private System.Windows.Forms.Label hoursLabel;
        private System.Windows.Forms.ToolStripMenuItem timerEnbToolStripMenuItem;
        internal System.Windows.Forms.Button showChart;
        private System.Windows.Forms.ToolStripMenuItem showChartToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label minTimeVal;
        private System.Windows.Forms.Label minTimeLabl;
        private System.Windows.Forms.ToolStripMenuItem openChartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeFileDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem русскийToolStripMenuItem;
    }
}

