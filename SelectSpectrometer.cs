using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UV_Quant
{
    public partial class SelectSpectrometer : Form
    {
        Spectrometer current_spectrometer;

        public SelectSpectrometer()
        {
            InitializeComponent();
        }

        public SelectSpectrometer(ref Spectrometer current_spect)
        {
            InitializeComponent();
            current_spectrometer = current_spect;
        }


        private void lstType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstSn.SelectedIndex = lstType.SelectedIndex;
        }

        private void SelectSpectrometer_Load(object sender, EventArgs e)
        {
            if (current_spectrometer is AvantesSpectrometer)
            {
                ((AvantesSpectrometer)current_spectrometer).getSpectrometers(lstType, lstSn);
            }
            else if (current_spectrometer is Spectrometer)
            {
                Spectrometer.getSpectrometers(lstType, lstSn);
            }
        }

        private void cmdSelectSpectrometer_Click(object sender, EventArgs e)
        {
            current_spectrometer.selected_spectrometer = lstSn.SelectedIndex;
        }

        private void lstSn_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstType.SelectedIndex = lstSn.SelectedIndex;
        }

    }
}