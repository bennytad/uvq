using System;
using System.Collections;
using System.IO;

namespace UV_Quant
{
	/// <summary>
	/// LibraryMatrix encapsulates the main matrix to be used for analysis. 
	/// It supplies a constructor where a file name will be give from which
	/// a matrix will be instantiated.
	/// </summary>
	public class LibraryMatrix : Matrix
	{

		//instantiate it from a file name
		//parameters
		//		1. array of gas names
		//		2. array of concentrations
		//		3. array of gas IDs
		///
		///Functions:
		///		1. constructor from file name
		///		2. get methods for parameters
		
		private string[] gas_names;
		//private double[] concentrations;
        private double[] default_treshold;
		private int[] gas_ids;
		private int[] derivatives;
        private double[] wl;
		private FileInfo library_file;
        Hashtable header_info = new Hashtable();
        Hashtable concentrations = new Hashtable();
        private string lib_name;

        public string library_name
        {
            get { return lib_name; }
            set { lib_name = value; }
        }

        public int file_version
        {
            get { return int.Parse((string)header_info[PLSMatrixHeader.FILE_VERSION]); }
        }

        public int elements
        {
            get { return int.Parse((string)header_info[PLSMatrixHeader.ELEMENTS]); }
        }

        public int pls_entries
        {
            get { return int.Parse((string)header_info[PLSMatrixHeader.PLS_ENTRIES]); }
        }

        public int pls_groups
        {
            get { return int.Parse((string)header_info[PLSMatrixHeader.PLS_GROUPS]); }
        }

        public int sg_width
        {
            get { return int.Parse((string)header_info[PLSMatrixHeader.SG_WIDTH]); }
        }

        public int sg_deriv
        {
            get { return int.Parse((string)header_info[PLSMatrixHeader.SG_DERIV]); }
        }

        public string pls_desc
        {
            get{return (string)header_info[PLSMatrixHeader.PLS_DESC];}
        }

        public int pixle_shift
        {
            get 
            {
                string pixel_shift = (string)header_info[PLSMatrixHeader.PIXLE_SHIFT];

                if (pixel_shift == null)
                {
                    pixel_shift = "0";
                }
                return int.Parse(pixel_shift); 
            }
        }

        public double startWL
        {
            get {return double.Parse((string)header_info[PLSMatrixHeader.WL_START]); }
        }

        public double endWL
        {
            get { return double.Parse((string)header_info[PLSMatrixHeader.WL_STOP]); }
        }

        public int numOfEntries
        {
            get { return int.Parse((string)header_info[PLSMatrixHeader.ENTRIES]); }
        }

		public string[] name
		{
			get {return gas_names;}
			set {gas_names = value;}
		}

        public double[] conc
        {
            get {
                IDictionaryEnumerator temp = concentrations.GetEnumerator();
                if (temp != null)
                {
                    temp.MoveNext();
                    double[] conc = new double[concentrations.Count];
                    for (int i = 0; i < conc.Length; ++i)
                    {
                        conc[i] = (double)temp.Value;
                        temp.MoveNext();
                    }
                    return conc;
                }
                return null;
            }
        }

            public double[] tresholds
        {
            get { return default_treshold; }
            set { default_treshold = value; }
        }

		public int[] id
		{
			get {return gas_ids;}
			set {gas_ids = value;}
		}
		public int[] derivative
		{
			get {return derivatives;}
			set {derivatives = value;}
		}

		public FileInfo lib_file
		{
			get {return library_file;}
			set {library_file = value;}
		}

		public string getGas_name(int i)
		{
			if(i < gas_names.Length)
				return gas_names[i];
			else
				return null;
		}

        public double getDefaultTresholds(int i)
        {
            if (i < default_treshold.Length)
                return default_treshold[i];
            else
                return -1;
        }

		public double getConcentration(string gas_name)
		{
            //if(i < concentrations.Length)
            //    return concentrations[i];
            //else
            //    return -1;
            return (double)concentrations[gas_name];
		}
		
		public int getGas_id(int i)
		{
			if(i < gas_ids.Length)
				return gas_ids[i];
			else
				return -1;
		}

		public int getDerivative(int i)
		{
			if(i < derivatives.Length)
				return derivatives[i];
			else
				return -1;
		}

        public double getWL(int i)
        {
            return wl[i];
        }

