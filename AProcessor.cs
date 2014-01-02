using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using ZedGraph;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;

namespace UV_Quant
{
    abstract public class AProcessor
    {
        //process related variables
        public Spectrometer current_spectrometer;
        public int shift_amount = 0;
        public bool processing = false;
        public bool scanning = false;
        public string site_name = "";
        public double path_length;
        public string uvs_operator = "";       
        public int box_car;
        public int averages;
        public int integration_time;
        public string spect_serial_number = "";
        public string spec_type = "";
        public int file_number;
        public int collection_time = 30;
        public int dynamic_integration = 0;
        public int dynamic_background = 0;
        public string user_notes = "";
        public int admin_mode = 0;
        public int min_int_value = 40;
        public int max_int_value = 300;
        public bool tab_changing = false;
        public bool finished_loading = false;
        public String current_password = null;
        public string bkgd_file = "";
        public string data_folder = "";
        public int console_mode = 0;
        public string ds_file = "";
        public double minimum_signal = Double.Parse(DefaultValues.MINIMUM_SIGNAL);
        public double maximum_signal = Double.Parse(DefaultValues.MAXIMUM_SIGNAL);
        public bool flag_data = false;
        public bool autorun = false;
        public String loaded_library_name = "";
        public DataMatrix current_data; 
        public LibraryMatrix lib_matrix;
        //flag to indicate what library mode the user is using
        public int library_mode = 0;
        public int hardware_type = 1;
        public Hashtable gas_detection_limits;

        public DataMatrix background_data;
        public DataMatrix[] backgrounds = new DataMatrix[Libraries.getLibraryNames().Length];
        public DataMatrix abs_data;
        public double start_wl = 0;
        public double end_wl = 0;
        public double num_entries = 0;
        //current library container
        public LibraryMatrix[] libraries;
        //currently selected gases
        //the first array will be the index of the libraries they belong to
        public int[][] selected_gases;
        //This variable is used to store what gases in what libraries have been selected
        //The format is libraryindex|gasindex
        public string[] raw_selected_gases;
        //this variable will tell if we are using multiple libraries
        public bool multiple_libraries = false;
        //Hashtable to contain concentration factor
        public Hashtable concetrations = new Hashtable();
        //Hashtable to contain pixle shifts
        public Hashtable pixelshifts = new Hashtable();
        public static string ON_FILE = "ONFILE";

        //THE FOLLOWING ARE ONLY OXYGEN PROCESS DEPENDENT
        public int start_integral_pixel = 0;
        public int end_integral_pixel = 0;
        public double OXLowRef = 0;
        public double OXHighRef = 21;
        public double integral_low = 0;
        public double integral_high = 0;
        public double A = 0;
        public double B = 0;
        public double LO = 0;
        public double K = 0;
        public int current_mode = OxygenProcessorMode.NORMAL;
        public bool command_processing = false; //0 is disabled
        public int command_timer = 1000; //default is every second
        public string command_file = "";
        public int data_interval = 0;

        public int calibration_interval = 240; //interval in minutes
        public int calibration_duration = 5; //difference between low and high measurement in minutes
        public bool calibration_enabled = false;


        //initialization method for every type of method
        public abstract void init();

        public abstract void createTable(DataGridView cm_datagrid, DataGridView rep_datagrid);

        public abstract void loadLibraries();

        public abstract void reprocessData(DataGridView cm_datagrid,
            DataGridView rep_datagrid,
            ZedGraphControl cm_graph,
            ZedGraphControl rep_graph,
            TextBox txtStatus,
            StatusStrip status_bar);

        public abstract void compileData(double[,] values, TextBox txtStatus, StatusStrip statusBar);

        public abstract void saveSingleBeam(TextBox txtStatus, StatusStrip statusBar);

        public abstract void performQuantifications(
           double[,] values,
           DataGridView cm_table,
           DataGridView rep_table,
           TextBox txtStatus,
           StatusStrip statusBar);

