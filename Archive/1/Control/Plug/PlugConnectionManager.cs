using System;
using PlugwiseLib;
using System.IO;

namespace Controller
{
    class PlugConnectionManager
    {
        private static plugwiseControl _objConnection;
        private static Object objLock = new Object();

        public static plugwiseControl GetConnection()
        {
            lock (objLock)
            {
                if (_objConnection == null)
                {
                    Console.WriteLine("Creating connection to Plugwise serial adapter");
                    _objConnection = new plugwiseControl(ComPort());
                    _objConnection.Open();
                }
            }
            return _objConnection;

        }

        private static string ComPort()
        {
            var strBasePath = "/dev/serial/by-id/";
            var strDeviceName = "usb-FTDI_FT232R_USB_UART_A8005W0Y-if00-port0";

            Console.WriteLine("Locating Plugwise COM Port...");
            Console.WriteLine("Using " + strBasePath + strDeviceName);

            string[] lstFiles = Directory.GetFiles(strBasePath, strDeviceName);
            if (lstFiles.Length > 0)
            {
                FileInfo objInfo = new FileInfo(lstFiles[0]);
                Mono.Unix.UnixSymbolicLinkInfo info = new Mono.Unix.UnixSymbolicLinkInfo(objInfo.FullName);
                string strFullPath = new FileInfo("/dev/serial/by-id/" + info.ContentsPath).FullName;

                Console.WriteLine("Located Plug at " + strFullPath);

                return strFullPath;
            }
            throw new Exception("Unable to locate Plugwise serial adapter");
        }

        ~PlugConnectionManager()
        {
            if (_objConnection != null)
            {
                Console.WriteLine("Closing Plugwise adapter connection");
                _objConnection.Close();
            }
        }
    }
}
