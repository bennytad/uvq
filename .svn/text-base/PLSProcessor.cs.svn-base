using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using ZedGraph;
using System.Drawing;
using System.Xml.Serialization;
using System.Xml;

namespace UV_Quant
{
    public class PLSProcessor:AProcessor
    { 
        /// <summary>
        /// This initializes the PLSProcessor Object. Note, 
        /// this method should be called after retrieveRegistry
        /// method is called. 
        /// </summary>
        public override void init()
        {
            about_uv_quant.status = "Loading PLS library...";
            if (library_mode == LibraryMode.PREDEFINED)
            {
                String[] available_names = Libraries.names;
                
                bool available = false;
                for (int i = 0; i < available_names.Length; ++i)
                {
                    if(available_names[i].Equals(loaded_library_name))
                    {
                        available = true;
                        break;
                    }
                }
                
                if(!available)
                {
                    loaded_library_name = available_names[0];
                }

                String current_library_data = Libraries.getLibrary(loaded_library_name);
                lib_matrix = new LibraryMatrix(current_library_data);
                lib_matrix.library_name = loaded_library_name;

            }
            else if (library_mode == LibraryMode.ONFILE)
            {
                FileInfo main_lib_file = new FileInfo(Directory.GetCurrentDirectory() + "\\ArgosPlsLibrary.csv");

                lib_matrix = new LibraryMatrix(main_lib_file);
                lib_matrix.library_name = ON_FILE;
            }
        }

      
        /// <summary>
        /// generates an ArrayList of the library to be used by a combox
        /// </summary>
        /// <returns></returns>
        public new ArrayList getLibrayForComboBox()
        {
            String[] library_names = Libraries.getLibraryNames();
            
            ArrayList temp_libraries = new ArrayList();
            for (int i = 0; i < library_names.Length; ++i)
            {
               temp_libraries.Add(library_names[i]);
            }

            return temp_libraries;
        }

        /// <summary>
        /// This method is called when a library is loaded in memory
        /// such as during start up and during admin update of the library
        /// </summary>
        public override void createTable(DataGridView cm_datagrid, DataGridView rep_datagrid)
        {
            String[] library_names = Libraries.getLibraryNames();
            retreiveTresholds();

            while (cm_datagrid.ColumnCount != 0 &&
                rep_datagrid.ColumnCount != 0)
            {
                cm_datagrid.Columns.RemoveAt(0);
                rep_datagrid.Columns.RemoveAt(0);
            }

            //initialize table for reprocessing mode
            DataGridViewCheckBoxColumn rep_bolCol = new DataGridViewCheckBoxColumn();

            rep_datagrid.Columns.Add("Name", "Gas");
            rep_datagrid.Columns.Add("conc", "Concentration");
            rep_datagrid.Columns.Add(rep_bolCol);
            rep_datagrid.Columns[2].HeaderText = "Auto Background";
            rep_datagrid.Columns.Add("Limit", "Limit (ppb)");

            int j = 0;

            rep_datagrid.Columns[0].Width = (rep_datagrid.Width / 4) - 12;            
            rep_datagrid.Columns[1].Width = (rep_datagrid.Width / 4) - 12;
            rep_datagrid.Columns[2].Width = (rep_datagrid.Width / 4) - 12;
            rep_datagrid.Columns[3].Width = (rep_datagrid.Width / 4) - 12;


            rep_datagrid.Columns[1].ReadOnly = true;
            rep_datagrid.Columns[0].ReadOnly = true;
            rep_datagrid.Columns[3].ReadOnly = true;


            //if multiple libraries are used, we know the exact gases to use
            if (multiple_libraries)
            {
                for (int i = 0; i < libraries.Length; ++i)
                {
                    if (selected_gases[i] != null)
                    {
                        for (int k = 0; k < selected_gases[i].Length; ++k)
                        {
                            string key = i + "_" + k;
                            int current_gas_index = selected_gases[i][k];
                            rep_datagrid.Rows.Add();
                            //attached libindex_gasindex in the tag of the cell as a unique gas id to be
                            //associated with the admin table entry
                            if (libraries[i].getGas_name(current_gas_index) == "Benzene")
                                rep_datagrid[0, j].Value = library_names[i];
                            else
                                rep_datagrid[0, j].Value = libraries[i].getGas_name(current_gas_index);

                            rep_datagrid[3, j].Tag = key;
                            rep_datagrid[3, j].Style.BackColor = System.Drawing.Color.Gray;
                            if (gas_detection_limits[key] != null)
                            {
                                rep_datagrid[3, j].Value = Convert.ToString((double)gas_detection_limits[key]);
                            }
                            int current_id = Math.Abs(libraries[i].getGas_id(current_gas_index));
                            j = j + 1;
                        }//
                    }
                }
            }
            else
            {
                int lib_index = -1;

                while (++lib_index < libraries.Length &&
                    libraries[lib_index].library_name!=lib_matrix.library_name)
                {
                }

                    string key = lib_index + "_0";
                    rep_datagrid.Rows.Add();

                    if (lib_matrix.getGas_name(lib_index) == "Benzene")
                        rep_datagrid[0, j].Value = library_names[lib_index];
                    else                    
                        rep_datagrid[0, j].Value = lib_matrix.getGas_name(0);
                    
                    rep_datagrid[3, j].Style.BackColor = System.Drawing.Color.Gray;

                    rep_datagrid[3, j].Tag = key;
                    rep_datagrid[3, j].Style.BackColor = System.Drawing.Color.Gray;
                    if (gas_detection_limits[key] != null)
                    {
                        rep_datagrid[3, j].Value = Convert.ToString((double)gas_detection_limits[key]);
                    }
                    j = j + 1;
            }
            //  Now populate CM datagrid
            j = 0;
            DataGridViewCheckBoxColumn cm_bolCol = new DataGridViewCheckBoxColumn();
            
            cm_datagrid.Columns.Add("Name", "Gas");            
            cm_datagrid.Columns.Add("conc", "Concentration (ppb)");
            cm_datagrid.Columns.Add(cm_bolCol);
            cm_datagrid.Columns[2].HeaderText = "Auto Background";
            cm_datagrid.Columns.Add("Limit", "Limit (ppb)");
            

            cm_datagrid.Columns[0].Width = (rep_datagrid.Width / 4) - 12;
            cm_datagrid.Columns[1].Width = (rep_datagrid.Width / 4) - 12;
            cm_datagrid.Columns[2].Width = (rep_datagrid.Width / 4) - 12;
            cm_datagrid.Columns[3].Width = (rep_datagrid.Width / 4) - 12;

            cm_datagrid.Columns[0].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            cm_datagrid.Columns[1].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            cm_datagrid.Columns[3].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;

            cm_datagrid.Columns[0].ReadOnly = true;
            cm_datagrid.Columns[2].ReadOnly = true;
            cm_datagrid.Columns[1].ReadOnly = true;            
            cm_datagrid.Columns[3].ReadOnly = true;

            if (multiple_libraries)
            {
                for (int i = 0; i < libraries.Length; ++i)
                {
                    if (selected_gases[i] != null)
                    {
                        for (int k = 0; k < selected_gases[i].Length; ++k)
                        {
                            string key = i + "_" + k;

                            int current_gas_index = selected_gases[i][k];
                            cm_datagrid.Rows.Add();
                            
                            if (libraries[i].getGas_name(current_gas_index) == "Benzene")
                                cm_datagrid[0, j].Value = library_names[i];
                            else
                                cm_datagrid[0, j].Value = libraries[i].getGas_name(current_gas_index);


                            cm_datagrid[2, j].ReadOnly = true;
                            cm_datagrid[2, j].ToolTipText = "Limit of the gas must be set in order to select the auto-background option.";

                            cm_datagrid[3, j].Style.BackColor = System.Drawing.Color.Gray;                                
                                   
                            cm_datagrid[3, j].Tag = key;
                            cm_datagrid[3, j].Style.BackColor = System.Drawing.Color.Gray;
                            if (gas_detection_limits[key] != null)
                            {
                                cm_datagrid[3, j].Value = Convert.ToString((double)gas_detection_limits[key]);
                                cm_datagrid[2, j].ReadOnly = false;
                                cm_datagrid[2, j].ToolTipText = null;
                            }
                            int current_id = Math.Abs(libraries[i].getGas_id(current_gas_index));
                            j = j + 1;
                        }
                    }
                }
            }
            else
            {
                int lib_index = -1;

                while (++lib_index < libraries.Length &&
                    libraries[lib_index].library_name != lib_matrix.library_name)
                {
                }
                {
                }
 
                    string key = lib_index + "_0";
                    cm_datagrid.Rows.Add();
                    if (lib_matrix.getGas_name(lib_index) == "Benzene")
                        cm_datagrid[0, j].Value = library_names[lib_index];
                    else
                        cm_datagrid[0, j].Value = lib_matrix.getGas_name(0);

                    
                cm_datagrid[2, j].ReadOnly = true;
                cm_datagrid[2, j].ToolTipText = "Limit of the gas must be set in order to select the auto-background option.";
                cm_datagrid[3, j].Style.BackColor = System.Drawing.Color.Gray;

                    cm_datagrid[3, j].Tag = key;
                    cm_datagrid[3, j].Style.BackColor = System.Drawing.Color.Gray;
                    if (gas_detection_limits[key] != null)
                    {
                        cm_datagrid[3, j].Value = Convert.ToString((double)gas_detection_limits[key]);
                        cm_datagrid[2, j].ReadOnly = false;
                        cm_datagrid[2, j].ToolTipText = null;
                    }
                    j = j + 1;
            }            
        }



