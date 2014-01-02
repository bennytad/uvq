using System;
using System.Collections;
using System.Text;
using System.IO;
using ZedGraph;

namespace UV_Quant
{
    public class DataMatrix : Matrix
    {
        private double[] wl;
        private Hashtable header_info = new Hashtable();
        private FileInfo data_file;
        private ZedGraph.PointPairList list = new ZedGraph.PointPairList();

        public Hashtable getHeaderInfo()
        {
            return header_info;
        }


        public String getInfo(String key)
        {
            return (String)header_info[key];
        }

        public double getWL(int i)
        {
            return wl[i];
        }

        public double[] getWL()
        {
            return wl;
        }

        public DataMatrix(double[] value1, double[,] value2, int rows, Hashtable o_header_info)
        {
            this.header_info = o_header_info;
            this.wl = value1;
            this.m_iElement = value2;
            m_iRows = rows;
            m_iCols = 1;
            buildPointPairList();
        }

        public PointPairList getPointPairList()
        {
            return list;
        }

        public void buildPointPairList()
        {
            for (int i = 0; i < m_iRows; ++i)
            {
                double x = wl[i];
                double y = m_iElement[i, 0];
                list.Add(x, y);
            }
        }

        public DataMatrix(FileInfo file)
        {
            this.data_file = file;
            loadFile();
            buildPointPairList();
        }

        public DataMatrix()
        {

        }

        public DataMatrix(DataMatrix originl_matrix)
        {
            this.m_iCols = originl_matrix.m_iCols;
            this.m_iRows = originl_matrix.m_iRows;
            this.wl = originl_matrix.wl;
            this.data_file = originl_matrix.data_file;
            this.header_info = originl_matrix.header_info;

            m_iElement = new double[m_iRows, m_iCols];

            for (int i = 0; i < this.Rows; ++i)
            {
                this[i, 0] = originl_matrix[i, 0];
            }
            buildPointPairList();
        }

        private void loadFile()
        {
            int file_header = 11;
			//This should be variable in the future, but for now it will be 10
			String line_read = null;
            StreamReader input_file = null;
            try
            {
                input_file = File.OpenText(data_file.FullName);
                line_read = input_file.ReadLine();
                String[] tempHeader = line_read.Split(new char[] { ',' });
                if (tempHeader[1] == UV_QUANT_FILE_FORMAT_VERSION_1)
                {
                    line_read = input_file.ReadLine();
                    tempHeader = line_read.Split(new char[] { ',' });
                    
                    file_header = Int32.Parse(tempHeader[1]);
                }
            }
            catch (Exception exp)
            {
                //return;
            }
            finally
            {
                input_file.Close();
            }

            input_file = File.OpenText(data_file.FullName);

			int i = 0;
            for (i = 0;
                i < file_header &&
                ((line_read = input_file.ReadLine()) != null);
                ++i)
            {
                String[] tempHeader = line_read.Split(new char[] { ',' });

                switch (tempHeader[0])
                {
                    case FileHeader.FILE_VERSION:
                        this.header_info.Add(FileHeader.FILE_VERSION, tempHeader[1]);
                        break;
                    case FileHeader.AVERAGE:
                        this.header_info.Add(FileHeader.AVERAGE, tempHeader[1]);
                        break;
                    case FileHeader.FILE_COUNT:
                        this.header_info.Add(FileHeader.FILE_COUNT, tempHeader[1]);
                        break;
                    case FileHeader.INTEGRATION_TIME:
                        this.header_info.Add(FileHeader.INTEGRATION_TIME, tempHeader[1]);
                        break;
                    case FileHeader.OPERATOR:
                        this.header_info.Add(FileHeader.OPERATOR, tempHeader[1]);
                        break;
                    case FileHeader.PATH_LENGTH:
                        this.header_info.Add(FileHeader.PATH_LENGTH, tempHeader[1]);
                        break;
                    case FileHeader.REFERENCE_DATE:
                        this.header_info.Add(FileHeader.REFERENCE_DATE, tempHeader[1]);
                        break;
                    case FileHeader.REFERENCE_SPECTRA:
                        this.header_info.Add(FileHeader.REFERENCE_SPECTRA, tempHeader[1]);
                        break;
                    case FileHeader.SIGNAL_STRENGTH:
                        this.header_info.Add(FileHeader.SIGNAL_STRENGTH, tempHeader[1]);
                        break;
                    case FileHeader.SITE:
                        this.header_info.Add(FileHeader.SITE, tempHeader[1]);
                        break;
                    case FileHeader.DATE:
                        this.header_info.Add(FileHeader.DATE, tempHeader[1]);
                        break;
                }
            }

            while((line_read = input_file.ReadLine()) != null &&
                "".Equals(line_read))
            {
            }
            ArrayList temp_list = new ArrayList();
            temp_list.Add(line_read);

            while ((line_read = input_file.ReadLine()) != null)
            {
                temp_list.Add(line_read);
            }
            input_file.Close();

            m_iRows = temp_list.Count;
            m_iCols = 1;
            wl = new double[Rows];
            m_iElement = new double[m_iRows, 1];
            for (i = 0; i < Rows; ++i)
            {
                string entry = (string)temp_list[i];
                string[] entries = entry.Split(new char[] { ',' });
                wl[i] = double.Parse(entries[0]);
                this[i, 0] = double.Parse(entries[1]);
            }
        }