        /// <summary>
        /// Override this method to control processor behavior
        /// </summary>
        /// <returns></returns>
        public bool shouldProcessData()
        {
            return true;
        }

        /// <summary>
        /// All parameters of UV Quant are stored in the registery. This utility method
        /// stores all the current instances into the registery at: 
        /// <code>HKEY_LOCAL_MACHINE\SOFTWARE\ArogsQuant</code>
        /// </summary>
        public void saveRegistryEntries()
        {
            RegistryKey regKey = Registry.LocalMachine.CreateSubKey(@"Software\ArgosQuant");
            if (regKey != null)
            {
                regKey.SetValue("shift_amount", shift_amount);
                regKey.SetValue("site_name", site_name);
                regKey.SetValue("path_length", path_length);
                regKey.SetValue("uvs_operator", uvs_operator);
                regKey.SetValue("box_car", box_car);
                regKey.SetValue("collection_time", collection_time);
                regKey.SetValue("integration_time", integration_time);
                regKey.SetValue("spec_type", spec_type);
                regKey.SetValue("file_number", file_number);
                regKey.SetValue("min_int_value", min_int_value);
                regKey.SetValue("max_int_value", max_int_value);

                regKey.SetValue("background_file", bkgd_file);
                regKey.SetValue("data_folder", data_folder);
                regKey.SetValue("data_summary", ds_file);
                regKey.SetValue("minimum_signal", minimum_signal);
                regKey.SetValue("maximum_signal", maximum_signal);
                regKey.SetValue("flag_data", flag_data);
                regKey.SetValue("autorun", autorun);
                regKey.SetValue(Constants.UVQuantRegistryKeys.HARDWARE_TYPE,
                                    hardware_type);
            }
        }

        /// <summary>
        /// This method is used to retrieve all the saved parameters from the registery
        /// at location:
        /// <code>HKEY_LOCAL_MACHINE\SOFTWARE\ArogsQuant</code>
        /// </summary>
        public void retrieveRegistryEntries()
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\ArgosQuant");
            if (regKey != null)
            {
                shift_amount = (int)regKey.GetValue("shift_amount", 0);
                site_name = (string)regKey.GetValue("site_name", "argossci");
                path_length = Double.Parse((string)regKey.GetValue("path_length", "1"));
                uvs_operator = (string)regKey.GetValue("uvs_operator", "NA");
                box_car = (int)regKey.GetValue("box_car", 0);
                collection_time = (int)regKey.GetValue("collection_time", 30);
                integration_time = (int)regKey.GetValue("integration_time", 3);
                spect_serial_number = (string)regKey.GetValue("spect_serial_number", "");
                spec_type = (string)regKey.GetValue("spec_type", "");
                file_number = (int)regKey.GetValue("file_number", 0);
                dynamic_integration = (int)regKey.GetValue("integration_update", 0);
                current_password  = (String)regKey.GetValue("current_password", "default");

                bkgd_file = (string)regKey.GetValue("Background_file", "");
                data_folder = (string)regKey.GetValue("Data_folder", "");

                dynamic_background = (int)regKey.GetValue("background_update", 1);
                user_notes = (string)regKey.GetValue("user_notes", "");
                admin_mode = (int)regKey.GetValue("admin_mode", 1);
                min_int_value = (int)regKey.GetValue("min_int_value", 40);
                max_int_value = (int)regKey.GetValue("max_int_value", 300);
                
                ds_file = (string)regKey.GetValue("data_summary","");
                maximum_signal = Double.Parse((string)regKey.GetValue("maximum_signal", DefaultValues.MAXIMUM_SIGNAL));
                minimum_signal = Double.Parse((string)regKey.GetValue("minimum_signal", DefaultValues.MINIMUM_SIGNAL));
                flag_data = Util.getBoolValue(regKey,"flag_data", DefaultValues.FLAG_DATA);
                autorun = Util.getBoolValue(regKey, "AUTORUN", DefaultValues.AUTORUN);
                hardware_type = (int)regKey.GetValue(Constants.UVQuantRegistryKeys.HARDWARE_TYPE, 1);
            }
            else
            {
                shift_amount = 0;
                site_name = "Argos Sci";
                path_length = 1.0;
                uvs_operator = "N/A";
                box_car = 0;
                autorun = false;
                collection_time = 30;
                integration_time = 3;
                spect_serial_number = "";
                spec_type = "";
                file_number = 0;
                dynamic_integration = 0;
                dynamic_background = 1;
                user_notes = "";
                admin_mode = 1;
                ds_file = "";
                hardware_type = 1;
            }
            updateNumberOfAverages();
        }

