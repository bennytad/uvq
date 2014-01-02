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
    public partial class OxyParameters : Form
    {
        public OxyParameters()
        {
            InitializeComponent();

            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\ArgosQuant");
            if (regKey != null)
            {
                txtStart.Text = ((int)regKey.GetValue("start_integral_pixel", 0)).ToString();
                txtEnd.Text = ((int)regKey.GetValue("end_integral_pixel", 0)).ToString();
                txtLowPer.Text = Double.Parse((string)regKey.GetValue("OXLowRef", "0")).ToString();
                txtHighPer.Text = Double.Parse((string)regKey.GetValue("OXHighRef", "21")).ToString();
                txtInterval.Text = ((int)regKey.GetValue("calibration_interval", 240)).ToString();
                txtDuration.Text = ((int)regKey.GetValue("calibration_duration", 5)).ToString();
                txtCollectInterval.Text = ((int)regKey.GetValue("data_interval", 0)).ToString();
                chkEnabled.Checked = Util.getBoolValue(regKey, "calibration_enabled", false);
                chkCommandProcess.Checked = Util.getBoolValue(regKey,"command_processing", false);
                txtCommandInterval.Text = ((int)regKey.GetValue("command_timer", 1000)).ToString();
                txtCommandFile.Text= (string)regKey.GetValue("command_file", "");
            }
        }

        public bool validate()
        {
            if (!Util.isNumeric(txtStart.Text.Trim()) ||
                !Util.isNumeric(txtEnd.Text.Trim()))
            {
                return false;
            }

            if (!chkCommandProcess.Checked &&
                Util.isNumeric(txtDuration.Text.Trim()) &&
                Util.isNumeric(txtInterval.Text.Trim()) &&
                Util.isNumeric(txtHighPer.Text.Trim()) &&
                Util.isNumeric(txtLowPer.Text.Trim()) &&
                Util.isNumeric(txtCollectInterval.Text.Trim())
                )
            {
                return true;
            }
            else if (chkCommandProcess.Checked)
            {
                if (Util.isNumeric(txtCommandInterval.Text.Trim()) &&
                    txtCommandFile.Text.Trim() != "")
                {
                    return true;
                }
            }
            return false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                RegistryKey regKey = Registry.LocalMachine.CreateSubKey(@"Software\ArgosQuant");
                if (regKey != null)
                {
                    regKey.SetValue("start_integral_pixel", 
                        Convert.ToInt32(txtStart.Text.Trim()));
                    regKey.SetValue("end_integral_pixel", 
                        Convert.ToInt32(txtEnd.Text.Trim()));

                    if (!chkCommandProcess.Checked)
                    {
                        regKey.SetValue("OXLowRef",
                            Convert.ToDouble(txtLowPer.Text.Trim()));
                        regKey.SetValue("OXHighRef",
                            Convert.ToDouble(txtHighPer.Text.Trim()));
                        regKey.SetValue("calibration_interval",
                            Convert.ToInt32(txtInterval.Text.Trim()));
                        regKey.SetValue("calibration_duration",
                            Convert.ToInt32(txtDuration.Text.Trim()));
                        regKey.SetValue("data_interval",
                            Convert.ToInt32(txtCollectInterval.Text.Trim()));
                        regKey.SetValue("calibration_enabled", chkEnabled.Checked);
                        regKey.SetValue("command_processing", false);
                    }
                    else
                    {
                        regKey.SetValue("command_processing", true);
                        regKey.SetValue("command_timer", Convert.ToInt32(txtCommandInterval.Text.Trim()));
                        regKey.SetValue("command_file", txtCommandFile.Text.Trim());
                    }
                }
            }
            else
            {
                MessageBox.Show("Please specify correct parameters!\nNote: empty fields are now allowed.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void chkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnabled.Checked)
            {
                chkCommandProcess.Checked = false;
                txtCommandFile.Enabled = false;
                txtCommandInterval.Enabled = false;

                txtLowPer.Enabled = true;
                txtHighPer.Enabled = true;
                txtInterval.Enabled = true;
                txtDuration.Enabled = true;
                txtCollectInterval.Enabled = true;
            }
        }

        private void chkCommandProcess_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCommandProcess.Checked)
            {
                chkEnabled.Checked = false;

                txtCommandFile.Enabled = true;
                txtCommandInterval.Enabled = true;

                txtLowPer.Enabled = false;
                txtHighPer.Enabled = false;
                txtInterval.Enabled = false;
                txtDuration.Enabled = false;
                txtCollectInterval.Enabled = false;
            }
            else
            {
                txtCommandFile.Enabled = false;
                txtCommandInterval.Enabled = false;

                txtLowPer.Enabled = true;
                txtHighPer.Enabled = true;
                txtInterval.Enabled = true;
                txtDuration.Enabled = true;
                txtCollectInterval.Enabled = true;
            }
        }

        private void OxyParameters_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Opens a file dialogue to get the command file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandFile_Click(object sender, EventArgs e)
        {
             openCommandFile.FileName = "";
             DialogResult dr = openCommandFile.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (openCommandFile.FileName != null &&
                    openCommandFile.FileName != "")
                {
                    txtCommandFile.Text = openCommandFile.FileName;
                }
            }
        }

        private void txtCollectInterval_TextChanged(object sender, EventArgs e)
        {

        }
    }
}