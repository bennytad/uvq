using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;//thread
using System.IO;

namespace UV_Quant
{
    /// <summary>
    /// This class is used as an interface to the BnW spectrometer. 
    /// It inherits from the Spectrometer class which is used to connect
    /// to the OO spectrometer
    /// </summary>
    class BNWSpectrometer : Spectrometer
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RS232PARA
        {
            int nPort;
            int nBaudRate;
            int nAverage;
            int nTimeDelay;
            int nReserve;
            double fCoef0;
            double fCoef1;
            double fCoef2;
            double fCoef3;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DataBufPARA
        {
            char[] databuf;
        }

        [DllImport("BWTEKUSB.DLL")]
        static extern int bwtekTestUSB(int TimngMode, int PixelNumber, int InputMode, int chnnel, ref RS232PARA lpRS232PARA);

        [DllImport("BWTEKUSB.DLL")]
        static extern int bwtekSetTimeUSB(int nTime, int channel);
        //function bwtekSetTimeUSB(lTime:i32;Channel:i32):integer;stdcall;external 'BWTEKUSB.dll';

        //      [DllImport("BWTEKUSB.DLL")]
        //		static extern int bwtekDataReadUSB(int TriggerMode,char [] MemHandle,int channel);
        //function bwtekDataReadUSB(TriggerMode:i32;MemHandle:pi16;Channel:i32):integer;stdcall;external 'BWTEKUSB.dll';	
        [DllImport("BWTEKUSB.DLL")]
        static extern int bwtekDataReadUSB(int TriggerMode, [In, Out] UInt16[] MemHandle, int channel);
        //function bwtekDataReadUSB(TriggerMode:i32;MemHandle:pi16;Channel:i32):integer;stdcall;external 'BWTEKUSB.dll';

        [DllImport("BWTEKUSB.DLL")]
        static extern int bwtekCloseUSB(int channel);
        //function bwtekCloseUSB(Channel:i32):integer;stdcall;external 'BWTEKUSB.dll';

        [DllImport("BWTEKUSB.DLL")]
        static extern int bwtekReadEEPROMUSB(UInt32[] FileName, int nChannel);

        /*
         *int bwtekPolyFit
                    (
                    double *x, // Array of independent variables
                    double *y, // Array of dependent variables
                    int const numPts, // Number of points in independent and dependent arrays
                    double *coefs, // Pointer to array containing calculated coeffi cients [index from 0 to order]
                    int const order // Desired order of polynomial fi t
                    ) 
         */
        [DllImport("BWTEKUSB.DLL")]
        static extern int bwtekPolyFit(double[] x, double[] y, int numPts, double[] coefs, int order);


        /*
         * bwtekPolyCalc
                void bwtekPolyCalc
                (
                double *coefs,
                int const order,
                int const x,
                double *y
                );
         */
        [DllImport("BWTEKUSB.DLL")]
        static extern void bwtekPolyCalc(double[] coef, int order, long x, double[] y);

        public const String SPECTROMETER_TYPE_BNWTEC = "BnW Tech";

        int scan_flag;
        int scan_enable;


        public BNWSpectrometer(int ass_integration_time, short ass_correct_elec_dark): 
            base(ass_integration_time,ass_correct_elec_dark)
        {
            
        }
        /// <summary>
        /// This function tests the accessibility of a BnW spectrometer
        /// </summary>
        /// <param name="QuiteMode"></param>
        /// <returns></returns>
        public override int TestForSpec(int QuiteMode)
        {
            int nTimigMode = 0;
            int nPixelNumber = 2048;
            int nInputMode = 1;
            int nChannel = 0;
            RS232PARA myrs232para = new RS232PARA();

            int retcode = bwtekTestUSB(nTimigMode, nPixelNumber, nInputMode, nChannel, ref myrs232para);
            if (retcode == 1) 
            {
                if (QuiteMode == 0) MessageBox.Show("Please select another spectrometer");
                return 0; 
            }

            else 
            {
                return retcode; 
            }
        }

        public override void FillInWLArray()
        {
            UInt32[] FileName = { 'S' };
            int ret = bwtekReadEEPROMUSB(FileName, 0);
            /*Once you save the EEProm data to disk in an ini file:
             * Get [EEPROM] section
             *  get Number of calibration points data
             * 
             * GET [CALIBRATION] section
             *  get Wavelength, Pixel(i) data in the format (wl;pix) for i = 0 to number of clali - 1
             * (you can actually get the coeficients from [common] section
             * coefs_a0 to coefs_a3
             * 
             * Then do bwtekPolyFit with wl as Y and pixels as x 
             * test the order of 3! 
             * 
             * Once you get your coefficients, run bwtekPolyCalc for each pixel
            */

            if (ret > 0)
            {
                string cur_dir = Directory.GetCurrentDirectory();
                IniFile settings = new IniFile(cur_dir + "\\S");
                String str_num_of_points = settings.IniReadValue("EEPROM", "Number of calibration points");

                int num_of_points = Convert.ToInt32(str_num_of_points);

                double[] wl = new double[num_of_points];
                double[] px = new double[num_of_points];

                for (int i = 0; i < num_of_points; ++i)
                {
                    String calib = settings.IniReadValue(
                        "CALIBRATION",
                        "Wavelength, Pixel(" + i + ")");

                    String[] calibs = calib.Split(new char[] { ';' });
                    wl[i] = Convert.ToDouble(calibs[0]);
                    px[i] = Convert.ToDouble(calibs[1]);
                }
                WLCoefs = new double[4];
                ret = bwtekPolyFit(px, wl, 6, WLCoefs, 3);

                WLCoefs[0] /= 10;
                WLCoefs[1] /= 10;
                WLCoefs[2] /= 10;
                WLCoefs[3] /= 10;

                for (long i = 0; i < 2048; ++i)
                {
                    ss_wl[i] = WLCoefs[3] * (Math.Pow(i, 3)) +
                        WLCoefs[2] * (Math.Pow(i, 2)) +
                        WLCoefs[1] * (Math.Pow(i, 1)) +
                        WLCoefs[0] * (Math.Pow(i, 0));//new_wl[0];
                }
            }
        }

        public override void AttachToSpec()
        {
            if (TestForSpec(1) == 0)
            {
                //Hardware Code
                string cur_dir = Directory.GetCurrentDirectory();
                IniFile settings = new IniFile(cur_dir + "\\S");
                String hardware_code = settings.IniReadValue("EEPROM", "Hardware Code");

                SerialNumber = hardware_code;
                adc_type = SPECTROMETER_TYPE_BNWTEC;
                Selected = true;

                setIntegrationTime(integration_time);
            }
        }

        public void setIntegrationTime(int int_time)
        {
            //now set the integration time
            int nChannel = 0;
            int retcode = bwtekSetTimeUSB(int_time, nChannel);
        }

        public override int collectData()
        {
            int TriggerMode = 0;
            UInt16[] MemHandle = new UInt16[2048];
            int nChannel = 0;

            int retcode = bwtekDataReadUSB(TriggerMode, MemHandle, nChannel);
        
            if (retcode == 2048)
            {
                max_uv_count = -1000;
                for (int i = 1; i <= 2047; ++i)
                {
                    data[i] = MemHandle[i];
                    if (max_uv_count < MemHandle[i])
                    {
                        max_uv_count = MemHandle[i];
                    }
                }

                return retcode;
            }
            else 
            {
                Selected = false;
                return 0;
            }
        }

        public override int DynamicIntegrationAdjustment()
        {
            //'   Automatically adjuts the integration time to keep the peak UV intensity between
            //'   3000 & 3900 counts

            double mx = max_uv_count;
            int i;
            int r = -1;
            int loopcount;
            double d;
            int imx = 0;
            bool satflag;
            int pmax;
            int original_int_time = integration_time;
            setIntegrationTime(integration_time);

            loopcount = 1;

            while (((mx < 3000) || (mx > 3700)) && (loopcount < 5))
            {
                UInt16[] MemHandle = new UInt16[2048];
                int retcode = bwtekDataReadUSB(0, MemHandle, 0);
                while (retcode != 2048)
                {
                    retcode = bwtekDataReadUSB(0, MemHandle, 0);
                }

                for (i = 1; i < 2048; ++i)
                {
                    data[i] = MemHandle[i];
                }

                mx = 0;
                i = 24;

                while ((ss_wl[i] < 350) && (i <= 2047))
                {
                    i = i + 1;
                    if (data[i] > mx)
                    {
                        mx = data[i];
                        imx = i;
                    }
                }

                if (mx > 3400)
                {
                    satflag = true;                     //test to see if its saturated by looking
                    pmax = imx + 100;                    //at 100 pixels past max value to see if the
                    if (pmax > 2047) pmax = 2047;        //are in a 10 count band
                    for (i = imx; i <= pmax; ++i)
                    {
                        if ((Math.Abs(data[i] - mx)) > 10)
                        {
                            satflag = false;
                            break;
                        }
                    }
                }
                else
                {
                    satflag = false;
                }

                if (satflag == true)
                {
                    integration_time = 3;
                    setIntegrationTime(integration_time);
                    mx = 4095;
                }
                else if (mx < 3000)
                {
                    d = (integration_time) / mx * 3400;      // determine adjustment amount-desired 3400 cts
                    if (d > 1000) d = 1000;       // limit integration time
                    if (d < 3) d = 3;
                    integration_time = Convert.ToInt32(d);                    // new integration time
                    setIntegrationTime(integration_time);
                }
                else if (mx > 3700)
                {
                    integration_time = integration_time / 2;
                    setIntegrationTime(integration_time);
                    if (integration_time < 3) integration_time = 3;
                }
                loopcount = loopcount + 1;

            }

            if (integration_time > 0)
            {
                if (integration_time > 1000)
                {
                    integration_time = 1000;
                    setIntegrationTime(integration_time);
                }
                integration_time = integration_time;
                return integration_time;
            }
            else
            {
                integration_time = original_int_time;
                setIntegrationTime(integration_time);
                integration_time = integration_time;
                return integration_time;
            }
        }
    }
}
