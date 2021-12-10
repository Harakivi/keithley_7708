//using NationalInstruments.NI4882;
using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Ivi.Visa;
using NationalInstruments.Visa;
using System.Xml.Serialization;
using System.Threading;
using System.Globalization;

namespace keithley
{
    public partial class KeithleyLogger : Form
    {
        private System.Windows.Forms.Timer aTimer;
        private string streamerPath = null;
        private MessageBasedSession mbSession;
        private string lastResourceString = null;
        internal List<Channel> channels;
        private int timerInterval = 1000;
        private chartView chartInLog = new chartView(false);
        private bool writeFlag = false;

        public KeithleyLogger()
        {
            if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
            }
            else { Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US"); }
            InitializeComponent();
            if (CultureInfo.GetCultureInfo("ru-Ru") == Thread.CurrentThread.CurrentCulture)
            {
                englishToolStripMenuItem.Enabled = true;
                русскийToolStripMenuItem.Enabled = false;
            }
            if (CultureInfo.GetCultureInfo("en-Us") == Thread.CurrentThread.CurrentCulture)
            {
                englishToolStripMenuItem.Enabled = false;
                русскийToolStripMenuItem.Enabled = true;
            }
        }
        private async void createTimerButt_Click(object sender, EventArgs e)
        {
            if (createTimerBtn.Text == adRes.strRecord && InitializeTimer())
            {
                //mbSession.RawIO.Write("DISP:TEXT:DATA 'LOG'");
                //mbSession.RawIO.Write("DISP:TEXT:STAT ON");
                notifyIcon.Visible = true;
                createTimerBtn.Text = adRes.stpRecord;
                timerEnbToolStripMenuItem.Text = adRes.stpRecord;
                hoursNumeric.Enabled = false;
                minutesNumeric.Enabled = false;
                secondsNumeric.Enabled = false;
                SetupBtn.Enabled = false;
                //chartWindow.chart1.DataSource = channels;
            }
            else if (createTimerBtn.Text == adRes.stpRecord)
            { 
                await Task.Run(() => waitWriteEnds());
                DeinitializeTimer();
                SetupBtn.Enabled = true;
                notifyIcon.Visible = false;
                createTimerBtn.Text = adRes.strRecord;
                timerEnbToolStripMenuItem.Text = adRes.strRecord;
                hoursNumeric.Enabled = true;
                minutesNumeric.Enabled = true;
                secondsNumeric.Enabled = true;
                mbSession.RawIO.Write("DISP:TEXT:DATA ''");
                mbSession.RawIO.Write("DISP:TEXT:STAT OFF");
            }
        }
        private bool InitializeFile()
        {
            using (saveFileWindow saveFile = new saveFileWindow(streamerPath))
            {
                DialogResult res = saveFile.ShowDialog();
                if (saveFile.streamerDirTextBox.Text != "" && res == DialogResult.OK)
                {
                    streamerPath = saveFile.streamerDirTextBox.Text;
                    using (StreamWriter sw = new StreamWriter(streamerPath, false, System.Text.Encoding.Default))
                    {
                        sw.Write("time\t");
                        sw.Write("Value\t");
                        sw.Write("Function\t");
                        sw.Write("Channel_Name\t");
                        //DateTime thisChanDate = DateTime.Now;
                        //for (int i = 0; i < 100000; i++)
                        //{
                        //    sw.Write(sw.NewLine);
                        //    string readValue = $"1,1{i}";
                        //    sw.Write(thisChanDate.AddSeconds(i) + "\t");
                        //    sw.Write(readValue + "\t");
                        //    sw.Write("DCV" + "\t");
                        //    sw.Write("'101'" + "\t");
                        //}
                    }
                    return true;
                }
                
            }
            return false;
        }
        private bool InitializeTimer()
        {
            try
            {
                if(0 != timerInterval)
                {
                    aTimer = new System.Windows.Forms.Timer();
                    aTimer.Interval = timerInterval;
                    aTimer.Tick += new EventHandler(Timer_Tick);
                    aTimer.Enabled = true;
                    return true;
                }
                MessageBox.Show(adRes.incrTime);
                return false;
            }
            catch
            {
                return false;
            }
        }
        private void DeinitializeTimer()
        {
            try
            {
                aTimer.Dispose();
                aTimer = null;
            }
            catch(NullReferenceException)
            {
                return;
            }
        }
        private async void Timer_Tick(object Sender, EventArgs e)
        {
            writeFlag = true;
            try
            {
                for (int i = 0; i < channels.Count; i++)
                {
                    using (StreamWriter sw = new StreamWriter(streamerPath, true, System.Text.Encoding.Default))
                    {
                        sw.Write(sw.NewLine);
                        DateTime thisChanDate = DateTime.Now;
                        string readValue = null;
                        if (i == 0)
                        {
                            readValue = await Task.Run(() => channels[i].readValue(mbSession, channels[channels.Count - 1])); //Считывание значения через класс
                        }
                        else
                        {
                            readValue = await Task.Run(() => channels[i].readValue(mbSession, channels[i - 1]));
                        }
                        double result = double.NaN;
                        if(double.TryParse(readValue, out result))
                        {
                            channels[i].AddXY(thisChanDate, result);
                        }
                        
                        sw.Write(thisChanDate.ToString(CultureInfo.GetCultureInfo("ru-RU")) + "\t");
                        sw.Write(readValue + "\t");
                        sw.Write(channels[i].Function + "\t");
                        sw.Write(channels[i].Name + "\t");
                    }
                }
            }
            catch (IOException)
            {
                DeinitializeTimer();
                SetupBtn.Enabled = true;
                MessageBox.Show(adRes.incrTime);
            }
            //documentToSave.Save(savePath);
            Thread.Sleep(500);
            writeFlag = false;
        }

