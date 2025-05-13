using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Mail;
using System.IO;

namespace krri_ver1
{
    public partial class Form1 : Form
    {
        string DataIn;
        string CSV_Data;

        //Threshold value
        float Dust_TH = Properties.Settings.Default.Dust_TH;
        float Co2_TH = Properties.Settings.Default.Co2_TH;
        float Sound_Th = Properties.Settings.Default.Sound_TH;
        float Wind_TH = Properties.Settings.Default.Wind_TH;
        float Voc_TH = Properties.Settings.Default.Voc_TH;
        float Temperature_TH = Properties.Settings.Default.Temperature_TH;
        float Humidity_TH = Properties.Settings.Default.Humidity_TH;




        public void Com_data(string Com, string Baud, string Data, string Stop, string parity)
        {
            Label_Com_Info.Text = Com;
            Label_Baud_Info.Text = Baud;
            Label_Data_Info.Text = Data;
            Label_Stop_Info.Text = Stop;
            Label_Parity_Info.Text = parity;
        }

        public void Threshold_Value(string Dust, string Co2, string Sound, string Wind, string Voc, string Temperature, string Humidity)
        {
            Properties.Settings.Default.Dust_TH = float.Parse(Dust);
            Properties.Settings.Default.Co2_TH = float.Parse(Co2);
            Properties.Settings.Default.Sound_TH = float.Parse(Sound);
            Properties.Settings.Default.Wind_TH = float.Parse(Wind);
            Properties.Settings.Default.Voc_TH = float.Parse(Voc);
            Properties.Settings.Default.Temperature_TH = float.Parse(Temperature);
            Properties.Settings.Default.Humidity_TH = float.Parse(Humidity);
            Properties.Settings.Default.Save();

            Dust_TH = Properties.Settings.Default.Dust_TH;
            Co2_TH = Properties.Settings.Default.Co2_TH;
            Sound_Th = Properties.Settings.Default.Sound_TH;
            Wind_TH = Properties.Settings.Default.Wind_TH;
            Voc_TH = Properties.Settings.Default.Voc_TH;
            Temperature_TH = Properties.Settings.Default.Temperature_TH;
            Humidity_TH = Properties.Settings.Default.Humidity_TH;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            string[] ports = SerialPort.GetPortNames();
            Combobox_Com.Items.Clear();
            Combobox_Com.Items.AddRange(ports);
            */
            Com_para com_Para = new Com_para(this);
            com_Para.Show();


        }

        private void Button_connect_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = Label_Com_Info.Text;
                serialPort1.BaudRate = Convert.ToInt32(Label_Baud_Info.Text);
                serialPort1.DataBits = Convert.ToInt32(Label_Data_Info.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Label_Stop_Info.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), Label_Parity_Info.Text);
                serialPort1.DtrEnable = true;
                serialPort1.RtsEnable = true;

