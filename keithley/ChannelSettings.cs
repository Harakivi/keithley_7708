using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ivi.Visa;
using NationalInstruments.Visa;

namespace keithley
{
    public partial class ChannelSettings : Form
    {
        public Channel channel { get; }
        private List<Channel> channels { get; set; }
        MessageBasedSession session;

        //public ChannelSettings(string addr, List<Channel> Channels)
        //{
        //    InitializeComponent();
        //    channel = new Channel(addr);
        //    channels = Channels;
        //    nameTextBox.Text = channel.Name.Replace("'", "");
        //    this.Text = $"Channel {channel.portAddr} Settings";
        //    modeComboBox.SelectedIndex = 0;
        //    filterComboBox.SelectedIndex = 0;
        //    bwcomboBox.SelectedIndex = 0;
        //}
        /// <summary> 
        /// Конструктор
        /// </summary> 
        public ChannelSettings(Channel Channel, List<Channel> Channels, MessageBasedSession mbSession)
        {
            InitializeComponent();
            channels = Channels;
            channel = Channel;
            session = mbSession;
            modeComboBoxSetup();
            if (setupParameters()) { }
            else
            {
                MessageBox.Show(adRes.Error);
                Close();
            }
            this.Text = adRes.chan + " " + Channel.portAddr + " " + adRes.sets;
        }

        private void ModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            modeSet();
        }