        private async void openSession_Click(object sender, EventArgs e)
        {
            if (openSession.Text == adRes.openSession)
            {
                using (SelectResource sr = new SelectResource())
                {
                    if (lastResourceString != null)
                    {
                        sr.ResourceName = lastResourceString;
                    }
                    DialogResult result = sr.ShowDialog(this);
                    if (result == DialogResult.OK)
                    {
                        lastResourceString = sr.ResourceName;
                        Cursor = Cursors.WaitCursor;
                        using (var rmSession = new ResourceManager())
                        {
                            try
                            {
                                mbSession = sr.mbSession;
                                mbSession.TimeoutMilliseconds = 20000;
                                SetupControlState(true);
                            }
                            catch (InvalidCastException)
                            {
                                MessageBox.Show("Resource selected must be a message-based session");
                            }
                            catch (Exception exp)
                            {
                                MessageBox.Show(exp.Message);
                            }
                            finally
                            {
                                Cursor = Cursors.Default;
                            }
                        }
                        openSession.Text = adRes.closeSession;
                    }
                }
            }
            else if (openSession.Text == adRes.closeSession)
            {
                await Task.Run(() => waitWriteEnds());
                DeinitializeTimer();
                createTimerBtn.Enabled = false;
                createTimerBtn.Text = adRes.strRecord;
                channels = null;
                SetupControlState(false);
                mbSession.Dispose();
                openSession.Text = adRes.openSession;
            }
        }
        private void SetupControlState(bool isSessionOpen)
        {
            SetupBtn.Enabled = isSessionOpen;
            hoursNumeric.Enabled = isSessionOpen;
            minutesNumeric.Enabled = isSessionOpen;
            secondsNumeric.Enabled = isSessionOpen;
        }

        private void KeithleyLogger_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (channels != null && aTimer != null)
            {
                //при запущеном таймере приложение не закрывается, а сворачивается в трей
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                notifyIcon.ShowBalloonTip(5000, adRes.Attention, adRes.logCont, ToolTipIcon.Info);
            }
            else
            {
                if (mbSession != null && !mbSession.IsDisposed)
                {
                    mbSession.RawIO.Write("DISP:TEXT:DATA ''");
                    mbSession.RawIO.Write("DISP:TEXT:STAT OFF");
                    mbSession.Dispose();
                }
                if (notifyIcon != null)
                {
                    notifyIcon.Dispose();
                }
            }
        }
        
