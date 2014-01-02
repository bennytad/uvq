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
    public partial class GasTreshold : Form
    {
        private LibraryMatrix[] matrices;
        private Hashtable tresholds;

        public GasTreshold(LibraryMatrix[] lib_array)
        {
            this.matrices = lib_array;
            InitializeComponent();
            buildTable();
        }

        public GasTreshold()
        {
            InitializeComponent();
        }

        private void GasTreshold_Load(object sender, EventArgs e)
        {
            //buildTable();
        }

        private void buildTable()
        {
            String[] library_names = Libraries.getLibraryNames();

            if (matrices == null || matrices.Length <= 0)
            {
                return;
            }
            retreiveTresholds();

            while (tblTreshold.ColumnCount != 0)
            {
                tblTreshold.Columns.RemoveAt(0);
            }

            tblTreshold.Columns.Add("Name", "Gas");
            tblTreshold.Columns.Add("Limit", "Limit (ppm-m)");

            tblTreshold.Columns[0].Width = (tblTreshold.Width / 2) - 3;
            tblTreshold.Columns[1].Width = (tblTreshold.Width / 2) - 3;


            tblTreshold.Columns[1].ReadOnly = false;
            tblTreshold.Columns[0].ReadOnly = true;
            int j = 0;

            for (int i = 0; i < matrices.Length; ++i)
            {
                string key = i + "_0";                   
                tblTreshold.Rows.Add();
                    //attached libindex_gasindex in the tag of the cell as a unique gas id to be
                    //associated with the admin table entry
                if (matrices[i].getGas_name(0) == "Benzene")
                    tblTreshold[0, j].Value = library_names[i];
                else                
                    tblTreshold[0, j].Value = matrices[i].getGas_name(0);
                
                if (tresholds[key] != null)                
                {                        
                    tblTreshold[1, j].Value = Convert.ToString((double)tresholds[key]);                    
                }
                 
                tblTreshold[1, j].Tag = key;                
                ++j;
            }
        }

        private void retreiveTresholds()
        {
            tresholds = new Hashtable();

            string saved_values = Util.getStringInRegistry(
                                        Constants.UVQuantRegistryKeys.TRESHOLDS, 
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
                    double value = Double.Parse(pairs[2]);

                    string key = lib_index + "_" + gas_index;

                    tresholds.Add(key, value);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string treshold_values = "";
            for (int i = 0; i < tblTreshold.RowCount; ++i)
            {
                if (tblTreshold[1, i].Value != null)
                {
                    treshold_values += (string)tblTreshold[1, i].Tag + Constants.Delimiters.VALUE_DELIMITER[0] +
                                        (string)tblTreshold[1, i].Value + Constants.Delimiters.SET_DELIMITER[0];
                }
            }

            if (treshold_values.Length > 0)
            {
                treshold_values = treshold_values.Remove(treshold_values.Length - 1);
            }

            Util.saveValueInRegistry(treshold_values, Constants.UVQuantRegistryKeys.TRESHOLDS);
        }
    }
}