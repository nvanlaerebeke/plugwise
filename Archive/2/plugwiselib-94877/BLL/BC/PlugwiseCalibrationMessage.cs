using System;
using System.Collections.Generic;

using System.Text;
using plugwiseLib.BLL.BPC;
using System.Text.RegularExpressions;
using PlugwiseLib.UTIL;

namespace PlugwiseLib.BLL.BC
{
    public class PlugwiseCalibrationMessage
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

        private float _GainA;

        public float GainA
        {
            get { return _GainA; }
            set { _GainA = value; }
        }
        private float _GainB;

        public float GainB
        {
            get { return _GainB; }
            set { _GainB = value; }
        }
        private float _offTot;

        public float OffTot
        {
            get { return _offTot; }
            set { _offTot = value; }
        }
        private float _offRuis;

        public float OffRuis
        {
            get { return _offRuis; }
            set { _offRuis = value; }
        }

        public PlugwiseCalibrationMessage(PlugwiseMessage msg)
        {
            this.Mac = msg.Owner;
            this.Type = PlugwiseActions.Calibration;
            string[] values = Regex.Split(msg.Message, "\\|");
            this.GainA = ConversionClass.HexStringToFloat(values[0]);
            
            this.GainB = ConversionClass.HexStringToFloat(values[1]);

            this.OffRuis = ConversionClass.HexStringToFloat(values[2]);
            this.OffTot = ConversionClass.HexStringToFloat(values[3]);
        }
    }
}