                serialPort1.Open();
                serialPort1.DiscardInBuffer();
                Thread.Sleep(2000);
                Button_Connet_state.Text = Label_Com_Info.Text+" conneted";
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_Disconnet_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen)
            {
                serialPort1.Close();
                Button_Connet_state.Text = "-";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            Label_Com_Info.Text = ports[0];
        }

        private void Button_Clear_Click(object sender, EventArgs e)
        {
            if(TextBox_main.Text!="")
            {
                TextBox_main.Text = "";
            }
            if(CSV_Data!="")
            {
                CSV_Data = "";
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            DataIn = serialPort1.ReadLine();
            this.Invoke(new EventHandler(ShowData));
        }

        private void ShowData(object sender, EventArgs e)
        {
            string[] sp_data = DataIn.Split(',');
            string temp_Show_data = "";
            string temp_CSV_data = "";
            string [] FFT_HZ = new string[128];
            string [] FFT_Mag= new string[128];
            string FFT_main_Hz = "";
            string FFT_Max_Value = "";

            if (sp_data.Length > 5 && sp_data.Length < 10)
            {

                //sp_data.Length-1 하는 이유 = 마지막 배열은 \r\n
                for (int i = 0; i < sp_data.Length - 1; i++)
                {
                    temp_Show_data = temp_Show_data + sp_data[i] + "\t";
                    temp_CSV_data = temp_CSV_data + sp_data[i] + ",";
                }
                temp_CSV_data.TrimEnd(',');

                TextBox_main.Text = DateTime.Now.ToString("yyyy.MM.dd.HH:mm:ss") + "\t" + temp_Show_data + "\r\n" + TextBox_main.Text;
                CSV_Data = CSV_Data + DateTime.Now.ToString("yyyy.MM.dd.HH:mm:ss") + "," + temp_CSV_data + "\r\n";

                Chart_Dust.Series["Series1"].Points.Add(float.Parse(sp_data[0]));
                Chart_Dust.Series["Series2"].Points.Add(Dust_TH);
                Chart_Co2.Series["Series1"].Points.Add(float.Parse(sp_data[1]));
                Chart_Co2.Series["Series2"].Points.Add(Co2_TH);
                Chart_Sound.Series["Series1"].Points.Add(float.Parse(sp_data[2]));
                Chart_Sound.Series["Series2"].Points.Add(Sound_Th);
                Chart_Wind.Series["Series1"].Points.Add(float.Parse(sp_data[3]));
                Chart_Wind.Series["Series2"].Points.Add(Wind_TH);
                Chart_Voc.Series["Series1"].Points.Add(float.Parse(sp_data[4]));
                Chart_Voc.Series["Series2"].Points.Add(Voc_TH);
                Chart_Temp.Series["Series1"].Points.Add(float.Parse(sp_data[5]));
                Chart_Temp.Series["Series2"].Points.Add(Temperature_TH);
                Chart_Humid.Series["Series1"].Points.Add(float.Parse(sp_data[6]));
                Chart_Humid.Series["Series2"].Points.Add(Humidity_TH);
                Label_Dust.Text = sp_data[0];
                Label_Co2.Text = sp_data[1];
                Label_Sound.Text = sp_data[2];
                Label_Wind.Text = sp_data[3];
                Label_Vos.Text = sp_data[4];
                Label_Temp.Text = sp_data[5];
                Label_Humi.Text = sp_data[6];

                if (Chart_Dust.Series["Series1"].Points.Count > 20)
                {
                    Chart_Dust.Series["Series1"].Points.RemoveAt(0);
                    Chart_Co2.Series["Series1"].Points.RemoveAt(0);
                    Chart_Sound.Series["Series1"].Points.RemoveAt(0);
                    Chart_Wind.Series["Series1"].Points.RemoveAt(0);
                    Chart_Voc.Series["Series1"].Points.RemoveAt(0);
                    Chart_Temp.Series["Series1"].Points.RemoveAt(0);
                    Chart_Humid.Series["Series1"].Points.RemoveAt(0);

                    Chart_Dust.Series["Series2"].Points.RemoveAt(0);
                    Chart_Co2.Series["Series2"].Points.RemoveAt(0);
                    Chart_Sound.Series["Series2"].Points.RemoveAt(0);
                    Chart_Wind.Series["Series2"].Points.RemoveAt(0);
                    Chart_Voc.Series["Series2"].Points.RemoveAt(0);
                    Chart_Temp.Series["Series2"].Points.RemoveAt(0);
                    Chart_Humid.Series["Series2"].Points.RemoveAt(0);
                }


                //알람 트리거링
                if(float.Parse(sp_data[0])> Dust_TH)
                {
                    Button_Dust_Alram.BackColor = Color.LightSkyBlue;
                    dataGridView_Alram.Rows.Add(DateTime.Now.ToString("yyyy.MM.dd.HH:mm:ss"), "Dust", sp_data[0]);
                }
                if (float.Parse(sp_data[1]) > Co2_TH)
                {
                    Button_Co2_Alram.BackColor = Color.LightSkyBlue;
                    dataGridView_Alram.Rows.Add(DateTime.Now.ToString("yyyy.MM.dd.HH:mm:ss"), "Co2", sp_data[1]);
                }
                if (float.Parse(sp_data[3]) > Wind_TH)
                {
                    Button_Wind_Alram.BackColor = Color.LightSkyBlue;
                    dataGridView_Alram.Rows.Add(DateTime.Now.ToString("yyyy.MM.dd.HH:mm:ss"), "Wind", sp_data[3]);
                }
                if (float.Parse(sp_data[4]) > Voc_TH)
                {
                    Button_Voc_Alram.BackColor = Color.LightSkyBlue;
                    dataGridView_Alram.Rows.Add(DateTime.Now.ToString("yyyy.MM.dd.HH:mm:ss"), "Voc", sp_data[4]);
                }
                if (float.Parse(sp_data[5]) > Temperature_TH)
                {
                    Button_Temp_Alram.BackColor = Color.LightSkyBlue;
                    dataGridView_Alram.Rows.Add(DateTime.Now.ToString("yyyy.MM.dd.HH:mm:ss"), "Temperature", sp_data[5]);
                }
                if (float.Parse(sp_data[6]) > Humidity_TH)
                {
                    Button_Humi_Alram.BackColor = Color.LightSkyBlue;
                    dataGridView_Alram.Rows.Add(DateTime.Now.ToString("yyyy.MM.dd.HH:mm:ss"),"Humidity",sp_data[6]);
                }





            }
            else if (sp_data.Length > 10)   //FFT 발생
            {
                for (int i = 0; i < sp_data.Length - 3; i++)
                {
                    if (i % 2 == 0)
                        FFT_HZ[i / 2] =  sp_data[i];
                    else
                        FFT_Mag[i / 2] = sp_data[i];
                }

                FFT_main_Hz = sp_data[sp_data.Length - 2];
                FFT_Max_Value = sp_data[sp_data.Length - 3];
                string FFT_Hz_Save = "";
                string FFT_Mag_Save = "";
                for(int i = 0;i<FFT_HZ.Length-1;i++)
                {
                    FFT_Hz_Save += FFT_HZ[i]+",";
                    FFT_Mag_Save += FFT_Mag[i]+",";
                }
                int lengthdd = FFT_HZ.Length;
                FFT_Hz_Save += FFT_HZ[FFT_HZ.Length-1];
                FFT_Mag_Save += FFT_Mag[FFT_HZ.Length-1];

                DataGridView_FFT.Rows.Add(DateTime.Now.ToString("yyyy.MM.dd.HH:mm:ss"), FFT_main_Hz, FFT_Max_Value,FFT_Hz_Save,FFT_Mag_Save);
                
                Chart_FFT.Series["Series1"].Points.Clear();
                Lable_FFT_main.Text = FFT_main_Hz;
                for (int i = 0; i < FFT_HZ.Length - 1; i++)
                {
                    Chart_FFT.Series["Series1"].Points.AddXY(float.Parse(FFT_HZ[i]), float.Parse(FFT_Mag[i]));
                }
                Button_FFT_Alram.BackColor = Color.LightSkyBlue;
            }
        }

        private void Button_Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text file(*.txt)|*.txt|CSV file(*.csv)|*.csv";
            sfd.FileName = DateTime.Now.ToString("yyyyMMddhhmm");
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName, true, Encoding.Default))
                {
                    sw.WriteLine(CSV_Data);
                }
            }
        }

        private void DataGridView_FFT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string Disp_Main_Hz = DataGridView_FFT.Rows[this.DataGridView_FFT.CurrentCellAddress.Y].Cells[1].Value.ToString();
            string Disp_Hz = DataGridView_FFT.Rows[this.DataGridView_FFT.CurrentCellAddress.Y].Cells[3].Value.ToString();
            string Disp_Mag = DataGridView_FFT.Rows[this.DataGridView_FFT.CurrentCellAddress.Y].Cells[4].Value.ToString();

            string[] Disp_Hz_splite = Disp_Hz.Split(',');
            string[] Disp_Mag_splite = Disp_Mag.Split(',');

            Chart_FFT.Series["Series1"].Points.Clear();
            Lable_FFT_main.Text = Disp_Main_Hz;
            for (int i = 0; i < Disp_Hz_splite.Length - 1; i++)
            {
                Chart_FFT.Series["Series1"].Points.AddXY(float.Parse(Disp_Hz_splite[i]), float.Parse(Disp_Mag_splite[i]));
            }
        }

        private void Button_FFT_Alram_Click(object sender, EventArgs e)
        {
            Button_FFT_Alram.BackColor = Color.Black;
        }

        private void Button_Dust_Alram_Click(object sender, EventArgs e)
        {
            Button_Dust_Alram.BackColor = Color.Black;
        }

        private void Button_Co2_Alram_Click(object sender, EventArgs e)
        {
            Button_Co2_Alram.BackColor = Color.Black;
        }

        private void Button_Wind_Alram_Click(object sender, EventArgs e)
        {
            Button_Wind_Alram.BackColor = Color.Black;
        }

        private void Button_Voc_Alram_Click(object sender, EventArgs e)
        {
            Button_Voc_Alram.BackColor = Color.Black;
        }

        private void Button_Temp_Alram_Click(object sender, EventArgs e)
        {
            Button_Temp_Alram.BackColor = Color.Black;
        }

        private void Button_Humi_Alram_Click(object sender, EventArgs e)
        {
            Button_Humi_Alram.BackColor = Color.Black;
        }



        //FFT 버튼

        private void Button_FFT_Clear_Click(object sender, EventArgs e)
        {
            DataGridView_FFT.Rows.Clear();
        }

        private void Button_FFT_Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text file(*.txt)|*.txt|CSV file(*.csv)|*.csv";
            sfd.FileName = DateTime.Now.ToString("yyyyMMddhhmm")+"_FFT";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.Default))
                {
                    int rowCount = DataGridView_FFT.Rows.Count;
                    string header = "";
                    for (int j = 0; j < DataGridView_FFT.Columns.Count; j++)
                    {
                        header += DataGridView_FFT.Columns[j].HeaderText+',';
                    }
                    sw.WriteLine(header);
                    for (int i=0;i<rowCount;i++)
                    {
                        List<string> strList = new List<string>();
                        for (int j=0; j<DataGridView_FFT.Columns.Count;j++)
                        {
                            strList.Add(DataGridView_FFT[j, i].Value.ToString());
                        }
                        string[] strArray = strList.ToArray();
                        string strcsvData = strArray[0] +','+strArray[1] + ',' +strArray[2] + strArray[3] + strArray[4];
                         
                        sw.WriteLine(strcsvData);
                    }
                    sw.Close();
                }
            }
            
        }

        // 알람 버튼

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView_Alram.Rows.Clear();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text file(*.txt)|*.txt|CSV file(*.csv)|*.csv";
            sfd.FileName = DateTime.Now.ToString("yyyyMMddhhmm") + "_Alram_log";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.Default))
                {
                    int rowCount = dataGridView_Alram.Rows.Count;
                    string header = "";
                    for (int j = 0; j < dataGridView_Alram.Columns.Count; j++)
                    {
                        header += dataGridView_Alram.Columns[j].HeaderText + ',';
                    }
                    sw.WriteLine(header);
                    for (int i = 0; i < rowCount; i++)
                    {
                        List<string> strList = new List<string>();
                        for (int j = 0; j < dataGridView_Alram.Columns.Count; j++)
                        {
                            strList.Add(dataGridView_Alram[j, i].Value.ToString());
                        }
                        string[] strArray = strList.ToArray();
                        string strcsvData = strArray[0] + ',' + strArray[1] + ',' + strArray[2];

                        sw.WriteLine(strcsvData);
                    }
                    sw.Close();
                }
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            Form2 form = new Form2(this);
            form.Threshold_Value_change(Dust_TH,Co2_TH,Sound_Th,Wind_TH,Voc_TH,Temperature_TH,Humidity_TH);
            form.Show();

        }

        
    }

}
