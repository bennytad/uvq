using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using ZedGraph;
using System.Collections;
using Microsoft.Win32;
using System.Diagnostics;

namespace UV_Quant
{
    class OxygenProcessor : AProcessor
    {
        public static Stopwatch interval_timer = new Stopwatch();
        public bool collected_data = false;
        public override void init()
        {
            //do nothing for oxygen processor
        }

        /// <summary>
        /// Creates the table both for the continuous and reprocessing modes
        /// </summary>
        /// <param name="cm_datagrid"></param>
        /// <param name="rep_datagrid"></param>
        public override void createTable(DataGridView cm_datagrid, DataGridView rep_datagrid)
        {
            while (cm_datagrid.ColumnCount != 0 &&
                rep_datagrid.ColumnCount != 0)
            {
                cm_datagrid.Columns.RemoveAt(0);
                rep_datagrid.Columns.RemoveAt(0);
            }

            //initialize table
            DataGridViewCheckBoxColumn rep_bolCol = new DataGridViewCheckBoxColumn();
       
            rep_datagrid.Columns.Add("Name", "Gas");
            rep_datagrid.Columns.Add("perc", "%");
            

            rep_datagrid.Columns[0].Width = (rep_datagrid.Width / 4) - 12;
            rep_datagrid.Columns[1].Width = (rep_datagrid.Width / 4) - 12;


            rep_datagrid.Columns[1].ReadOnly = true;
            rep_datagrid.Columns[0].ReadOnly = true;
            rep_datagrid.Rows.Add();
            rep_datagrid[0, 0].Value = "Oxygen";

           
            //  Now populate CM datagrid
            
            DataGridViewCheckBoxColumn cm_bolCol = new DataGridViewCheckBoxColumn();

            cm_datagrid.Columns.Add("Name", "Gas");
            cm_datagrid.Columns.Add("perc", "%");

            cm_datagrid.Columns[0].Width = (rep_datagrid.Width / 4) - 12;
            cm_datagrid.Columns[1].Width = (rep_datagrid.Width / 4) - 12;

            cm_datagrid.Columns[1].ReadOnly = true;
            cm_datagrid.Columns[0].ReadOnly = true;
            cm_datagrid.Rows.Add();
            cm_datagrid[0, 0].Value = "Oxygen";
        }

        // we don't need to load any library for the oxygen processor
        public override void loadLibraries()
        {
           //no library to load
        }

        public override void reprocessData(DataGridView cm_datagrid,
           DataGridView rep_datagrid,
           ZedGraphControl cm_graph,
           ZedGraphControl rep_graph,
           TextBox txtStatus,
           StatusStrip status_bar)
        {
            //we don't need to reprocess here
        }

        /// <summary>
        /// Prepares the data in certain format
        /// </summary>
        /// <param name="values"></param>
        /// <param name="txtStatus"></param>
        /// <param name="statusBar"></param>
        public override void compileData(double[,] values, TextBox txtStatus, StatusStrip statusBar)
        {
            start_wl = current_spectrometer.wavelength[0];
            end_wl = current_spectrometer.wavelength[current_spectrometer.wavelength.Length-1];
            num_entries = current_spectrometer.wavelength.Length;
            

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
                (!multiple_libraries && (background_data.getColumn(0).Length != num_entries)))
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
            if (current_mode != OxygenProcessorMode.INTERMIDIATE)
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
            if (current_mode != OxygenProcessorMode.LOW_OXYGEN &&
                current_mode != OxygenProcessorMode.HIGH_OXYGEN)
            {
                MainConsole.updateMainStatus("Performing oxygen quantifications...", site_name, txtStatus, statusBar);

                double current_sum = 0;

                for (int i = start_integral_pixel;
                    i <= end_integral_pixel;
                    ++i)
                {
                    current_sum += values[i, 0];
                }

                double oxygen_level = ((LO / current_sum) - 1.0) / K;

                MainConsole.updateMainStatus("Updating tabular displays...", site_name, txtStatus, statusBar);
                displayConc(oxygen_level, cm_table, rep_table);

                MainConsole.updateMainStatus("Saving results to file...", site_name, txtStatus, statusBar);
                writeData(oxygen_level, cm_table, rep_table);
            }
            else if (current_mode == OxygenProcessorMode.LOW_OXYGEN)
            {
                integral_low = 0;
                MainConsole.updateMainStatus("Acquiring integral for low Oxygen...", site_name, txtStatus, statusBar);
                for (int i = start_integral_pixel; i <= end_integral_pixel; ++i)
                {
                    integral_low += values[i, 0];
                }
                current_mode = OxygenProcessorMode.INTERMIDIATE;
            }
            else if (current_mode == OxygenProcessorMode.HIGH_OXYGEN)
            {
                integral_high = 0;
                MainConsole.updateMainStatus("Acquiring integral for high Oxygen...", site_name, txtStatus, statusBar);
                for (int i = start_integral_pixel; i <= end_integral_pixel; ++i)
                {
                    integral_high += values[i, 0];
                }
                //since we now have both the integral_high and integral_low, 
                //we can proceed to calculate K, LO
                MainConsole.updateMainStatus("Performing calibration calculation...", site_name, txtStatus, statusBar);
                A = integral_low * integral_high * (OXHighRef - OXLowRef);
                B = (integral_high * OXHighRef) - (integral_low * OXLowRef);
                LO = A / B;
                K = ((LO / integral_high) - 1) / OXHighRef;

                current_mode = OxygenProcessorMode.NORMAL;
            }
        }