        /// <summary>
        /// This method will load all the available libraries
        /// into <code>libraries</code> and populate the <code>selected_gases</code>
        /// </summary>
        public override void loadLibraries()
        {
            LibraryMatrix temp_lib_matrix;
            String[] library_names = Libraries.getLibraryNames();

            libraries = new LibraryMatrix[library_names.Length];

            for (int i = 0; i < library_names.Length; ++i)
            {
                String current_library_data = Libraries.getLibrary(library_names[i]);
                temp_lib_matrix = new LibraryMatrix(current_library_data);
                libraries[i] = temp_lib_matrix;
                libraries[i].library_name = library_names[i];
            }
            FileInfo main_lib_file = new FileInfo(Directory.GetCurrentDirectory() + "\\curlib");
            Util.saveLibraryMatrices(main_lib_file, libraries, library_names);

        }

        /// <summary>
        /// This method will load any cached backgrounds - if available.
        /// </summary>
        public void loadCachedBackgrounds()
        {
            BackgroundManager background_manager = new BackgroundManager(true);
            if (multiple_libraries)
            {
                for (int i = 0; i < libraries.Length; ++i)
                {
                    DataMatrix cached_background = background_manager.getCachedBackGround(libraries[i].library_name, this.library_mode);
                    if (cached_background != null)
                    {
                        DataMatrix splined_data = Util.spline(cached_background, libraries[i].startWL, libraries[i].endWL, ((short)libraries[i].numOfEntries), 0);
                        this.backgrounds[i] = splined_data;
                    }
                }
            }
            else
            {
                DataMatrix cached_background = background_manager.getCachedBackGround(this.lib_matrix.library_name, this.library_mode);
                if (cached_background != null)
                {
                    DataMatrix splined_data = Util.spline(cached_background, lib_matrix.startWL, lib_matrix.endWL, ((short)lib_matrix.numOfEntries), 0);
                    this.background_data = splined_data;
                }
            }
        }

        /// <summary>
        /// This method is responsible for reprocessing data.
        /// Before this method is called, all the validation of data and setup of properties
        /// such as background file and processing folder should be verified
        /// </summary>
        public override void reprocessData(DataGridView cm_datagrid, 
            DataGridView rep_datagrid, 
            ZedGraphControl cm_graph,
            ZedGraphControl rep_graph,
            TextBox txtStatus,
            StatusStrip status_bar)
        {
            saveRegistryEntries();

            if (!multiple_libraries)
            {
                start_wl = lib_matrix.startWL;
                end_wl = lib_matrix.endWL;
                num_entries = lib_matrix.numOfEntries;
            }
            

            bool updat_bkgd = false;
            DirectoryInfo dir = null;
            FileInfo[] source_files = null;            
            

            if (Path.GetExtension(data_folder) != "")
            {
                dir = new DirectoryInfo(Path.GetDirectoryName(data_folder));
                source_files = dir.GetFiles(Path.GetFileName(data_folder));
            }

            else
            {
                dir = new DirectoryInfo(data_folder);
                source_files = dir.GetFiles("UVQuant*.csv");                
            }            

            background_data = null;

            for (int i = 0; i < libraries.Length; ++i)
            {
                backgrounds[i] = null;
            }

            if (bkgd_file != "" && !multiple_libraries)
            {
                this.background_data = new DataMatrix(new FileInfo(bkgd_file));
                DataMatrix splined_data = Util.spline(background_data, start_wl, end_wl, (short)num_entries, 0);
                //Util.saveDataMatrix(new FileInfo("C:\\temp\\splined_bkgd.csv"), splined_data);
                background_data = splined_data;
            }
            
            //if (bkgd_file == "")
            for (int i = 0; i < rep_datagrid.RowCount; ++i)
            {
                if ((rep_datagrid[2, i].Value != null && (bool)rep_datagrid[2, i].Value) &&
                    (!"".Equals((string)rep_datagrid[3, i].Value)))
                {
                    updat_bkgd = true;
                    break;
                }
            }

            foreach (FileInfo lib_file in source_files)
            {                
                   
                DataMatrix smoothed_result = null;
                DataMatrix result = null;
                Matrix result_matrix = null;
                Matrix[] result_matrices = null;
                DataMatrix splined_data = null;

                //we need a background for each library matrix
                if (!processing)
                    break;

                if (!multiple_libraries)
                {
                    this.current_data = new DataMatrix(lib_file);
                    splined_data = Util.spline(current_data, start_wl, end_wl, (short)num_entries, 0);

                   // Util.saveDataMatrix(new FileInfo("C:\\temp\\splined_data.csv"), splined_data);
                    if (background_data == null ||
                        background_data.getColumn(0).Length != num_entries)
                    {
                        background_data = splined_data;
                    }

                   // current_data = splined_data;
                    //abs_data = Util.getAbsorbance(background_data, current_data);
                    abs_data = Util.getAbsorbanceViaTransmitance(background_data, splined_data);

                   // Util.saveDataMatrix(new FileInfo("C:\\temp\\splined_abs.csv"), abs_data);
                    int sg_width = lib_matrix.sg_width;
                    int sg_deriv = lib_matrix.sg_deriv;

                    smoothed_result = Util.sGolay(abs_data, sg_width, sg_deriv);
                 //   Util.saveDataMatrix(new FileInfo("C:\\temp\\splined_abs_smoothed.csv"), smoothed_result);
                    if (shift_amount == 0)
                    {
                        //result = Util.shift(smoothed_result, lib_matrix.pixle_shift);
                        result = Util.shift(smoothed_result, getPixelShift(lib_matrix));
                    }
                    else
                    {
                        result = Util.shift(smoothed_result, shift_amount);
                    }
               //     Util.saveDataMatrix(new FileInfo("C:\\temp\\splined_abs_shifted.csv"), result);
                    result_matrix = null;
                    result_matrix = result.Transpose() * lib_matrix;
                }
                else
                {
                    result_matrices = new Matrix[libraries.Length];
                    
                    for (int i = 0; i < libraries.Length; ++i)
                    {
                        //Ax change | no need to go through each gas. Only the ones that are selected
                        if (selected_gases[i] == null)
                            continue;
                        start_wl = libraries[i].startWL;
                        end_wl = libraries[i].endWL;
                        num_entries = libraries[i].numOfEntries;


                        if (!processing)
                            break;

                        this.current_data = new DataMatrix(lib_file);

                        splined_data = null;

                        splined_data = Util.spline(current_data, start_wl, end_wl, (short)num_entries, 0);

                        if (backgrounds[i] == null ||
                            backgrounds[i].getWL(0) != start_wl ||
                            backgrounds[i].getWL(backgrounds[i].getWL().Length - 1) != end_wl)
                        {
                            if (bkgd_file != "")
                            {
                                DataMatrix mult_background_data = new DataMatrix(new FileInfo(bkgd_file));
                                DataMatrix mult_splined_data = Util.spline(mult_background_data, start_wl, end_wl, (short)num_entries, 0);
                                backgrounds[i] = mult_splined_data;              
                            }
                            else
                            {
                                backgrounds[i] = splined_data;
                            }
                        }

                        abs_data = Util.getAbsorbanceViaTransmitance(backgrounds[i], splined_data);

                        int sg_width = libraries[i].sg_width;
                        int sg_deriv = libraries[i].sg_deriv;
                        smoothed_result = Util.sGolay(abs_data, sg_width, sg_deriv);

                        if (shift_amount == 0)
                        {
                            result = Util.shift(smoothed_result, getPixelShift(libraries[i]));
                        }
                        else
                        {
                            result = Util.shift(smoothed_result, shift_amount);
                        }

                        result_matrix = result.Transpose() * libraries[i];
                        result_matrices[i] = result_matrix;
                    }
                }

                double path_length = Double.Parse((String)current_data.getInfo(DataMatrix.FileHeader.PATH_LENGTH));

                if (multiple_libraries)
                {
                    displayConc(result_matrices, path_length, cm_datagrid, rep_datagrid);
                    writeData(result_matrices, path_length, current_data, cm_datagrid, rep_datagrid,false);
                }
                else
                {
                    displayConc(lib_matrix.id, result_matrix.getRow(0), path_length, cm_datagrid, rep_datagrid);
                    writeData(lib_matrix.id, result_matrix.getRow(0), path_length, current_data, cm_datagrid, rep_datagrid,false);
                }
                PointPairList list = abs_data.getPointPairList();
                displayGraph(list, cm_graph, rep_graph, txtStatus, status_bar);
                

                if (updat_bkgd)
                {
                    updateBackground(current_data, cm_datagrid, rep_datagrid, false);
                }
            }

        }

