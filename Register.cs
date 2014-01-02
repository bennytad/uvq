using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace UV_Quant
{
    public partial class Register : Form
    {

        public string serial_number;

        public Register(string serial_number)
        {
            InitializeComponent();
            if (serial_number == null)
            {
                RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\ArgosQuant");
                if (regKey == null)
                {
                    regKey = Registry.LocalMachine.CreateSubKey(@"Software\ArgosQuant");
                }

                this.serial_number = (string)regKey.GetValue("spect_serial_number", null);
            }
            else
            {
                this.serial_number = serial_number;
            }

            if (this.serial_number == null || this.serial_number.Trim() == "")
            {
                MessageBox.Show("You need a spectrometer to activate!");
                txt1.Enabled = false;
                txt2.Enabled = false;
                txt3.Enabled = false;
                txt4.Enabled = false;
                btnOk.Enabled = false;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string soft_serial = txt1.Text.Trim() + txt2.Text.Trim() + txt3.Text.Trim() + txt4.Text.Trim();
            if (Util.isCorrectPassword(soft_serial, serial_number))
            {
                RegistryKey regKey = Registry.LocalMachine.CreateSubKey(@"Software\ArgosQuant");
                if (regKey != null)
                {
                    regKey.SetValue("soft_serial", soft_serial);
                    regKey.SetValue("spect_serial_number", serial_number);
                    MessageBox.Show("You have succesfully activated your copy of UV Quant!");
                }
            }
            else
            {
                MessageBox.Show("That was not the correct serial number!");
            }
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void txt1_TextChanged(object sender, EventArgs e)
        {
            if (txt1.Text.Length >= 4)
            {
                txt2.Focus();
            }
        }

        private void txt2_TextChanged(object sender, EventArgs e)
        {
            if (txt2.Text.Length >= 4)
            {
                txt3.Focus();
            }
        }

        private void txt3_TextChanged(object sender, EventArgs e)
        {
            if (txt3.Text.Length >= 4)
            {
                txt4.Focus();
            }
        }
    }
}