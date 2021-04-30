namespace keithley
{
    partial class SetupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupForm));
            this.cancelBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.addBtn = new System.Windows.Forms.Button();
            this.avaliableChannelsList = new System.Windows.Forms.ListBox();
            this.selectedChannelsList = new System.Windows.Forms.ListBox();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.editBtn = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.loadSetsBtn = new System.Windows.Forms.Button();
            this.saveSetsBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cancelBtn
            // 
            resources.ApplyResources(this.cancelBtn, "cancelBtn");
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // saveBtn
            // 
            resources.ApplyResources(this.saveBtn, "saveBtn");
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // addBtn
            // 
            resources.ApplyResources(this.addBtn, "addBtn");
            this.addBtn.Name = "addBtn";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // avaliableChannelsList
            // 
            resources.ApplyResources(this.avaliableChannelsList, "avaliableChannelsList");
            this.avaliableChannelsList.FormattingEnabled = true;
            this.avaliableChannelsList.Name = "avaliableChannelsList";
            this.avaliableChannelsList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.avaliableChannelsList.SelectedIndexChanged += new System.EventHandler(this.AvaliableChannelsList_SelectedIndexChanged);
            this.avaliableChannelsList.Enter += new System.EventHandler(this.AvaliableChannelsList_Enter);
            // 
            // selectedChannelsList
            // 
            resources.ApplyResources(this.selectedChannelsList, "selectedChannelsList");
            this.selectedChannelsList.FormattingEnabled = true;
            this.selectedChannelsList.Name = "selectedChannelsList";
            this.selectedChannelsList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.selectedChannelsList.SelectedIndexChanged += new System.EventHandler(this.SelectedChannelsList_SelectedIndexChanged);
            this.selectedChannelsList.Enter += new System.EventHandler(this.SelectedChannelsList_Enter);
            // 
            // deleteBtn
            // 
            resources.ApplyResources(this.deleteBtn, "deleteBtn");
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // editBtn
            // 
            resources.ApplyResources(this.editBtn, "editBtn");
            this.editBtn.Name = "editBtn";
            this.editBtn.UseVisualStyleBackColor = true;
            this.editBtn.Click += new System.EventHandler(this.EditBtn_Click);
            // 
            // statusStrip
            // 
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            // 
            // loadSetsBtn
            // 
            resources.ApplyResources(this.loadSetsBtn, "loadSetsBtn");
            this.loadSetsBtn.Name = "loadSetsBtn";
            this.loadSetsBtn.UseVisualStyleBackColor = true;
            this.loadSetsBtn.Click += new System.EventHandler(this.LoadSetsBtn_Click);
            // 
            // saveSetsBtn
            // 
            resources.ApplyResources(this.saveSetsBtn, "saveSetsBtn");
            this.saveSetsBtn.Name = "saveSetsBtn";
            this.saveSetsBtn.UseVisualStyleBackColor = true;
            this.saveSetsBtn.Click += new System.EventHandler(this.SaveSetsBtn_Click);
            // 
            // SetupForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.Controls.Add(this.saveSetsBtn);
            this.Controls.Add(this.loadSetsBtn);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.editBtn);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.selectedChannelsList);
            this.Controls.Add(this.avaliableChannelsList);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.cancelBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Click += new System.EventHandler(this.SetupForm_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.ListBox selectedChannelsList;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.Button editBtn;
        internal System.Windows.Forms.ListBox avaliableChannelsList;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Button loadSetsBtn;
        private System.Windows.Forms.Button saveSetsBtn;
    }
}