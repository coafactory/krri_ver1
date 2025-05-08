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

namespace krri_ver1
{
    public partial class Com_para : Form
    {
        private Form1 form1;                                            //form1 형의 form1 선언
        public Com_para(Form1 form)
        {
            InitializeComponent();
            form1 = form;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            Combobox_Com.Items.Clear();
            Combobox_Com.Items.AddRange(ports);
        }

        private void Com_para_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            Combobox_Com.Items.Clear();
            Combobox_Com.Items.AddRange(ports);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form1.Com_data(Combobox_Com.Text,Combobox_Baud.Text,Combobox_Data.Text,Combobox_Stop.Text,Combobox_Parity.Text);
            this.Close();
        }
    }
}