        public void writeData(StreamWriter w)
        {
            String file_version = (string)header_info[FileHeaderUvQuan.FILE_VERSION];

            if(UV_QUANT_FILE_FORMAT_VERSION_1.Equals(file_version))
            {
                w.WriteLine(FileHeaderUvQuan.FILE_VERSION + "," +
                (string) header_info[FileHeaderUvQuan.FILE_VERSION]);
                w.WriteLine(FileHeaderUvQuan.NUMER_OF_HEADERS + "," +
                (string) header_info[FileHeaderUvQuan.NUMER_OF_HEADERS]);
                w.WriteLine(FileHeaderUvQuan.SITE + "," +
                (string) header_info[FileHeaderUvQuan.SITE]);
                w.WriteLine(FileHeaderUvQuan.PATH_LENGTH + "," +
                (string) header_info[FileHeaderUvQuan.PATH_LENGTH]);
                w.WriteLine(FileHeaderUvQuan.FILE_COUNT + "," +
                (string) header_info[FileHeaderUvQuan.FILE_COUNT]);
                w.WriteLine(FileHeaderUvQuan.INTEGRATION_TIME + "," +
                (string) header_info[FileHeaderUvQuan.INTEGRATION_TIME]);
                w.WriteLine(FileHeaderUvQuan.AVERAGE + "," +
                (string) header_info[FileHeaderUvQuan.AVERAGE]);
                w.WriteLine(FileHeaderUvQuan.DATE + "," +
                (string) header_info[FileHeaderUvQuan.DATE]);
                w.WriteLine(FileHeaderUvQuan.OPERATOR + "," +
                (string) header_info[FileHeaderUvQuan.OPERATOR]);
                w.WriteLine(FileHeaderUvQuan.REFERENCE_SPECTRA + "," +
                (string) header_info[FileHeaderUvQuan.REFERENCE_SPECTRA]);
                w.WriteLine(FileHeaderUvQuan.SIGNAL_STRENGTH + "," +
                (string) header_info[FileHeaderUvQuan.SIGNAL_STRENGTH]);
                w.WriteLine(FileHeaderUvQuan.REFERENCE_DATE + "," +
                (string)header_info[FileHeaderUvQuan.REFERENCE_DATE]);
                w.WriteLine(FileHeaderUvQuan.USER_NOTE + "," +
                (string) header_info[FileHeaderUvQuan.USER_NOTE]);

                w.WriteLine("");
                w.WriteLine("");

                for (int i = 0; i < this.m_iRows; ++i)
                {
                    w.WriteLine(wl[i] + "," + m_iElement[i,0]);
                }
            
            }
        }

        public const string UV_QUANT_FILE_FORMAT_VERSION_1 = "UV Quant Version 1";
        public const int UV_QUANT_FILE_FORMAT_VERSION_1_NUM_OF_HEADERS = 13;

        public static class FileHeader
        {
            public const string FILE_VERSION = "UVS File Format";
            public const string SITE = "Site";
            public const string PATH_LENGTH = "Pathlength";
            public const string FILE_COUNT = "File Count";
            public const string INTEGRATION_TIME = "Integration Time";
            public const string AVERAGE = "Averages";
            public const string DATE = "Date";
            public const string OPERATOR = "Operator";
            public const string REFERENCE_SPECTRA = "Reference Spectra";
            public const string SIGNAL_STRENGTH = "Signal Strength";
            public const string REFERENCE_DATE = "Reference Date";
        }

        public static class FileHeaderUvQuan
        {
            public const string FILE_VERSION = "UVS File Format";
            public const string NUMER_OF_HEADERS = "# of Headers";
            public const string SITE = "Site";
            public const string PATH_LENGTH = "Pathlength";
            public const string FILE_COUNT = "File Count";
            public const string INTEGRATION_TIME = "Integration Time";
            public const string AVERAGE = "Averages";
            public const string DATE = "Date";
            public const string OPERATOR = "Operator";
            public const string REFERENCE_SPECTRA = "Reference Spectra";
            public const string SIGNAL_STRENGTH = "Signal Strength";
            public const string REFERENCE_DATE = "Reference Date";
            public const string USER_NOTE = "User Note";
        }

    }
}
