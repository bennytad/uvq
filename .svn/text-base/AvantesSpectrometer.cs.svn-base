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
    class AvantesSpectrometer : Spectrometer
    {
        bool dll_loaded = false;
        public long m_DeviceHandle;
        ushort number_of_pixels = 0;
        public string sensor_type = "";
        public string fpga_version = "";
        public string fw_version = "";
        public string dll_version = "";
        decimal minimum_pixel = 0;
        decimal maximum_pixel = 0;
        AVS5216.DeviceConfigType l_pDeviceData;
        AVS5216.MeasConfigType l_PrepareMeasData;
        IntPtr form_handle;
        public string[] serial_numbers = null;

        public const String SPECTROMETER_TYPE_AVANTES = "Avantes";

        public AvantesSpectrometer(int ass_integration_time, short ass_correct_elec_dark)
            :
            base(ass_integration_time, ass_correct_elec_dark)
        {
            integration_time = ass_integration_time;
        }

        
        ///// <summary>
        ///// This function tests the accessibility of a BnW spectrometer
        ///// </summary>
        ///// <param name="QuiteMode"></param>
        ///// <returns></returns>
        public override int TestForSpec(int QuiteMode)
        {
            string[] test = getSerialsNrs();

            if (test != null)
            {
                // If you get some serial numbers,
                // that means you have some Avantes spectrometer attached
                return 0;
            }
            else
            {
                if (QuiteMode == 0) MessageBox.Show("Please select another spectrometer");
                return -1;
            }
        }

        public override void FillInWLArray()
        {
            if (!dll_loaded)
            {
                if (!stabilishConnection(form_handle))
                {
                    return;
                }

            }
            //this will load wavelength along with every other settings
            loadInitialData();
        }

        public override void AttachToSpec()
        {
            if (TestForSpec(1) == 0)
            {
                if (!dll_loaded)
                {
                    if (stabilishConnection(form_handle))
                    {
                        adc_type = SPECTROMETER_TYPE_AVANTES;
                        Selected = true;
                        setIntegrationTime(integration_time);
                    }
                }
                else
                {
                    adc_type = SPECTROMETER_TYPE_AVANTES;
                    Selected = true;
                    setIntegrationTime(integration_time);
                }
            }
        }

        public override int collectData()
        {
            Stopwatch stop_watch = new Stopwatch();
            stop_watch.Start();
            int rlt = getDataFromAvantSpec(integration_time, ref data, ref ss_wl);
            stop_watch.Stop();

            if(rlt==0)
            {
                max_uv_count = -1000;
                for (int i = 1; i < data.Length; ++i)
                {
                    if (max_uv_count < data[i])
                    {
                        max_uv_count = data[i];
                    }
                }

               // DynamicIntegrationAdjustment();
            }
            return rlt;
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

            loopcount = 1;

            while (((mx < 3000) || (mx > 3700)) && (loopcount < 5))
            {
                double[] av_data = null;
                double[] av_ss_wl = null;
                int retcode = getDataFromAvantSpec(integration_time, ref av_data, ref av_ss_wl);
                while (retcode != 0)
                {
                    return -1;
                }

                mx = 0;
                i = 24;

                while ((ss_wl[i] < 350) && (i <= data.Length))
                {
                    i = i + 1;
                    if (av_data[i] > mx)
                    {
                        mx = av_data[i];
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
                        if ((Math.Abs(av_data[i] - mx)) > 10)
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

        //Spectrometer specific code to follow code to follow

        /// <summary>
        /// This method tries to estabilish with the Avantes DLL interface
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public bool stabilishConnection(IntPtr handle)
        {
            form_handle = handle;

            int l_Port = AVS5216.AVS_Init(0);
           
            AVS5216.AVS_Register(handle);

            if (l_Port > 0)
            {
                dll_loaded = true;
                return true;
            }
            else
            {
                //AVS5216.AVS_Done();
                //This is where we would try to setup 
                //bluetooth or RS232 connection if we
                //were interested
                return false;
            }
        }

        /// <summary>
        /// Once connection to the DLL has been estabilished,
        /// this method retrieves the serial numbers of all the 
        /// spectrometers attached to the system
        /// </summary>
        /// <returns></returns>
        public string[] getSerialsNrs()
        {
            // if connection has not been estabilished, 
            // return a null
            if (!dll_loaded)
            {
                if (!stabilishConnection(form_handle))
                {
                    return null;
                }

            }

            uint l_Size = 0;
            uint l_RequiredSize = 0;
            int l_NrDevices = 0;

            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

            l_NrDevices = AVS5216.AVS_GetNrOfDevices();

            AVS5216.AvsIdentityType[] l_Id = new AVS5216.AvsIdentityType[l_NrDevices];
            l_RequiredSize = ((uint)l_NrDevices) * (uint)Marshal.SizeOf(typeof(AVS5216.AvsIdentityType)); //Marshal.SizeOf(typeof(PackedRecNative));
            byte[] l_Data = new byte[l_RequiredSize];

            if (l_RequiredSize > 0)
            {
                l_Size = l_RequiredSize;
                l_NrDevices = AVS5216.AVS_GetList(l_Size, ref l_RequiredSize, ref l_Data[0]);//ref l_Data[0]);

                IntPtr aBuffer = Marshal.AllocHGlobal((int)l_Size);
                try
                {
                    for (int i = 0; i < l_NrDevices; i++)
                    {
                        Marshal.Copy(l_Data, (i * (int)Marshal.SizeOf(typeof(AVS5216.AvsIdentityType))), aBuffer, (int)Marshal.SizeOf(typeof(AVS5216.AvsIdentityType)));
                        l_Id[i] = (AVS5216.AvsIdentityType)Marshal.PtrToStructure(aBuffer, typeof(AVS5216.AvsIdentityType));
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(aBuffer);
                }

                string[] serials = new string[l_NrDevices];

                for (int i = 0; i < l_NrDevices; i++)
                {
                    switch (l_Id[i].m_Status)
                    {
                        case AVS5216.DEVICE_STATUS.UNKNOWN:
                            serials[i] = l_Id[i].m_SerialNumber + "::" + "UNKNOWN";
                            break;
                        case AVS5216.DEVICE_STATUS.AVAILABLE:
                            serials[i] = l_Id[i].m_SerialNumber + "::" + "AVAILABLE";
                            SerialNumber = serials[i];
                            break;
                        case AVS5216.DEVICE_STATUS.IN_USE_BY_APPLICATION:
                            serials[i] = l_Id[i].m_SerialNumber + "::" + "IN_USE_BY_APPLICATION";
                            break;
                        case AVS5216.DEVICE_STATUS.IN_USE_BY_OTHER:
                            serials[i] = l_Id[i].m_SerialNumber + "::" + "IN_USE_BY_OTHER";
                            break;
                        default:
                            serials[i] = l_Id[i].m_SerialNumber + "::" + "??????";
                            break;
                    }
                }
                serial_numbers = serials;
                return serials;
            }
            
            return null;
        }

        /// <summary>
        /// This method connects to a spectrometer with the
        /// provided serial number. If the connection was
        /// stabilished, a value of true will be returned
        /// else false will be returned
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public bool connectToSpectrometer()
        {
            AVS5216.AvsIdentityType l_Active = new AVS5216.AvsIdentityType();
            long l_hDevice = 0;

            l_Active.m_SerialNumber = SerialNumber;

            l_hDevice = (long)AVS5216.AVS_Activate(ref l_Active);

            if (AVS5216.INVALID_AVS_HANDLE_VALUE == l_hDevice)
            {
                return false;
            }
            else
            {
                m_DeviceHandle = l_hDevice;
                //read IMP data sets
                loadInitialData();
                loadEEProm();
                return true;
            }
        }

        /// <summary>
        /// Once the spectrometer is connected and activated, 
        /// this method will load few parameters such as the number
        /// of pixels, wavelength etc.
        /// </summary>
        public void loadInitialData()
        {
            AVS5216.DeviceConfigType l_pDeviceData = new AVS5216.DeviceConfigType();
			
			uint l_Size = 0;
			byte[] a_Fpga	= new byte[16];
			byte[] a_As5216 = new byte[16];
			byte[] a_Dll	= new byte[16];

            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
		
			int l_Res = (int)AVS5216.AVS_GetVersionInfo((IntPtr)m_DeviceHandle, ref a_Fpga[0], ref a_As5216[0], ref a_Dll[0]);
			
			if (AVS5216.ERR_SUCCESS == l_Res)
			{
				fpga_version = enc.GetString(a_Fpga);
				fw_version =	enc.GetString(a_As5216);
				dll_version = enc.GetString(a_Dll);
			}

            if (AVS5216.ERR_SUCCESS == (int)AVS5216.AVS_GetNumPixels((IntPtr)m_DeviceHandle, ref number_of_pixels))
			{

			}

			l_Size = (uint)Marshal.SizeOf(typeof(AVS5216.DeviceConfigType));
			byte[] l_Data = new byte[l_Size];

			l_Res = (int)AVS5216.AVS_GetParameter((IntPtr)m_DeviceHandle, l_Size, ref l_Size, ref l_Data[0]);//l_pDeviceData[0]);
			
			if ( l_Res == AVS5216.ERR_INVALID_SIZE)
			{
				l_Data = null;
				l_Data = new byte[l_Size];
				l_Res = (int)AVS5216.AVS_GetParameter((IntPtr)m_DeviceHandle, l_Size, ref l_Size, ref l_Data[0]);//l_pDeviceData[0]);
			}
			
			if (l_Res != AVS5216.ERR_SUCCESS)
			{
				//DisconnectGui();
				return;
			}

		
			IntPtr aBuffer = Marshal.AllocHGlobal((int)l_Size);
			try 
			{
				Marshal.Copy(l_Data, 0, aBuffer, (int)l_Size);
				l_pDeviceData = (AVS5216.DeviceConfigType)Marshal.PtrToStructure(aBuffer, typeof(AVS5216.DeviceConfigType));
			} 
			finally 
			{
				Marshal.FreeHGlobal(aBuffer);
			}

			switch (l_pDeviceData.m_Detector.m_SensorType)
			{
				case (byte)AVS5216.SENS_TYPE.SENS_HAMS8378_256: sensor_type   = "HAMS8378_256";
					break;
                case (byte)AVS5216.SENS_TYPE.SENS_HAMS8378_1024: sensor_type = "HAMS8378_1024";
					break;
                case (byte)AVS5216.SENS_TYPE.SENS_ILX554: sensor_type = "ILX554";
					break;
                case (byte)AVS5216.SENS_TYPE.SENS_HAMS9201: sensor_type = "HAMS9201";
					break;
                case (byte)AVS5216.SENS_TYPE.SENS_TCD1304: sensor_type = "TCD1304";
                    l_Res = AVS5216.AVS_SetPrescanMode((IntPtr)m_DeviceHandle, true); //we always want prescan enabled
					break;
                case (byte)AVS5216.SENS_TYPE.SENS_TSL1301: sensor_type = "TSL1301";
					break;
                case (byte)AVS5216.SENS_TYPE.SENS_TSL1401: sensor_type = "TSL1401";
					break;
                case (byte)AVS5216.SENS_TYPE.SENS_HAMS8378_512: sensor_type = "HAMS8378_512";
					break;
                case (byte)AVS5216.SENS_TYPE.SENS_HAMS9840: sensor_type = "HAMS9840";
					break;
                default: sensor_type = "???????";
					break;

			}
		    minimum_pixel = l_pDeviceData.m_StandAlone.m_Meas.m_StartPixel;
			maximum_pixel = l_pDeviceData.m_StandAlone.m_Meas.m_StopPixel;

			if (null != ss_wl)
			{
				// delete old structure because number of pixels can be changed.
                ss_wl = null;
			}
            ss_wl = new double[number_of_pixels];
            data = new double[number_of_pixels];
            if (AVS5216.ERR_SUCCESS == (int)AVS5216.AVS_GetLambda((IntPtr)m_DeviceHandle, ref ss_wl[0]))
			{

			}
        }

        /// <summary>
        /// This load EEProm
        /// </summary>
        public void loadEEProm()
        {
            l_pDeviceData = new AVS5216.DeviceConfigType();
            uint l_Size;


            l_Size = (uint)Marshal.SizeOf(typeof(AVS5216.DeviceConfigType));
            byte[] l_Data = new byte[l_Size];

            int l_Res = (int)AVS5216.AVS_GetParameter((IntPtr)m_DeviceHandle, l_Size, ref l_Size, ref l_Data[0]);//l_pDeviceData[0]);

            if (l_Res == AVS5216.ERR_INVALID_SIZE)
            {
                l_Data = null;
                l_Data = new byte[l_Size];
                l_Res = (int)AVS5216.AVS_GetParameter((IntPtr)m_DeviceHandle, l_Size, ref l_Size, ref l_Data[0]);//l_pDeviceData[0]);
            }

            if (AVS5216.ERR_SUCCESS != l_Res)
            {
                MessageBox.Show("Error ", "Avantes",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            IntPtr aBuffer = Marshal.AllocHGlobal((int)l_Size);

            try
            {
                Marshal.Copy(l_Data, 0, aBuffer, (int)l_Size);
                l_pDeviceData = (AVS5216.DeviceConfigType)Marshal.PtrToStructure(aBuffer, typeof(AVS5216.DeviceConfigType));
            }
            finally
            {
                Marshal.FreeHGlobal(aBuffer);
            }
        }

        public void setIntegrationTime(int integration_time)
        {
            //first update the integration time in the device 
            this.integration_time = integration_time;
            l_pDeviceData.m_StandAlone.m_Meas.m_IntegrationTime = (float)System.Convert.ToDouble(integration_time);
            int l_Res = (int)AVS5216.AVS_SetParameter((IntPtr)m_DeviceHandle, ref l_pDeviceData);//ref l_Data[0]);//

            if(AVS5216.ERR_SUCCESS != l_Res)
			{
                MessageBox.Show("Current integration time was not set!");
            }
        }

        public int getDataFromAvantSpec(int av_integration_time, ref double[] av_data, ref double[] av_ss_wl)
        {
            l_PrepareMeasData = new AVS5216.MeasConfigType();

            l_PrepareMeasData.m_StartPixel = (ushort)minimum_pixel;
            l_PrepareMeasData.m_StopPixel = System.Convert.ToUInt16(maximum_pixel);
            l_PrepareMeasData.m_IntegrationTime = (ushort)av_integration_time;
            double l_NanoSec = System.Convert.ToDouble(0);//we don't need any delay
            uint l_FPGAClkCycles = (uint)(6.0 * (l_NanoSec + 20.84) / 125.0);
            l_PrepareMeasData.m_IntegrationDelay = l_FPGAClkCycles;
            l_PrepareMeasData.m_NrAverages = 1;
            l_PrepareMeasData.m_Trigger.m_Mode = (byte)0;
            l_PrepareMeasData.m_Trigger.m_Source = (byte)0;
            l_PrepareMeasData.m_Trigger.m_SourceType = (byte)0;
            l_PrepareMeasData.m_SaturationDetection = System.Convert.ToByte(0);
            l_PrepareMeasData.m_CorDynDark.m_Enable = (byte)1;
            l_PrepareMeasData.m_CorDynDark.m_ForgetPercentage = System.Convert.ToByte(100);

            UInt16 av_boxCar = (UInt16)(boxcar % 2);

            l_PrepareMeasData.m_Smoothing.m_SmoothPix = av_boxCar;
            l_PrepareMeasData.m_Smoothing.m_SmoothModel = System.Convert.ToByte(0);
            l_PrepareMeasData.m_Control.m_StrobeControl = 0;
            l_NanoSec = 0;
            l_FPGAClkCycles = (uint)(6.0 * l_NanoSec / 125.0);
            l_PrepareMeasData.m_Control.m_LaserDelay = l_FPGAClkCycles;
            l_NanoSec = 0;
            l_FPGAClkCycles = (uint)(6.0 * l_NanoSec / 125.0);
            l_PrepareMeasData.m_Control.m_LaserWidth = 0;
            l_PrepareMeasData.m_Control.m_LaserWaveLength = 0;
            l_PrepareMeasData.m_Control.m_StoreToRam = 0;

            int l_Res = (int)AVS5216.AVS_PrepareMeasure((IntPtr)m_DeviceHandle, ref l_PrepareMeasData);
            if (AVS5216.ERR_SUCCESS != l_Res)
            {
                MessageBox.Show("Error " + l_Res.ToString(), "Avantes",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            //Get Nr Of Scans
            short l_NrOfScans = 1;

            //this is only used if you want to store the data to RAM - which we don't do here
            if ((l_PrepareMeasData.m_Control.m_StoreToRam > 0) && (l_NrOfScans != 1))
            {
                l_NrOfScans = 1;
                MessageBox.Show(l_PrepareMeasData.m_Control.m_StoreToRam.ToString() +
                                " scans will be stored to RAM. " +
                                "The number of measurements (a_Nmsr in AVS_Measure) has been set to 1",
                                "Avantes",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }
            //Start Measurement

            l_Res = (int)AVS5216.AVS_Measure((IntPtr)m_DeviceHandle, form_handle, l_NrOfScans);

            if (AVS5216.ERR_SUCCESS != l_Res)
            {
                switch (l_Res)
                {
                    case AVS5216.ERR_INVALID_PARAMETER:
                        MessageBox.Show("Meas.Status: invalid parameter");
                        break;
                    default:
                        MessageBox.Show("Meas.Status: start failed, code: " + l_Res.ToString());
                        break;
                }
            }
            else
            {
                //StatusBar1.Panels[1].Text = "Meas.Status: pending";
                int ready = AVS5216.AVS_PollScan((IntPtr)m_DeviceHandle);

                while (ready != 1)
                {
                    System.Windows.Forms.Application.DoEvents();
                    System.Threading.Thread.Sleep(av_integration_time + 2);
                    ready = AVS5216.AVS_PollScan((IntPtr)m_DeviceHandle);
                }

                //Once we ascertain that the data is ready, we will proceed to read it

               if (number_of_pixels > 0)
                {
                    av_data = new double[number_of_pixels];

                    if (av_ss_wl == null)
                    {
                        av_ss_wl = new double[number_of_pixels];
                    }

                    //this is a bit redundant since we are reading the same wavelength at the begining as well
                    if (0 == (int)AVS5216.AVS_GetLambda((IntPtr)m_DeviceHandle, ref av_ss_wl[0]))
                    {
                        ;
                    }

                    uint l_Time = 0;
                    if (AVS5216.ERR_SUCCESS != (int)AVS5216.AVS_GetScopeData((IntPtr)m_DeviceHandle, ref l_Time, ref av_data[0]))
                    {
                        MessageBox.Show("There was an error reading your data!");
                    }
                }

            }

            return 0;
        }

        public void setWindowsHandle(IntPtr handle)
        {
            form_handle = handle;
        }

        public new void getSpectrometers(ListBox type_list, ListBox sn_list)
        {
            int index;
            int numberOfSpectrometers;

            if (serial_numbers == null)
            {
                getSerialsNrs();
            }

            if (serial_numbers != null)
            {
                numberOfSpectrometers = serial_numbers.Length;

                if (numberOfSpectrometers == 0)
                {
                    return;
                }

                for (index = 0; index < numberOfSpectrometers; ++index)
                {
                    type_list.Items.Add("Avantes Spec");
                    sn_list.Items.Add(serial_numbers[index]);
                }
            }
        }
    }
}
