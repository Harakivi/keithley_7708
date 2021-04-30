using System.Windows.Forms;
using NationalInstruments.Visa;
using System;

namespace keithley
{
    /// <summary>
    /// Summary description for SelectResource.
    /// </summary>
    public class SelectResource : System.Windows.Forms.Form
    {
        private System.Windows.Forms.ListBox availableResourcesListBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.TextBox visaResourceNameTextBox;
        private System.Windows.Forms.Label AvailableResourcesLabel;
        private System.Windows.Forms.Label ResourceStringLabel;
        public MessageBasedSession mbSession;
        private Button refrashListBtn;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public SelectResource()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectResource));
            this.availableResourcesListBox = new System.Windows.Forms.ListBox();
            this.okButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.visaResourceNameTextBox = new System.Windows.Forms.TextBox();
            this.AvailableResourcesLabel = new System.Windows.Forms.Label();
            this.ResourceStringLabel = new System.Windows.Forms.Label();
            this.refrashListBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // availableResourcesListBox
            // 
            resources.ApplyResources(this.availableResourcesListBox, "availableResourcesListBox");
            this.availableResourcesListBox.Name = "availableResourcesListBox";
            this.availableResourcesListBox.SelectedIndexChanged += new System.EventHandler(this.availableResourcesListBox_SelectedIndexChanged);
            this.availableResourcesListBox.DoubleClick += new System.EventHandler(this.availableResourcesListBox_DoubleClick);
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.Name = "okButton";
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Name = "closeButton";
            // 
            // visaResourceNameTextBox
            // 
            resources.ApplyResources(this.visaResourceNameTextBox, "visaResourceNameTextBox");
            this.visaResourceNameTextBox.Name = "visaResourceNameTextBox";
            // 
            // AvailableResourcesLabel
            // 
            resources.ApplyResources(this.AvailableResourcesLabel, "AvailableResourcesLabel");
            this.AvailableResourcesLabel.Name = "AvailableResourcesLabel";
            // 
            // ResourceStringLabel
            // 
            resources.ApplyResources(this.ResourceStringLabel, "ResourceStringLabel");
            this.ResourceStringLabel.Name = "ResourceStringLabel";
            // 
            // refrashListBtn
            // 
            resources.ApplyResources(this.refrashListBtn, "refrashListBtn");
            this.refrashListBtn.Name = "refrashListBtn";
            this.refrashListBtn.UseVisualStyleBackColor = true;
            this.refrashListBtn.Click += new System.EventHandler(this.RefrashListBtn_Click);
            // 
            // SelectResource
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.closeButton;
            this.Controls.Add(this.refrashListBtn);
            this.Controls.Add(this.ResourceStringLabel);
            this.Controls.Add(this.AvailableResourcesLabel);
            this.Controls.Add(this.visaResourceNameTextBox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.availableResourcesListBox);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectResource";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private void OnLoad(object sender, System.EventArgs e)
        {
            // This example uses an instance of the NationalInstruments.Visa.ResourceManager class to find resources on the system.
            // Alternatively, static methods provided by the Ivi.Visa.ResourceManager class may be used when an application
            // requires additional VISA .NET implementations.
            refrashList();
        }
        private void refrashList()
        {
            var rm = new ResourceManager();
            var resources = rm.Find("GPIB?*");
            availableResourcesListBox.Items.Clear();
            foreach (string s in resources)
            {
                availableResourcesListBox.Items.Add(s);
            }
        }
        private void availableResourcesListBox_DoubleClick(object sender, System.EventArgs e)
        {
            //if((string)availableResourcesListBox.SelectedItem != null)
            //{
            //    string selectedString = (string)availableResourcesListBox.SelectedItem;
            //    ResourceName = selectedString;
            //    this.DialogResult = DialogResult.OK;
            //    this.Close();
            //}
            SelRes();
        }

        private void SelRes()
        {
            if ((string)availableResourcesListBox.SelectedItem != null)
            {
                string selectedString = (string)availableResourcesListBox.SelectedItem;
                ResourceName = selectedString;
                using (var rmSession = new ResourceManager())
                {
                    try
                    {
                        mbSession = (MessageBasedSession)rmSession.Open(ResourceName);
                        mbSession.RawIO.Write("*OPT?");
                        string cardInSlot = mbSession.RawIO.ReadString();
                        //if (cardInSlot.Contains("7708"))
                        //{
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        ////}
                        //else
                        //{
                        //    MessageBox.Show(adRes.brd7708notfound);
                        //}
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }

            }
        }

        private void availableResourcesListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string selectedString = (string)availableResourcesListBox.SelectedItem;
            ResourceName = selectedString;
        }

        public string ResourceName
        {
            get
            {
                return visaResourceNameTextBox.Text;
            }
            set
            {
                visaResourceNameTextBox.Text = value;
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            SelRes();
        }

        private void RefrashListBtn_Click(object sender, EventArgs e)
        {
            refrashList();
        }
    }
}