        private void modeSet()
        {
            rangeComboBoxSetup();
            rangeComboBox.SelectedItem = "AUTO ON";
            switch ((string)modeComboBox.SelectedItem)
            {
                case "ACV":
                    bwLabel.Visible = true;
                    bwcomboBox.Visible = true;
                    bwLabelVal.Visible = true;
                    pairLabel.Visible = false;
                    avaliablePairList.Visible = false;
                    shuntVal.Visible = false;
                    shuntLabel.Visible = false;
                    Size = new Size(232, 325);
                    okBtn.Location = new Point(118, 253);
                    cnlBtn.Location = new Point(27, 253);
                    if (bwcomboBox.SelectedItem.ToString() != "300")
                    {
                        NPLCLABEL.Enabled = false;
                        NPLCVal.Enabled = false;
                    }
                    break;
                case "ACI":
                    Size = new Size(232, 370);
                    shuntLabel.Location = new Point(52, 250);
                    shuntVal.Location = new Point(46, 266);
                    okBtn.Location = new Point(118, 296);
                    cnlBtn.Location = new Point(27, 296);
                    bwLabel.Visible = true;
                    bwcomboBox.Visible = true;
                    bwLabelVal.Visible = true;
                    pairLabel.Visible = false;
                    avaliablePairList.Visible = false;
                    shuntLabel.Visible = true;
                    shuntVal.Visible = true;
                    if (bwcomboBox.SelectedItem.ToString() != "300")
                    {
                        NPLCLABEL.Enabled = false;
                        NPLCVal.Enabled = false;
                    }
                    break;
                case "DCI":
                    Size = new Size(232, 325);
                    shuntLabel.Location = new Point(52, 210);
                    shuntVal.Location = new Point(46, 227);
                    okBtn.Location = new Point(118, 253);
                    cnlBtn.Location = new Point(27, 253);
                    pairLabel.Visible = false;
                    avaliablePairList.Visible = false;
                    bwLabel.Visible = false;
                    bwcomboBox.Visible = false;
                    bwLabelVal.Visible = false;
                    NPLCLABEL.Enabled = true;
                    NPLCVal.Enabled = true;
                    shuntLabel.Visible = true;
                    shuntVal.Visible = true;
                    break;
                case "Ω4":
                    Size = new Size(232, 325);
                    okBtn.Location = new Point(118, 253);
                    cnlBtn.Location = new Point(27, 253);
                    pairLabel.Visible = true;
                    avaliablePairList.Visible = true;
                    bwLabel.Visible = false;
                    bwcomboBox.Visible = false;
                    bwLabelVal.Visible = false;
                    pairComboBoxSetup();
                    break;
                //case "PERIOD":
                //    thrVoltLabel.Visible = true;
                //    thrVoltNumUD.Visible = true;
                //    pairLabel.Visible = false;
                //    avaliableChanList.Visible = false;
                //    break;
                //case "FREQ":
                //    thrVoltLabel.Visible = true;
                //    thrVoltNumUD.Visible = true;
                //    pairLabel.Visible = false;
                //    avaliableChanList.Visible = false;
                //    break;
                default:
                    pairLabel.Visible = false;
                    avaliablePairList.Visible = false;
                    bwLabel.Visible = false;
                    bwcomboBox.Visible = false;
                    bwLabelVal.Visible = false;
                    NPLCLABEL.Enabled = true;
                    NPLCVal.Enabled = true;
                    shuntLabel.Visible = false;
                    shuntVal.Visible = false;
                    shuntVal.Text = "0";
                    Size = new Size(232, 301);
                    shuntLabel.Location = new Point(52, 210);
                    shuntVal.Location = new Point(46, 227);
                    okBtn.Location = new Point(118, 224);
                    cnlBtn.Location = new Point(27, 224);
                    break;
            }
        }
        private void OkBtn_Click(object sender, EventArgs e)
        {
            if (modeComboBox.SelectedItem == null)
            {
                MessageBox.Show(adRes.needMode);
            }
            else if("Ω4" == (string)modeComboBox.SelectedItem && null == avaliablePairList.SelectedItem)
            {
                MessageBox.Show(adRes.needPair);
            }
            else if (("ACI" == (string)modeComboBox.SelectedItem || "DCI" == (string)modeComboBox.SelectedItem) && "0" == shuntVal.Text)
            {
                MessageBox.Show(adRes.shuntMust);
            }
            else
            {
                channel.Name = $"'{nameTextBox.Text}'";
                channel.Function = modeComboBox.SelectedItem.ToString();
                if(rangeComboBox.SelectedItem.ToString() == "AUTO ON")
                {
                    channel.range = ":" + rangeComboBox.SelectedItem.ToString();
                }
                else { channel.range = rangeComboBox.SelectedItem.ToString(); }
                channel.NPLC = NPLCVal.Value.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                channel.FILT = filterComboBox.SelectedItem.ToString();
                channel.bandWidth = bwcomboBox.SelectedItem.ToString();
                channel.shuntRes = shuntVal.Text;
                switch ((string)modeComboBox.SelectedItem)
                {
                    case "Ω4":
                        channel.Pair = (string)avaliablePairList.SelectedItem;
                        channel.thrVoltage = null;
                        break;
                    //case "PERIOD":
                    //    Channel.thrVoltage = Convert.ToString(thrVoltNumUD.Value);
                    //    Channel.Pair = null;
                    //    break;
                    //case "FREQ":
                    //    Channel.thrVoltage = Convert.ToString(thrVoltNumUD.Value);
                    //    Channel.Pair = null;
                    //    break;
                    default:
                        channel.Pair = null;
                        channel.thrVoltage = null;
                        break;
                }
                //channel.readTime(session, channel);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        /// <summary> 
        /// Выставление значений окна на основании полученного класса.
        /// </summary> 
        private bool setupParameters()
        {
            nameTextBox.Text = channel.Name.Replace("'", "");
            modeComboBox.SelectedItem = channel.Function;
            rangeComboBoxSetup();
            rangeComboBox.SelectedItem = channel.range.Replace(":","");
            if (channel.Pair != null)
            {
                avaliablePairList.SelectedItem = channel.Pair;
                avaliablePairList.Visible = true;
                pairLabel.Visible = true;
            }
            else
            {
                avaliablePairList.Visible = false;
                pairLabel.Visible = false;
            }
            NPLCVal.Value = Convert.ToDecimal(channel.NPLC, System.Globalization.CultureInfo.GetCultureInfo("en-US"));
            filterComboBox.SelectedItem = channel.FILT;
            bwcomboBox.SelectedItem = channel.bandWidth;
            shuntVal.Text = channel.shuntRes;
            if (bwcomboBox.SelectedItem.ToString() != "300")
            {
                NPLCVal.Value = 1;
                NPLCLABEL.Enabled = false;
                NPLCVal.Enabled = false;
            }
            modeSet();
            this.modeComboBox.SelectedIndexChanged += new System.EventHandler(this.ModeComboBox_SelectedIndexChanged);
            return true;
        }

        private void BwcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bwcomboBox.SelectedItem.ToString() == "300")
            {
                NPLCLABEL.Enabled = true;
                NPLCVal.Enabled = true;
            }
            else {
                NPLCLABEL.Enabled = false;
                NPLCVal.Enabled = false;
            }
        }
        /// <summary> 
        /// Вызывается для заполнения rangeComboBoxSetup необходимыми значениями
        /// </summary> 
        private void rangeComboBoxSetup()
        {
            rangeComboBox.Items.Clear();
            switch (modeComboBox.SelectedItem.ToString())
            {
                case "DCV":
                    string[] dcranges = {"AUTO ON", " 0.1", " 1", " 10", " 100", " 1000" };
                    rangeComboBox.Items.AddRange(dcranges);
                    rangeLabelVal.Text = adRes.volt;
                    break;
                case "ACV":
                    string[] acranges = { "AUTO ON", " 0.1", " 1", " 10", " 100", " 750" };
                    rangeComboBox.Items.AddRange(acranges);
                    rangeLabelVal.Text = adRes.volt;
                    break;
                case "Ω2":
                    string[] resranges = { "AUTO ON", " 100", " 1k", " 10k", " 100k", " 1M", " 10M", " 100M" };
                    rangeComboBox.Items.AddRange(resranges);
                    rangeLabelVal.Text = adRes.ohm;
                    break;
                case "Ω4":
                    string[] fresranges = { "AUTO ON", " 100", " 1k", " 10k", " 100k", " 1M", " 10M", " 100M" };
                    rangeComboBox.Items.AddRange(fresranges);
                    rangeLabelVal.Text = adRes.ohm;
                    break;
                case "DCI":
                    string[] dciranges = { "AUTO ON", " 0.1", " 1", " 10", " 100", " 1000" };
                    rangeComboBox.Items.AddRange(dciranges);
                    rangeLabelVal.Text = adRes.volt;
                    break;
                case "ACI":
                    string[] aciranges = { "AUTO ON", " 0.1", " 1", " 10", " 100", " 750" };
                    rangeComboBox.Items.AddRange(aciranges);
                    rangeLabelVal.Text = adRes.volt;
                    break;
            }
        }
        private void pairComboBoxSetup()
        {
            avaliablePairList.Items.Clear();
            for(int i = 121; i <= 140; i++)
            {
                bool flagContinue = false;
                for(int k = 0; k < channels.Count; k++)
                {
                    if (channels[k].portAddr == i.ToString() || channels[k].Pair == i.ToString() && channel.Pair != i.ToString())
                    {
                        flagContinue = true;
                        break;
                    }
                    else if(channels[k].portAddr == i.ToString() || channels[k].Pair == i.ToString() && channel.Pair != null)
                    {

                    }
                }
                if (!flagContinue)
                {
                    avaliablePairList.Items.Add(i.ToString());
                }
            }
            if(channel.Pair != null)
            {
                avaliablePairList.SelectedItem = channel.Pair;
            }
        }
       private void modeComboBoxSetup()
        {
            if(Convert.ToInt16(channel.portAddr) <= 120 && Convert.ToInt16(channel.portAddr) >= 101)
            {
                string[] inputChan = { "DCV", "ACV", "DCI", "ACI", "Ω2", "Ω4" };
                modeComboBox.Items.AddRange(inputChan);
            }
            else
            {
                string[] inputChan = { "DCV", "ACV", "DCI", "ACI", "Ω2" };
                modeComboBox.Items.AddRange(inputChan);
            }
        }

