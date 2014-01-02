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
    public partial class SampleTime : Form
    {

        int collection_time = 30;

        public SampleTime()
        {
            InitializeComponent();
        }

        private void SampleTime_Load(object sender, EventArgs e)
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\ArgosQuant");
            if (regKey != null)
            {
                collection_time = (int)regKey.GetValue("collection_time", 30);
            }

            if (collection_time == 30)
            {
                opt30Sec.Checked = true;
                optInst.Checked = false;
                opt1Min.Checked = false;
                opt5Min.Checked = false;
            }
            else if (collection_time == 1)
            {
                opt30Sec.Checked = false;
                optInst.Checked = true;
                opt1Min.Checked = false;
                opt5Min.Checked = false;
            }
            else if (collection_time == 60)
            {
                opt30Sec.Checked = false;
                optInst.Checked = false;
                opt1Min.Checked = true;
                opt5Min.Checked = false;
            }
            else if (collection_time == 300)
            {
                opt30Sec.Checked = false;
                optInst.Checked = false;
                opt1Min.Checked = false;
                opt5Min.Checked = true;
            }
            else
            {
                opt30Sec.Checked = true;
                optInst.Checked = false;
                opt1Min.Checked = false;
                opt5Min.Checked = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegistryKey regKey = Registry.LocalMachine.CreateSubKey(@"Software\ArgosQuant");
            if (regKey != null)
            {
                regKey.SetValue("collection_time", collection_time);
            }
        }

        private void optInst_CheckedChanged(object sender, EventArgs e)
        {
            collection_time = 1;
        }

        private void opt1Min_CheckedChanged(object sender, EventArgs e)
        {
            collection_time = 60;
        }

        private void opt5Min_CheckedChanged(object sender, EventArgs e)
        {
            collection_time = 300;
        }

        private void opt30Sec_CheckedChanged(object sender, EventArgs e)
        {
            collection_time = 30;
        }
    }
}