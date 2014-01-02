using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UV_Quant
{
    public partial class gas_selector : Form
    {
        public int num_of_gases = 0;
        public string[] gas_values;
        //this is a copy of the selected gases index found
        //in the main console.
        public int[][] selected_gases;


        public gas_selector(int[][] selected_gases_main_copy)
        {
            this.selected_gases = selected_gases_main_copy;
            InitializeComponent();
        }


        /// <summary>
        /// This method is called during the initalization of this form. 
        /// This method will load all the gases existing in the current hard coded
        /// libraries and create a check box for each of them.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gas_selector_Load(object sender, EventArgs e)
        {
            LibraryMatrix temp_lib_matrix;
            String[] library_names = Libraries.getLibraryNames();

            LibraryMatrix[] libraries = new LibraryMatrix[library_names.Length];

            int column_width = this.Width/4;
            int row_height = warningContainer.Top + warningContainer.Height + 40;
            int column_number = 0;

            gas_values = new string[library_names.Length];

            int current_gas_index = -1;

            for (int i = 0; i < library_names.Length; ++i)
            {
                String current_library_data = Libraries.getLibrary(library_names[i]);
                temp_lib_matrix = new LibraryMatrix(current_library_data);
                libraries[i] = temp_lib_matrix;


                    CheckBox my_chb = new CheckBox();
                    my_chb.Appearance = Appearance.Normal;
                    if (temp_lib_matrix.getGas_name(0) == "Benzene")
                        my_chb.Text = library_names[i];
                    else
                        my_chb.Text = temp_lib_matrix.getGas_name(0);
                    
                    //we need to record the library and gas id combo for later 
                    //retreival
                    gas_values[++current_gas_index] = i + "_0";
                    
                    my_chb.Name = gas_values[current_gas_index];

                    my_chb.Left = column_width * column_number;
                    if (column_number == 0)
                    {
                        my_chb.Left = warningContainer.Left;
                    }
                    my_chb.Top = row_height;

                    if (column_number >= 3)
                    {
                        column_number = 0;
                        row_height += 30;
                    }
                    else
                    {
                        ++column_number;
                    }
                    my_chb.BackColor = Color.Transparent;
                    Controls.Add(my_chb);
            }

            //Once you have finished loading, populate all the check boxes based on the selected_gases value
            for (int i = 0; i < selected_gases.Length; ++i)
            {
                if (selected_gases[i] != null && selected_gases[i].Length != 0)
                {
                    for (int j = 0; j < selected_gases[i].Length; ++j)
                    {
                        CheckBox temp_chb = (CheckBox)Controls.Find(i + "_" + selected_gases[i][j], true)[0];
                        temp_chb.Checked = true;
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }
    }
}