        public new void retrieveRegistryEntries()
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\ArgosQuant");
            if (regKey != null)
            {
                string string_selected_gases = (string)regKey.GetValue("selected_gases", "");
                selected_gases = Util.convertStringToIntArray(string_selected_gases, Libraries.getLibraryNames().Length);
                library_mode = (int)regKey.GetValue("library_mode", 0);
                loaded_library_name = (string)regKey.GetValue("loaded_library_name", "Default");
                string concentration_factors = (string)regKey.GetValue("concentration_factors", null);
                setCurrentConcentrations(concentration_factors);

                string pixel_shifts = (string)regKey.GetValue("pixel_shifts", null);
                setPixelShifts(pixel_shifts);
            }
            else
            {
                library_mode = 0;
            }
            
            if (library_mode == LibraryMode.SELECTEDGASES)
            {
                multiple_libraries = true;
            }
            else
            {
                multiple_libraries = false;
            }

            base.retrieveRegistryEntries();
        }

        public new void saveRegistryEntries()
        {
            RegistryKey regKey = Registry.LocalMachine.CreateSubKey(@"Software\ArgosQuant");
            if (regKey != null)
            {
                regKey.SetValue("loaded_library_name", loaded_library_name);
                regKey.SetValue("library_mode", library_mode);
                string string_selected_gases = Util.convertIntArrayToString(selected_gases);
                regKey.SetValue("selected_gases", string_selected_gases);
                regKey.SetValue("concentration_factors", getCurrentConcetrations());
                regKey.SetValue("pixel_shifts", getPixelShifts());
            }
            base.saveRegistryEntries();
        }


