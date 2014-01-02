using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace UV_Quant
{
    public partial class FactorInputForAutoCalibration : Form
    {

        private String[] library_names;
        private String[] keys;
        private double[] sde;
        private double path_length;
        private Hashtable tresholds;

        public FactorInputForAutoCalibration(String[] library_names, String[] keys, double[] sde, double path_length)
        {
            this.library_names = library_names;
            this.sde = sde;
            this.keys = keys;
            this.path_length = path_length;

            InitializeComponent();
            buildTable();
        }

        public FactorInputForAutoCalibration()
        {
            InitializeComponent();
        }

        private void CalibrationGases_Load(object sender, EventArgs e)
        {

        }

        private void buildTable()
        {
            if (library_names == null || library_names.Length <= 0)
            {
                return;
            }
            
            DataGridViewCheckBoxColumn selected_bolCol = new DataGridViewCheckBoxColumn();

            while (tblCalibGases.ColumnCount != 0)
            {
                tblCalibGases.Columns.RemoveAt(0);
            }

            tblCalibGases.Columns.Add("Name", "Gas");
            tblCalibGases.Columns.Add("Standard Dev.", "Standard Dev.");
            tblCalibGases.Columns.Add("Factor", "Factor");

            tblCalibGases.Columns[0].Width = (tblCalibGases.Width / 3) - 3;
            tblCalibGases.Columns[1].Width = (tblCalibGases.Width / 3) - 3;
            tblCalibGases.Columns[2].Width = (tblCalibGases.Width / 3) - 3;

            tblCalibGases.Columns[0].ReadOnly = true;
            tblCalibGases.Columns[1].ReadOnly = true;
            tblCalibGases.Columns[2].ReadOnly = false;

            for (int j = 0; j < library_names.Length; ++j)
            {
                    tblCalibGases.Rows.Add();

                    tblCalibGases[0, j].Value = library_names[j];
                    tblCalibGases[0, j].Tag = keys[j];
                    tblCalibGases[1, j].Value = sde[j].ToString();
                    tblCalibGases[2, j].Value = "1";

                    tblCalibGases[0,j].Style.BackColor = System.Drawing.Color.Gray;
                    tblCalibGases[1,j].Style.BackColor = System.Drawing.Color.Gray;
                    tblCalibGases[2,j].Style.BackColor = System.Drawing.Color.White;
            }
        }

        /// <summary>
        /// This method is fired up when the user clicks OK to save the selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!validate())
            {
                MessageBox.Show("Your entry was not saved because you did not specify valid concentration limits!");
                return;
            }

            string treshold_values = "";
            for (int i = 0; i < tblCalibGases.RowCount; ++i)
            {
                double input_factor = Double.Parse(tblCalibGases[2, i].Value.ToString());
                //convert to parts per million from parts per billion
                double final_value = input_factor * sde[i] * path_length/1000;
                treshold_values += (string)tblCalibGases[0, i].Tag + Constants.Delimiters.VALUE_DELIMITER[0] +
                                        final_value.ToString() + Constants.Delimiters.SET_DELIMITER[0];
            }

            if (treshold_values.Length > 0)
            {
                treshold_values = treshold_values.Remove(treshold_values.Length - 1);
            }

            Util.saveValueInRegistry(treshold_values, Constants.UVQuantRegistryKeys.TRESHOLDS);
        }

        /// <summary>
        /// This method will verify that all the selected gasses have a numeric concentration limit has 
        /// been specified. 
        /// </summary>
        /// <returns></returns>
        public bool validate()
        {
            bool validated = true;

            for (int i = 0; i < tblCalibGases.RowCount; ++i)
            {
                String current_value = (String)tblCalibGases[2,i].Value;
                if (!Util.isNumeric(current_value))
                {
                    validated = false;
                    break;
                }
            }

            return validated;
        }
    }
}