        /// <summary>
        /// This method is a utility method used to update the number of averages
        /// baed on the collection time and integration time currently in memory
        /// </summary>
        public void updateNumberOfAverages()
        {
            if (collection_time == 1)
                averages = 1;
            else
                averages =
                    (int)(collection_time / ((double)integration_time / 1000));
        }

        /// <summary>
        /// This method displays a graph of whatever <code>PointPairList</code>
        /// it is passed in. It is also smart enought to figure out which graph
        /// to update based on which tabe we are currently on
        /// </summary>
        public void displayGraph(PointPairList list, ZedGraphControl cm_graph, ZedGraphControl rep_graph, TextBox txtStatus, StatusStrip statsBar)
        {
            MainConsole.updateMainStatus("Updating graphical displays...", site_name, txtStatus, statsBar);
            if (console_mode == ConsoleState.REPROCESSING)
            {
                rep_graph.GraphPane.CurveList.Clear();// = "Argos Scientific Inc.";
                ZedGraph.GraphPane graph_pane = rep_graph.GraphPane;

                LineItem myCurve = graph_pane.AddCurve("Absorbance", list, Color.Red,
                                               ZedGraph.SymbolType.None);
                myCurve.Line.Width = 1.5F;

                graph_pane.XAxis.Scale.Min = 180;
                graph_pane.XAxis.Scale.Max = 320;
//                graph_pane.XAxis.Scale.MinorStep = 1;
//                graph_pane.XAxis.Scale.MajorStep = 5;


                rep_graph.AxisChange();
                rep_graph.Refresh();
                
                
            }
            else
            {
                cm_graph.GraphPane.CurveList.Clear();// = "Argos Scientific Inc.";
                ZedGraph.GraphPane graph_pane = cm_graph.GraphPane;

                LineItem myCurve = graph_pane.AddCurve("Absorbance", list, Color.Red,
                                               ZedGraph.SymbolType.None);
                myCurve.Line.Width = 1.5F;
                cm_graph.AxisChange();
                cm_graph.Refresh();
            }
        }

        /// <summary>
        /// Adjusts the integration time after each data collection when:
        ///     a. Dynamic integration adjustment has been enabled
        ///     b. It is a non-open path system
        /// </summary>
        /// <param name="txtStatus"></param>
        /// <param name="status_bar"></param>
        public void adjustIntegrationTime(TextBox txtStatus, StatusStrip status_bar)
        {
            if (dynamic_integration == 1 ||
                hardware_type != Constants.HardWareType.OPEN_PATH)
            {
                MainConsole.updateMainStatus("Adjusting integration period...", site_name, txtStatus, status_bar);
                current_spectrometer.DynamicIntegrationAdjustment();
                integration_time = current_spectrometer.integration_time;
                MainConsole.updateMainStatus("Adjusting number of averages...", site_name, txtStatus, status_bar);
                updateNumberOfAverages();
            }
        }

