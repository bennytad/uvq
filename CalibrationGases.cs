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
    public partial class CalibrationGases : Form
    {

        private LibraryMatrix[] matrices;
        private Hashtable tresholds;

        public CalibrationGases(LibraryMatrix[] lib_array)
        {
            this.matrices = lib_array;
            InitializeComponent();
            buildTable();
        }

        public CalibrationGases()
        {
            InitializeComponent();
        }

        private void CalibrationGases_Load(object sender, EventArgs e)
        {

        }

        private void buildTable()
        {
            String[] library_names = Libraries.getLibraryNames();

            if (matrices == null || matrices.Length <= 0)
            {
                return;
            }
            retreiveCalibrationGases();
            
            DataGridViewCheckBoxColumn selected_bolCol = new DataGridViewCheckBoxColumn();

            while (tblCalibGases.ColumnCount != 0)
            {
                tblCalibGases.Columns.RemoveAt(0);
            }


            tblCalibGases.Columns.Add(selected_bolCol);
            tblCalibGases.Columns[0].HeaderText = "Select Gas";
            tblCalibGases.Columns.Add("Name", "Gas");
            tblCalibGases.Columns.Add("Limit", "Limit");

            tblCalibGases.Columns[0].Width = (tblCalibGases.Width / 3) - 3;
            tblCalibGases.Columns[1].Width = (tblCalibGases.Width / 3) - 3;
            tblCalibGases.Columns[2].Width = (tblCalibGases.Width / 3) - 3;

            tblCalibGases.Columns[0].ReadOnly = false;
            tblCalibGases.Columns[1].ReadOnly = true;
            tblCalibGases.Columns[2].ReadOnly = true;

            int j = 0;

            for (int i = 0; i < matrices.Length; ++i)
            {
                    string key = i + "_0";
                    tblCalibGases.Rows.Add();
                  
                    //attached libindex_gasindex in the tag of the cell as a unique gas id to be
                    //associated with the admin table entry

                    if (matrices[i].getGas_name(0) == "Benzene")
                        tblCalibGases[1, j].Value = library_names[i];
                    else
                        tblCalibGases[1, j].Value = matrices[i].getGas_name(0);
                    
                    tblCalibGases[0,j].Style.BackColor = System.Drawing.Color.Gray;
                    tblCalibGases[1,j].Style.BackColor = System.Drawing.Color.Gray;
                    tblCalibGases[2,j].Style.BackColor = System.Drawing.Color.Gray;

                    if (tresholds[key] != null)
                    {
                        tblCalibGases[2, j].Value = Convert.ToString((double)tresholds[key]);
                        tblCalibGases[0, j].Value = true;
                        tblCalibGases[0, j].Style.BackColor = System.Drawing.Color.White;
                        tblCalibGases[1, j].Style.BackColor = System.Drawing.Color.White;
                        tblCalibGases[2, j].Style.BackColor = System.Drawing.Color.White;

                        tblCalibGases.Columns[2].ReadOnly = false;
                    }
                    else
                    {
                        tblCalibGases[0, j].Value = false;
                    }
                    tblCalibGases[2, j].Tag = key;
                    ++j;
            }
        }

        private void retreiveCalibrationGases()
        {
            tresholds = new Hashtable();

            string saved_values = Util.getStringInRegistry(
                                        Constants.UVQuantRegistryKeys.CALIBRATION_GASES,
                                        null);
            if (saved_values == null)
            {
                return;
            }

            string[] sets = saved_values.Split(Constants.Delimiters.SET_DELIMITER);

            if (sets == null)
            {
                return;
            }

            for (int i = 0; i < sets.Length; ++i)
            {
                string[] pairs = sets[i].Split(Constants.Delimiters.VALUE_DELIMITER);
                if (pairs != null && pairs.Length == 3)
                {
                    string lib_index = pairs[0];
                    string gas_index = pairs[1];
                    if(pairs[2] == "")
                    {
                        pairs[2] = "0";
                    }

                    double value = Double.Parse(pairs[2]);

                    string key = lib_index + "_" + gas_index;

                    tresholds.Add(key, value);
                }
            }
        }

        /// <summary>
        /// This method is fired up when a user clicks on the table. We only do something
        /// the clicking was done on the check box to select a gas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tblCalibGases_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                tblCalibGases.EndEdit();
                if ((bool)tblCalibGases[e.ColumnIndex, e.RowIndex].Value)
                {
                    tblCalibGases[0, e.RowIndex].Style.BackColor = System.Drawing.Color.White;
                    tblCalibGases[1, e.RowIndex].Style.BackColor = System.Drawing.Color.White;
                    tblCalibGases[2, e.RowIndex].Style.BackColor = System.Drawing.Color.White;

                    tblCalibGases[2, e.RowIndex].ReadOnly = false;
                }
                else
                {
                    tblCalibGases[0, e.RowIndex].Style.BackColor = System.Drawing.Color.Gray;
                    tblCalibGases[1, e.RowIndex].Style.BackColor = System.Drawing.Color.Gray;
                    tblCalibGases[2, e.RowIndex].Style.BackColor = System.Drawing.Color.Gray;

                    tblCalibGases[2, e.RowIndex].ReadOnly = true;
                }
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
                if ((bool)tblCalibGases[0, i].Value)
                {
                    treshold_values += (string)tblCalibGases[2, i].Tag + Constants.Delimiters.VALUE_DELIMITER[0] +
                                        (string)tblCalibGases[2, i].Value + Constants.Delimiters.SET_DELIMITER[0];
                }
            }

            if (treshold_values.Length > 0)
            {
                treshold_values = treshold_values.Remove(treshold_values.Length - 1);
            }

            Util.saveValueInRegistry(treshold_values, Constants.UVQuantRegistryKeys.CALIBRATION_GASES);
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
                if ((bool)tblCalibGases[0, i].Value)
                {
                    String current_value = (String)tblCalibGases[2,i].Value;
                    if (!Util.isNumeric(current_value))
                    {
                        validated = false;
                        break;
                    }
                }
            }

            return validated;
        }
    }
}