        private void waitWriteEnds()
        {
            while (writeFlag) { };
        }

        private void CloseApp_Click(object sender, EventArgs e)
        {
            mbSession.RawIO.Write("DISP:TEXT:DATA ''");
            mbSession.RawIO.Write("DISP:TEXT:STAT OFF");
            mbSession.RawIO.Write("SYST:PRES");
            notifyIcon.Dispose();
            mbSession.Dispose();
            Application.Exit();
        }

        private void OpenApp_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }
        private void SetupBtn_Click(object sender, EventArgs e)
        {
            using (SetupForm SF = new SetupForm(channels, mbSession))
            {

                DialogResult result = SF.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    if(SF.channels.Count == 0)
                    {
                        channels = null;
                        createTimerBtn.Enabled = false;
                    }
                    else
                    {
                        if(channels != SF.channels)
                        {

                            channels = SF.channels;
                            for (int i = 0; i < channels.Count; ++i)
                            {
                                if (i == 0)
                                {
                                    channels[0].readTime(mbSession, new Channel());
                                }
                                else
                                {
                                    channels[i].readTime(mbSession, channels[i - 1]);
                                }
                            }
                            
                            chartInLog.chartRefrashCheckBox(channels);
                            TimeSpan timeSpan = new TimeSpan();
                            for (int i = 0; i < channels.Count; i++)
                            {
                                timeSpan += channels[i].timeToRead;
                            }
                            decimal secondsVal = Convert.ToDecimal(Math.Ceiling(timeSpan.TotalSeconds));
                            minTimeVal.Visible = true;
                            minTimeVal.Text = secondsVal.ToString();
                            if (streamerPath == null)
                            {
                                if (InitializeFile())
                                {
                                    createTimerBtn.Enabled = true;
                                }
                            }
                            else { createTimerBtn.Enabled = true; }
                        }
                    }
                }
            }
        }

        private void HoursNumeric_ValueChanged(object sender, EventArgs e)
        {
            intervalChanged();
        }

        private void MinutesNumeric_ValueChanged(object sender, EventArgs e)
        {
            intervalChanged();
        }

        private void SecondsNumeric_ValueChanged(object sender, EventArgs e)
        {
            intervalChanged();
        }

        private void intervalChanged()
        {
           timerInterval = (int)hoursNumeric.Value * 1000 * 60 * 60 + (int)minutesNumeric.Value * 1000 * 60 + (int)secondsNumeric.Value * 1000;
        }

        private void KeithleyLogger_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
            notifyIcon.Dispose();
        }

        private void ShowChart_Click(object sender, EventArgs e)
        {
            if(!chartInLog.Visible)
            {
                chartInLog.Show(this);
                showChart.Text = adRes.hChart;
                showChartToolStripMenuItem.Text = adRes.hChart;
            }
            else if (chartInLog.Visible)
            {
                chartInLog.Hide();
                showChart.Text = adRes.sChart;
                showChartToolStripMenuItem.Text = adRes.sChart;
            }
        }
        private void ShowChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChart_Click(sender, e);
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(mbSession != null)
            {
                if (!mbSession.IsDisposed)
                {
                    mbSession.RawIO.Write("DISP:TEXT:DATA ''");
                    mbSession.RawIO.Write("DISP:TEXT:STAT OFF");
                    mbSession.RawIO.Write("SYST:PRES");
                    mbSession.Dispose();
                }
                
            }
            if (notifyIcon != null)
            {
                notifyIcon.Dispose();
            }
            Application.Exit();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(adRes.about);
        }

        private void OpenChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chartView fileChartView = new chartView(true);
            if (!fileChartView.IsDisposed)
            {
                fileChartView.Show(this);
            }
        }

        private void timerEnbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createTimerButt_Click(sender, e);
        }

        private void ChangeFileDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (InitializeFile() && mbSession != null && channels != null)
            {
                createTimerBtn.Enabled = true;
            }
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Language = "en-US";
            if (DialogResult.Yes == MessageBox.Show(adRes.messCls, adRes.messClsName, MessageBoxButtons.YesNo))
            {
                Application.Restart();
            }
        }

        private void русскийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Language = "ru-RU";
            if(DialogResult.Yes == MessageBox.Show(adRes.messCls, adRes.messClsName, MessageBoxButtons.YesNo))
            {
                Application.Restart();

            }
        }
    }
    [Serializable]
    public class Channel
    {
        /// <summary> 
        /// Не используется
        /// </summary> 
        public string thrVoltage { get; set; }
        /// <summary> 
        /// Имя канала
        /// </summary> 
        public string Name { get; set; }
        /// <summary> 
        /// Адрес порта
        /// </summary>
        public string portAddr { get; }
        /// <summary> 
        /// Выбранный режим работы
        /// </summary>
        public string Function { get; set; }
        /// <summary> 
        /// Пара для работы в 4х проводном режиме
        /// </summary>
        public string Pair { get; set; }
        /// <summary> 
        /// Список точек данных
        /// </summary>
        [NonSerialized]
        public Series channelSeries;
        /// <summary> 
        /// Необходимое время на чтение канала
        /// </summary>
        public TimeSpan timeToRead { get; private set; }
        /// <summary> 
        /// Количество циклов питающей сети, за которое будет выполнено измерение
        /// </summary>
        public string NPLC { get; set; } 
        /// <summary> 
        /// Состояние функции фильтрации мультиметра
        /// </summary>
        public string FILT { get; set; }
        /// <summary> 
        /// Полоса пропускания для режима работы AC
        /// </summary>
        public string bandWidth { get; set; }
        /// <summary> 
        /// Полоса пропускания для режима работы AC
        /// </summary>
        public string range { get; set; }
        public string shuntRes { get; set; }
        public Channel(string addr)
        {
            portAddr = addr;
            Name = $"'{addr}'";
            Function = "DCV"; // Настройки по умолчанию
            range = "AUTO ON";
            Pair = null;
            NPLC = "1";
            FILT = "ON";
            bandWidth = "300";
            shuntRes = "0";
            channelSeries = new Series(Name);
            //element = new XElement("Series");
            //element.Add(new XAttribute("Name", Name));
            //dataPoints = new DataPointCollection();
        }
        public Channel(string addr, Channel channelToCopy)
        {
            portAddr = addr;
            Name = $"'{addr}'";
            Function = channelToCopy.Function; // Настройки по умолчанию
            range = channelToCopy.range;
            Pair = null;
            NPLC = channelToCopy.NPLC;
            FILT = channelToCopy.FILT;
            bandWidth = channelToCopy.bandWidth;
            shuntRes = "0";
            channelSeries = new Series(Name);
            //element = new XElement("Series");
            //element.Add(new XAttribute("Name", Name));
            //dataPoints = new Collection<DataPoint>;
        }
        public Channel()
        {
        }
        /// <summary> 
        /// Считать значение на основании настроек внутри экземпляра
        /// </summary> 
        public string readValue(MessageBasedSession mbSession, Channel prevChan)
        {
            string Value = "";
            switch (Function)
            {
                case "DCV":
                    Value = readDCV(mbSession, prevChan);
                    break;
                case "ACV":
                    Value = readACV(mbSession, prevChan);
                    break;
                case "Ohm2":
                    Value = readRES(mbSession, prevChan);
                    break;
                case "Ohm4":
                    Value = readFRES(mbSession, prevChan);
                    break;
                case "DCI":
                    Value = readDCV(mbSession, prevChan);
                    double currDCReg = Convert.ToDouble(Value);
                    currDCReg /= Convert.ToDouble(shuntRes);
                    Value = currDCReg.ToString();
                    break;
                case "ACI":
                    Value = readACV(mbSession, prevChan);
                    double currACReg = Convert.ToDouble(Value);
                    currACReg /= Convert.ToDouble(shuntRes);
                    Value = currACReg.ToString();
                    break;
                    //case "FREQ":
                    //    Value = readFREQ(mbSession);
                    //    break;
                    //case "PER":
                    //    Value = readPER(mbSession);
                    //    break;
                    //case "TEMP":
                    //    Value = readTEMP(mbSession);
                    //    break;

            }
            return Value;
        }
        public bool readValueBool(MessageBasedSession mbSession, Channel prevChan)
        {
            string Value = "";
            switch (Function)
            {
                case "DCV":
                    Value = readDCV(mbSession, prevChan);
                    if(Value != null)
                    {
                        return true;
                    }
                    return false;
                case "ACV":
                    readACV(mbSession, prevChan);
                    if (Value != null)
                    {
                        return true;
                    }
                    return false;
                case "Ohm2":
                    Value = readRES(mbSession, prevChan);
                    if (Value != null)
                    {
                        return true;
                    }
                    return false;
                case "Ohm4":
                    Value = readFRES(mbSession, prevChan);
                    if (Value != null)
                    {
                        return true;
                    }
                    return false;
                case "DCI":
                    Value = readDCV(mbSession, prevChan);
                    double currDCReg = Convert.ToDouble(Value);
                    currDCReg /= Convert.ToDouble(shuntRes, CultureInfo.GetCultureInfo("en-US"));
                    Value = currDCReg.ToString();
                    if (Value != null)
                    {
                        return true;
                    }
                    return false;
                case "ACI":
                    Value = readACV(mbSession, prevChan);
                    double currACReg = Convert.ToDouble(Value);
                    Value = currACReg.ToString();
                    if (Value != null)
                    {
                        return true;
                    }
                    return false;
                    //case "FREQ":
                    //    Value = readFREQ(mbSession);
                    //    break;
                    //case "PER":
                    //    Value = readPER(mbSession);
                    //    break;
                    //case "TEMP":
                    //    Value = readTEMP(mbSession);
                    //    break;

            }
            return false;
        }
        private string readDCV(MessageBasedSession mbSession, Channel prevChan)
        {
            try
            {
                mbSession.RawIO.Write("ROUT:OPEN:ALL");
                Thread.Sleep(20);
                mbSession.RawIO.Write($"ROUT:MULT:CLOS (@{portAddr},143)");
                Thread.Sleep(20);
                if (this.Function != prevChan.Function)
                {
                    mbSession.RawIO.Write("SENS:FUNC 'VOLT:DC'");
                }
                if (this.range != prevChan.range)
                {
                    mbSession.RawIO.Write($":SENS:VOLT:DC:RANG{range}");
                }
                if (this.NPLC != prevChan.NPLC)
                {
                    mbSession.RawIO.Write($":SENS:VOLT:DC:NPLC {NPLC}");
                }
                if (this.FILT != prevChan.FILT)
                {
                    mbSession.RawIO.Write($":SENS:VOLT:DC:AVER:STAT {FILT}");
                }
                for (int i = 0; i < 2; i++)
                {
                    mbSession.RawIO.Write("DATA:FRESH?");
                    //-6.26574010E-02VDC,+900.272SECS,+69495RDNG#  пример возвращаемой строки
                    string readStr = mbSession.RawIO.ReadString();
                    //string readStr = "-6.26574010E-02VDC,+900.272SECS,+69495RDNG#";
                    string[] separator = { "," };
                    string[] nameAr = readStr.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    string[] separator2 = { "VDC" };
                    nameAr = nameAr[0].Split(separator2, StringSplitOptions.RemoveEmptyEntries);
                    nameAr[0] = nameAr[0].Replace(".", ",");
                    double result = 0;
                    if (double.TryParse(nameAr[0], out result) && nameAr[0] != "+9,9E37" && nameAr[0] != "-9,9E37")
                    {
                        return nameAr[0];
                    };
                }
                return "OVRFLW";
            }
            catch (IOTimeoutException exp)
            {
                MessageBox.Show("Failed to read value. Reason: " + exp.Message);
            }
            catch (NativeVisaException exp)
            {
                MessageBox.Show("Failed to read value. Reason: " + exp.Message);
            }
            return null;
        }
        private string readACV(MessageBasedSession mbSession, Channel prevChan)
        {
            try
            {
                mbSession.RawIO.Write($"ROUT:OPEN:ALL");
                Thread.Sleep(20);
                mbSession.RawIO.Write($"ROUT:MULT:CLOS (@{portAddr},143)");
                Thread.Sleep(20);
                if (this.Function != prevChan.Function)
                {
                    mbSession.RawIO.Write($"SENS:FUNC 'VOLT:AC'");
                }
                if (this.range != prevChan.range)
                {
                    mbSession.RawIO.Write($":SENS:VOLT:AC:RANG{range}");
                }
                if(this.bandWidth != prevChan.bandWidth)
                {
                    mbSession.RawIO.Write($":SENS:VOLT:AC:DET:BAND {bandWidth}");
                }
                if (bandWidth == "300")
                {
                    if(this.NPLC != prevChan.NPLC)
                    {
                        mbSession.RawIO.Write($":SENS:VOLT:AC:NPLC {NPLC}");
                    }
                }
                if(this.FILT != prevChan.FILT)
                {
                    mbSession.RawIO.Write($":SENS:VOLT:AC:AVER:STAT {FILT}");
                }
                for (int i = 0; i < 2; i++)
                {
                    mbSession.RawIO.Write("DATA:FRESH?");
                    //-6.26574010E-02VDC,+900.272SECS,+69495RDNG#  пример возвращаемой строки
                    string readStr = mbSession.RawIO.ReadString();
                    //string readStr = "-6.26574010E-02VDC,+900.272SECS,+69495RDNG#";
                    string[] separator = { "," };
                    string[] nameAr = readStr.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    string[] separator2 = { "VAC" };
                    nameAr = nameAr[0].Split(separator2, StringSplitOptions.RemoveEmptyEntries);
                    nameAr[0] = nameAr[0].Replace(".", ",");
                    double result = 0;
                    if (double.TryParse(nameAr[0], out result) && nameAr[0] != "+9,9E37" && nameAr[0] != "-9,9E37")
                    {
                        return nameAr[0];
                    };
                }
                return "OVRFLW";
            }
            catch (IOTimeoutException exp)
            {
                MessageBox.Show("Failed to read value. Reason: " + exp.Message);
            }
            catch (NativeVisaException exp)
            {
                MessageBox.Show("Failed to read value. Reason: " + exp.Message);
            }
            return null;
        }
        private string readRES(MessageBasedSession mbSession, Channel prevChan)
        {
            try
            {
                mbSession.RawIO.Write($"ROUT:OPEN:ALL");
                Thread.Sleep(20);
                mbSession.RawIO.Write($"ROUT:MULT:CLOS (@{portAddr},143)");
                Thread.Sleep(20);
                if (this.Function != prevChan.Function)
                {
                    mbSession.RawIO.Write($"SENS:FUNC 'RES'");
                }
                if (this.range != prevChan.range)
                {
                    string rangeDyn = range.Replace("k", "000");
                    rangeDyn = range.Replace("M", "000000");
                    mbSession.RawIO.Write($":SENS:RES:RANG{rangeDyn}");
                }
                if (this.NPLC != prevChan.NPLC)
                {
                    mbSession.RawIO.Write($":SENS:RES:NPLC {NPLC}");
                }
                if (this.FILT != prevChan.FILT)
                {
                    mbSession.RawIO.Write($":SENS:RES:AVER:STAT {FILT}");
                }
                for (int i = 0; i < 2; i++)
                {
                    mbSession.RawIO.Write("DATA:FRESH?");
                    //-6.26574010E-02VDC,+900.272SECS,+69495RDNG#  пример возвращаемой строки
                    string readStr = mbSession.RawIO.ReadString();
                    //string readStr = "-6.26574010E-02VDC,+900.272SECS,+69495RDNG#";
                    string[] separator = { "," };
                    string[] nameAr = readStr.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    string[] separator2 = { "OHM" };
                    nameAr = nameAr[0].Split(separator2, StringSplitOptions.RemoveEmptyEntries);
                    nameAr[0] = nameAr[0].Replace(".", ",");
                    double result = 0;
                    if (double.TryParse(nameAr[0], out result) && nameAr[0] != "+9,9E37" && nameAr[0] != "-9,9E37")
                    {
#warning ТЕСТ НАГРУЗКИ
                        //ДЛЯ ТЕСТА НАГРУЗКИ
                        double temp = 0;
                        double A = 0.001125308852122;
                        double B = 0.000234711863267;
                        double C = 0.000000085663516;
                        temp = 1 / (A + B * Math.Log(result) + C * Math.Pow(Math.Log(result), 3)) - 273.15;
                        nameAr[0]= temp.ToString();
                        //ДЛЯ ТЕСТА НАГРУЗКИ
#warning ТЕСТ НАГРУЗКИ
                        return nameAr[0];
                    };
                }
                return "OVRFLW";
            }
            catch (IOTimeoutException exp)
            {
                MessageBox.Show("Failed to read value. Reason: " + exp.Message);
            }
            catch (NativeVisaException exp)
            {
                MessageBox.Show("Failed to read value. Reason: " + exp.Message);
            }
            return null;
        }
        private string readFRES(MessageBasedSession mbSession, Channel prevChan)
        {
            try
            {
                mbSession.RawIO.Write($"ROUT:OPEN:ALL");
                Thread.Sleep(20);
                mbSession.RawIO.Write($"ROUT:MULT:CLOS (@{portAddr},{Pair},141,142,143)");
                Thread.Sleep(20);
                if (this.Function != prevChan.Function)
                {
                    mbSession.RawIO.Write($"SENS:FUNC 'FRES'");
                }
                if (this.range != prevChan.range)
                {
                    string rangeDyn = range.Replace("k", "000");
                    rangeDyn = range.Replace("M", "000000");
                    mbSession.RawIO.Write($":SENS:FRES:RANG{rangeDyn}");
                }
                if (this.NPLC != prevChan.NPLC)
                {
                    mbSession.RawIO.Write($":SENS:FRES:NPLC {NPLC}");
                }
                if (this.FILT != prevChan.FILT)
                {
                    mbSession.RawIO.Write($":SENS:FRES:AVER:STAT {FILT}");
                }
                for(int i = 0; i < 2; i++)
                {
                    mbSession.RawIO.Write("DATA:FRESH?");
                    //-6.26574010E-02VDC,+900.272SECS,+69495RDNG#  пример возвращаемой строки
                    string readStr = mbSession.RawIO.ReadString();
                    //string readStr = "-6.26574010E-02VDC,+900.272SECS,+69495RDNG#";
                    string[] separator = { "," };
                    string[] nameAr = readStr.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    string[] separator2 = { "OHM4W" };
                    nameAr = nameAr[0].Split(separator2, StringSplitOptions.RemoveEmptyEntries);
                    nameAr[0] = nameAr[0].Replace(".", ",");
                    double result = 0;
                    if (double.TryParse(nameAr[0], out result) && nameAr[0] != "+9,9E37" && nameAr[0] != "-9,9E37")
                    {
                        return nameAr[0];
                    };
                }
                return "OVRFLW";
            }
            catch (IOTimeoutException exp)
            {
                MessageBox.Show("Failed to read value. Reason: " + exp.Message);
            }
            catch (NativeVisaException exp)
            {
                MessageBox.Show("Failed to read value. Reason: " + exp.Message);
            }
            return null;
        }
        //private string readFREQ(MessageBasedSession mbSession)
        //{
        //    mbSession.RawIO.Write($"ROUT:OPEN:ALL");
        //    mbSession.RawIO.Write($"ROUT:MULT:CLOS (@{portAddr},143)");
        //    mbSession.RawIO.Write($"SENS:FUNC 'FREQ'");
        //    mbSession.RawIO.Write("DATA:FRESH?");
        //    //-6.26574010E-02VDC,+900.272SECS,+69495RDNG#  пример возвращаемой строки
        //    string readStr = mbSession.RawIO.ReadString();
        //    //string readStr = "-6.26574010E-02VDC,+900.272SECS,+69495RDNG#";
        //    string[] separator = { "," };
        //    string[] nameAr = readStr.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        //    string[] separator2 = { "VDC" };
        //    nameAr = nameAr[0].Split(separator2, StringSplitOptions.RemoveEmptyEntries);
        //    nameAr[0] = nameAr[0].Replace(".", ",");
        //    return nameAr[0];
        //}
        //private string readPER(MessageBasedSession mbSession)
        //{
        //    mbSession.RawIO.Write($"ROUT:OPEN:ALL");
        //    mbSession.RawIO.Write($"ROUT:MULT:CLOS (@{portAddr},143)");
        //    mbSession.RawIO.Write($"SENS:FUNC 'PER'");
        //    mbSession.RawIO.Write("DATA:FRESH?");
        //    //-6.26574010E-02VDC,+900.272SECS,+69495RDNG#  пример возвращаемой строки
        //    string readStr = mbSession.RawIO.ReadString();
        //    //string readStr = "-6.26574010E-02VDC,+900.272SECS,+69495RDNG#";
        //    string[] separator = { "," };
        //    string[] nameAr = readStr.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        //    string[] separator2 = { "VAC" };
        //    nameAr = nameAr[0].Split(separator2, StringSplitOptions.RemoveEmptyEntries);
        //    nameAr[0] = nameAr[0].Replace(".", ",");
        //    return nameAr[0];
        //}
        //private string readTEMP(MessageBasedSession mbSession)
        //{
        //    mbSession.RawIO.Write($"ROUT:OPEN:ALL");
        //    mbSession.RawIO.Write($"ROUT:MULT:CLOS (@{portAddr},143)");
        //    mbSession.RawIO.Write($"SENS:FUNC 'TEMP'");
        //    mbSession.RawIO.Write("DATA:FRESH?");
        //    //-6.26574010E-02VDC,+900.272SECS,+69495RDNG#  пример возвращаемой строки
        //    string readStr = mbSession.RawIO.ReadString();
        //    //string readStr = "-6.26574010E-02VDC,+900.272SECS,+69495RDNG#";
        //    string[] separator = { "," };
        //    string[] nameAr = readStr.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        //    string[] separator2 = { "VAC" };
        //    nameAr = nameAr[0].Split(separator2, StringSplitOptions.RemoveEmptyEntries);
        //    nameAr[0] = nameAr[0].Replace(".", ",");
        //    return nameAr[0];
        //}
        public string ConvertToHead()
        {
            switch (Function)
            {
                case "DCV":
                    return "V, DC";
                case "ACV":
                    return "V, AC";
                case "Ohm2":
                    return "Ohm";
                case "Ohm4":
                    return "Ohm, 4W";
                case "DCI":
                    return "A, DC";
                case "ACI":
                    return "A, AC";
                    //case "FREQ":
                    //    return "Hz";
                    //case "PER":
                    //    return "s";
                    //case "TEMP":
                    //    return "°C";
            }
            return "";
        }
        public bool AddXY(DateTime xValue, double yValue)
        {
            channelSeries.Points.AddXY(xValue, yValue);
            return true;
        }
        public void  readTime(MessageBasedSession mbSession, Channel prevChan)
        {
            mbSession.RawIO.Write("DISP:TEXT:DATA 'TEST'");
            mbSession.RawIO.Write("DISP:TEXT:STAT ON");
            DateTime dateTimeStart;
            DateTime dateTimeEnd;
            dateTimeStart = DateTime.Now;
            //string readValue = await Task.Run(() => this.readValue(mbSession)); //Считывание значения через класс
            readValueBool(mbSession, prevChan);
            dateTimeEnd = DateTime.Now;
            timeToRead = dateTimeEnd - dateTimeStart;
            mbSession.RawIO.Write("DISP:TEXT:STAT OFF");
        }
    }
    class ChannelEqualityComparer : IEqualityComparer<Channel>
    {
        public bool Equals(Channel c1, Channel c2)
        {
            if ((c1 == null && c2 == null) && c1.Name == c2.Name)
                return true;
            return false;
        }

        public int GetHashCode(Channel chan)
        {
            int hCode = Convert.ToInt32(chan.Name);
            return hCode.GetHashCode();
        }
    }
}

