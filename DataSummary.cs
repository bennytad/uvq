using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ZedGraph;
using System.IO;

namespace UV_Quant
{
    public class DataSummary
    {
        protected Hashtable gas_data = new Hashtable();
        protected Hashtable site_data = new Hashtable();
        protected string[] gas_array = null;
        protected string file_name = "";
        bool multiple_library = false;


        public bool multipleLibrary
        {
            get { return multiple_library; }
        }

        /// <summary>
        /// Initializes the class from the data summary file specified
        /// with the parameter that is passed
        /// </summary>
        /// <param name="value"></param>
        public DataSummary(string value)
        {
            if (value != null)
            {
                file_name = value.Trim();
                init();
            }
        }

        /// <summary>
        /// Returns the file name of the current data summary
        /// </summary>
        public string file
        {
            get { return file_name; }
            set { file_name = value; }
        }

        /// <summary>
        /// Returns an array containing all the gas names
        /// in the data summary
        /// </summary>
        public string[] gasses
        {
            get { return gas_array; }
        }

        public ArrayList getGasNamesForCmbBox()
        {
            ArrayList temp_gas = new ArrayList();
            for (int i = 0; i < gas_array.Length; ++i)
            {
                temp_gas.Add(gas_array[i]);
            }

            return temp_gas;
        }

        /// <summary>
        /// The class is initialized from the file_name that is already
        /// set in the constructor method
        /// </summary>
        protected void init()
        {
            StreamReader input_file = File.OpenText(file_name);
            string line_read = input_file.ReadLine();
            string temp_line_read = input_file.ReadLine();
            int number_of_gases = 0;

            string[] data_read = temp_line_read.Split(new char[] { ',' });

            string[] tempHeader = line_read.Split(new char[] { ',' });

            if (data_read[5].Trim() == "MULTIPLE-LIBRARY")
            {
                multiple_library = true;
                number_of_gases = (data_read.Length - 7) / 2;

                gas_array = new string[number_of_gases];

                int j = 0;

                for (int i = 7; i < data_read.Length; i+=2)
                {
                    gas_array[j] = tempHeader[i].Trim();
                    ++j;
                    this.gas_data.Add(tempHeader[i].Trim(), new ArrayList());
                    ((ArrayList)gas_data[tempHeader[i].Trim()]).Add(double.Parse(data_read[i]));
                }
            }
            else
            {
                number_of_gases = data_read.Length - 7;
                gas_array = new string[number_of_gases];
                for (int i = 7; i < data_read.Length; ++i)
                {
                    gas_array[i - 7] = tempHeader[i].Trim();
                    this.gas_data.Add(tempHeader[i].Trim(), new ArrayList());
                    ((ArrayList)gas_data[tempHeader[i].Trim()]).Add(double.Parse(data_read[i]));
                }
            }
            
            //Initialize array lists
            site_data.Add("DATES",new ArrayList());
            ((ArrayList)site_data["DATES"]).Add(data_read[0]);

            site_data.Add("FILE_NUMBERS",new ArrayList());
            ((ArrayList)site_data["FILE_NUMBERS"]).Add(data_read[2]);

            site_data.Add("SIGNAL_STRENGTHS",new ArrayList());
            ((ArrayList)site_data["SIGNAL_STRENGTHS"]).Add(double.Parse(data_read[4].Trim()));

            site_data.Add("BACKGROUNDS",new ArrayList());
            ((ArrayList)site_data["BACKGROUNDS"]).Add(data_read[5]);

            while ((line_read = input_file.ReadLine())!=null)
            {
                data_read = line_read.Split(new char[] { ',' });

                ((ArrayList)site_data["DATES"]).Add(data_read[0]);
                ((ArrayList)site_data["FILE_NUMBERS"]).Add(data_read[2]);
                ((ArrayList)site_data["SIGNAL_STRENGTHS"]).Add(double.Parse(data_read[4].Trim()));
                ((ArrayList)site_data["BACKGROUNDS"]).Add(data_read[5]);

                if (multiple_library)
                {
                    int j = 0;

                    for (int i = 7; i < data_read.Length; i += 2)
                    {
                        ((ArrayList)gas_data[gas_array[j]]).Add(double.Parse(data_read[i]));
                        j++;
                    }
                }
                else
                {
                    for (int i = 7; i < data_read.Length; ++i)
                    {
                        ((ArrayList)gas_data[gas_array[i - 7]]).Add(double.Parse(data_read[i]));
                    }
                }
            }
        }

        /// <summary>
        /// Builds a point pair list for graphing for a specific gas
        /// </summary>
        /// <param name="gas_name"></param>
        /// <param name="sig_str_threshold"></param>
        /// <param name="conc_threshold"></param>
        public ZedGraph.PointPairList getPointPairList(
            string gas_name, 
            double sig_str_threshold, 
            double conc_threshold)
        {
            ZedGraph.PointPairList list = new ZedGraph.PointPairList();
            
            ArrayList current_data = (ArrayList)gas_data[gas_name.Trim()];
            ArrayList date_set = (ArrayList)site_data["DATES"];
            ArrayList sig_str = (ArrayList)site_data["SIGNAL_STRENGTHS"];
           
            if (current_data != null && date_set!=null)
            {
                for (int i = 0; i < current_data.Count; ++i)
                {
                    string current_date = (string)date_set[i];
                    double value = (double)current_data[i];

                    if (sig_str_threshold > -1 &&
                        (double)sig_str[i] < sig_str_threshold)
                    {
                        value = 0;
                    }

                    if (conc_threshold > -1 &&
                        value < conc_threshold)
                    {
                        value = 0;
                    }

                    DateTime dt = DateTime.Parse(current_date);

                    XDate xd = new XDate(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);

                    list.Add((double)xd, value);
                }
            }
            return list;
        }

    }

    class SiteData
    {
        public static string DATE = "Sample Date";
        public static string SITE_NAME = "Site Name";
        public static string FILE_NUMBER = "File Number";
        public static string SIGNAL_STRENGTH = "Signal Strength";
        public static string BACKGROUND_FILE = "Background file";
        public static string ERROR_CODE = "Error Code";
    }
}