        /// <summary>
        /// This method is responsible for updating background after each data processing.
        /// Before updating the background, it verifies that the current data in memory is
        /// worthy of being a background
        /// </summary>
        public void updateBackground(DataMatrix current_sample, DataGridView cm_datagrid, DataGridView rep_datagrid, bool force_update)
        {

            //this method should only be used in multiple library mode
            if (multiple_libraries)
            {
                //While in real time mode, we only update the background if dynamic background is turned on
                if (force_update || 
                    console_mode == ConsoleState.REPROCESSING ||
                    (dynamic_background == 1 && console_mode == ConsoleState.CONTINUOUS_MONITORING))
                {
                    double signal_strength = Double.Parse(current_data.getInfo(DataMatrix.FileHeader.SIGNAL_STRENGTH));
                    if (force_update || 
                        (signal_strength > minimum_signal &&
                        signal_strength < maximum_signal))
                    {
                        if (console_mode == ConsoleState.REPROCESSING)
                        {
                            int j = 0;
                            for (int i = 0; i < libraries.Length; ++i)
                            {
                                if (selected_gases[i] != null)
                                {
                                    for (int k = 0; k < selected_gases[i].Length; ++k)
                                    {
                                        if (force_update || (rep_datagrid[2, j].Value != null && (bool)rep_datagrid[2, j].Value))
                                        {
                                            //TODO: The updating scheme here is assuming we have only one gas per library
                                            if (force_update ||(!"".Equals((string)rep_datagrid[3, j].Value)))
                                                if (force_update || 
                                                    (((double.Parse(rep_datagrid[1, j].Value.ToString())) < double.Parse(rep_datagrid[3, j].Value.ToString())) &&
                                                    ((double.Parse(rep_datagrid[1, j].Value.ToString())) > (-1 * double.Parse(rep_datagrid[3, j].Value.ToString()))) &&
                                                    ("Ozone"!=((string)rep_datagrid[0,j].Value))))//we don't want to base our scheme on Ozone
                                                {
                                                    start_wl = libraries[i].startWL;
                                                    end_wl = libraries[i].endWL;
                                                    num_entries = libraries[i].numOfEntries;

                                                    DataMatrix splined_data = Util.spline(current_sample, start_wl, end_wl, (short)num_entries, 0);

                                                    backgrounds[i] = splined_data;
                                                }
                                        }
                                        j = j + 1;
                                    }
                                }
                            }
                        }
                        else
                        {
                            int j = 0;

                            for (int i = 0; i < libraries.Length; ++i)
                            {
                                if (selected_gases[i] != null)
                                {
                                    for (int k = 0; k < selected_gases[i].Length; ++k)
                                    {
                                        if ( force_update || (cm_datagrid[2, j].Value != null && (bool)cm_datagrid[2, j].Value))
                                        {
                                            if (force_update || (!"".Equals((string)cm_datagrid[3, j].Value)))
                                                if (force_update || 
                                                    (((double.Parse(cm_datagrid[1, j].Value.ToString())) < double.Parse(cm_datagrid[3, j].Value.ToString())) &&
                                                    ((double.Parse(cm_datagrid[1, j].Value.ToString())) > (-1 * double.Parse(cm_datagrid[3, j].Value.ToString()))) &&
                                                    ("Ozone" != ((string)cm_datagrid[0, j].Value))))//we don't want to base our scheme on Ozone
                                                {
                                                    start_wl = libraries[i].startWL;
                                                    end_wl = libraries[i].endWL;
                                                    num_entries = libraries[i].numOfEntries;

                                                    DataMatrix splined_data = Util.spline(current_sample, start_wl, end_wl, (short)num_entries, 0);

                                                    backgrounds[i] = splined_data;
                                                }
                                        }
                                        j = j + 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {

                //While in real time mode, we only update the background if dynamic background is turned on
                if (force_update || 
                    console_mode == ConsoleState.REPROCESSING ||
                    (dynamic_background == 1 && console_mode == ConsoleState.CONTINUOUS_MONITORING))
                {
                    double signal_strength = Double.Parse(current_data.getInfo(DataMatrix.FileHeader.SIGNAL_STRENGTH));
                    if (force_update||
                        (signal_strength > minimum_signal &&
                        signal_strength < maximum_signal))
                    {
//                        bool found_a_detect = false;
                        for (int i = 0; i < rep_datagrid.RowCount; ++i)
                        {
                            if (console_mode == ConsoleState.REPROCESSING)
                            {
                                if (force_update || (rep_datagrid[2, i].Value != null && (bool)rep_datagrid[2, i].Value))
                                {
                                    if (force_update || (!"".Equals((string)rep_datagrid[3, i].Value)))
                                        if (force_update || 
/*                                            (((double.Parse(rep_datagrid[1, i].Value.ToString())) >= double.Parse(rep_datagrid[3, i].Value.ToString())) &&
                                            ((double.Parse(rep_datagrid[1, i].Value.ToString())) <= (-1 * double.Parse(rep_datagrid[3, i].Value.ToString()))) &&
                                            ("Ozone" != ((string)rep_datagrid[0, i].Value))))//we don't want to base our scheme on Ozone */
                                            (((double.Parse(rep_datagrid[1, i].Value.ToString())) < double.Parse(rep_datagrid[3, i].Value.ToString())) &&
                                            ((double.Parse(rep_datagrid[1, i].Value.ToString())) > (-1 * double.Parse(rep_datagrid[3, i].Value.ToString()))) &&
                                            ("Ozone" != ((string)rep_datagrid[0, i].Value))))//we don't want to base our scheme on Ozone
                                        {
//                                            found_a_detect = true;
//                                            break;
                                            start_wl = lib_matrix.startWL;
                                            end_wl = lib_matrix.endWL;
                                            num_entries = lib_matrix.numOfEntries;

                                            DataMatrix splined_data = Util.spline(current_sample, start_wl, end_wl, (short)num_entries, 0);

                                            background_data = splined_data;
                                        }
                                }
                            }
                            else
                            {
                                if (force_update || (cm_datagrid[2, i].Value != null && (bool)cm_datagrid[2, i].Value))
                                {
                                    if (force_update || (!"".Equals((string)cm_datagrid[3, i].Value)))
                                        if (force_update || 
/*                                            (((double.Parse(cm_datagrid[1, i].Value.ToString())) >= double.Parse(cm_datagrid[3, i].Value.ToString())) &&
                                            ((double.Parse(cm_datagrid[1, i].Value.ToString())) <= (-1 * double.Parse(cm_datagrid[3, i].Value.ToString()))) &&
                                            ("Ozone" != ((string)cm_datagrid[0, i].Value))))//we don't want to base our scheme on Ozone*/
                                            (((double.Parse(cm_datagrid[1, i].Value.ToString())) < double.Parse(cm_datagrid[3, i].Value.ToString())) &&
                                            ((double.Parse(cm_datagrid[1, i].Value.ToString())) > (-1 * double.Parse(cm_datagrid[3, i].Value.ToString()))) &&
                                            ("Ozone" != ((string)cm_datagrid[0, i].Value))))//we don't want to base our scheme on Ozone                                        
                                        {
//                                            found_a_detect = true;
  //                                          break;
                                            start_wl = lib_matrix.startWL;
                                            end_wl = lib_matrix.endWL;
                                            num_entries = lib_matrix.numOfEntries;

                                            DataMatrix splined_data = Util.spline(current_sample, start_wl, end_wl, (short)num_entries, 0);

                                            background_data = splined_data;
                                        }
                                }
                            }
                        }
                        //only update background if there was not a detect
/*                        if (!found_a_detect)
                        {
                            start_wl = lib_matrix.startWL;
                            end_wl = lib_matrix.endWL;
                            num_entries = lib_matrix.numOfEntries;

                            DataMatrix splined_data = Util.spline(current_sample, start_wl, end_wl, (short)num_entries, 0);

                            background_data = splined_data;
                        } */
                    }
                }
            }
        }

        /// <summary>
        /// This method updates the selected background
        ///     
        ///     
        /// </summary>


        private void selectBkgd()
        {
            return;
        }


        /// <summary>
        /// This method is called at the end of each data processing to display the results of 
        /// the data processing
        ///     results   -     an array of matrices containing results
        ///     path_length -   the current path length to be used in concentration calculation
        /// </summary>
        private void displayConc(Matrix[] results, double path_length, DataGridView cm_datagrid, DataGridView rep_datagrid)
        {
            int j = 0;
            for (int k = 0; k < results.Length; ++k)
            {
                if (results[k] != null)
                {
                    double[] conc = results[k].getRow(0);
                    for (int i = 0;
                        (selected_gases[k] != null) &&
                        (i < selected_gases[k].Length);
                        ++i)
                    {
                        int current_gas_index = selected_gases[k][i];
                        double sum = conc[current_gas_index];
                        int current_id = Math.Abs(libraries[k].getGas_id(current_gas_index));
                        current_gas_index = current_gas_index + 1;
                        while ((current_gas_index < libraries[k].id.Length) &&
                            (Math.Abs(libraries[k].getGas_id(current_gas_index)) == current_id))
                        {
                            sum = sum + conc[current_gas_index];
                            current_gas_index = current_gas_index + 1;
                        }
                        
                        //sum = sum * (1000 / path_length) * libraries[k].getConcentration(libraries[k].getGas_name(current_gas_index - 1));
                        sum = sum * (1000 / path_length) * getConcetrationFactor(libraries[k],(current_gas_index -1));
                        if (console_mode == ConsoleState.CONTINUOUS_MONITORING)
                        {
                            cm_datagrid[1, j].Value = Math.Round(sum, 2);

                            if (cm_datagrid[2, j].Value != null && (bool)cm_datagrid[2, j].Value)
                            {
                                if (cm_datagrid[3, j].Value != null && !"".Equals((string)cm_datagrid[3, j].Value))
                                {
                                    if ((double.Parse(cm_datagrid[1, j].Value.ToString())) >= double.Parse(cm_datagrid[3, j].Value.ToString()))
                                    {
                                        cm_datagrid[1, j].Style.BackColor = System.Drawing.Color.Lime;
                                    }
                                    else
                                    {
                                        cm_datagrid[1, j].Style.BackColor = System.Drawing.Color.White;
                                        cm_datagrid[1, j].Value = double.Parse(cm_datagrid[3, j].Value.ToString()) / 2;
                                    }
                                }
                                else
                                {
                                    cm_datagrid[1, j].Style.BackColor = System.Drawing.Color.White;
                                }
                            }
                            else
                            {
                                cm_datagrid[1, j].Style.BackColor = System.Drawing.Color.White;
                            }

                            j = j + 1;
                        }
                        else
                        {
                            rep_datagrid[1, j].Value = Math.Round(sum, 2);

                            if (rep_datagrid[2, j].Value != null && (bool)rep_datagrid[2, j].Value)
                            {
                                if (rep_datagrid[3, j].Value != null && (!"".Equals((string)rep_datagrid[3, j].Value)))
                                {
                                    if ((double.Parse(rep_datagrid[1, j].Value.ToString())) >= double.Parse(rep_datagrid[3, j].Value.ToString()))
                                    {
                                        rep_datagrid[1, j].Style.BackColor = System.Drawing.Color.Lime;
                                    }
                                    else
                                    {
                                        rep_datagrid[1, j].Style.BackColor = System.Drawing.Color.White;
                                        rep_datagrid[1, j].Value = double.Parse(rep_datagrid[3, j].Value.ToString()) / 2;
                                    }
                                }
                                else
                                {
                                    rep_datagrid[1, j].Style.BackColor = System.Drawing.Color.White;
                                }
                            }
                            else
                            {
                                rep_datagrid[1, j].Style.BackColor = System.Drawing.Color.White;
                            }

                            j = j + 1;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method is called at the end of each data processing to display the results of 
        /// the data processing
        ///     gas_id -        an array of gas ids from the library to be displayed
        ///     conc   -        an array of raw concentrations that correspond to each gas
        ///     path_length -   the current path length to be used in concentration calculation
        /// </summary>
        private void displayConc(int[] gas_id, double[] conc, double path_length, DataGridView cm_datagrid, DataGridView rep_datagrid)
        {
            int j = 0;
            for (int i = 0; i < gas_id.Length; ++i)
            {
                double sum = conc[i];
                int current_id = Math.Abs(gas_id[i]);
                i = i + 1;
                while ((i < gas_id.Length)) //&&
                    //(Math.Abs(gas_id[i]) == current_id))
                {
                    sum = sum + conc[i];
                    i = i + 1;
                }
                i = i - 1;
                //sum = sum * (1000 / path_length) * lib_matrix.getConcentration(lib_matrix.getGas_name(i));
                sum = sum * (1000 / path_length) * getConcetrationFactor(lib_matrix,i);

                if (console_mode == ConsoleState.CONTINUOUS_MONITORING)
                {
                    cm_datagrid[1, j].Value = sum;

                    if (cm_datagrid[2, j].Value != null && (bool)cm_datagrid[2, j].Value)
                    {
                        if (!"".Equals((string)cm_datagrid[3, j].Value))
                        {
                            if ((double.Parse(cm_datagrid[1, j].Value.ToString())) >= double.Parse(cm_datagrid[3, j].Value.ToString()))
                            {
                                cm_datagrid[1, j].Style.BackColor = System.Drawing.Color.Lime;
                            }
                            else
                            {
                                cm_datagrid[1, j].Style.BackColor = System.Drawing.Color.White;
                                cm_datagrid[1, j].Value = double.Parse(cm_datagrid[3, j].Value.ToString()) / 2;
                            }
                        }
                        else
                        {
                            cm_datagrid[1, j].Style.BackColor = System.Drawing.Color.White;
                        }
                    }
                    else
                    {
                        cm_datagrid[1, j].Style.BackColor = System.Drawing.Color.White;
                    }

                    j = j + 1;
                }
                else
                {
                    rep_datagrid[1, j].Value = sum;

                    if (rep_datagrid[2, j].Value != null && (bool)rep_datagrid[2, j].Value)
                    {
                        if (!"".Equals((string)rep_datagrid[3, j].Value))
                        {
                            if ((double.Parse(rep_datagrid[1, j].Value.ToString())) >= double.Parse(rep_datagrid[3, j].Value.ToString()))
                            {
                                rep_datagrid[1, j].Style.BackColor = System.Drawing.Color.Lime;
                            }
                            else
                            {
                                rep_datagrid[1, j].Style.BackColor = System.Drawing.Color.White;
                                rep_datagrid[1, j].Value = double.Parse(rep_datagrid[3, j].Value.ToString()) / 2;
                            }
                        }
                        else
                        {
                            rep_datagrid[1, j].Style.BackColor = System.Drawing.Color.White;
                        }
                    }
                    else
                    {
                        rep_datagrid[1, j].Style.BackColor = System.Drawing.Color.White;
                    }

                    j = j + 1;
                }
            }
        }

        /// <summary>
        /// This method is responsible for writing the summary of our calculation and analysis
        /// during a multi library 
        /// </summary>
        /// <param name="results">An array of matrices of the PLS analysis result</param>
        /// <param name="path_length">The current path length</param>
        /// <param name="sample_data">The current data in memory</param>
        private void writeData(Matrix[] results, double path_length, DataMatrix sample_data, DataGridView cm_datagrid, DataGridView rep_datagrid, bool calib_mode)
        {
            string error_code = ",##";
            if (calib_mode)
            {
                error_code = ",calibration";
            }
            string data_out = (string)sample_data.getHeaderInfo()[DataMatrix.FileHeader.DATE];
            data_out += "," + (string)sample_data.getHeaderInfo()[DataMatrix.FileHeader.SITE];
            data_out += "," + (string)sample_data.getHeaderInfo()[DataMatrix.FileHeader.FILE_COUNT];
            data_out += "," + (string)sample_data.getHeaderInfo()[DataMatrix.FileHeader.PATH_LENGTH];
            data_out += "," + (string)sample_data.getHeaderInfo()[DataMatrix.FileHeader.SIGNAL_STRENGTH];
            data_out += ",MULTIPLE-LIBRARY";
            data_out += error_code;
            string header_text = "Sample Date, Site Name, File Number, Path Length, Signal Strength, Background file, Error Code";

            int j = 0;

            for (int k = 0; k < results.Length; ++k)
            {
                if (results[k] != null)
                {
                    double[] conc = results[k].getRow(0);
                    for (int i = 0;
                        (selected_gases[k] != null) &&
                        (i < selected_gases[k].Length);
                        ++i)
                    {
                        int current_gas_index = selected_gases[k][i];
                        double sum = conc[current_gas_index];
                        int current_id = Math.Abs(libraries[k].getGas_id(current_gas_index));
                        current_gas_index = current_gas_index + 1;
                        while ((current_gas_index < libraries[k].id.Length) &&
                            (Math.Abs(libraries[k].getGas_id(current_gas_index)) == current_id))
                        {
                            sum = sum + conc[current_gas_index];
                            current_gas_index = current_gas_index + 1;
                        }

                       // sum = sum * (1000 / path_length) * libraries[k].getConcentration(libraries[k].getGas_name(current_gas_index-1));
                        sum = sum * (1000 / path_length) * getConcetrationFactor(libraries[k], (current_gas_index - 1));
                        if (console_mode == ConsoleState.CONTINUOUS_MONITORING)
                        {
                            if (cm_datagrid[2, j].Value != null && (bool)cm_datagrid[2, j].Value)
                            {
                                if ((cm_datagrid[3, j].Value != null) && !"".Equals((string)cm_datagrid[3, j].Value))
                                {
                                    if (sum < double.Parse(cm_datagrid[3, j].Value.ToString()))
                                    {
                                        sum = double.Parse(cm_datagrid[3, j].Value.ToString()) / 2;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (rep_datagrid[2, j].Value != null && (bool)rep_datagrid[2, j].Value)
                            {
                                if ((cm_datagrid[3, j].Value != null) && (!"".Equals((string)rep_datagrid[3, j].Value)))
                                {
                                    if (sum < double.Parse(rep_datagrid[3, j].Value.ToString()))
                                    {
                                        sum = double.Parse(rep_datagrid[3, j].Value.ToString()) / 2;
                                    }
                                }
                            }
                        }

                        data_out += "," + Convert.ToString(sum) + "," + 
                                            (string)backgrounds[k].getHeaderInfo()[DataMatrix.FileHeader.SITE] + "-" +
                                            (string)backgrounds[k].getHeaderInfo()[DataMatrix.FileHeader.FILE_COUNT];

                        header_text += "," + rep_datagrid[0, j].Value + "," + rep_datagrid[0,j].Value + "-Background";
                        j = j + 1;
                    }
                }
            }

            if (flag_data)
            {
                double sig_str = Convert.ToDouble((string)sample_data.getHeaderInfo()[DataMatrix.FileHeader.SIGNAL_STRENGTH]);
                int int_time = Convert.ToInt32((string)sample_data.getHeaderInfo()[DataMatrix.FileHeader.INTEGRATION_TIME]);
                data_out += "," + getDataFlag(sig_str,int_time);
                header_text += "," + "Data Flag";
            }

            String dir = null;
            
            if (Path.GetExtension(data_folder) != "")
                dir = Path.GetDirectoryName(data_folder);
            else
                dir = data_folder;


            //  If in reprocessing mode
            if (console_mode == ConsoleState.REPROCESSING)
            {
                if (!File.Exists(dir + "\\Data Summary-Reprocessed.csv"))
                {
                    StreamWriter w = File.AppendText(dir + "\\Data Summary-Reprocessed.csv");

                    w.WriteLine(header_text);
                    w.WriteLine(data_out);
                    w.Close();
                }
                else
                {
                    StreamWriter w = File.AppendText(dir + "\\Data Summary-Reprocessed.csv");
                    w.WriteLine(data_out);
                    w.Close();
                }
            }
            else
            {
                if (!File.Exists("C:\\" + site_name + "\\Data Summary.csv"))
                {
                    StreamWriter w = File.AppendText("C:\\" + site_name + "\\Data Summary.csv");

                    w.WriteLine(header_text);
                    w.WriteLine(data_out);
                    w.Close();
                }
                else
                {
                    StreamWriter w = File.AppendText("C:\\" + site_name + "\\Data Summary.csv");
                    w.WriteLine(data_out);
                    w.Close();
                }
            }

        }

        /// <summary>
        /// This method is responsible for writing every bit of information (both single beam and data summary)
        /// after each data collection
        /// </summary>
        private void writeData(int[] gas_id, double[] conc, double path_length, DataMatrix sample_data, DataGridView cm_datagrid, DataGridView rep_datagrid, bool calib_mode)
        {
            string error_code = ",##";
            if (calib_mode)
            {
                error_code = ",calibration";
            }
            string data_out = (string)sample_data.getHeaderInfo()[DataMatrix.FileHeader.DATE];
            data_out += "," + (string)sample_data.getHeaderInfo()[DataMatrix.FileHeader.SITE];
            data_out += "," + (string)sample_data.getHeaderInfo()[DataMatrix.FileHeader.FILE_COUNT];
            data_out += "," + (string)sample_data.getHeaderInfo()[DataMatrix.FileHeader.PATH_LENGTH];
            data_out += "," + (string)sample_data.getHeaderInfo()[DataMatrix.FileHeader.SIGNAL_STRENGTH];
            data_out += "," +
                (string)background_data.getHeaderInfo()[DataMatrix.FileHeader.SITE] + "-" +
                (string)background_data.getHeaderInfo()[DataMatrix.FileHeader.FILE_COUNT];
            data_out += error_code;
            string header_text = "Sample Date, Site Name, File Number, Path Length, Signal Strength, Background file, Error Code";

            int j = 0;
            for (int i = 0; i < gas_id.Length; ++i)
            {
                double sum = conc[i];
                int current_id = Math.Abs(gas_id[i]);
                i = i + 1;
                while ((i < gas_id.Length)) //&&
                    //(Math.Abs(gas_id[i]) == current_id))
                {
                    sum = sum + conc[i];
                    i = i + 1;
                }
                i = i - 1;
                //sum = sum * (1000 / path_length) * lib_matrix.getConcentration(lib_matrix.getGas_name(i));
                sum = sum * (1000 / path_length) * getConcetrationFactor(lib_matrix, i);
                if (console_mode == ConsoleState.CONTINUOUS_MONITORING)
                {
                    if (cm_datagrid[2, j].Value != null && (bool)cm_datagrid[2, j].Value)
                    {
                        if (!"".Equals((string)cm_datagrid[3, j].Value))
                        {
                            if (sum < double.Parse(cm_datagrid[3, j].Value.ToString()))
                            {
                                sum = double.Parse(cm_datagrid[3, j].Value.ToString()) / 2;
                            }
                        }
                    }
                }
                else
                {
                    if (rep_datagrid[2, j].Value != null && (bool)rep_datagrid[2, j].Value)
                    {
                        if (!"".Equals((string)rep_datagrid[3, j].Value))
                        {
                            if (sum < double.Parse(rep_datagrid[3, j].Value.ToString()))
                            {
                                sum = double.Parse(rep_datagrid[3, j].Value.ToString()) / 2;
                            }
                        }
                    }
                }


                data_out += "," + Convert.ToString(sum);
                header_text += "," + rep_datagrid[0, j].Value;

                j = j + 1;
            }


            //  If in reprocessing mode
            if (console_mode == ConsoleState.REPROCESSING)
            {
                if (!File.Exists(data_folder + "\\Data Summary-Reprocessed.csv"))
                {
                    StreamWriter w = File.AppendText(data_folder + "\\Data Summary-Reprocessed.csv");

                    w.WriteLine(header_text);
                    w.WriteLine(data_out);
                    w.Close();
                }
                else
                {
                    StreamWriter w = File.AppendText(data_folder + "\\Data Summary-Reprocessed.csv");
                    w.WriteLine(data_out);
                    w.Close();
                }
            }
            else
            {
                if (!File.Exists("C:\\" + site_name + "\\Data Summary.csv"))
                {
                    StreamWriter w = File.AppendText("C:\\" + site_name + "\\Data Summary.csv");

                    w.WriteLine(header_text);
                    w.WriteLine(data_out);
                    w.Close();
                }
                else
                {
                    StreamWriter w = File.AppendText("C:\\" + site_name + "\\Data Summary.csv");
                    w.WriteLine(data_out);
                    w.Close();
                }
            }

        }

        /// <summary>
        /// This method is responsible for directing all real time data collection and
        /// processing. It acts the manager of the whole continuous monitoring.
        /// </summary>
        public override void compileData(double[,] values, TextBox txtStatus, StatusStrip statusBar)
        {
            if (!multiple_libraries)
            {
                start_wl = lib_matrix.startWL;
                end_wl = lib_matrix.endWL;
                num_entries = lib_matrix.numOfEntries;
            }

            //IF THE BACKGROUND_DATA MATRIX IS EMPTY, CREATE IT WITHT HE CURRENT DATA
            //THIS SHOULD ONLY HAPPEN WHEN UV QUANT IS RUN FOR THE VERY FIRST TIME
            Hashtable bkgd_header_info = new Hashtable();
            //bkgd_header_info.Add(DataMatrix.FileHeaderUvQuan.NUMER_OF_HEADERS, "12");
            bkgd_header_info.Add(DataMatrix.FileHeaderUvQuan.SITE, site_name);
            bkgd_header_info.Add(DataMatrix.FileHeaderUvQuan.AVERAGE, averages.ToString());
            bkgd_header_info.Add(DataMatrix.FileHeaderUvQuan.DATE, System.DateTime.Now.ToString());
            bkgd_header_info.Add(DataMatrix.FileHeaderUvQuan.FILE_COUNT, file_number.ToString());
            bkgd_header_info.Add(DataMatrix.FileHeaderUvQuan.INTEGRATION_TIME, integration_time);
            bkgd_header_info.Add(DataMatrix.FileHeaderUvQuan.FILE_VERSION, "UV Quant Version 1");
            bkgd_header_info.Add(DataMatrix.FileHeaderUvQuan.OPERATOR, uvs_operator);
            bkgd_header_info.Add(DataMatrix.FileHeaderUvQuan.PATH_LENGTH, path_length.ToString());
            bkgd_header_info.Add(DataMatrix.FileHeaderUvQuan.REFERENCE_DATE, System.DateTime.Now.ToString());
            bkgd_header_info.Add(DataMatrix.FileHeaderUvQuan.REFERENCE_SPECTRA, site_name + "-" + file_number);
            bkgd_header_info.Add(DataMatrix.FileHeaderUvQuan.SIGNAL_STRENGTH, current_spectrometer.max_uv_count.ToString());

            if ((background_data == null && !multiple_libraries) ||
                (!multiple_libraries && (background_data.getColumn(0).Length!=num_entries)))
            {
                MainConsole.updateMainStatus("Acquiring background...", site_name, txtStatus, statusBar);

                background_data = new DataMatrix(current_spectrometer.wavelength, values, current_spectrometer.spectra.Length, bkgd_header_info);
                DataMatrix splined_bkgd_data = Util.spline(background_data, start_wl, end_wl, (short)num_entries, 0);
                background_data = splined_bkgd_data;
            }

            //if you are using multiple libraries, you need to check
            //if the backgrounds for each library are initialized
            if (multiple_libraries)
            {
                for (int k = 0; k < libraries.Length; ++k)
                {
                    start_wl = libraries[k].startWL;
                    end_wl = libraries[k].endWL;
                    num_entries = libraries[k].numOfEntries;

                    if (backgrounds[k] == null ||
                        backgrounds[k].getWL(0) != start_wl ||
                        backgrounds[k].getWL(backgrounds[k].getWL().Length - 1) != end_wl)
                    {
                        backgrounds[k] = new DataMatrix(current_spectrometer.wavelength, values, current_spectrometer.spectra.Length, bkgd_header_info);
                        DataMatrix splined_bkgd_data = Util.spline(backgrounds[k], start_wl, end_wl, (short)num_entries, 0);
                        backgrounds[k] = splined_bkgd_data;
                    }
                }
            }

            MainConsole.updateMainStatus("Updating data matrix...", site_name, txtStatus, statusBar);

            Hashtable header_info = new Hashtable();
            header_info.Add(DataMatrix.FileHeaderUvQuan.NUMER_OF_HEADERS, "13");
            header_info.Add(DataMatrix.FileHeaderUvQuan.SITE, site_name);
            header_info.Add(DataMatrix.FileHeaderUvQuan.AVERAGE, averages.ToString());
            header_info.Add(DataMatrix.FileHeaderUvQuan.DATE, System.DateTime.Now.ToString());
            header_info.Add(DataMatrix.FileHeaderUvQuan.FILE_COUNT, file_number.ToString());
            header_info.Add(DataMatrix.FileHeaderUvQuan.INTEGRATION_TIME, integration_time.ToString());
            header_info.Add(DataMatrix.FileHeaderUvQuan.FILE_VERSION, "UV Quant Version 1");
            header_info.Add(DataMatrix.FileHeaderUvQuan.OPERATOR, uvs_operator);
            header_info.Add(DataMatrix.FileHeaderUvQuan.PATH_LENGTH, path_length.ToString());
            if (!multiple_libraries)
                header_info.Add(DataMatrix.FileHeaderUvQuan.REFERENCE_DATE, background_data.getInfo(DataMatrix.FileHeaderUvQuan.DATE.ToString()));
            else
                header_info.Add(DataMatrix.FileHeaderUvQuan.REFERENCE_DATE, "Multiple Library Date");
            if (!multiple_libraries)
                header_info.Add(DataMatrix.FileHeaderUvQuan.REFERENCE_SPECTRA, background_data.getInfo(DataMatrix.FileHeaderUvQuan.REFERENCE_SPECTRA));
            else
                header_info.Add(DataMatrix.FileHeaderUvQuan.REFERENCE_SPECTRA, "Multiple Library Data");
            header_info.Add(DataMatrix.FileHeaderUvQuan.SIGNAL_STRENGTH, current_spectrometer.max_uv_count.ToString());
            header_info.Add(DataMatrix.FileHeaderUvQuan.USER_NOTE, user_notes.Replace("\r", " ").Replace("\n", ""));

            current_data = new DataMatrix(current_spectrometer.wavelength, values, current_spectrometer.spectra.Length, header_info);
            
        }

        /// <summary>
        /// This method is used to save the current single beam to file
        /// </summary>
        public override void saveSingleBeam(TextBox txtStatus, StatusStrip statusBar)
        {
            MainConsole.updateMainStatus("Saving data to file...", site_name, txtStatus, statusBar);
            if (!Directory.Exists("C:\\" + site_name))
            {
                Directory.CreateDirectory("C:\\" + site_name);
            }


            string site_dir = "C:\\" + site_name + "\\" +
                System.DateTime.Now.Year + "-" +
                System.DateTime.Now.Month + "-" +
                System.DateTime.Now.Day;

            if (!Directory.Exists(site_dir))
            {
                Directory.CreateDirectory(site_dir);
            }

            string csv_file_name = "UVQuant " +
                System.DateTime.Now.Year + "-" +
                System.DateTime.Now.Month + "-" +
                System.DateTime.Now.Day + " " +
                current_data.getInfo(DataMatrix.FileHeaderUvQuan.FILE_COUNT) + ".csv";

            StreamWriter w = File.AppendText(site_dir + "\\" + csv_file_name);

            current_data.writeData(w);

            w.Close();

        }

        /// <summary>
        /// This purely retrieves the result Matrix
        /// </summary>
        /// <param name="values"></param>
        /// <param name="cm_table"></param>
        /// <param name="rep_table"></param>
        /// <param name="txtStatus"></param>
        /// <param name="statusBar"></param>
        /// <returns></returns>
        public Matrix[] getResultMatrix(
            double[,] values,
            DataGridView cm_table,
            DataGridView rep_table,
            TextBox txtStatus,
            StatusStrip statusBar)
        {
            Matrix[] result_matrix = null;

            if (!multiple_libraries)
            {
                //SPLINE THE CURRENT_DATA MATRIX

                DataMatrix splined_data = null;
                splined_data = Util.spline(current_data, start_wl, end_wl, (short)num_entries, 0);

                //Note, by the time we are here, the background data is already set (look in the 
                //compileData method
                abs_data = Util.getAbsorbanceViaTransmitance(background_data, splined_data);
                int sg_width = lib_matrix.sg_width;
                int sg_deriv = lib_matrix.sg_deriv;

                DataMatrix smoothed_result = Util.sGolay(abs_data, sg_width, sg_deriv);
                DataMatrix result = null;
                if (shift_amount == 0)
                {
                    //result = Util.shift(smoothed_result, lib_matrix.pixle_shift);
                    result = Util.shift(smoothed_result, getPixelShift(lib_matrix));
                }
                else
                {
                    result = Util.shift(smoothed_result, shift_amount);
                }
                Matrix result_matrix_temp = result.Transpose() * lib_matrix;

                // since we have only one matrix result, we return a one member matrix
                result_matrix = new Matrix[1] { result_matrix_temp };
            }
            else
            {
                result_matrix = new Matrix[libraries.Length];

                for (int i = 0; i < libraries.Length; ++i)
                {
                    start_wl = libraries[i].startWL;
                    end_wl = libraries[i].endWL;
                    num_entries = libraries[i].numOfEntries;

                    //since we are iterating through each library, 
                    //we have to load the library each time for correct splining
                    DataMatrix splined_data = null;
                    splined_data = Util.spline(current_data, start_wl, end_wl, (short)num_entries, 0);

                    if (backgrounds[i] == null ||
                        backgrounds[i].getWL(0) != start_wl ||
                        backgrounds[i].getWL(backgrounds[i].getWL().Length - 1) != end_wl)
                    {
                        MessageBox.Show("Found incorrect/null bkgd: " + libraries[i].library_name);
                        backgrounds[i] = splined_data;
                    }

                    abs_data = Util.getAbsorbanceViaTransmitance(backgrounds[i], splined_data);

                    int sg_width = libraries[i].sg_width;
                    int sg_deriv = libraries[i].sg_deriv;
                    DataMatrix smoothed_result = Util.sGolay(abs_data, sg_width, sg_deriv);

                    DataMatrix result = null;
                    if (shift_amount == 0)
                    {
                        // result = Util.shift(smoothed_result, libraries[i].pixle_shift);
                        result = Util.shift(smoothed_result, getPixelShift(libraries[i]));
                    }
                    else
                    {
                        result = Util.shift(smoothed_result, shift_amount);
                    }

                    Matrix result_matrix_temp = result.Transpose() * libraries[i];
                    result_matrix[i] = result_matrix_temp;
                }
            }
            return result_matrix;
        }

        /// <summary>
        /// This method enables all reprocessing components
        /// when the reprocessing tab is selected
        /// 
        /// values -    are needed just incase a background
        ///             needs to be instantiated here
        /// </summary>
        public override void performQuantifications(
            double[,] values, 
            DataGridView cm_table, 
            DataGridView rep_table, 
            TextBox txtStatus, 
            StatusStrip statusBar)
        {
            MainConsole.updateMainStatus("Performing quantifications...", site_name, txtStatus, statusBar);

            Matrix[] result_matrix = getResultMatrix(values, cm_table, rep_table, txtStatus, statusBar);
            
            if (!multiple_libraries)
            {
                Matrix result_matrix_temp = result_matrix[0];

                MainConsole.updateMainStatus("Updating tabular displays...", site_name, txtStatus, statusBar);
                displayConc(lib_matrix.id,result_matrix_temp.getRow(0), path_length, cm_table, rep_table);
                MainConsole.updateMainStatus("Saving results to file...", site_name, txtStatus, statusBar);
                writeData(lib_matrix.id, result_matrix_temp.getRow(0), path_length, current_data, cm_table, rep_table,false);
            }
            else
            {
                MainConsole.updateMainStatus("Updating tabular displays...", site_name, txtStatus, statusBar);
                displayConc(result_matrix, path_length, cm_table, rep_table);
                MainConsole.updateMainStatus("Saving results to file...", site_name, txtStatus, statusBar);
                writeData(result_matrix, path_length, current_data, cm_table, rep_table,false);   
            }

            if (dynamic_background == 1)
            {
                MainConsole.updateMainStatus("Updating background...", site_name, txtStatus, statusBar);
                updateBackground(current_data, cm_table, rep_table, false);
            }
            //always persist your background data
            cacheCurrentBackgroundSet();
        }

        /// <summary>
        /// Updates the background information saved on disk
        /// </summary>
        public void cacheCurrentBackgroundSet()
        {
            BackgroundManager bkgd_manager = new BackgroundManager(false);
            if (multiple_libraries)
            {
                bkgd_manager.persistBackgroundInfo(backgrounds, libraries, this.library_mode, this.site_name);
            }
            else
            {
                DataMatrix[] current_bkgd = { background_data };
                LibraryMatrix[] current_lib_matrix = { lib_matrix };
                bkgd_manager.persistBackgroundInfo(current_bkgd, current_lib_matrix, this.library_mode, this.site_name);
            }
        }

        /// <summary>
        /// Calculates and returs a result set matrix using the current background
        /// and data set. Note: this method is to be used only while using
        /// multiple libraries
        /// </summary>
        /// <returns></returns>
        public Matrix[] getResultSet()
        {
             Matrix[] result_matrix = new Matrix[libraries.Length];

             for (int i = 0; i < libraries.Length; ++i)
             {
                 start_wl = libraries[i].startWL;
                 end_wl = libraries[i].endWL;
                 num_entries = libraries[i].numOfEntries;

                 //since we are iterating through each library, 
                 //we have to load the library each time for correct splining
                 DataMatrix splined_data = null;
                 splined_data = Util.spline(current_data, start_wl, end_wl, (short)num_entries, 0);

                 if (backgrounds[i] == null ||
                     backgrounds[i].getWL(0) != start_wl ||
                     backgrounds[i].getWL(backgrounds[i].getWL().Length - 1) != end_wl)
                 {
                     backgrounds[i] = splined_data;
                 }

                 abs_data = Util.getAbsorbanceViaTransmitance(backgrounds[i], splined_data);

                 int sg_width = libraries[i].sg_width;
                 int sg_deriv = libraries[i].sg_deriv;
                 DataMatrix smoothed_result = Util.sGolay(abs_data, sg_width, sg_deriv);

                 DataMatrix result = null;
                 if (shift_amount == 0)
                 {
                     // result = Util.shift(smoothed_result, libraries[i].pixle_shift);
                     result = Util.shift(smoothed_result, getPixelShift(libraries[i]));
                 }
                 else
                 {
                     result = Util.shift(smoothed_result, shift_amount);
                 }

                 Matrix result_matrix_temp = result.Transpose() * libraries[i];
                 result_matrix[i] = result_matrix_temp;
             }

             return result_matrix;
        }

        /// <summary>
        /// Concstructs a key by concatenating the library name with the
        /// gas name as libraryname_gasname
        /// </summary>
        /// <param name="library"></param>
        /// <param name="gas_id"></param>
        public static string getConcetrationKey(LibraryMatrix library, int gas_id)
        {
            string name = library.library_name;
            if (name == "Benzene" || name == "Ozone")
                return name + "_" + name;
            else
            {
                string gas_name = library.getGas_name(gas_id);
                return name + "_" + gas_name;
            }
        }

        /// <summary>
        /// Retrieves the concentration factor for each gas in a given library
        /// If the concentration is not already loaded in memory (from previous sessiosn)
        /// then it will get it from the library matrix and then store it in memory for 
        /// later retrieval
        /// </summary>
        /// <param name="library"></param>
        /// <param name="gas_id"></param>
        /// <returns></returns>
        public double getConcetrationFactor(LibraryMatrix library, int gas_id)
        {
            double factor;
            string key = getConcetrationKey(library, gas_id);
            if (concetrations.Contains(key))
            {
                factor = (double)concetrations[key];
            }
            else
            {
                string name = library.library_name;
                if (name == "Ozone")
                    factor = library.getConcentration("Ozone");
                else
                    factor = library.getConcentration(library.getGas_name(gas_id));

                concetrations.Add(key,factor);
            }
            return factor;
        }

        /// <summary>
        /// Returns a string representation of the current conecntration hashtable as
        /// key&value|key&value|key&value...
        /// </summary>
        /// <returns></returns>
        public string getCurrentConcetrations()
        {
            string hash_value = "";
            IDictionaryEnumerator it = concetrations.GetEnumerator();
            if(it.MoveNext())
            {
                hash_value = it.Key + "&" + it.Value;
            }
            while (it.MoveNext())
            {
                hash_value += "|" + it.Key + "&" + it.Value;
            }

            return hash_value;
        }
        

        /// <summary>
        /// Instantiates the concentration factor hashtable 
        /// from a string representation of the form
        /// key&value|key&value...
        /// </summary>
        /// <param name="str_concentrations"></param>
        public void setCurrentConcentrations(string str_concentrations)
        {
            if (str_concentrations != null)
            {
                string[] pairs = str_concentrations.Split('|');

                if (pairs != null)
                {
                    for (int i = 0; i < pairs.Length; ++i)
                    {
                        string[] values = pairs[i].Split('&');
                        if (values != null && values.Length == 2)
                        {
                            string key = (string)values[0];
                            double value = double.Parse((string)values[1]);
                            if (concetrations.ContainsKey(key))
                            {
                                concetrations.Remove(key);
                            }
                            concetrations.Add(key, value);
                        }
                    }
                }
            }
        }


        public int getPixelShift(LibraryMatrix library)
        {
            if (pixelshifts.ContainsKey(library.library_name))
            {
                return (int)pixelshifts[library.library_name];
            }
            else
            {
                int pixel_shift = library.pixle_shift;
                pixelshifts.Add(library.library_name, pixel_shift);
                return pixel_shift;
            }
        }

        public string getPixelShifts()
        {
            string hash_value = "";
            IDictionaryEnumerator it = pixelshifts.GetEnumerator();
            if (it.MoveNext())
            {
                hash_value = it.Key + "&" + it.Value;
            }
            while (it.MoveNext())
            {
                hash_value += "|" + it.Key + "&" + it.Value;
            }

            return hash_value;
        }

        public void setPixelShifts(string str_pixelshifts)
        {
            if (str_pixelshifts != null)
            {
                string[] pairs = str_pixelshifts.Split('|');

                if (pairs != null)
                {
                    for (int i = 0; i < pairs.Length; ++i)
                    {
                        string[] values = pairs[i].Split('&');
                        if (values != null && values.Length == 2)
                        {
                            string key = (string)values[0];
                            int value = int.Parse((string)values[1]);
                            if (pixelshifts.ContainsKey(key))
                            {
                                pixelshifts.Remove(key);
                            }
                            pixelshifts.Add(key, value);
                        }
                    }
                }
            }
        }

        private void retreiveTresholds()
        {
            gas_detection_limits = new Hashtable();

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
                    //note, the stored value is ppm-m, hence we need to chat this to ppb
                    value = 1000 * value / path_length;
                    string key = lib_index + "_" + gas_index;

                    gas_detection_limits.Add(key, value);
                }
            }
        }

        /// <summary>
        /// Retrieves the concentration for each gas
        /// num_gasses can be calcuated as (cm_datagrid.RowCount-1)
        /// </summary>
        private double[] getConc(Matrix[] results, double path_length, int num_gasses)
        {

            double[] final_conc = new double[num_gasses];

            int j = 0;
            for (int k = 0; k < results.Length; ++k)
            {
                if (results[k] != null)
                {
                    double[] conc = results[k].getRow(0);
                    for (int i = 0;
                        (selected_gases[k] != null) &&
                        (i < selected_gases[k].Length);
                        ++i)
                    {
                        int current_gas_index = selected_gases[k][i];
                        double sum = conc[current_gas_index];
                        int current_id = Math.Abs(libraries[k].getGas_id(current_gas_index));
                        current_gas_index = current_gas_index + 1;
                        while ((current_gas_index < libraries[k].id.Length) &&
                            (Math.Abs(libraries[k].getGas_id(current_gas_index)) == current_id))
                        {
                            sum = sum + conc[current_gas_index];
                            current_gas_index = current_gas_index + 1;
                        }

                        sum = sum * (1000 / path_length) * getConcetrationFactor(libraries[k], (current_gas_index - 1));
                       
                        final_conc[j] = Math.Round(sum, 2);

                        j = j + 1;
                    }
                }
            }
            return final_conc;
        }

        /// <summary>
        /// Retrieves the concentration for each gas
        /// num_gasses can be calcuated as (cm_datagrid.RowCount-1)
        /// </summary>
        private double[] getConc(int[] gas_id, double[] conc, double path_length, int num_gasses)
        {
            double[] final_conc = new double[num_gasses];

            int j = 0;
            for (int i = 0; i < gas_id.Length; ++i)
            {
                double sum = conc[i];
                int current_id = Math.Abs(gas_id[i]);
                i = i + 1;
                while ((i < gas_id.Length)) //&&
                //(Math.Abs(gas_id[i]) == current_id))
                {
                    sum = sum + conc[i];
                    i = i + 1;
                }
                i = i - 1;
                //sum = sum * (1000 / path_length) * lib_matrix.getConcentration(lib_matrix.getGas_name(i));
                sum = sum * (1000 / path_length) * getConcetrationFactor(lib_matrix, i);

                final_conc[j] = sum;
                j = j + 1;
            }
            return final_conc;
        }

        public double[] getConcentrationsForCalibration(
            double[,] values, 
            DataGridView cm_table, 
            DataGridView rep_table, 
            TextBox txtStatus, 
            StatusStrip statusBar)
        {

            double[] concentrations = null;

            MainConsole.updateMainStatus("Acquiring data for calibration...", site_name, txtStatus, statusBar);

            Matrix[] result_matrix = getResultMatrix(values, cm_table, rep_table, txtStatus, statusBar);
            
            if (!multiple_libraries)
            {
                Matrix result_matrix_temp = result_matrix[0];

                MainConsole.updateMainStatus("Getting concentrations...", site_name, txtStatus, statusBar);
                concentrations=getConc(lib_matrix.id, result_matrix_temp.getRow(0), path_length, cm_table.RowCount);
                //handy to write the result here
                writeData(lib_matrix.id, result_matrix_temp.getRow(0), path_length, current_data, cm_table, rep_table,true);
            }
            else
            {
                MainConsole.updateMainStatus("Getting concentrations...", site_name, txtStatus, statusBar);
                concentrations=getConc(result_matrix, path_length, cm_table.RowCount);
                //handy to write the result here
                writeData(result_matrix, path_length, current_data, cm_table, rep_table,true);   
            }

            updateBackground(current_data, cm_table, rep_table, true);
            return concentrations;
        }

    }


    //**********************************************************************************************//
    //INTERNAL CLASSES//
    //**********************************************************************************************//
    public static class LibraryMode
    {
        public const int PREDEFINED = 0;
        public const int SELECTEDGASES = 1;
        public const int ONFILE = 2;
    }
}