        /// <summary>
        /// This method is called at the end of each data processing to display the result of 
        /// the data processing
        /// </summary>
        private void displayConc(double oxygen_level, DataGridView cm_datagrid, DataGridView rep_datagrid)
        {
            if (console_mode == ConsoleState.CONTINUOUS_MONITORING)
            {
                cm_datagrid[1, 0].Value = oxygen_level;
            }
            else
            {
                rep_datagrid[1, 0].Value = oxygen_level;
            }
        }

        /// <summary>
        /// This method is responsible for writing every bit of information (both single beam and data summary)
        /// after each data collection
        /// </summary>
        private void writeData(double oxygen_level, DataGridView cm_datagrid, DataGridView rep_datagrid)
        {

            string data_out = System.DateTime.Now.ToString() + "," + oxygen_level;
            string header_text = "Sample Date, Oxygen Level (%)";

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

        public new void retrieveRegistryEntries()
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\ArgosQuant");
            if (regKey != null)
            {
                start_integral_pixel = (int)regKey.GetValue("start_integral_pixel", 0);
                end_integral_pixel = (int)regKey.GetValue("end_integral_pixel", 0);
                OXLowRef = Double.Parse((string)regKey.GetValue("OXLowRef", "0"));
                OXHighRef = Double.Parse((string)regKey.GetValue("OXHighRef",  "0"));
                integral_low = Double.Parse((string)regKey.GetValue("integral_low",  "0"));
                integral_high = Double.Parse((string)regKey.GetValue("integral_high",  "0"));
                A = Double.Parse((string)regKey.GetValue("A",  "0"));
                B = Double.Parse((string)regKey.GetValue("B",  "0"));
                LO = Double.Parse((string)regKey.GetValue("LO",  "0"));
                K = Double.Parse((string)regKey.GetValue("K", "1"));
                calibration_interval = (int)regKey.GetValue("calibration_interval", 240);
                calibration_duration = (int)regKey.GetValue("calibration_duration", 5);
                calibration_enabled = Util.getBoolValue(regKey,"calibration_enabled", false);
                data_interval = (int)regKey.GetValue("data_interval", 0);
                command_timer = (int)regKey.GetValue("command_timer", 1000);
                command_processing = Util.getBoolValue(regKey, "command_processing", false);
                command_file = (string)regKey.GetValue("command_file", "");
            }

            base.retrieveRegistryEntries();
        }

        public new void saveRegistryEntries()
        {
            RegistryKey regKey = Registry.LocalMachine.CreateSubKey(@"Software\ArgosQuant");
            if (regKey != null)
            {
                regKey.SetValue("start_integral_pixel", start_integral_pixel);
                regKey.SetValue("end_integral_pixel", end_integral_pixel);
                regKey.SetValue("OXLowRef", OXLowRef);
                regKey.SetValue("OXHighRef", OXHighRef);
                regKey.SetValue("integral_low", integral_low);
                regKey.SetValue("integral_high", integral_high);
                regKey.SetValue("A", A);
                regKey.SetValue("B", B);
                regKey.SetValue("LO", LO);
                regKey.SetValue("K", K);
                regKey.SetValue("calibration_interval", calibration_interval);
                regKey.SetValue("calibration_duration", calibration_duration);
                regKey.SetValue("calibration_enabled", calibration_enabled);
                regKey.SetValue("data_interval", data_interval);
                regKey.SetValue("command_timer", command_timer);
                regKey.SetValue("command_processing", command_processing);
                regKey.SetValue("command_file", command_file);
            }
            base.saveRegistryEntries();
        }

