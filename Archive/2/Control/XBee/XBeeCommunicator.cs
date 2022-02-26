using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using XBee;

namespace Controller
{
    public class XBeeCommunicator : SerialConnection
    {
        private static UInt32 _strDelimiter = 0x7E;
        private static FrameType _enuFrameType = FrameType.RemoteATCommand;
        private static UInt32 _strFrameID = 0x05;
        private static UInt32 _str16BitAddress = 0xFFFE;
        private static UInt32 _strRemoteCmdOptions = 0x02;
        private static XBeeCommunicator _objInstance; 

        public XBeeCommunicator(string port, int baudRate) : base(port, baudRate) {  }

        public static void Send(XBeePortController pXbee, XBeePort pPort, XBeePortValue pValue)
        {
            if (_objInstance == null) {
                _objInstance = new XBeeCommunicator(XBeeCommunicator.ComPort(), 9600); //new XBeeCommunicator(ComPort, 9600); 
                //_objInstance = new XBeeCommunicator("/dev/ttyUSB1", 9600); //new XBeeCommunicator(ComPort, 9600); 
                _objInstance.Open();
            }
            Console.WriteLine("Calculating UInt64bitAddress");
            UInt64 int64BitAddress = UInt64.Parse(pXbee.Mac, System.Globalization.NumberStyles.HexNumber);
            
            Console.WriteLine("Addr:" + int64BitAddress.ToString());
            Console.WriteLine("Creating payload hex string...");
            string strPayload = GetHexString((UInt64)_enuFrameType) + 
                GetHexString(_strFrameID) +
                GetHexString(int64BitAddress, 16) + 
                GetHexString(_str16BitAddress, 4) + 
                GetHexString(_strRemoteCmdOptions) +
                GetHexString((UInt64)pPort) + 
                GetHexString((UInt16)pValue);
            
            Console.WriteLine("Payload:" + strPayload);
            
            List<String> lstValues = new List<String>(SplitInParts(strPayload, 2));
            
            int total = 0;
            foreach(string item in lstValues) {
                total += Int32.Parse(item, System.Globalization.NumberStyles.HexNumber);
            }
            Console.WriteLine("Calculating checksum");
            string strCheckSum  = total.ToString("X");
            strCheckSum = (255 - int.Parse(strCheckSum.Substring(strCheckSum.Length - 2), System.Globalization.NumberStyles.HexNumber)).ToString("X");
            Console.WriteLine("Checksum: " + strCheckSum);

            lstValues = new List<String>(SplitInParts(strPayload, 2));
            UInt32 length = (UInt32)lstValues.Count;
            string strPacket = GetHexString(_strDelimiter) + 
                GetHexString(length, 4) + 
                strPayload + 
                strCheckSum;

            Console.WriteLine("Packet: "+ strPacket);
            byte[] data = StringToByteArray(strPacket);
            serialPort.Write(data, 0, data.Length);
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
            if (lstFiles.Length > 0) {
                FileInfo objInfo = new FileInfo(lstFiles[0]);
                Mono.Unix.UnixSymbolicLinkInfo info = new Mono.Unix.UnixSymbolicLinkInfo(objInfo.FullName);
                string strFullPath = new FileInfo("/dev/serial/by-id/" + info.ContentsPath).FullName;

                Console.WriteLine("Located XBEE at " + strFullPath);
                return strFullPath;
            }
            throw new Exception("Unable to locate XBEE serial adapter");
        }
    }
}
