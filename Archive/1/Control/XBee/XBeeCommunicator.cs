using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;

namespace Controller.XBee
{
    class XBeeCommunicator
    {
        private static UInt32 _strDelimiter = 0x7E;
        private static FrameType _enuFrameType = FrameType.RemoteATCommand;
        private static UInt32 _strFrameID = 0x05;
        private static UInt32 _str16BitAddress = 0xFFFE;
        private static UInt32 _strRemoteCmdOptions = 0x02;

        // private static string _
        public static void Send(XBee pXbee, XBeePort pPort, XBeePortValue pValue)
        {
            UInt64 int64BitAddress = UInt64.Parse(pXbee.Mac, System.Globalization.NumberStyles.HexNumber);
            
            string strPayload = GetHexString((UInt64)_enuFrameType) + 
                GetHexString(_strFrameID) +
                GetHexString(int64BitAddress, 16) + 
                GetHexString(_str16BitAddress, 4) + 
                GetHexString(_strRemoteCmdOptions) +
                GetHexString((UInt64)pPort) + 
                GetHexString((UInt16)pValue);

            List<String> lstValues = new List<String>(SplitInParts(strPayload, 2));
            int total = 0;
            foreach(string item in lstValues) {
                total += Int32.Parse(item, System.Globalization.NumberStyles.HexNumber);
            }
            string strCheckSum  = total.ToString("X");
            strCheckSum = (255 - int.Parse(strCheckSum.Substring(strCheckSum.Length - 2), System.Globalization.NumberStyles.HexNumber)).ToString("X");

            lstValues = new List<String>(SplitInParts(strPayload, 2));
            UInt32 length = (UInt32)lstValues.Count;
            string strPacket = GetHexString(_strDelimiter) + 
                GetHexString(length, 4) + 
                strPayload + 
                strCheckSum;


            byte[] data = StringToByteArray(strPacket);

            SerialPort port = new SerialPort(ComPort());
            port.BaudRate = 9600;
            port.DataBits = 8;
            port.Parity = Parity.None;
            port.StopBits = StopBits.One;
            
            port.Open();
            port.Write(data, 0, data.Length);
            port.Close();
        }

        public static string GetHexString(UInt64 pValue, int pLength = 2) {
            return string.Format("{0:X" + pLength + "}", pValue);
        }

        public static IEnumerable<String> SplitInParts(String s, Int32 partLength)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", "partLength");

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }


        private static string ComPort()
        {
            string strBasePath = "/dev/serial/by-id/";
            string strDeviceName = "usb-FTDI_FT232R_USB_UART_A702NV06-if00-port0";

            Console.WriteLine("Locating XBee Controller COM Port...");
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
    }
}
