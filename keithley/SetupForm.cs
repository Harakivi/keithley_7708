using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using keithley;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using Ivi.Visa;
using NationalInstruments.Visa;

namespace keithley
{
    public partial class SetupForm : Form
    {
        internal List<Channel> channels { get; set; }
        MessageBasedSession session;
        public SetupForm(List<Channel> channel, MessageBasedSession mbSeesion)
        {
            InitializeComponent();
            session = mbSeesion;
            channels = new List<Channel>();
            if(channel != null)
            {
                channels.Clear();
                channels.AddRange(channel);
                saveSetsBtn.Enabled = true;
            }
            refrashLists();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            List<string> listOfChan = new List<string>();
            for (int i = 0; i < avaliableChannelsList.SelectedItems.Count; i++)
            {
                listOfChan.Add(avaliableChannelsList.SelectedItems[i].ToString());
            }
            avaliableChannelsList.SelectedItem = null;
            addBtn.Enabled = false;
            while (listOfChan.Count != 0)
            {
                Channel channel = new Channel(listOfChan[0].ToString());
                using (ChannelSettings chanSet = new ChannelSettings(channel, channels, session))
                {
                    chanSet.ShowDialog(this);
                    if (chanSet.DialogResult == DialogResult.OK)
                    {
                        avaliableChannelsList.Items.Remove(chanSet.channel.portAddr);
                        if (chanSet.channel.Pair != null)
                        {
                            avaliableChannelsList.Items.Remove(chanSet.channel.Pair);
                        }
                        channels.Add(chanSet.channel);
                        refrashLists();
                        
                    }
                    else if (chanSet.DialogResult == DialogResult.Cancel)
                    {
                        addBtn.Enabled = true;
                    }
                }
                listOfChan.RemoveAt(0);
            }
            if(channels.Count != 0)
            {
                saveSetsBtn.Enabled = true;
            }
            selectedChannelsList.Select();
        }

        private void AvaliableChannelsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (avaliableChannelsList.SelectedItems.Count != 0)
            {
                addBtn.Enabled = true;
            }
            else { addBtn.Enabled = false; }
            //addBtnEnb();
        }

