using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace UV_Quant
{
    
    /// <summary>
    /// This class is used to encapsulate utility methods that will be used for data manipulation
    /// as a rule all methods will be static members so that no class instatiation will not be
    /// needed
    /// </summary>
    class Util
    {

        public static String divider = "*****";

        public static String MASTER_SERIAL_1 = "FakeSerial";
        public static String MASTER_SERIAL_2 = "ImpossibleSerial";
        public static String MASTER_SERIAL_3 = "MasterSerial";
        public static String REGISTRY_KEY = @"Software\ArgosQuant";

        [DllImport("cerex_mn.dll")]
        public static extern bool Spec_Interpolate(ref double wlIn,			// Pointer to an array of wavelengths of the original spectrum
                                                    ref double dataIn,		// Pointer to an array of intensity values of the original spectrum
                                                        short numPointsIn,	// Number of data points in the original spectrum
                                                        short derivative,		// Order of the derivative to perform when doing the cubic spline interpolation
                                                    ref double wlOut,			// Pointer to an array with desired interpolated wavelengths
                                                        short numPointsOut,	// Desired number of points in the interpolated spectrum
                                                    ref double resultArray);	// Pointer to an array filled with resulting interpolated spectrum

       
        public static DataMatrix spline(DataMatrix original_data, double start_wl, double end_wl, short num_out, short derivative)
        {
            double steps = (end_wl - start_wl) / (num_out - 1);
            double[] wl_in = original_data.getWL();
            double[] data_in = original_data.getColumn(0);
            short num_in = (short)original_data.getWL().Length;
            double[] wl_out = new double[num_out];
            double[] data_out = new double[num_out];

            for (int i = 0; i < num_out; ++i)
            {
                wl_out[i] = start_wl + (steps * i);
            }

            Spec_Interpolate(ref wl_in[0], ref data_in[0], num_in, derivative, ref wl_out[0], num_out, ref data_out[0]);

            double[,] new_matrix_double = new double[num_out, 1];
           
            for (int i = 0; i < num_out; ++i)
            {
                new_matrix_double[i, 0] = data_out[i];
            }

            DataMatrix mod_matrix = new DataMatrix(wl_out, new_matrix_double, num_out, original_data.getHeaderInfo());
           
            return mod_matrix;
        }

        public static DataMatrix getAbsorbance(DataMatrix background, DataMatrix data)
        {
            double[,] new_matrix_double = new double[background.Rows, 1];
            double[] background_data = background.getColumn(0);
            double[] data_data = data.getColumn(0);
            
            for (int i = 0; i < background.Rows; ++i)
            {
                if (background_data[i] != 0 && data_data[i] != 0)
                    new_matrix_double[i, 0] = Math.Log(background_data[i]) / Math.Log(data_data[i]);
                else
                    new_matrix_double[i, 0] = -1;
            }

            DataMatrix abs_matrix = new DataMatrix(background.getWL(), new_matrix_double, data_data.Length, data.getHeaderInfo());
            return abs_matrix;
        }

        public static DataMatrix getAbsorbanceViaTransmitance(DataMatrix background, DataMatrix data)
        {
            double[,] new_matrix_double = new double[background.Rows, 1];
            double[] background_data = background.getColumn(0);
            double[] data_data = data.getColumn(0);
            int av_start = 0;
            int av_end = background.Rows - 1;
            double cor = 1;
            double data_mean = 0;
            double bkgd_mean = 0;
            double temp_sum = 0;

            while (background.getWL(av_start) != 276 && av_start < av_end)
            {
                ++av_start;
            }

            while (background.getWL(av_end) != 277.35 && av_end > av_start)
            {
                --av_end;
            }

            for (int i = av_start; i <= av_end; ++i)
            {
                temp_sum += data[i, 0];
            }
            data_mean = temp_sum / (av_end - av_start + 1);

            temp_sum = 0;
            for (int i = av_start; i <= av_end; ++i)
            {
                temp_sum += background[i, 0];
            }
            bkgd_mean = temp_sum / (av_end - av_start + 1);
            cor = bkgd_mean / data_mean;


            for (int i = 0; i < background.Rows; ++i)
            {
                if (background_data[i] != 0 && data_data[i] != 0)
                {
                    new_matrix_double[i, 0] = -1 * Math.Log((cor) * (data_data[i] / background_data[i]))/Math.Log(10);
                   // new_matrix_double[i, 0] = (cor) * (data_data[i] / background_data[i]);
                }
                else
                    new_matrix_double[i, 0] = -1;
            }

            DataMatrix abs_matrix = new DataMatrix(background.getWL(), new_matrix_double, data_data.Length, data.getHeaderInfo());
            return abs_matrix;
        }

        public static DataMatrix sGolay(DataMatrix data, int width, int deriv)
        {
            double[] coef = SGolayConstants.getCoefficients(width,deriv);

            if (coef != null)
            {
                double[,] new_matrix_double = new double[data.Rows, 1];

                for (int i = width - 1; i < data.Rows; ++i)
                {
                    double current_sum = 0;
                    for (int j = 0; j < width; ++j)
                    {
                        current_sum = current_sum + data[i - j, 0] * coef[j];
                    }
                    new_matrix_double[i, 0] = current_sum;
                }

                DataMatrix result = new DataMatrix(data.getWL(), new_matrix_double, data.getColumn(0).Length, data.getHeaderInfo());

                return result;
            }

            return null;
        }

        public static DataMatrix shift(DataMatrix data, int amount)
        {
            DataMatrix result = new DataMatrix(data);
            
            int start_point = 0;
            int end_point = result.Rows - 1;

            if (amount > 0)
            {
                start_point = amount;
            }
            else
            {
                end_point = end_point + amount;
            }
            amount = -1 * amount;
            for (int i = start_point; i < end_point; ++i)
            {
                result[i, 0] = data[i + amount, 0];
            }
            result.buildPointPairList();
            return result;
        }

        /// <summary>
        /// A generic way to save a any given matrix. If you need to save
        /// a <code>DataMatrix</code>, then use the <code>saveDataMatrix</code>
        /// method
        /// </summary>
        /// <param name="out_put"></param>
        /// <param name="matrix"></param>
        public static void saveMatrix(FileInfo out_put, Matrix matrix)
        {
            StreamWriter writer = out_put.CreateText();

            for (int i = 0; i < matrix.Rows; ++i)
            {
                string one_line = Convert.ToString(matrix[i,0]);
                for (int j = 1; j < matrix.Cols; ++j)
                {
                    one_line += "," + Convert.ToString(matrix[i, j]);
                }
                writer.WriteLine(one_line);
            }
            writer.Close();
        }

        /// <summary>
        /// A utility method to write out a <code>DataMatrix</code> in the form
        /// wl,data
        /// </summary>
        /// <param name="out_put"></param>
        /// <param name="matrix"></param>
        public static void saveDataMatrix(FileInfo out_put, DataMatrix matrix)
        {
            StreamWriter writer = out_put.CreateText();


            double[] wl = matrix.getWL();
            double[] data = matrix.getColumn(0);

            for (int i = 0; i < matrix.Rows; ++i)
            {
                string one_line = Convert.ToString(wl[i]);
                one_line += "," + Convert.ToString(data[i]);
            
                writer.WriteLine(one_line);
            }
            writer.Close();
        }

        public static void saveLibraryMatrices(FileInfo out_put, LibraryMatrix[] matrices, string[] names)
        {
            if (out_put == null
                || matrices == null
                || names == null
                || (names.Length != matrices.Length))
            {
                return;
            }
            
            int number_of_martices = matrices.Length;

            StreamWriter writer = out_put.CreateText();
            writer.WriteLine(number_of_martices);

            for (int k = 0; k < number_of_martices; ++k)
            {
                LibraryMatrix matrix = matrices[k];

                writer.WriteLine(divider);
                writer.WriteLine(names[k]);
                writer.WriteLine(divider);

                writer.WriteLine(LibraryMatrix.PLSMatrixHeader.FILE_VERSION + "," + matrix.file_version);
                writer.WriteLine(LibraryMatrix.PLSMatrixHeader.ELEMENTS + "," + matrix.elements);
                writer.WriteLine(LibraryMatrix.PLSMatrixHeader.ENTRIES + "," + matrix.numOfEntries);
                writer.WriteLine(LibraryMatrix.PLSMatrixHeader.WL_START + "," + matrix.startWL);
                writer.WriteLine(LibraryMatrix.PLSMatrixHeader.WL_STOP + "," + matrix.endWL);
                writer.WriteLine(LibraryMatrix.PLSMatrixHeader.PLS_ENTRIES + "," + matrix.pls_entries);
                writer.WriteLine(LibraryMatrix.PLSMatrixHeader.PLS_GROUPS + "," + matrix.pls_groups);
                writer.WriteLine(LibraryMatrix.PLSMatrixHeader.SG_WIDTH + "," + matrix.sg_width);
                writer.WriteLine(LibraryMatrix.PLSMatrixHeader.SG_DERIV + "," + matrix.sg_deriv);
                writer.WriteLine(LibraryMatrix.PLSMatrixHeader.PIXLE_SHIFT + "," + matrix.pixle_shift);
                writer.WriteLine(LibraryMatrix.PLSMatrixHeader.PLS_DESC + "," + matrix.pls_desc);
                writer.WriteLine("");
                writer.WriteLine("Spectra_Start");

                string header = LibraryMatrix.GASHeaders.GAS_NAME;

                for (int h = 0; h < matrix.conc.Length; ++h)
                {
                    if (matrix.getGas_name(h) == "Benzene" && matrix.library_name == "Ozone")
                        header += "," + matrix.library_name;
                    else
                        header += "," + matrix.getGas_name(h);
                }

                writer.WriteLine(header);

                header = LibraryMatrix.GASHeaders.START;

                for (int h = 0; h < matrix.conc.Length; ++h)
                {
                    header += "," + matrix.startWL;
                }

                writer.WriteLine(header);

                header = LibraryMatrix.GASHeaders.STOP;

                for (int h = 0; h < matrix.conc.Length; ++h)
                {
                    header += "," + matrix.endWL;
                }

                writer.WriteLine(header);

                header = LibraryMatrix.GASHeaders.PLS_GROUP;

                for (int h = 0; h < matrix.conc.Length; ++h)
                {
                    header += "," + "-1";
                }

                writer.WriteLine(header);
                header = LibraryMatrix.GASHeaders.RESULT_NUM;

                for (int h = 0; h < matrix.conc.Length; ++h)
                {
                    header += "," + matrix.getGas_id(h);
                }

                writer.WriteLine(header);
                header = LibraryMatrix.GASHeaders.MIN_RSQUARED;

                for (int h = 0; h < matrix.conc.Length; ++h)
                {
                    header += "," + "0.9";
                }

                writer.WriteLine(header);
                header = LibraryMatrix.GASHeaders.CAL_CONC;

                for (int h = 0; h < matrix.conc.Length; ++h)
                {
                    if (matrix.getGas_name(h) == "Benzene" && matrix.library_name == "Ozone")
                        header += "," + matrix.getConcentration(matrix.library_name);
                    else
                        header += "," + matrix.getConcentration(matrix.getGas_name(h));
                }

                writer.WriteLine(header);
                header = LibraryMatrix.GASHeaders.DEFAULT_TRESHOLD;

                for (int h = 0; h < matrix.conc.Length; ++h)
                {
                    header += "," + matrix.getDefaultTresholds(h);
                }

                writer.WriteLine(header);

                for (int i = 0; i < matrix.Rows; ++i)
                {
                    string one_line = Convert.ToString(matrix.getWL(i));
                    for (int j = 0; j < matrix.Cols; ++j)
                    {
                        one_line += "," + Convert.ToString(matrix[i, j]);
                    }
                    writer.WriteLine(one_line);
                }
            }
            writer.Close();
        }

        public static class SGolayConstants
        {
            private static Hashtable ht_coeficients = new Hashtable();

            public static double[] getCoefficients(int sg_width, int derivative)
            {
                string key = sg_width.ToString() + "_" + derivative.ToString();
                double[] result = (double[])ht_coeficients[key];
                return result;
            }

            static SGolayConstants()
            {
                double[] _5_POINT_2DER_COEF = {0.285714286,-0.142857143,-0.285714286,-0.142857143,0.285714286};
                double[] _7_POINT_2DER_COEF = { 0.119047619, -1.249E-16, -0.071428571, -0.095238095, -0.071428571, 3.1225E-17, 0.119047619 };
                double[] _9_POINT_2DER_COEF = { 0.060606061, 0.015151515, -0.017316017, -0.036796537, -0.043290043, -0.036796537, -0.017316017, 0.015151515, 0.060606061 };
                double[] _11_POINT_2DER_COEF = { 0.034965035, 0.013986014, -0.002331002, -0.013986014, -0.020979021, -0.023310023, -0.020979021, -0.013986014, -0.002331002, 0.013986014, 0.034965035 };
                double[] _13_POINT_2DER_COEF = { 0.021978022, 0.010989011, 0.001998002, -0.004995005, -0.00999001, -0.012987013, -0.013986014, -0.012987013, -0.00999001, -0.004995005, 0.001998002, 0.010989011, 0.021978022 };
                double[] _15_POINT_2DER_COEF = { 0.014705882, 0.008403361, 0.003070459, -0.001292825, -0.00468649, -0.007110537, -0.008564964, -0.009049774, -0.008564964, -0.007110537, -0.00468649, -0.001292825, 0.003070459, 0.008403361, 0.014705882 };
                double[] _17_POINT_2DER_COEF = { 0.010319917, 0.006449948, 0.003095975, 0.000257998, -0.002063983, -0.003869969, -0.005159959, -0.005933953, -0.00619195, -0.005933953, -0.005159959, -0.003869969, -0.002063983, 0.000257998, 0.003095975, 0.006449948, 0.010319917 };
                double[] _19_POINT_2DER_COEF = { 0.007518797, 0.005012531, 0.00280112, 0.000884564, -0.000737137, -0.002063983, -0.003095975, -0.003833112, -0.004275394, -0.004422822, -0.004275394, -0.003833112, -0.003095975, -0.002063983, -0.000737137, 0.000884564, 0.00280112, 0.005012531, 0.007518797 };
                double[] _21_POINT_2DER_COEF = { 0.005646527, 0.003952569, 0.002436922, 0.001099587, -5.94371E-05, -0.00104015, -0.001842551, -0.002466641, -0.002912419, -0.003179886, -0.003269042, -0.003179886, -0.002912419, -0.002466641, -0.001842551, -0.00104015, -5.94371E-05, 0.001099587, 0.002436922, 0.003952569, 0.005646527 };
                double[] _23_POINT_2DER_COEF = { 0.004347826, 0.003162055, 0.002089215, 0.001129305, 0.000282326, -0.000451722, -0.00107284, -0.001581028, -0.001976285, -0.002258611, -0.002428007, -0.002484472, -0.002428007, -0.002258611, -0.001976285, -0.001581028, -0.00107284, -0.000451722, 0.000282326, 0.001129305, 0.002089215, 0.003162055, 0.004347826 };

                double[] _5_POINT_1DER_COEF = {-0.77142857,0.18571429,0.57142857,0.38571429,-0.37142857};
                double[] _7_POINT_1DER_COEF = {-0.46428571,-0.07142857,0.17857143,0.28571429,0.25,0.07142857,-0.25};
                double[] _9_POINT_1DER_COEF = {-0.30909091,-0.11060606,0.03593074,0.13051948,0.17316017,0.16385281,0.1025974,-0.01060606,-0.17575758};
                double[] _11_POINT_1DER_COEF = {-0.22027972,-0.10629371,-0.01561772,0.05174825,0.0958042,0.11655012,0.11398601,0.08811189,0.03892774,-0.03356643,-0.12937063};
                double[] _13_POINT_1DER_COEF = {-0.16483516,-0.09340659,-0.03396603,0.01348651,0.04895105,0.07242757,0.08391608,0.08341658,0.07092907,0.04645355,0.00999001,-0.03846154,-0.0989011};
                double[] _15_POINT_1DER_COEF = {-0.12794118,-0.0802521,-0.03935036,-0.00523594,0.02209114,0.0426309,0.05638332,0.06334842,0.06352618,0.05691661,0.04351972,0.02333549,-0.00363607,-0.03739496,-0.07794118};
                double[] _17_POINT_1DER_COEF = {-0.10216718,-0.06875645,-0.03947368,-0.01431889,0.00670795,0.02360681,0.03637771,0.04502064,0.0495356,0.0499226,0.04618163,0.03831269,0.02631579,0.01019092,-0.01006192,-0.03444272,-0.0629515};
                double[] _19_POINT_1DER_COEF = {-0.08345865,-0.05914787,-0.03749079,-0.01848739,-0.0021377,0.01155831,0.02260062,0.03098924,0.03672416,0.0398054,0.04023294,0.03800678,0.03312693,0.0255934,0.01540616,0.00256524,-0.01292938,-0.03107769,-0.0518797};
                double[] _21_POINT_1DER_COEF = {-0.06945229,-0.051214,-0.03475883,-0.02008678,-0.00719784,0.00390799,0.01323071,0.02077031,0.02652679,0.03050016,0.03269042,0.03309757,0.0317216,0.02856251,0.02362032,0.016895,0.00838658,-0.00190496,-0.01397961,-0.02783738,-0.04347826};
                double[] _23_POINT_1DER_COEF = { -0.05869565, -0.04466403, -0.03187465, -0.0203275, -0.01002259, -0.00095991, 0.00686053, 0.01343874, 0.0187747, 0.02286844, 0.02571993, 0.02732919, 0.02769622, 0.02682101, 0.02470356, 0.02134387, 0.01674195, 0.0108978, 0.00381141, -0.00451722, -0.01408809, -0.02490119, -0.03695652 };

                double[] _5_POINT_0DER_COEF = { 0.88571429, 0.25714286, -0.08571429, -0.14285714, 0.08571429 };
                double[] _7_POINT_0DER_COEF = {0.76190476,0.35714286,0.07142857,-0.0952381,-0.14285714,-0.07142857,0.11904762 };
                double[] _9_POINT_0DER_COEF = {0.66060606,0.38181818,0.16363636,0.00606061,-0.09090909,-0.12727273,-0.1030303,-0.01818182,0.12727273 };
                double[] _11_POINT_0DER_COEF = {0.58041958,0.37762238,0.20979021,0.07692308,-0.02097902,-0.08391608,-0.11188811,-0.1048951,-0.06293706,0.01398601,0.12587413 };
                double[] _13_POINT_0DER_COEF = {0.51648352,0.36263736,0.23076923,0.12087912,0.03296703,-0.03296703,-0.07692308,-0.0989011,-0.0989011,-0.07692308,-0.03296703,0.03296703,0.12087912 };
                double[] _15_POINT_0DER_COEF = {0.46470588,0.34411765,0.23823529,0.14705882,0.07058824,0.00882353,-0.03823529,-0.07058824,-0.08823529,-0.09117647,-0.07941176,-0.05294118,-0.01176471,0.04411765,0.11470588 };
                double[] _17_POINT_0DER_COEF = {0.42208462,0.3250774,0.23839009,0.1620227,0.09597523,0.04024768,-0.00515996,-0.04024768,-0.06501548,-0.07946336,-0.08359133,-0.07739938,-0.06088751,-0.03405573,0.00309598,0.0505676,0.10835913 };
                double[] _19_POINT_0DER_COEF = {0.38646617,0.30676692,0.23458647,0.16992481,0.11278195,0.06315789,0.02105263,-0.01353383,-0.0406015,-0.06015038,-0.07218045,-0.07669173,-0.07368421,-0.06315789,-0.04511278,-0.01954887,0.01353383,0.05413534,0.10225564 };
                double[] _21_POINT_0DER_COEF = {0.35629588,0.28966685,0.22868436,0.17334839,0.12365895,0.07961604,0.04121965,0.00846979,-0.01863354,-0.04009034,-0.05590062,-0.06606437,-0.07058159,-0.06945229,-0.06267645,-0.05025409,-0.03218521,-0.00846979,0.02089215,0.05590062,0.09655562 };
                double[] _23_POINT_0DER_COEF = {0.33043478,0.27391304,0.22173913,0.17391304,0.13043478,0.09130435,0.05652174,0.02608696,0,-0.02173913,-0.03913043,-0.05217391,-0.06086957,-0.06521739,-0.06521739,-0.06086957,-0.05217391,-0.03913043,-0.02173913,0,0.02608696,0.05652174,0.09130435 };

                ht_coeficients.Add("5_2", _5_POINT_2DER_COEF);
                ht_coeficients.Add("7_2", _7_POINT_2DER_COEF);
                ht_coeficients.Add("9_2", _9_POINT_2DER_COEF);
                ht_coeficients.Add("11_2", _11_POINT_2DER_COEF);
                ht_coeficients.Add("13_2", _13_POINT_2DER_COEF);
                ht_coeficients.Add("15_2", _15_POINT_2DER_COEF); 
                ht_coeficients.Add("17_2", _17_POINT_2DER_COEF);
                ht_coeficients.Add("19_2", _19_POINT_2DER_COEF);
                ht_coeficients.Add("21_2", _21_POINT_2DER_COEF);
                ht_coeficients.Add("23_2", _23_POINT_2DER_COEF);

                ht_coeficients.Add("5_1", _5_POINT_1DER_COEF);
                ht_coeficients.Add("7_1", _7_POINT_1DER_COEF);
                ht_coeficients.Add("9_1", _9_POINT_1DER_COEF);
                ht_coeficients.Add("11_1", _11_POINT_1DER_COEF);
                ht_coeficients.Add("13_1", _13_POINT_1DER_COEF);
                ht_coeficients.Add("15_1", _15_POINT_1DER_COEF);
                ht_coeficients.Add("17_1", _17_POINT_1DER_COEF);
                ht_coeficients.Add("19_1", _19_POINT_1DER_COEF);
                ht_coeficients.Add("21_1", _21_POINT_1DER_COEF);
                ht_coeficients.Add("23_1", _23_POINT_1DER_COEF);

                ht_coeficients.Add("5_0", _5_POINT_0DER_COEF);
                ht_coeficients.Add("7_0", _7_POINT_0DER_COEF);
                ht_coeficients.Add("9_0", _9_POINT_0DER_COEF);
                ht_coeficients.Add("11_0", _11_POINT_0DER_COEF);
                ht_coeficients.Add("13_0", _13_POINT_0DER_COEF);
                ht_coeficients.Add("15_0", _15_POINT_0DER_COEF);
                ht_coeficients.Add("17_0", _17_POINT_0DER_COEF);
                ht_coeficients.Add("19_0", _19_POINT_0DER_COEF);
                ht_coeficients.Add("21_0", _21_POINT_0DER_COEF);
                ht_coeficients.Add("23_0", _23_POINT_0DER_COEF);
            }

        }

        /// <summary>
        /// This method takes a jagged array and convert it to a string representation
        /// Eg: 
        /// 1,5,8
        /// 4,2,8,10,23
        /// will be:
        /// 1_1|1_5|1_8|2_4|2_2|2_8|2_10|2_23
        /// </summary>
        /// <param name="give_array"></param>
        /// <returns></returns>
        public static string convertIntArrayToString(int[][] given_array)
        {
            string return_string = "";
            for (int i = 0; i < given_array.Length; ++i)
            {
                if (given_array[i] != null)
                {
                    for (int k = 0; k < given_array[i].Length; ++k)
                    {
                        if (return_string != "")
                        {
                            return_string = return_string + "|" + i + "_" + given_array[i][k];
                        }
                        else
                        {
                            return_string = i + "_" + given_array[i][k];
                        }
                    }
                }
            }

            return return_string;
        }

        /// <summary>
        /// This method converts a string of the form
        /// 1_y1|1_y2|2_y3|2_y4|2_y5...to a jagged array of 
        /// 
        /// y1,y2
        /// y3,y4,y5
        /// </summary>
        /// <param name="given_string"></param>
        /// <param name="array_size">This describes the size of the needed array (i.e. int[array_size][])</param>
        /// <returns></returns>
        public static int[][] convertStringToIntArray(string given_string, int array_size)
        {
            if (given_string != null && given_string.Length != 0)
            {
                int[][] return_array = new int[array_size][];
                string[] temp_array = given_string.Split(new char[] { '|' });
                ArrayList current_gases = new ArrayList();
                int current_lib_index = 0;

                for (int i = 0; i < temp_array.Length; ++i)
                {
                    string[] lib_gas_index = temp_array[i].Split(new char[] { '_' });
                    if (current_lib_index != int.Parse(lib_gas_index[0]))
                    {
                        if (current_gases.Count != 0)
                        {
                            int[] temp = new int[current_gases.Count];
                            current_gases.CopyTo(temp);
                            return_array[current_lib_index] = temp;
                        }
                        current_lib_index = int.Parse(lib_gas_index[0]);
                        current_gases = new ArrayList();
                    }

                    current_gases.Add(int.Parse(lib_gas_index[1]));
                }

                if (current_gases.Count != 0)
                {
                    int[] temp = new int[current_gases.Count];
                    current_gases.CopyTo(temp);
                    return_array[current_lib_index] = temp;
                }

                return return_array;
            }
            return new int[array_size][];
        }

        /// <summary>
        /// checks if a string is a double or an integer, 
        /// if neither, it will return false
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool isNumeric(string value)
        {
            if (value == null)
            {
                return false;
            }

            try
            {
                Convert.ToDouble(value);
            }
            catch (Exception ex1)
            {
                try
                {
                    Convert.ToInt32(value);
                }
                catch (Exception ex2)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Fetchs a boolean value from the registry
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static bool getBoolValue(RegistryKey key, string name, bool def)
        {
            string value = (string)key.GetValue(name, null);

            if (value == null || "" == value)
            {
                return def;
            }
            else if ("false" == value.ToLower() ||
                (Util.isNumeric(value) && Convert.ToDouble(value) == 0))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// This method checks if a particular password matches the current spectrometer
        /// </summary>
        /// <param name="password"></param>
        /// <param name="serial_number"></param>
        /// <returns></returns>
        public static bool isCorrectPassword(string password, string serial_number)
        {
            string correct_password = getPasswordFromSerial(serial_number);
            string master_password1 = getPasswordFromSerial(MASTER_SERIAL_1);
            string master_password2 = getPasswordFromSerial(MASTER_SERIAL_2);
            string master_password3 = getPasswordFromSerial(MASTER_SERIAL_3);

            if (correct_password == password ||
                master_password1 == password ||
                master_password2 == password ||
                master_password3 == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// This method generates a password/software serial number from the
        /// serial number of the spectrometer
        /// </summary>
        /// <param name="serial_number"></param>
        /// <returns></returns>
        public static string getPasswordFromSerial(string serial_number)
        {
            String string_serial = "";
            int sum = 0;
            for (int i = 0; i < serial_number.Length; ++i)
            {
                //all we try to do here convert every character in the serial number
                //to its representation
                char x = serial_number[i];
                int y = System.Convert.ToInt16(x);
                sum += y;
                string_serial += y;
            }

            //once we get the int representation of all the characters in the string
            //we will prepend a check digit at the begining with length indicator and add the year to 
            //have the code expire every year

            DateTime dt = DateTime.Now;
            sum = sum + dt.Year;
            string sum_of_characters = sum.ToString();
            int length_of_sum_indicator = sum_of_characters.Length;

            string_serial = length_of_sum_indicator.ToString() +
                                sum_of_characters +
                                string_serial;

            //once we have the check sum prepended, we will make sure our generated
            //serial number is actually 16 digits
            if (string_serial.Length > 16)
            {
                string_serial = string_serial.Substring(0, 16);
            }
            else if (string_serial.Length < 16)
            {
                string_serial = string_serial.PadRight(16, '0');
            }
            
            return string_serial.ToString();

        }

        /// <summary>
        /// This methods tries to check if the software has been activated
        /// </summary>
        /// <returns></returns>
        public static bool isActivated()
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\ArgosQuant");
            if (regKey == null)
            {
                regKey = Registry.LocalMachine.CreateSubKey(@"Software\ArgosQuant");
            }

            string serial_number = (string)regKey.GetValue("spect_serial_number", null);
            string soft_serial = (string)regKey.GetValue("soft_serial", null);

            if (soft_serial == null || serial_number == null)
            {
                return false;
            }


            if (isCorrectPassword(soft_serial,serial_number))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// This method checks if the current software installation has expired
        /// </summary>
        /// <returns></returns>
        public static bool isExpired()
        {
            if (isActivated())
            {
                //if it has been activated, it won't expire
                return false;
            }

            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\ArgosQuant");
            if (regKey == null)
            {
                regKey = Registry.LocalMachine.CreateSubKey(@"Software\ArgosQuant");
            }

            string start_off = (string)regKey.GetValue("start_off", null);

            //if the start off time has been removed for some reason, we want to force expiration (tampered with)
            if (start_off == null)
            {
                regKey = Registry.LocalMachine.CreateSubKey(@"Software\ArgosQuant");
                start_off = System.DateTime.Now.ToString();
                regKey.SetValue("start_off", start_off);
            }

            DateTime installation_time = System.DateTime.Parse(start_off);
            DateTime current_time = System.DateTime.Now;

            System.TimeSpan date_difference = current_time - installation_time;
            
            if (date_difference.Days > 30)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// This method checks if the password is a mater serial where it will be
        /// allowed to connect to ALL spectrometers
        /// </summary>
        /// <param name="soft_serial"></param>
        /// <returns></returns>
        public static bool isMasterSerial(string soft_serial)
        {
            string master_password1 = getPasswordFromSerial(MASTER_SERIAL_1);
            string master_password2 = getPasswordFromSerial(MASTER_SERIAL_2);
            string master_password3 = getPasswordFromSerial(MASTER_SERIAL_3);

            if (master_password1 == soft_serial ||
                master_password2 == soft_serial ||
                master_password3 == soft_serial)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Saves an int in the uv quant registry
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool saveValueInRegistry(int value, string key)
        {
            RegistryKey regKey = Registry.LocalMachine.CreateSubKey(REGISTRY_KEY);
            if (regKey != null)
            {
                regKey.SetValue(key, value);
            }
            return true;
        }

        /// <summary>
        /// Saves an double in the uv quant registry
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool saveValueInRegistry(double value, string key)
        {
            RegistryKey regKey = Registry.LocalMachine.CreateSubKey(REGISTRY_KEY);
            if (regKey != null)
            {
                regKey.SetValue(key, value);
            }
            return true;
        }

        /// <summary>
        /// Saves an string in the uv quant registry
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool saveValueInRegistry(string value, string key)
        {
            RegistryKey regKey = Registry.LocalMachine.CreateSubKey(REGISTRY_KEY);
            if (regKey != null)
            {
                regKey.SetValue(key, value);
            }
            return true;
        }

        /// <summary>
        /// retrieves a double value from the uv quant registry
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static double getDoubleInRegistry(string key, double defValue)
        {

            RegistryKey regKey = Registry.LocalMachine.CreateSubKey(REGISTRY_KEY);
            double value = defValue;
            if (regKey != null)
            {
                value = Double.Parse((string)regKey.GetValue(key, defValue)); 
            }
            return value;
        }

        /// <summary>
        /// retrieves an int value from the uv quant registry
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static int getIntInRegistry(string key, int defValue)
        {
            RegistryKey regKey = Registry.LocalMachine.CreateSubKey(REGISTRY_KEY);
            int value = defValue;
            if (regKey != null)
            {
                value = (int)regKey.GetValue(key, defValue);
            }
            return value;
        }

        /// <summary>
        /// retrieves a string value from the uv quant registry
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static string getStringInRegistry(string key, string defValue)
        {
            RegistryKey regKey = Registry.LocalMachine.CreateSubKey(REGISTRY_KEY);
            string value = defValue;
            if (regKey != null)
            {
                value = (string)regKey.GetValue(key, defValue);
            }
            return value;
        }

    }
}
