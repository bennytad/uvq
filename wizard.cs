using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UV_Quant
{
    public partial class wizard : Form
    {
        private static PLSProcessor processor = null;
        private DataGridView cm_datagrid;
        private DataGridView rep_datagrid;

        public wizard(PLSProcessor main_processor, DataGridView main_cm_datagrid, DataGridView main_rep_datagrid)
        {
            InitializeComponent();
            processor = main_processor;
            this.cm_datagrid = main_cm_datagrid;
            this.rep_datagrid = main_rep_datagrid;
        }
        
        public wizard()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            DialogResult dr = new DialogResult();
            gas_selector gasselector = new gas_selector(processor.selected_gases);
            dr = gasselector.ShowDialog(); 

            if (DialogResult.Cancel == dr)
            {
                return;
            }

            //for each check box, whose name is in the gas_values string array
            //check whether it is checked or not, if it is, add it to the gas library array
            int current_lib_index = 0;
            
            ArrayList temp_selected_gases = new ArrayList();

            processor.selected_gases = new int[processor.libraries.Length][];
            for (int i = 0; i < gasselector.gas_values.Length; ++i)
            {
                CheckBox temp_check_box = (CheckBox)(gasselector.Controls.Find(gasselector.gas_values[i], true)[0]);
                if (temp_check_box.Checked)
                {
                    processor.multiple_libraries = true;
                    processor.library_mode = LibraryMode.SELECTEDGASES;
                    string[] lib_gas_index = gasselector.gas_values[i].Split(new char[] { '_' });
                    if (current_lib_index != int.Parse(lib_gas_index[0]))
                    {
                        if (temp_selected_gases.Count != 0)
                        {
                            int[] temp = new int[temp_selected_gases.Count];
                            temp_selected_gases.CopyTo(temp);
                            processor.selected_gases[current_lib_index] = temp;
                        }
                        current_lib_index = int.Parse(lib_gas_index[0]);
                        temp_selected_gases = new ArrayList();
                    }

                    temp_selected_gases.Add(int.Parse(lib_gas_index[1]));

                }
            }
            if (temp_selected_gases.Count != 0)
            {
                int[] temp = new int[temp_selected_gases.Count];
                temp_selected_gases.CopyTo(temp);
                processor.selected_gases[current_lib_index] = temp;
            }
            processor.createTable(cm_datagrid, rep_datagrid);
            gasselector.Dispose();

        }
    }
}