        //private void addBtnEnb()
        //{
        //   if(avaliableChannelsList.SelectedItems != null)
        //    {
        //        addBtn.Enabled = true;
        //    }
        //}

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (selectedChannelsList.SelectedItems != null)
            {
                addBtn.Enabled = false;
                List<Channel> listOfChan = new List<Channel>();
                for (int i = 0; i < selectedChannelsList.SelectedItems.Count; i++)
                {
                    listOfChan.Add((Channel)selectedChannelsList.SelectedItems[i]);
                }
                while (listOfChan.Count != 0)
                {
                    channels.Remove(listOfChan[0]);
                    listOfChan.RemoveAt(0);
                }
                refrashLists();
            }
            if (channels.Count == 0)
            {
                saveSetsBtn.Enabled = false;
            }
        }

        private void SelectedChannelsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedChannelsList.SelectedItem != null)
            {
                deleteBtn.Enabled = true;
                editBtn.Enabled = true;
            }
            else { deleteBtn.Enabled = false; editBtn.Enabled = false; }
            if (selectedChannelsList.SelectedItems != null && selectedChannelsList.SelectedItems.Count == 1)
            {
                statusStripFill();
            }
            else { statusStrip.Items.Clear(); }
        }

        private void SetupForm_Click(object sender, EventArgs e)
        {
            avaliableChannelsList.SelectedItem = null;
            selectedChannelsList.SelectedItem = null;
            addBtn.Enabled = false;
            editBtn.Enabled = false;
            deleteBtn.Enabled = false;
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            editBtn.Enabled = false;
            List<Channel> listOfChan = new List<Channel>();
            for (int i = 0; i < selectedChannelsList.SelectedItems.Count; i++)
            {
                listOfChan.Add((Channel)selectedChannelsList.SelectedItems[i]);
            }
            while (listOfChan.Count != 0)
            {
                using (ChannelSettings chanSet = new ChannelSettings(listOfChan[0], channels, session))
                {
                    chanSet.ShowDialog(this);
                    if (chanSet.DialogResult == DialogResult.OK)
                    {
                        avaliableChannelsList.Items.Remove(listOfChan[0]);
                        refrashLists();
                    }
                    else if (chanSet.DialogResult == DialogResult.Cancel)
                    {
                        editBtn.Enabled = true;
                    }
                }
                listOfChan.RemoveAt(0);
            }
            refrashLists();
        }
        private void refrashLists()
        {
            avaliableChannelsList.Items.Clear();
            selectedChannelsList.DataSource = null;
            selectedChannelsList.DataSource = channels;
            selectedChannelsList.DisplayMember = "Name";
            for(int i = 1; i < 41; i++)
            {
                bool flag = true;
                for(int j = 0; j < channels.Count; j++)
                {
                    if(Convert.ToInt32(channels[j].portAddr) == i + 100 || Convert.ToInt32(channels[j].Pair) == i + 100)
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    avaliableChannelsList.Items.Add(i + 100);
                }
            }
            if (channels.Count != 0)
            {
                saveSetsBtn.Enabled = true;
            }
        }
        private void statusStripFill()
        {
            statusStrip.Items.Clear();
            string separator = " | ";
            string info = "";
            string chanInfo = $"Chan: {channels[selectedChannelsList.SelectedIndex].portAddr.Remove(0, 1)}";
            info += chanInfo + separator;
            string modeInfo = $"Mode: {channels[selectedChannelsList.SelectedIndex].Function}";
            info += modeInfo + separator;
            string NPLCInfo;
            if (channels[selectedChannelsList.SelectedIndex].bandWidth == "300" || channels[selectedChannelsList.SelectedIndex].Function != "ACV")
            {
                NPLCInfo = $"NPLC: {channels[selectedChannelsList.SelectedIndex].NPLC}";
                info += NPLCInfo + separator;
            }
            string FILTInfo = $"FILT: {channels[selectedChannelsList.SelectedIndex].FILT}";
            info += FILTInfo + separator;
            string BWInfo;
            if (channels[selectedChannelsList.SelectedIndex].Function == "ACV" || channels[selectedChannelsList.SelectedIndex].Function == "ACI") 
            {
                BWInfo = $"BW: {channels[selectedChannelsList.SelectedIndex].bandWidth}";
                info += BWInfo + separator;
            }
            string pairInfo;
            if (channels[selectedChannelsList.SelectedIndex].Function == "Ω4")
            {
                pairInfo = $"Pair: {channels[selectedChannelsList.SelectedIndex].Pair.Remove(0, 1)}";
                info += pairInfo + separator;
            }
            string shuntInfo;
            if (channels[selectedChannelsList.SelectedIndex].Function == "DCI" || channels[selectedChannelsList.SelectedIndex].Function == "ACI")
            {
                shuntInfo = $"Shunt: {channels[selectedChannelsList.SelectedIndex].shuntRes}Ω";
                info += shuntInfo + separator;
            }
            ToolStripStatusLabel toolStripItem = new ToolStripStatusLabel(info);
            statusStrip.Items.Add(toolStripItem);
        }

        private void AvaliableChannelsList_Enter(object sender, EventArgs e)
        {
            selectedChannelsList.SelectedItem = null;
        }

        private void SelectedChannelsList_Enter(object sender, EventArgs e)
        {
            avaliableChannelsList.SelectedItem = null;
        }

        private void SaveSetsBtn_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            // получаем поток, куда будем записывать сериализованный объект
            using (SaveFileDialog saveFile = new SaveFileDialog())
            {
                saveFile.Filter = adRes.files + "(*.dat)|*.dat";
                saveFile.DefaultExt = "dat";
                saveFile.ShowDialog();
                if (saveFile.FileName != ""){
                    using (FileStream fs = new FileStream(saveFile.FileName, FileMode.OpenOrCreate))
                    {
                        try
                        {
                            formatter.Serialize(fs, channels);
                            MessageBox.Show(adRes.saveComplete);
                        }
                        catch (SerializationException exp)
                        {
                            MessageBox.Show(adRes.failSerialize + exp.Message);
                        }
                    }
                }
            }
        }

        private void LoadSetsBtn_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.Filter = adRes.files + "(*.dat)|*.dat";
                openFile.DefaultExt = "dat";
                openFile.ShowDialog();
                if (openFile.FileName != "")
                {
                    using (FileStream fs = new FileStream(openFile.FileName, FileMode.OpenOrCreate))
                    {
                        try
                        {
                            channels = (List<Channel>)formatter.Deserialize(fs);
                            for(int i = 0; i < channels.Count; i++)
                            {
                                channels[i].channelSeries = new System.Windows.Forms.DataVisualization.Charting.Series(channels[i].Name);
                            }
                            MessageBox.Show(adRes.loadComplete);
                        }
                        catch (SerializationException exp)
                        {
                            MessageBox.Show(adRes.failDeserialize + exp.Message);
                        }
                    }
                }
            }
            refrashLists();
        }
    }
}