        public double[,] scanData(StatusStrip statusBar, TextBox txtStatus)
        {
            RegistryKey regKey = Registry.LocalMachine.CreateSubKey(@"Software\ArgosQuant");
            System.Windows.Forms.Application.DoEvents();

            //FIRST GET THE DATA FROM THE SPECTROMETER HOWEVER MANY TIMES SPECIFIED BY THE AVERAGE

            double[] data = current_spectrometer.spectra;
            double[,] values = new double[data.Length, 1];

            MainConsole.updateMainStatus("Collecting data from spectrometer...", site_name, txtStatus, statusBar);

            Stopwatch stop_watch = new Stopwatch();

            stop_watch.Start();
            int scanns = 0;
            while(stop_watch.ElapsedMilliseconds < 1000 * collection_time && scanning)
            //for (int scanns = 1; scanns <= averages && scanning; ++scanns)
            {
                ++scanns;
                if (scanns > averages)
                {
                    scanns = averages;
                }
                statusBar.Items["scan_status"].Text = "Scan " + scanns + " out of ~ " + averages;
                
                
                current_spectrometer.collectData();
                
                if (!current_spectrometer.selected)
                {
                    MainConsole.updateMainStatus("Stopping scan...", site_name, txtStatus, statusBar);
                    return null;
                }
                
                for (int i = 0; i < data.Length; ++i)
                {
                    values[i, 0] += current_spectrometer.spectra[i];// *10E45;
                }
            }

            if (!scanning)
            {
                return values;
            }

            MainConsole.updateMainStatus("Calculating averages...", site_name, txtStatus, statusBar);
            double max_uv_count = -1000;
            for (int i = 0; i < data.Length; ++i)
            {
                values[i, 0] /= averages;
                if (max_uv_count < values[i, 0])
                {
                    max_uv_count = values[i, 0];
                }
            }
            current_spectrometer.max_uv_count = max_uv_count;
            return values;
        }

        public void incrementFileCount(StatusStrip statusBar)
        {
            RegistryKey regKey = Registry.LocalMachine.CreateSubKey(@"Software\ArgosQuant");
            ++file_number;
            statusBar.Items["file_count"].Text = "File number: " + file_number;

            if (regKey != null)
                regKey.SetValue("file_number", file_number);
        }

        /// <summary>
        /// This method calculates the status flag for the current 
        /// data collection based on the signal strenght and integration
        /// period
        /// </summary>
        /// <returns></returns>
        public int getDataFlag(double sig_str, int int_time)
        {
            if (sig_str > maximum_signal)
            {
                return DataStatusCodes.TOO_HIGH;
            }
            else if (sig_str < minimum_signal)
            {
                return DataStatusCodes.TOO_LOW;
            }
            else if (int_time > 300)
            {
                return DataStatusCodes.LOW_LIGHT;
            }

            return DataStatusCodes.GOOD;
        }

        
        /// <summary>
        /// Added for inheritance purposes. This function returns a null;
        /// </summary>
        /// <returns></returns>
        public ArrayList getLibrayForComboBox()
        {
            return null;
        }

        /// <summary>
        /// Override this method if the processor wants to do some
        /// cleaning up or other stuff at the end of each iteration
        /// </summary>
        public void endIteration(StatusStrip statusBar, TextBox txtStatus)
        {
            //default implementation does nothing
        }
    }

    //**********************************************************************************************//
    //INTERNAL CLASSES//
    //**********************************************************************************************//
    /// <summary>
    /// <code>ConsoleState</code> is a class used to 
    /// encapsulate the two main states of our console
    /// </summary>
    public static class ConsoleState
    {
        public const int REPROCESSING = 0;
        public const int CONTINUOUS_MONITORING = 1;
    }

    /// <summary>
    /// <code>DefaultValues</code> is used to 
    /// encapusulate default values for hte processor class
    /// </summary>
    public static class DefaultValues
    {
        public const string MAXIMUM_SIGNAL = "3500";
        public const string MINIMUM_SIGNAL = "2000";
        public const bool FLAG_DATA = false;
        public const bool AUTORUN = false;
    }

    /// <summary>
    /// <code>DataStatusCodes</code> class is used to
    /// encapsulate numbers used to identify the statuses of
    /// data collected
    /// </summary>
    public static class DataStatusCodes
    {
        public const int GOOD = 0;
        public const int TOO_HIGH = 1;
        public const int TOO_LOW = 2;
        public const int LOW_LIGHT = 3; //this indicates a low light situation compensated by too high integration period
    }
}