        /// <summary>
        /// Takes a string containg the entire library data
        /// and instantiates the library
        /// </summary>
        /// <param name="library_data"></param>
        public LibraryMatrix(String library_data)
        {
            FileInfo main_lib_file = new FileInfo(Directory.GetCurrentDirectory() + "\\argtemp.as");
            File.WriteAllText(main_lib_file.FullName, library_data);
            library_file = main_lib_file;
            load_library();
        }

		/// <summary>
		/// Takes the full path of a CSV file, reads in the contents
		/// and instatiate the class
		/// </summary>
		/// <param name="file_name"></param>
		public LibraryMatrix(FileInfo file_name)
		{
			library_file = file_name;
			load_library();
		}

		public void load_library()
		{
			StreamReader input_file = File.OpenText(lib_file.FullName);
			//This should be variable in the future, but for now it will be 10
			int header_size = 11;
			String line_read = null;

			int i = 0;
            int j = 0;
			for(i = 0; 
				i < header_size &&
				((line_read = input_file.ReadLine())!=null);
				++i)
			{
                String[] tempHeader = line_read.Split(new char[] { ',' });
                
                switch(tempHeader[0])
                {
                    case PLSMatrixHeader.ELEMENTS:
                        this.header_info.Add(PLSMatrixHeader.ELEMENTS, tempHeader[1]);
                        break;
                    case PLSMatrixHeader.FILE_VERSION:
                        this.header_info.Add(PLSMatrixHeader.FILE_VERSION, tempHeader[1]);
                        break;
                    //case PLSMatrixHeader.ENTRIES:
                    //    this.header_info.Add(PLSMatrixHeader.ENTRIES, tempHeader[1]);
                    //    break;
                    //case PLSMatrixHeader.WL_START:
                    //    this.header_info.Add(PLSMatrixHeader.WL_START, tempHeader[1]);
                    //    break;
                    //case PLSMatrixHeader.WL_STOP:
                    //    this.header_info.Add(PLSMatrixHeader.WL_STOP, tempHeader[1]);
                    //    break;
                    case PLSMatrixHeader.PLS_ENTRIES:
                        this.header_info.Add(PLSMatrixHeader.PLS_ENTRIES, tempHeader[1]);
                        break;
                    case PLSMatrixHeader.PLS_GROUPS:
                        this.header_info.Add(PLSMatrixHeader.PLS_GROUPS, tempHeader[1]);
                        break;
                    case PLSMatrixHeader.SG_WIDTH:
                        this.header_info.Add(PLSMatrixHeader.SG_WIDTH, tempHeader[1]);
                        break;
                    case PLSMatrixHeader.SG_DERIV:
                        this.header_info.Add(PLSMatrixHeader.SG_DERIV, tempHeader[1]);
                        break;
                    case PLSMatrixHeader.PIXLE_SHIFT:
                        this.header_info.Add(PLSMatrixHeader.PIXLE_SHIFT, tempHeader[1]);
                        break;
                    case PLSMatrixHeader.PLS_DESC:
                        this.header_info.Add(PLSMatrixHeader.PLS_DESC, tempHeader[1]);
                        break;
                }
			}

            //Now you need to read in the file until you encouter the word Spectra_Start

            while((line_read = input_file.ReadLine()) != null)
            {
                string[] tempHeader = line_read.Split(new char[] { ',' });
                //you are reading an empty line and do not need to do anything
                if(tempHeader[0].Equals("Spectra_Start"))
                    break;
            }

            //now readin the gas headers
            int gas_header_size = 8;

            string fake_wl_start = "";
            string fake_wl_end = "";

            for (i = 0;
                i < gas_header_size &&
                ((line_read = input_file.ReadLine()) != null);
                ++i)
            {
                String[] tempHeader = line_read.Split(new char[] { ',' });
                switch (tempHeader[0])
                {
                    case GASHeaders.GAS_NAME:
                        {
                            //a little cumbersome, but all we are trying to do is get the number of gases from the header
                            //information and instantiate our gas_names array to that number
                            gas_names = new string[Int32.Parse((string)header_info[PLSMatrixHeader.ELEMENTS].ToString())];
                     
                            for (j = 0; j < gas_names.Length; ++j)
                            {
                                gas_names[j] = tempHeader[j + 1];
                            }
                            break;
                        }
                    case GASHeaders.CAL_CONC:
                        {
                            int length = Int32.Parse((string)header_info[PLSMatrixHeader.ELEMENTS].ToString());
                     
                            for (j = 0; j < length; ++j)
                            {
                                //concentrations[j] = double.Parse(tempHeader[j + 1]);
                                if (!concentrations.ContainsKey(gas_names[j]))
                                {
                                    concentrations.Add(gas_names[j], double.Parse(tempHeader[j + 1]));
                                }
                            }
                            break;
                        }
                    case GASHeaders.DEFAULT_TRESHOLD:
                        {
                            default_treshold = new double[Int32.Parse((string)header_info[PLSMatrixHeader.ELEMENTS].ToString())];

                            for (j = 0; j < default_treshold.Length; ++j)
                            {
                                default_treshold[j] = double.Parse(tempHeader[j + 1]);
                            }
                            break;
                        }
                    case GASHeaders.RESULT_NUM:
                        {
                            int num_gas = Int32.Parse((string)header_info[PLSMatrixHeader.ELEMENTS].ToString());
                            gas_ids = new int[num_gas];
                   
                            for (j = 0; j < gas_ids.Length; ++j)
                            {
                                gas_ids[j] = int.Parse(tempHeader[num_gas + j + 1]);
                            }
                            break;
                        }
                    case GASHeaders.START:
                        {
                            fake_wl_start = tempHeader[j + 1];
                            break;
                        }
                    case GASHeaders.STOP:
                        {
                            fake_wl_end = tempHeader[j + 1];
                            break;
                        }
                }
            }   //End of reading gas headers

            this.header_info.Add(PLSMatrixHeader.WL_START, fake_wl_start);
            this.header_info.Add(PLSMatrixHeader.WL_STOP, fake_wl_end);

            //  Now read in the actual PLS matrix
            ArrayList temp_matrix = new ArrayList();
            //  First read until you reach the begining

            int entry_counter = 0;

            while (true)
            {
                line_read = input_file.ReadLine();
                string[] temp = line_read.Split(new char[] { ',' });
                if (double.Parse(temp[0]) >= double.Parse(fake_wl_start))
                {
                    temp_matrix.Add(line_read);
                    ++entry_counter;
                    break;
                }
            }
            while ((line_read = input_file.ReadLine()) != null)
            {
                string[] temp = line_read.Split(new char[] { ',' });
                if (double.Parse(temp[0]) > double.Parse(fake_wl_end))
                {
                    break;
                }

                temp_matrix.Add(line_read);
                ++entry_counter;
                
            }

            this.header_info.Add(PLSMatrixHeader.ENTRIES, Convert.ToString(entry_counter));

            input_file.Close();

            m_iCols = Int32.Parse((string)header_info[PLSMatrixHeader.ELEMENTS].ToString());
            m_iRows = temp_matrix.Count;
            m_iElement = new double[m_iRows,m_iCols];
            wl = new double[m_iRows];

            for (i = 0; i < m_iRows; ++i)
            {
                string entry = (string)temp_matrix[i];
                string[] entries = entry.Split(new char[] { ',' });
                wl[i] = double.Parse(entries[0]);

                for (j = 1; j <= m_iCols; ++j)
                {
                    m_iElement[i, j - 1] = double.Parse(entries[j + m_iCols]);
                }
            }

		}

        public static class PLSMatrixHeader
        {
            public const string FILE_VERSION = "File_Version";
            public const string ELEMENTS = "Elements";
            public const string ENTRIES = "Entries";
            public const string WL_START = "WL_Start";
            public const string WL_STOP = "WL_Stop";
            public const string PLS_ENTRIES = "PLSEntries";
            public const string PLS_GROUPS = "PLSGroups";
            public const string SG_WIDTH = "SGWidth";
            public const string SG_DERIV = "SGDeriv";
            public const string PIXLE_SHIFT = "PixleShift";
            public const string PLS_DESC = "PLSDesc";
        }

        public static class GASHeaders
        {
            public const string GAS_NAME = "Gas";
            public const string START = "Start";
            public const string STOP = "Stop";
            public const string PLS_GROUP = "PLSGroup";
            public const string RESULT_NUM = "ResultNum";
            public const string MIN_RSQUARED = "Min_Rsquared";
            public const string CAL_CONC = "Cal_conc";
            public const string DEFAULT_TRESHOLD = "DefaultTreshold";
        }
	}
}
