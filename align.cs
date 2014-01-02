using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using System.Collections;

namespace UV_Quant
{
    public partial class align : Form
    {
        public Spectrometer current_spectrometer;
        public DataMatrix current_data;
        private double max_data = 0;
        decimal old_int_value = 3;

        private bool scanning = false;
        public align()
        {
            InitializeComponent();
        }

        public align(ref Spectrometer current_spect)
        {
            InitializeComponent();
            current_spectrometer = current_spect;
        }

        private void align_Load(object sender, EventArgs e)
        {
            if (current_spectrometer.integration_time == 0)
            {
                current_spectrometer.integration_time = (int)numericUpDown1.Minimum;
            }
            numericUpDown1.Value = current_spectrometer.integration_time;
            zg1.GraphPane = new GraphPane(zg1.GraphPane.Rect, "Argos Scientific Light Data", "Wavelength (nm)", "Light count");
        }

        public void scannData()
        {
            while (scanning)
            {
                System.Windows.Forms.Application.DoEvents();
                current_spectrometer.collectData();
                max_data = current_spectrometer.max_uv_count;//-10000;
                double[] data = current_spectrometer.spectra;
                double[,] values = new double[data.Length, 1];
               for (int i = 0; i < data.Length; ++i)
                {
                    values[i, 0] = data[i];// *10E45;
                }
                System.Windows.Forms.Application.DoEvents();
                current_data = new DataMatrix(current_spectrometer.wavelength, values, data.Length, new Hashtable());
                displayGraph(current_data.getPointPairList());
            }
        }

        private void displayGraph(PointPairList list)
        {
            System.Windows.Forms.Application.DoEvents();
            zg1.GraphPane.CurveList.Clear();// = "Argos Scientific Inc.";
            // zg1.GraphPane = reserver_pane.Clone(); 
            ZedGraph.GraphPane graph_pane = zg1.GraphPane;
            
            LineItem myCurve = graph_pane.AddCurve("Absorbance", list, Color.Red,
                                           ZedGraph.SymbolType.None);
            zg1.AxisChange();
            zg1.Refresh();
            lblUVCount.Text = max_data.ToString("0.000");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
                scanning = true;
                scannData();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            scanning = false;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int current_integration_time = Convert.ToInt32(
                    numericUpDown1.Value.ToString());
                if (current_integration_time >= numericUpDown1.Minimum &&
                    current_integration_time <= numericUpDown1.Maximum)
                {
                    current_spectrometer.integration_time = current_integration_time;
                    current_spectrometer.integration_time = current_integration_time;
                    old_int_value =current_integration_time;
                }
                else
                    numericUpDown1.Value = old_int_value;
            }
            catch (Exception int_assignment_err)
            {
                //do nothing for now
                numericUpDown1.Value = old_int_value;
            }
        }
    }
}