        private void shuntVal_TextChanged(object sender, EventArgs e)
        {
            double val = 0;
            if ("" == shuntVal.Text)
            {
                shuntVal.Text = "0";
            }
            else if(!double.TryParse(shuntVal.Text, out val))
            {
                shuntVal.Text = shuntVal.Text.Remove(shuntVal.Text.Length - 1, 1);
                shuntVal.SelectionStart = shuntVal.Text.Length;
            }
            else if(shuntVal.Text.Length > 1)
            {
                if ("0" == shuntVal.Text.Substring(0, 1) && "," != shuntVal.Text.Substring(1, 1) && "." != shuntVal.Text.Substring(1, 1))
                {
                    int cursPos = shuntVal.SelectionStart;
                    shuntVal.Text = shuntVal.Text.Remove(0, 1);
                    shuntVal.SelectionStart = cursPos;
                }
            }
            else if ("," == shuntVal.Text.Substring(0, 1))
            {
                int cursPos = shuntVal.SelectionStart;
                shuntVal.Text = shuntVal.Text.Insert(0, "0");
                shuntVal.SelectionStart = cursPos + 1;
            }
            else if (shuntVal.Text.Contains("."))
            {
                int cursPos = shuntVal.SelectionStart;
                shuntVal.Text = shuntVal.Text.Replace(".", ",");
                shuntVal.SelectionStart = cursPos + 1;
            }
        }
    }
}
