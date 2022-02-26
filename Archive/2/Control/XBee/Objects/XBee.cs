using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controller
{
    public class XBeePortController
    {
        Dictionary<XBeePort, XBeePortValue> _dicPortValues;

        public XBeePortController(string pMac)
        {
            Mac = pMac;

            _dicPortValues = new Dictionary<XBeePort, XBeePortValue>();

            //ToDo: don't default it, just read it with a status req. message
            _dicPortValues.Add(XBeePort.D0, XBeePortValue.DigitalOutLow);
            _dicPortValues.Add(XBeePort.D1, XBeePortValue.DigitalOutLow);
            _dicPortValues.Add(XBeePort.D2, XBeePortValue.DigitalOutLow);
            _dicPortValues.Add(XBeePort.D3, XBeePortValue.DigitalOutLow);
            _dicPortValues.Add(XBeePort.D4, XBeePortValue.DigitalOutLow);
        }
        public string Mac { get; set; }

        public XBeePortValue GetPortValue(XBeePort pPort) {
            return _dicPortValues[pPort];
        }

        public void SetPortValue(XBeePort pPort, XBeePortValue pValue) {
            _dicPortValues[pPort] = pValue;
            XBeeCommunicator.Send(this, pPort, pValue);
        }
    }
}
