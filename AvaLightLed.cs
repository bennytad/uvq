using System;
using System.Collections.Generic;
using System.Text;

namespace UV_Quant
{
    class AvaLightLed : ALightSource
    {
        private long m_DeviceHandle;
        byte a_Id = System.Convert.ToByte(0); //this is pin 11 used for AvaLight-LED controlling
        bool initialized = false;

        public void init(long handle)
        {
            m_DeviceHandle = handle;
            initialized = true;
        }

        /// <summary>
        /// This function turns on the LED
        /// </summary>
        /// <returns></returns>
        public override int turnLightOn()
        {
            if (initialized)
            {
                byte l_Perc = System.Convert.ToByte(100);
                uint l_Freq = System.Convert.ToUInt32(1000);
                int l_Res = AVS5216.AVS_SetPwmOut((IntPtr)m_DeviceHandle, a_Id, l_Freq, l_Perc);
                return l_Res;
            }
            return -1;
        }

        /// <summary>
        /// turns the LED off
        /// </summary>
        /// <returns></returns>
        public override int turnLightOff()
        {
            if (initialized)
            {
                byte a_Value = System.Convert.ToByte(0);
                int l_Res = AVS5216.AVS_SetDigOut((IntPtr)m_DeviceHandle, a_Id, a_Value);
                return l_Res;
            }
            return -1;
        }
    }
}
