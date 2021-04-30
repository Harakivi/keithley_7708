using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace keithley
{
    public partial class saveFileWindow : Form
    {
        public saveFileWindow(string streamerDir)
        {
            InitializeComponent();
            streamerDirTextBox.Text = streamerDir;
        }

        private void ExploreBtn_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFile = new SaveFileDialog())
            {
                saveFile.Filter = adRes.files + "(*.txt)|*.txt";
                saveFile.DefaultExt = "txt";
                saveFile.InitialDirectory = streamerDirTextBox.Text;
                saveFile.ShowDialog();
                if (saveFile.FileName != "")
                {
                    streamerDirTextBox.Text = saveFile.FileName;
                }
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(streamerDirTextBox.Text != "" && streamerDirTextBox.Text.Contains(".txt"))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else if(streamerDirTextBox.Text == "")
            {
                MessageBox.Show(adRes.needFile);
            }
            else if(streamerDirTextBox.Text != "" && !streamerDirTextBox.Text.Contains(".txt"))
            {
                MessageBox.Show(adRes.needExt);
            }

        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
