using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace UV_Quant
{
    public class Spectrometer
    {
        public OmniDriver.CCoWrapper wrapper = new OmniDriver.CCoWrapper();

        public int selected_spectrometer = 0;
        public int number_of_spectrometers = 0;
        
        protected string SerialNumber;
        protected string ElementString;
        protected int[] Elements;
        protected double[] WLCoefs;
        protected bool Selected;
        protected int IntegrationTime;
        protected short Averages;
        protected short AcquisitionSecs;
        protected short boxcar;
        protected short CorrectElecDark;
        protected short NumBadScans;
        protected double[] ss_wl;
        protected double[] data;
        public string adc_type = "";
        public double max_uv_count = 0;        
        public bool scanning = false; //this will be used to indidate we are allowed to continue scanning

        public Spectrometer(int ass_integration_time, short ass_correct_elec_dark)
        {
            int t ;
            SerialNumber = "";
            ElementString = "";
            Elements = new int[4];
            WLCoefs = new double[4];
            Selected = true;
            IntegrationTime = ass_integration_time * 1000;
            Averages = 1;
            AcquisitionSecs = 0;
            boxcar = 0;
            CorrectElecDark = ass_correct_elec_dark;
            NumBadScans = 0;
            wrapper.CreateWrapper();
            t = wrapper.getIntegrationTime(0);
            //try
            //{
            int x = 1000000;
            wrapper.setIntegrationTime(selected_spectrometer, x);

            t = wrapper.getIntegrationTime(0);
            wrapper.setCorrectForElectricalDark(selected_spectrometer, IntegrationTime);
            number_of_spectrometers = wrapper.openAllSpectrometers();
            //}
            //catch (Exception ex)
            //{
            //}
        }

        public string serial_number
        {
            get { return SerialNumber; }
        }
        public string element_string
        {
            get { return ElementString; }
        }
        public int[] elements
        {
            get { return Elements; }
        }
        public double[] wl_coefs
        {
            get { return WLCoefs; }
        }
        public bool selected
        {
            get { return Selected; }
        }
        public int integration_time
        {
            get 
            {
                if (IntegrationTime > 1000000)
                {
                    IntegrationTime = 1000000;
                    wrapper.setIntegrationTime(selected_spectrometer, 1000000);
                }
                return IntegrationTime/1000; 
            }
            set 
            {
                if (value > 1000)
                {
                    IntegrationTime = 1000000;
                    wrapper.setIntegrationTime(selected_spectrometer, 1000000);
                }
                else
                {
                    IntegrationTime = value * 1000;
                }
            }
        }

        public short averages
        {
            get { return Averages; }
            set { Averages = value; }
        }

        public short acquisition_secs
        {
            get { return AcquisitionSecs; }
        }
        public short box_car
        {
            get { return box_car; }
            set { boxcar = value; }
        }

        public short correct_elec_dark
        {
            get { return CorrectElecDark; }
            set { CorrectElecDark = value; }
        }

        public short num_bad_scans
        {
            get { return NumBadScans; }
        }
        public double[] wavelength
        {
            get { return ss_wl; }
        }

        public double[] spectra
        {
            get { return data; }
        }

        public virtual int TestForSpec(int QuiteMode)
        {
            //Calls a doscan function call and test to see if a spectrometer
            //is attached. Retuens the doscan error codes
            //if QuiteMode is 1 then no msg boxes are displayed
            //Returns 0 if successful
            int r = -1;
            int old_ds; 
            try
            {
                if (number_of_spectrometers > 0)
                {
                    //old_ds = wrapper.getIntegrationTime(selected_spectrometer);
                    old_ds = IntegrationTime;
                    wrapper.setIntegrationTime(selected_spectrometer, 3);
                    data = (double[])wrapper.getSpectrum(selected_spectrometer);

                    IntegrationTime = old_ds;

                    wrapper.setIntegrationTime(selected_spectrometer, IntegrationTime);
                    r = 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                r = -1;
            }

            if (r != 0)
            {
                if (QuiteMode == 0) MessageBox.Show("Please select another spectrometer");
                return r;
            }
            return r;
        }

        public virtual void AttachToSpec()
        {
            //really nothing to do here
            //just select the index of the first spectrometer
            if (number_of_spectrometers > 0)
            {
                SerialNumber = wrapper.getSerialNumber(selected_spectrometer);
                adc_type = wrapper.getName(selected_spectrometer);
            }
        }

        public virtual void FillInWLArray()
        {
            if (number_of_spectrometers > 0)
            {
                ss_wl = (double[])wrapper.getWavelengths(selected_spectrometer);
            }
        }

        public virtual int collectData()
        {

            if (number_of_spectrometers > 0)
            {

                System.Windows.Forms.Application.DoEvents();
                wrapper.setIntegrationTime(selected_spectrometer, IntegrationTime);
                int x = wrapper.getIntegrationTime(selected_spectrometer);
                try
                {
                    data = (double[])wrapper.getSpectrum(selected_spectrometer);
                }
                catch (Exception ex)
                {
                    data = data; 
                }
           

                double corrector = 0;

                for (int i = 0; i < 25; ++i)
                {
                    corrector += data[i];
                }

                if (corrector == 0)
                {
                    Selected = false;
                    return 0;
                }
                else
                {
                    max_uv_count = -1000;
                    corrector /= 25;
                    for (int i = 1; i < data.Length; ++i)
                    {
                        data[i] = data[i] - corrector;
                        if (max_uv_count < data[i])
                        {
                            max_uv_count = data[i];
                        }
                    }

                    return 0;
                }
            }
            else
            {
                return -1;
            }
        }

        public void logData(String my_data, String file_name)
        {

            StreamWriter w = File.AppendText("C:\\" + file_name);

            w.WriteLine(System.DateTime.Now.ToString() + '\t' + my_data);

            w.Close();
        }

        public virtual int DynamicIntegrationAdjustment()
        {
            //'   Automatically adjuts the integration time to keep the peak UV intensity between
            //'   3000 & 3900 counts

            double mx = max_uv_count;
            int i;
            int r = -1;
            int loopcount;
            double d;
            int imx = 0;
            int original_int_time = wrapper.getIntegrationTime(selected_spectrometer);
            
  
            loopcount = 1;

            while(((mx < 3000) || (mx > 3700)) && (loopcount < 20))
            {
                System.Windows.Forms.Application.DoEvents();
                //if (!scanning)
                //{
                //    return original_int_time;
                //}
                ss_wl = (double[])wrapper.getWavelengths(0);
                data = (double[])wrapper.getSpectrum(0); 

                double corrector = 0;

                for (int j = 0; j < 25; ++j)
                {
                    System.Windows.Forms.Application.DoEvents();
                    //if (!scanning)
                    //{
                    //    return original_int_time;
                    //}
                    corrector += data[j];
                }

                if (corrector == 0)
                {
                    Selected = false;
                    return 0;
                }
                else
                {
                    corrector /= 25;
                    mx = 0;
                    i = 24;

                    while (i < (data.Length-1))
                    {
                        System.Windows.Forms.Application.DoEvents();
                        //if (!scanning)
                        //{
                        //    return original_int_time;
                        //}
                        i = i + 1;
                        data[i] = data[i] - corrector;
                        if (data[i] > mx)
                        {
                            mx = (double)data[i];
                            imx = i;
                        }
                    }
                    
                    if (mx < 3000 || mx > 3200)
                    {
                        d = (wrapper.getIntegrationTime(selected_spectrometer)) / mx * 3100;      // determine adjustment amount-desired 3400 cts
                        if (d > 1000000) d = 1000000;       // limit integration time
                        if (d < 3000) d = 3000;
                        wrapper.setIntegrationTime(selected_spectrometer,Convert.ToInt32(d));                    // new integration time
                    }
                    loopcount = loopcount + 1;

                }
                    
            }

            if (wrapper.getIntegrationTime(selected_spectrometer) > 0)
            {
                if (wrapper.getIntegrationTime(selected_spectrometer) > 1000000)
                {
                    wrapper.setIntegrationTime(selected_spectrometer,1000000);
                }

                IntegrationTime = wrapper.getIntegrationTime(selected_spectrometer);
                return IntegrationTime;
            }
            else
            {
                wrapper.setIntegrationTime(selected_spectrometer,original_int_time);
                IntegrationTime = wrapper.getIntegrationTime(selected_spectrometer);
                return integration_time;
            }
        }

        public static void getSpectrometers(ListBox type_list, ListBox sn_list)
        {
            int index;
            int numberOfSpectrometers;
            
           OmniDriver.CCoWrapper temp_wrapper = new OmniDriver.CCoWrapper();
           temp_wrapper.CreateWrapper();
           numberOfSpectrometers = temp_wrapper.openAllSpectrometers();

            if(numberOfSpectrometers == 0)
            {
                return;   
            }

            for(index = 0; index < numberOfSpectrometers; ++index)
            {
                type_list.Items.Add(temp_wrapper.getName(index));
                sn_list.Items.Add(temp_wrapper.getSerialNumber(index));
            }
        }

    }
}