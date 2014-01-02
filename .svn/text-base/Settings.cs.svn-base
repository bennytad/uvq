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
    public partial class Settings : Form
    {
        int collection_time = 30;
        int dynamic_int_time = 0;
        int dynamic_bk_update = 0;
        int box_car = 0;
        int shift_amount;
        double minimum_signal = 2000;
        double maximum_signal = 3500;
        bool flag_data = false;
        bool autorun = false;

        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\ArgosQuant");
            if (regKey != null)
            {
                collection_time = (int)regKey.GetValue("collection_time", 30);
                box_car = (int)regKey.GetValue("box_car", 0);
                dynamic_int_time = (int)regKey.GetValue("integration_update", 0);
                dynamic_bk_update = (int)regKey.GetValue("background_update", 1);
                shift_amount = (int)regKey.GetValue("shift_amount", 0);
                minimum_signal = Double.Parse((string)regKey.GetValue("minimum_signal", DefaultValues.MINIMUM_SIGNAL));
                maximum_signal = Double.Parse((string)regKey.GetValue("maximum_signal", DefaultValues.MAXIMUM_SIGNAL));
                autorun = Util.getBoolValue(regKey, "autorun", DefaultValues.AUTORUN);
                flag_data = Util.getBoolValue(regKey, "flag_data", DefaultValues.FLAG_DATA);
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

            chkFlagData.Checked = flag_data;
            chkAutoRun.Checked = autorun;

            if (dynamic_bk_update == 1)
                chkDynBkgdUp.Checked = true;
            if (dynamic_int_time == 1)
                chkDynIntTime.Checked = true;

            txtBoxCar.Text = Convert.ToString(box_car);
            txtShift_amount.Text = Convert.ToString(shift_amount);
            txtMinSigStr.Text = Convert.ToString(minimum_signal);
            txtMaxSigStr.Text = Convert.ToString(maximum_signal);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            shift_amount = Convert.ToInt32(txtShift_amount.Text);
            maximum_signal = Convert.ToDouble(txtMaxSigStr.Text);
            minimum_signal = Convert.ToDouble(txtMinSigStr.Text);

            RegistryKey regKey = Registry.LocalMachine.CreateSubKey(@"Software\ArgosQuant");
            if (regKey != null)
            {
                regKey.SetValue("collection_time", collection_time);
                if (chkDynIntTime.Checked)
                    regKey.SetValue("integration_update", 1);
                else
                    regKey.SetValue("integration_update", 0);

                if(chkDynBkgdUp.Checked)
                    regKey.SetValue("background_update", 1);
                else
                {
                    regKey.SetValue("background_update", 0);
                }

                regKey.SetValue("flag_data", chkFlagData.Checked);
                regKey.SetValue("autorun", chkAutoRun.Checked);
                regKey.SetValue("box_car", box_car);
                regKey.SetValue("shift_amount", shift_amount);
                regKey.SetValue("maximum_signal", maximum_signal);
                regKey.SetValue("minimum_signal", minimum_signal);
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {

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

        private void txtBoxCar_TextChanged(object sender, EventArgs e)
        {
            box_car = Convert.ToInt32(txtBoxCar.Text);
        }
    }
}