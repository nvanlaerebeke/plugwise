using System;
using System.Collections.Generic;
using System.Text;
using plugwiseLib.BLL.BPC;
using System.Text.RegularExpressions;

namespace PlugwiseLib.BLL.BC
{
    public class PlugwisePowerUsageMessage
    {
        private string _mac;
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

        private float _eightSec;

        public float EightSec
        {
            get { return _eightSec; }
            set { _eightSec = value; }
        }

        private float _oneSec;

        public float OneSec
        {
            get { return _oneSec; }
            set { _oneSec = value; }
        }

        public PlugwisePowerUsageMessage(PlugwiseMessage msg)
        {
            this.Mac = msg.Owner;
            this.Type = PlugwiseActions.powerinfo;
            string[] values = Regex.Split(msg.Message, "\\|");
            this.EightSec = float.Parse(values[0]);
            this.OneSec = float.Parse(values[1]);
        }
    }
}
