using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UV_Quant
{
    public partial class DeleteMe : Form
    {
        AvantesSpectrometer avspect = null;
        public DeleteMe()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            avspect = new AvantesSpectrometer(0,0);
            if (avspect.stabilishConnection(this.Handle))
            {
                MessageBox.Show("Connected!");
            }
            else
            {
                MessageBox.Show("Failed!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] nrs = avspect.getSerialsNrs();
            if (nrs != null)
            {
                for (int i = 0; i < nrs.Length; ++i)
                {
                    txtSerials.Text += "+" + nrs[i];
                }
            }
            else
            {
                txtSerials.Text = "NULL";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            avspect.setIntegrationTime(3);
            avspect.connectToSpectrometer();
            avspect.collectData();
        }
    }
}