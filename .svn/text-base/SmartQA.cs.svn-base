using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace UV_Quant
{
    public partial class SmartQA : Form
    {

        private bool continue_processing = true;
        private int current_state = 0;
        private static PLSProcessor processor = null;
        private StatusStrip statusBar;
        private TextBox txtStatus;
        private DataGridView cm_datagrid;
        private DataGridView rep_datagrid;

        private int[][] selected_gases = null;
        private double[] expected_concentrations = null;
        private double[] calibration_concentrations = null;
        private string[] calibration_gases = null; //this is just for convinience to contain the actual gas names

        private int bkgd_int_time = 0;
        private int data_int_time = 0;

        private int original_collection_time = 0;

        /// <summary>
        /// Note: You can only pass a PLS processor here
        /// </summary>
        /// <param name="main_processor"></param>
        /// <param name="main_statusBar"></param>
        /// <param name="main_txtStatus"></param>
        /// <param name="main_cm_datagrid"></param>
        /// <param name="main_rep_datagrid"></param>
        public SmartQA(PLSProcessor main_processor,
            StatusStrip main_statusBar, 
            TextBox main_txtStatus, 
            DataGridView main_cm_datagrid,
            DataGridView main_rep_datagrid)
        {
            InitializeComponent();
            processor = main_processor;
            original_collection_time = processor.collection_time;
            processor.collection_time = 30;

            this.statusBar = main_statusBar;
            this.txtStatus = main_txtStatus;
            this.cm_datagrid = main_cm_datagrid;
            this.rep_datagrid = main_rep_datagrid;

            readInCalibrationGases();
        }
        
        public SmartQA()
        {
            InitializeComponent();
        }

        private void SmartQA_Load(object sender, EventArgs e)
        {
            current_state = CalibrationState.CHECKING_SYSTEM;
            
            lblSystemCheck.Text = "When you are ready to proceed, press 'Next'...";
        }

        /// <summary>
        /// Sets necessary indicators to show Smart QA is
        /// processing
        /// </summary>
        private void processing(String current_process)
        {
            lblLoading.Text = current_process;
            lblLoading.Visible = true;
            System.Windows.Forms.Application.DoEvents();
        }

        /// <summary>
        /// Hides processing indicators as processing is done
        /// </summary>
        private void finishedProcessing()
        {
            lblLoading.Visible = false;
        }

        /// <summary>
        /// Indicates the user does not want to continue with the
        /// calibration anymore
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuit_Click(object sender, EventArgs e)
        {
            continue_processing = false;
            processor.collection_time = original_collection_time;
            processor.scanning = false;
        }

        /// <summary>
        /// User indicating he/she wants to proceed to the next step
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (CalibrationState.CHECKING_SYSTEM == current_state)
            {
                btnNext.Enabled = false;
                lblSystemCheck.Text = "Checking system status...";
                processing("Checking system...");
                if (!performSystemCheck())
                {
                    lblSystemCheck.Text = "Failed system check...";
                    btnNext.DialogResult = DialogResult.OK;
                    btnNext.Text = "Done";
                    current_state = CalibrationState.DONE;
                    btnNext.Enabled = true;
                    return;
                }
                finishedProcessing();
                lblSystemCheck.Text = "Passed system check...";
                System.Threading.Thread.Sleep(500);
                current_state = CalibrationState.COLLECTING_BACKGROUND;
                lblBackground.Text = "Please make sure the gas cell is removed....";
                btnNext.Enabled = true;
            }
            else if(CalibrationState.COLLECTING_BACKGROUND == current_state)
            {
                btnNext.Enabled = false;
                processing("Collecting background...");
                if (!collectBackground())
                {
                    lblBackground.Text = "Failed collecting background...";
                    btnNext.DialogResult = DialogResult.OK;
                    btnNext.Text = "Done";
                    current_state = CalibrationState.DONE;
                    btnNext.Enabled = true;
                    return;
                }
                finishedProcessing();
                lblBackground.Text = "Collected background...";
                System.Threading.Thread.Sleep(500);
                current_state = CalibrationState.COLLECTING_DATA;
                lblData.Text = "Please place your gas cell...";
                btnNext.Enabled = true;
            }
            else if(CalibrationState.COLLECTING_DATA == current_state)
            {
                btnNext.Enabled = false;
                processing("Collecting data...");
                if (!collectData())
                {
                    lblData.Text = "Failed collecting data...";
                    btnNext.DialogResult = DialogResult.OK;
                    btnNext.Text = "Done";
                    current_state = CalibrationState.DONE;
                    btnNext.Enabled = true;
                    return;
                }
                lblData.Text = "Collected data...";
                finishedProcessing();
                System.Threading.Thread.Sleep(500);
                current_state = CalibrationState.DISPLAYING_RESULT;
                lblResult.Text = "Please remove gas cell...";
                btnNext.Enabled = true;
            }
            else if (CalibrationState.DISPLAYING_RESULT == current_state)
            {
                processing("Final analysis...");
                btnNext.Enabled = false;
                bool passed_analysis = displayResult();
                if (!passed_analysis)
                {
                    lblResult.Text = "Failed QA Calibration.\nResults are in application directory.";
                    MessageBox.Show("Please contact Argos Scientific for further assessment of your system!");
                }
                else
                {
                    lblResult.Text = "Passed QA Calibration.\nResults are in application directory.";
                }

                finishedProcessing();
                saveResults(passed_analysis);
                current_state = CalibrationState.DONE;
                btnNext.DialogResult = DialogResult.OK;
                btnNext.Text = "Done";
                btnNext.Enabled = true;
            }
        }

        /// <summary>
        /// Performs redundant system checks such as integration period and
        /// signal strength
        /// </summary>
        /// <returns></returns>
        public bool performSystemCheck()
        {
            adjustIntTime();

            if (processor.current_spectrometer.integration_time >= processor.min_int_value &&
                processor.current_spectrometer.integration_time <= processor.max_int_value)
            {
                return true;
            }

            MessageBox.Show("Invalid Integration time/Signal Strength:\n" + 
                            "Integration time: " + processor.current_spectrometer.integration_time + "\n" + 
                            "Signal strength: " + processor.current_spectrometer.max_uv_count); 

            return false;
        }

        /// <summary>
        /// After checking that there is not obstruction, this will collect
        /// the background to be used for the calibration
        /// </summary>
        /// <returns></returns>
        public bool collectBackground()
        {
            adjustIntTime();
            bkgd_int_time = processor.integration_time;

            processor.scanning = true;

            double[,] values = processor.scanData(statusBar, txtStatus);

            if (!continue_processing)
            {
                return true;
            }

            processor.incrementFileCount(statusBar);

            processor.compileData(values, txtStatus, statusBar);
            processor.saveSingleBeam(txtStatus, statusBar);

            if (processor.current_spectrometer.max_uv_count < processor.minimum_signal ||
                processor.current_spectrometer.max_uv_count > processor.maximum_signal)
            {
                MessageBox.Show(
                    "Please make sure you have aligned your system properly.\n" +
                    "Currently your signal intensity is: "
                    + processor.current_spectrometer.max_uv_count);
                processor.scanning = false;
                return false;
            }
            else
            {
                processor.updateBackground(processor.current_data, cm_datagrid, rep_datagrid, true);
                processor.scanning = false;
            }

            
            return true;
        }

        /// <summary>
        /// After checking the cell has been placed, 
        /// this will collect the data for calibration
        /// </summary>
        /// <returns></returns>
        public bool collectData()
        {
            data_int_time = processor.integration_time;
//            MessageBox.Show(data_int_time.);
            try
            {
                adjustIntTime();
//                data_int_time = processor.integration_time;
  //              MessageBox.Show(data_int_time.ToString);
//                data_int_time = processor.integration_time;

/*                int trial_timer = 0;

                while (data_int_time < (105 * bkgd_int_time / 100)) //we have to make sure it is at least 5% more than the background int time
                {
                    adjustIntTime();
                    data_int_time = processor.integration_time;
                    if (trial_timer > 3)
                    {
                        return false;
                    }
                    MessageBox.Show("Calibration cell not detected!!!");
                    ++trial_timer;
                } */
                processor.scanning = true;

                double[,] values = processor.scanData(statusBar, txtStatus);

                if (values == null)
                {
                    MessageBox.Show("Spectrometer Disconnected!!!");
                    return false;
                }

                if (!continue_processing)
                {
                    return true;
                }

                // Now you can increment file number
                if (processor.shouldProcessData())
                {
                    processor.incrementFileCount(statusBar);
                    processor.compileData(values, txtStatus, statusBar);
                    processor.saveSingleBeam(txtStatus, statusBar);
                }

            }
            catch (Exception exp)
            {
                MainConsole.updateMainStatus("Error!", processor.site_name, txtStatus, statusBar);

                MessageBox.Show("There was an error collecting data!");
                return false;
            }
            finally
            {
                processor.scanning = false;
            }

            return true;
        }

        /// <summary>
        /// After making sure the cell has been removed, 
        /// this method will perform the analysis, compares it to what is
        /// expected and display/save the result
        /// </summary>
        /// <returns></returns>
        public bool displayResult()
        {

            adjustIntTime();
            int current_int = processor.integration_time;

/*            int trial_timer = 0;

            while (current_int > (95 * data_int_time / 100)) //we have to make sure it is at least 5% less than the data int time
            {
                if (trial_timer > 3)
                {
                    return false;
                }
                MessageBox.Show("Calibration cell still detected - please remove!!!");
                ++trial_timer;
            }*/

            Matrix[] result_matrix = processor.getResultSet();
            return performAnalysis(result_matrix);
        }

        /// <summary>
        /// Performs the same task as AProcessor.adjustIntegrationTime(), 
        /// except that it overrides any constraint that method could have
        /// </summary>
        public void adjustIntTime()
        {
            String current_process = lblLoading.Text;
            lblLoading.Text = "Adjusting integration time...";
            processor.current_spectrometer.DynamicIntegrationAdjustment();
            processor.integration_time = processor.current_spectrometer.integration_time;
            processor.updateNumberOfAverages();
            lblLoading.Text = current_process;
        }

        /// <summary>
        /// Core method that will make PLS computations and compare the results 
        /// with the pre defined calibration gases and determine if the system
        /// passed QA or not. 
        /// </summary>
        /// <param name="results"></param>
        public bool performAnalysis(Matrix[] results)
        {
            bool passed = true;
            double path_length = 1; //for calibration we use a path length of 1 as a normalized value
            LibraryMatrix[] libraries = processor.libraries;

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
                        
                        sum = sum * (1000 / path_length) * processor.getConcetrationFactor(libraries[k],(current_gas_index -1));
                        calibration_concentrations[j] = sum;

                        double percentage = (sum - expected_concentrations[j]) * 100 / expected_concentrations[j];

                        if ("NaN" == percentage.ToString() ||
                            Math.Abs(percentage) > 10)
                        {
                            passed = false;
                        }

                        j = j + 1;
                    }
                }
            }

            return passed;
        }

        /// <summary>
        /// Reads calibration gases and their expected concentration values
        /// from the registery
        /// </summary>
        public void readInCalibrationGases()
        {
            String[] library_names = Libraries.getLibraryNames();
             RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\ArgosQuant");
             string string_selected_gases = null;

             if (regKey != null)
             {
                 string_selected_gases = (string)regKey.GetValue(
                                                            Constants.UVQuantRegistryKeys.CALIBRATION_GASES, "");
                 selected_gases = Util.convertStringToIntArray(
                                                            string_selected_gases, Libraries.getLibraryNames().Length);
             }

            //Now that you have the selected gases in place, extract the expected concentrations
             if (string_selected_gases != "")
             {
                 string[] temp_array = string_selected_gases.Split(new char[] { '|' });
                 if (temp_array != null)
                 {
                     expected_concentrations = new double[temp_array.Length];
                     calibration_concentrations = new double[temp_array.Length];
                     calibration_gases = new string[temp_array.Length];

                     for (int i = 0; i < temp_array.Length; ++i)
                     {
                         string[] calib_conc = temp_array[i].Split(new char[] { '_' });
                         if (calib_conc[2] == "")
                         {                             
                             calib_conc[2] = "1";
                         }
                         expected_concentrations[i] = Double.Parse(calib_conc[2]);
                         int lib_index = int.Parse(calib_conc[0]);
                         int gas_index = int.Parse(calib_conc[1]);
                         if (processor.libraries[lib_index].getGas_name(gas_index) == "Benzene")
                             calibration_gases[i] = library_names[lib_index];
                         else
                             calibration_gases[i] = processor.libraries[lib_index].getGas_name(gas_index);
                     }
                 }
             }
        }

        /// <summary>
        /// Saves the QA result to a log file
        /// </summary>
        /// <param name="passed"></param>
        public void saveResults(bool passed)
        {
             string path = Directory.GetCurrentDirectory() + "\\qa logs\\qa.log";
             StreamWriter sw; 

            //first check if the log directory exists
             if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\qa logs"))
             {
                 Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\qa logs");
             }

            // This text is added only once to the file.
             if (!File.Exists(path))
             {
                 // Create a file to write to.
                 sw = File.CreateText(path);
             }
             else
             {
                sw = File.AppendText(path);
             }

            if (passed)
             {
                 sw.WriteLine(System.DateTime.Now.ToString() + " -- The system passed QA");
                
             }
             else
             {
                 sw.WriteLine(System.DateTime.Now.ToString() + " -- The system failed QA");
             }
             
            //note that the concentration is based on a 1 meter path to the customer
             sw.WriteLine("(Note: all concentrations are based on a 1 meter path length)");

             string header = "";
             for(int i = 0; i < calibration_gases.Length; ++i)
             {
                 header += calibration_gases[i] + "\t\t"; 
             }

             sw.WriteLine(header);

             string values = "";
             for(int j = 0 ; j < calibration_gases.Length; ++j)
             {
                 values += calibration_concentrations[j] + "\t\t";
             }

             sw.WriteLine(values);
             
             sw.WriteLine("\n\n");

             sw.WriteLine("---------------------------------------------");

             sw.WriteLine("\n\n");

             sw.Close();
        }

        /// <summary>
        /// Encapsulates all the different states the calbirator could be
        /// </summary>
        public class CalibrationState
        {
            public static int CHECKING_SYSTEM = 1;
            public static int COLLECTING_BACKGROUND = 2;
            public static int COLLECTING_DATA = 3;
            public static int DISPLAYING_RESULT = 4;
            public static int DONE = 0;
        }

        private void lblData_Click(object sender, EventArgs e)
        {

        }
    }
}