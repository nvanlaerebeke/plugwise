using System.IO;
using System.Linq;
using XBee.Exceptions;
using System.Collections.Generic;

namespace XBee
{
    public class PacketReader : IPacketReader
    {
        public event FrameReceivedHandler FrameReceived;

        protected MemoryStream Stream = new MemoryStream();
        
        private int _intPacketLength = 0;
        private List<byte> _lstIncomplete = new List<byte>();

        public void ReceiveData(byte[] data)
        {
            CopyAndProcessData(data);
        }

        private void CopyAndProcessData(byte[] data)
        {
            _lstIncomplete.AddRange(data);
            List<byte> lstPacket = new List<byte>();
            if (_lstIncomplete.Count < 3) {
                return;
            }
            for (int i = 0; i < _lstIncomplete.Count; i++)
            {
                if (_lstIncomplete[i] == (byte)XBeeSpecialBytes.StartByte)
                {
                    string strHexLength = _lstIncomplete[i + 1].ToString("X") + _lstIncomplete[i + 2].ToString("X");
                    
                    //the extra bytes are the checksum and length
                    _intPacketLength = int.Parse(strHexLength, System.Globalization.NumberStyles.HexNumber) + 4;
                    if (_lstIncomplete.Count >= (i + _intPacketLength))
                    {
                        byte[] arrPacket = _lstIncomplete.GetRange(i, _intPacketLength ).ToArray();
                        lstPacket.AddRange(arrPacket);
                        _lstIncomplete.RemoveRange(0, (i + _intPacketLength));
                    }
                    break;
                }
            }

            if(lstPacket.Count == _intPacketLength) {

                ProcessReceivedData(lstPacket.GetRange(0, _intPacketLength).ToArray());
            } else {
                return;
            }

            /*foreach (var b in data.Where(b => Stream.Length != 0 || b != (byte) XBeeSpecialBytes.StartByte)) {
                Stream.WriteByte(b);
            }

            if (packetLength == 0 && Stream.Length > 2) {
                var packet = Stream.ToArray();
                packetLength = (uint) (packet[0] << 8 | packet[1]) + 3;
            }

            if (Stream.Length < 3)
            {
                return;
            }

            if (packetLength != 0 && Stream.Length < packetLength)
            {
                return;
            }

            ProcessReceivedData();*/
        }

        protected virtual void ProcessReceivedData(byte[] pData)
        {
            string strHex = "";
            foreach (byte bytItem in pData) {
                strHex += bytItem.ToString("X");
            }
            
            try {
                var frame = XBeePacketUnmarshaler.Unmarshal(pData);
                if (FrameReceived != null)
                {
                    FrameReceived.Invoke(this, new FrameReceivedArgs(frame));
                }
            } catch (XBeeFrameException ex) {
                //ignore packet
                //throw new XBeeException("Unable to unmarshal packet.", ex);
            }
        }
    }
}
