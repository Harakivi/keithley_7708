using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

namespace keithley
{
    public partial class chartView : Form
    {
        public List<CheckBox> checkBoxs = new List<CheckBox>();
        public chartView(bool val)
        {
            InitializeComponent();
            if (val)
            {
                Text = adRes.windowsText;
                hideBtn.Text = adRes.Close;
                readFile();
            }
        }

        private void hideBtn_Click(object sender, EventArgs e)
        {
            if (hideBtn.Text == adRes.Close)
            {
                Close();
            }
            else if (hideBtn.Text == adRes.Hide)
            {
                Hide();
                (Owner as KeithleyLogger).showChart.Text = adRes.sChart;
            }
        }

        public void checkBox_checkChange(object sender, EventArgs e)
        {
            CheckBox obj = sender as CheckBox;
            chart1.Series.FindByName(obj.Text).Enabled = obj.Checked;
        }

        private void Chart_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((sender as chartView).Text != adRes.windowsText)
            {
                if (sender == this && e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true;
                    Hide();
                    (Owner as KeithleyLogger).showChart.Text = adRes.sChart;
                }
            }
        }
        public void chartRefrashCheckBox(List<Channel> channels)
        {
            chart1.Series.Clear();
            for (int i = 0; i < channels.Count; i++)
            {
                chart1.Series.Add(channels[i].channelSeries);
                chart1.Series[i].ChartType = SeriesChartType.FastLine;
                chart1.Series[i].XValueType = ChartValueType.DateTime;
            }
            flowLayoutPanel1.Controls.Clear();
            checkBoxs.Clear();
            for (int i = 0; i < chart1.Series.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.AutoSize = true;
                //checkBox.Location = new System.Drawing.Point(3, 3 + 3 * i);
                checkBox.Name = chart1.Series[i].Name;
                checkBox.Size = new System.Drawing.Size(80, 17);
                checkBox.TabIndex = i;
                checkBox.Text = chart1.Series[i].Name;
                checkBox.UseVisualStyleBackColor = true;
                checkBox.Checked = true;
                checkBoxs.Add(checkBox);
            }
            for (int i = 0; i < checkBoxs.Count; i++)
            {
                checkBoxs[i].CheckStateChanged += new EventHandler(checkBox_checkChange);
            }
            flowLayoutPanel1.Controls.AddRange(checkBoxs.ToArray());
        }

        private void ZoomYChkBx_CheckedChanged(object sender, EventArgs e)
        {
            if (zoomYChkBx.Checked)
            {
                chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
                chart1.ChartAreas[0].CursorY.Interval = 0.001;
                chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = false;
            }
            else
            {
                chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;
                chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
            }
        }

        private void ZoomXChkBx_CheckedChanged(object sender, EventArgs e)
        {
            if (zoomXChkBx.Checked)
            {
                chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
                chart1.ChartAreas[0].CursorX.Interval = 0.000000000001;
                chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = false;
            }
            else
            {
                chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
                chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            }
        }
        private async void readFile()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Файлы(*.txt)|*.txt";
            openFile.DefaultExt = "txt";
            openFile.ShowDialog();
            List<Channel> readingChannels = new List<Channel>();
            string line;
            if (openFile.FileName != "")
            {
                using (StreamReader stream = new StreamReader(openFile.FileName, System.Text.Encoding.Default))
                {
                    while ((line = await stream.ReadLineAsync()) != null)
                    {
                        if (line.Contains("time"))
                        {
                            continue;
                        }
                        else
                        {
                            bool findFlag = false;
                            string[] separator = { "\t" };
                            string[] lineAr = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                            DateTime readXValue = DateTime.Parse(lineAr[0], CultureInfo.GetCultureInfo("ru-RU"));
                            double readYValue = Convert.ToDouble(lineAr[1], CultureInfo.GetCultureInfo("ru-RU"));
                            string readFunction = lineAr[2];
                            string readName = lineAr[3];
                            for (int i = 0; i < readingChannels.Count; i++)
                            {
                                if (readingChannels[i].Name.Contains(readName))
                                {
                                    findFlag = true;
                                    readingChannels[i].AddXY(readXValue, readYValue);
                                    break;
                                }
                            }
                            if (!findFlag)
                            {
                                string[] separator2 = { "'" };
                                string[] addr = readName.Split(separator2, StringSplitOptions.RemoveEmptyEntries);
                                Channel chan = new Channel(addr[0]);
                                chan.Function = readFunction;
                                chan.AddXY(DateTime.Parse(lineAr[0], CultureInfo.GetCultureInfo("ru-RU")), Convert.ToDouble(lineAr[1], CultureInfo.GetCultureInfo("ru-RU")));
                                readingChannels.Add(chan);
                            }
                        }
                    }
                }
                //channels = readingChannels;
                chartRefrashCheckBox(readingChannels);
                chartScaleLabel();
            }
            else { Close(); }
        }

        private void Chart1_AxisViewChanged(object sender, ViewEventArgs e)
        {
            chartScaleLabel();
        }

        private void chartScaleLabel()
        {
            if(double.IsNaN(chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum) || double.IsNaN(chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum))
            {
                chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
            }
            DateTime minDate = DateTime.FromOADate(chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum);
            DateTime maxDate = DateTime.FromOADate(chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum);
            if ((maxDate - minDate).TotalDays >= 1)
            {
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM/yyyy";
            }
            else // if((maxDate - minDate).TotalHours > 0)
            {
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM/yyyy \n HH:mm:ss";
            }
        }
    }
}
