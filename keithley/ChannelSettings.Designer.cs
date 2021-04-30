namespace keithley
{
    partial class ChannelSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChannelSettings));
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.modeComboBox = new System.Windows.Forms.ComboBox();
            this.modeLabel = new System.Windows.Forms.Label();
            this.okBtn = new System.Windows.Forms.Button();
            this.cnlBtn = new System.Windows.Forms.Button();
            this.pairLabel = new System.Windows.Forms.Label();
            this.avaliablePairList = new System.Windows.Forms.ComboBox();
            this.bwLabel = new System.Windows.Forms.Label();
            this.NPLCVal = new System.Windows.Forms.NumericUpDown();
            this.NPLCLABEL = new System.Windows.Forms.Label();
            this.filterComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bwcomboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rangeComboBox = new System.Windows.Forms.ComboBox();
            this.rangeLabelVal = new System.Windows.Forms.Label();
            this.bwLabelVal = new System.Windows.Forms.Label();
            this.shuntLabel = new System.Windows.Forms.Label();
            this.shuntVal = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.NPLCVal)).BeginInit();
            this.SuspendLayout();
            // 
            // nameTextBox
            // 
            resources.ApplyResources(this.nameTextBox, "nameTextBox");
            this.nameTextBox.Name = "nameTextBox";
            // 
            // nameLabel
            // 
            resources.ApplyResources(this.nameLabel, "nameLabel");
            this.nameLabel.Name = "nameLabel";
            // 
            // modeComboBox
            // 
            this.modeComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.modeComboBox, "modeComboBox");
            this.modeComboBox.Name = "modeComboBox";
            // 
            // modeLabel
            // 
            resources.ApplyResources(this.modeLabel, "modeLabel");
            this.modeLabel.Name = "modeLabel";
            // 
            // okBtn
            // 
            resources.ApplyResources(this.okBtn, "okBtn");
            this.okBtn.Name = "okBtn";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // cnlBtn
            // 
            this.cnlBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cnlBtn, "cnlBtn");
            this.cnlBtn.Name = "cnlBtn";
            this.cnlBtn.UseVisualStyleBackColor = true;
            // 
            // pairLabel
            // 
            resources.ApplyResources(this.pairLabel, "pairLabel");
            this.pairLabel.Name = "pairLabel";
            // 
            // avaliablePairList
            // 
            this.avaliablePairList.FormattingEnabled = true;
            resources.ApplyResources(this.avaliablePairList, "avaliablePairList");
            this.avaliablePairList.Name = "avaliablePairList";
            // 
            // bwLabel
            // 
            resources.ApplyResources(this.bwLabel, "bwLabel");
            this.bwLabel.Name = "bwLabel";
            // 
            // NPLCVal
            // 
            this.NPLCVal.DecimalPlaces = 2;
            this.NPLCVal.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            resources.ApplyResources(this.NPLCVal, "NPLCVal");
            this.NPLCVal.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.NPLCVal.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.NPLCVal.Name = "NPLCVal";
            this.NPLCVal.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // NPLCLABEL
            // 
            resources.ApplyResources(this.NPLCLABEL, "NPLCLABEL");
            this.NPLCLABEL.Name = "NPLCLABEL";
            // 
            // filterComboBox
            // 
            this.filterComboBox.FormattingEnabled = true;
            this.filterComboBox.Items.AddRange(new object[] {
            resources.GetString("filterComboBox.Items"),
            resources.GetString("filterComboBox.Items1")});
            resources.ApplyResources(this.filterComboBox, "filterComboBox");
            this.filterComboBox.Name = "filterComboBox";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // bwcomboBox
            // 
            this.bwcomboBox.AutoCompleteCustomSource.AddRange(new string[] {
            resources.GetString("bwcomboBox.AutoCompleteCustomSource"),
            resources.GetString("bwcomboBox.AutoCompleteCustomSource1"),
            resources.GetString("bwcomboBox.AutoCompleteCustomSource2")});
            this.bwcomboBox.FormattingEnabled = true;
            this.bwcomboBox.Items.AddRange(new object[] {
            resources.GetString("bwcomboBox.Items"),
            resources.GetString("bwcomboBox.Items1"),
            resources.GetString("bwcomboBox.Items2")});
            resources.ApplyResources(this.bwcomboBox, "bwcomboBox");
            this.bwcomboBox.Name = "bwcomboBox";
            this.bwcomboBox.SelectedIndexChanged += new System.EventHandler(this.BwcomboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // rangeComboBox
            // 
            this.rangeComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.rangeComboBox, "rangeComboBox");
            this.rangeComboBox.Name = "rangeComboBox";
            // 
            // rangeLabelVal
            // 
            resources.ApplyResources(this.rangeLabelVal, "rangeLabelVal");
            this.rangeLabelVal.Name = "rangeLabelVal";
            // 
            // bwLabelVal
            // 
            resources.ApplyResources(this.bwLabelVal, "bwLabelVal");
            this.bwLabelVal.Name = "bwLabelVal";
            // 
            // shuntLabel
            // 
            resources.ApplyResources(this.shuntLabel, "shuntLabel");
            this.shuntLabel.Name = "shuntLabel";
            // 
            // shuntVal
            // 
            resources.ApplyResources(this.shuntVal, "shuntVal");
            this.shuntVal.Name = "shuntVal";
            this.shuntVal.TextChanged += new System.EventHandler(this.shuntVal_TextChanged);
            // 
            // ChannelSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cnlBtn;
            this.Controls.Add(this.shuntVal);
            this.Controls.Add(this.shuntLabel);
            this.Controls.Add(this.bwLabelVal);
            this.Controls.Add(this.rangeLabelVal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rangeComboBox);
            this.Controls.Add(this.bwcomboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filterComboBox);
            this.Controls.Add(this.NPLCLABEL);
            this.Controls.Add(this.NPLCVal);
            this.Controls.Add(this.bwLabel);
            this.Controls.Add(this.avaliablePairList);
            this.Controls.Add(this.pairLabel);
            this.Controls.Add(this.cnlBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.modeLabel);
            this.Controls.Add(this.modeComboBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.nameTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChannelSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.NPLCVal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.ComboBox modeComboBox;
        private System.Windows.Forms.Label modeLabel;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cnlBtn;
        private System.Windows.Forms.Label pairLabel;
        private System.Windows.Forms.ComboBox avaliablePairList;
        private System.Windows.Forms.Label bwLabel;
        private System.Windows.Forms.NumericUpDown NPLCVal;
        private System.Windows.Forms.Label NPLCLABEL;
        private System.Windows.Forms.ComboBox filterComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox bwcomboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox rangeComboBox;
        private System.Windows.Forms.Label rangeLabelVal;
        private System.Windows.Forms.Label bwLabelVal;
        private System.Windows.Forms.Label shuntLabel;
        private System.Windows.Forms.TextBox shuntVal;
    }
}