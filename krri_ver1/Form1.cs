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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            Combobox_Com.Items.Clear();
            Combobox_Com.Items.AddRange(ports);
        }

        private void Button_connect_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = Combobox_Com.Text;
                serialPort1.BaudRate = Convert.ToInt32(Combobox_Baud.Text);
                serialPort1.DataBits = Convert.ToInt32(Combobox_Data.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Combobox_Stop.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), Combobox_Parity.Text);
                serialPort1.DtrEnable = true;
                serialPort1.RtsEnable = true;

                serialPort1.Open();
                serialPort1.DiscardInBuffer();
                Thread.Sleep(2000);
                progressBar1.Value = 100;
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
                progressBar1.Value = 0;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            Combobox_Com.Items.Clear();
            Combobox_Com.Items.AddRange(ports);
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

            if (sp_data.Length > 5)
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

                Chart_Dust.Series["Series1"].Points.Add(Int32.Parse(sp_data[0]));
                Chart_Co2.Series["Series1"].Points.Add(Int32.Parse(sp_data[1]));
                Chart_Sound.Series["Series1"].Points.Add(Int32.Parse(sp_data[2]));
                Chart_Wind.Series["Series1"].Points.Add(Int32.Parse(sp_data[3]));
                Chart_Vos.Series["Series1"].Points.Add(Int32.Parse(sp_data[4]));

                if (Chart_Dust.Series["Series1"].Points.Count>10)
                {
                    Chart_Dust.Series["Series1"].Points.RemoveAt(0);
                    Chart_Co2.Series["Series1"].Points.RemoveAt(0);
                    Chart_Sound.Series["Series1"].Points.RemoveAt(0);
                    Chart_Wind.Series["Series1"].Points.RemoveAt(0);
                    Chart_Vos.Series["Series1"].Points.RemoveAt(0);
                }

                DataGridView_FFT.Rows.Add(DateTime.Now.ToString("yyyy.MM.dd.HH:mm:ss"),sp_data[0], sp_data[1], sp_data[2], sp_data[3], sp_data[4]);
                DataGridView_FFT.Rows.


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

    }

}
