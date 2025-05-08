namespace krri_ver1
{
    partial class Com_para
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Combobox_Parity = new System.Windows.Forms.ComboBox();
            this.Combobox_Stop = new System.Windows.Forms.ComboBox();
            this.Combobox_Data = new System.Windows.Forms.ComboBox();
            this.Combobox_Baud = new System.Windows.Forms.ComboBox();
            this.Combobox_Com = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "COM Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Baud Rate";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Data bits";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "Stop bits";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "Parity bits";
            // 
            // Combobox_Parity
            // 
            this.Combobox_Parity.FormattingEnabled = true;
            this.Combobox_Parity.Items.AddRange(new object[] {
            "None",
            "Even",
            "Odd"});
            this.Combobox_Parity.Location = new System.Drawing.Point(117, 160);
            this.Combobox_Parity.Name = "Combobox_Parity";
            this.Combobox_Parity.Size = new System.Drawing.Size(160, 20);
            this.Combobox_Parity.TabIndex = 14;
            this.Combobox_Parity.Text = "None";
            // 
            // Combobox_Stop
            // 
            this.Combobox_Stop.FormattingEnabled = true;
            this.Combobox_Stop.Items.AddRange(new object[] {
            "One",
            "Two"});
            this.Combobox_Stop.Location = new System.Drawing.Point(117, 126);
            this.Combobox_Stop.Name = "Combobox_Stop";
            this.Combobox_Stop.Size = new System.Drawing.Size(160, 20);
            this.Combobox_Stop.TabIndex = 13;
            this.Combobox_Stop.Text = "One";
            // 
            // Combobox_Data
            // 
            this.Combobox_Data.FormattingEnabled = true;
            this.Combobox_Data.Items.AddRange(new object[] {
            "6",
            "7",
            "8"});
            this.Combobox_Data.Location = new System.Drawing.Point(117, 92);
            this.Combobox_Data.Name = "Combobox_Data";
            this.Combobox_Data.Size = new System.Drawing.Size(160, 20);
            this.Combobox_Data.TabIndex = 12;
            this.Combobox_Data.Text = "8";
            // 
            // Combobox_Baud
            // 
            this.Combobox_Baud.FormattingEnabled = true;
            this.Combobox_Baud.Items.AddRange(new object[] {
            "9600",
            "19200",
            "28800",
            "38400",
            "57600",
            "76800",
            "115200"});
            this.Combobox_Baud.Location = new System.Drawing.Point(117, 58);
            this.Combobox_Baud.Name = "Combobox_Baud";
            this.Combobox_Baud.Size = new System.Drawing.Size(160, 20);
            this.Combobox_Baud.TabIndex = 11;
            this.Combobox_Baud.Text = "115200";
            // 
            // Combobox_Com
            // 
            this.Combobox_Com.FormattingEnabled = true;
            this.Combobox_Com.Location = new System.Drawing.Point(117, 24);
            this.Combobox_Com.Name = "Combobox_Com";
            this.Combobox_Com.Size = new System.Drawing.Size(82, 20);
            this.Combobox_Com.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(205, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Change Parameter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(29, 192);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(248, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Com_para
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 236);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Combobox_Parity);
            this.Controls.Add(this.Combobox_Stop);
            this.Controls.Add(this.Combobox_Data);
            this.Controls.Add(this.Combobox_Baud);
            this.Controls.Add(this.Combobox_Com);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Name = "Com_para";
            this.Text = "Com_para";
            this.Load += new System.EventHandler(this.Com_para_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox Combobox_Parity;
        private System.Windows.Forms.ComboBox Combobox_Stop;
        private System.Windows.Forms.ComboBox Combobox_Data;
        private System.Windows.Forms.ComboBox Combobox_Baud;
        private System.Windows.Forms.ComboBox Combobox_Com;
        private System.Windows.Forms.Button button1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button button2;
    }
}