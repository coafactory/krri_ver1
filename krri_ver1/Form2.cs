using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace krri_ver1
{
    public partial class Form2 : Form
    {
        private Form1 form1;
        public Form2(Form1 form)
        {
            InitializeComponent();
            form1 = form;
        }

        public void Threshold_Value_change(float Dust, float Co2, float Sound, float Wind, float Voc, float Temperature, float Humidity)
        {
            Dust_Value.Text = Convert.ToString(Dust);
            Co2_Value.Text = Convert.ToString(Co2);
            Sound_Value.Text = Convert.ToString(Sound);
            Wind_value.Text = Convert.ToString(Wind);
            Voc_Value.Text = Convert.ToString(Voc);
            Temperature_Value.Text = Convert.ToString(Temperature);
            Humidity_Value.Text = Convert.ToString(Humidity);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form1.Threshold_Value(Dust_Value.Text, Co2_Value.Text, Sound_Value.Text, Wind_value.Text, Voc_Value.Text, Temperature_Value.Text, Humidity_Value.Text);
            form1.Send_dB_change(Sound_Value.Text);
            this.Close();
        }
    }
}
