using System;
using Raspberry.IO.GeneralPurpose;

namespace Controller
{
    class GPIOConnectionManager
    {
        private static IGpioConnectionDriver _objConnection;
        private static Object _objLock = new Object();

        public static IGpioConnectionDriver GetConnection()
        {
            lock (_objLock)
            {
                if (_objConnection == null)
                {
                    _objConnection = GpioConnectionSettings.DefaultDriver;
                }
            }
            return _objConnection;
        }
    }
}
