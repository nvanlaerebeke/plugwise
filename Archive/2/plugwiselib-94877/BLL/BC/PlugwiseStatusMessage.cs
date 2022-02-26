using System;
using System.Collections.Generic;

using System.Text;
using plugwiseLib.BLL.BPC;
using System.Text.RegularExpressions;
using PlugwiseLib.UTIL;

namespace PlugwiseLib.BLL.BC
{
    public class PlugwiseStatusMessage : PlugwiseMessage
    {
        private bool _on;

        public bool On
        {
            get { return _on; }
            set { _on = value; }
        }
        private string _mac;
        private int _lastLog;

        public int LastLog
        {
            get { return _lastLog; }
            set { _lastLog = value; }
        }

        public string Mac
        {
            get { return _mac; }
            set { _mac = value; }
        }
        private PlugwiseActions _type;

        public PlugwiseActions Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public PlugwiseStatusMessage(PlugwiseMessage msg)
        {
            this.Mac = msg.Owner;
            this.Type = PlugwiseActions.Status;
            string[] values = Regex.Split(msg.Message, "\\|");
            this.On = Convert.ToBoolean(Convert.ToInt32(values[0]));
            this.LastLog = MessageHelper.ConvertPlugwiseLogHexToInt((int)ConversionClass.HexStringToUInt32(values[1]));
        }

    }
}