        public void updateBackground(DataMatrix current_sample, DataGridView cm_datagrid, DataGridView rep_datagrid, bool force_update)
        {
            //do nothing
        }

        /// <summary>
        /// We wanted to override this method because data acquisition is a little bit different for Oxygen processors
        /// because of the interval between data collection and the need to control the lighting
        /// </summary>
        /// <param name="statusBar"></param>
        /// <param name="txtStatus"></param>
        /// <param name="lblSignalStrength"></param>
        /// <returns></returns>
        public new double[,] scanData(StatusStrip statusBar, TextBox txtStatus, System.Windows.Forms.Label lblSignalStrength)
        {
            RegistryKey regKey = Registry.LocalMachine.CreateSubKey(@"Software\ArgosQuant");
            System.Windows.Forms.Application.DoEvents();

            double[] data = current_spectrometer.spectra;
            double[,] values = new double[data.Length, 1];

            if (
                interval_timer.IsRunning && 
                interval_timer.ElapsedMilliseconds >= 1000 * data_interval &&
                current_mode == OxygenProcessorMode.SLEEP_TIME)
            {
                current_mode = OxygenProcessorMode.NORMAL;
                interval_timer.Stop();
                interval_timer.Reset();
            }

            if (current_mode != OxygenProcessorMode.SLEEP_TIME)
            {
                collected_data = true;
                //FIRST GET THE DATA FROM THE SPECTROMETER HOWEVER MANY TIMES SPECIFIED BY THE AVERAGE
                //First turn the light on

                AvaLightLed light = new AvaLightLed();
                if (current_spectrometer is AvantesSpectrometer)
                {
                    long m_handle = ((AvantesSpectrometer)current_spectrometer).m_DeviceHandle;
                    light.init(m_handle);
                    int success = light.turnLightOn();
                    System.Threading.Thread.Sleep(30);//wait 30 milli seconds before collecting
                    MainConsole.updateMainStatus("Collecting data from spectrometer...", site_name, txtStatus, statusBar);
                }
                Stopwatch stop_watch = new Stopwatch();

                stop_watch.Start();
                int scanns = 0;
                while (stop_watch.ElapsedMilliseconds < 1000 * collection_time && scanning)
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

                //once you finish collecting data, turn the light off
                if (current_spectrometer is AvantesSpectrometer)
                {
                    System.Threading.Thread.Sleep(30);//wait 30 milli seconds after collecting
                    int success = light.turnLightOff();
                }

                MainConsole.updateMainStatus("Calculating averages...", site_name, txtStatus, statusBar);
                for (int i = 0; i < data.Length; ++i)
                {
                    values[i, 0] /= averages;
                }
            }
            else
            {
                collected_data = false;
            }

            return values;
        }

        /// <summary>
        /// Override this method to control processor behavior
        /// </summary>
        /// <returns></returns>
        public new bool shouldProcessData()
        {
            return collected_data;
        }

        /// <summary>
        /// We override this method to go to sleep at the end of each data processing
        /// </summary>
        public new void endIteration(StatusStrip statusBar, TextBox txtStatus)
        {
            if (shouldProcessData())
            {
                if (current_mode == OxygenProcessorMode.NORMAL && data_interval != 0)
                {
                    if (interval_timer.IsRunning)
                    {
                        interval_timer.Stop();
                    }
                    interval_timer.Reset();
                    interval_timer.Start();
                    current_mode = OxygenProcessorMode.SLEEP_TIME;
                    MainConsole.updateMainStatus("Going to sleep for " + data_interval + " seconds...", site_name, txtStatus, statusBar);
                }
            }
        }
    }

    public static class OxygenProcessorMode
    {
        public static int NORMAL = 0;
        public static int LOW_OXYGEN = 1;
        public static int HIGH_OXYGEN = 2;
        public static int SLEEP_TIME = 3;
        public static int INTERMIDIATE = -1; //during this state, no quanitifcation is done
    }
}
