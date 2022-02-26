using System;
using Raspberry.IO.GeneralPurpose;
using System.Timers;

namespace Controller.XBee
{
    class XBeeSwitchController
    {
        private Listener _objListener;
        private XBee _objXBee;
        private XBeePort _objXBeePort;
        private TimeSpan _objTimeSpan;
        private Timer _objTimer;

        public XBeeSwitchController(string pMac, XBeePort pPort, ConnectorPin pPin, SwitchType pType)
        {

            Console.WriteLine("Creating Switch " + pType.ToString() + " for " + pMac + " on pin " + pPin.ToString());

            //Create the xbee we're going to toggle
            _objXBee= new XBee(pMac);
            _objXBeePort = pPort;

            //Listen to input pin for controlling the Plug
            _objListener = new Listener(pPin, pType);
            _objListener.statusChanged += new StatusChanged(_objListener_statusChanged);
            _objListener.Start();

        }

        public XBeeSwitchController(string pMac, XBeePort pPort, ConnectorPin pPin, SwitchType pType, TimeSpan pTimeSpan, Boolean pDefaultOn)
            : this(pMac, pPort, pPin, pType)
        {
            Console.WriteLine("Initial value should be " + ((pDefaultOn) ? "ON" : "OFF"));
            _objXBee.SetPortValue(pPort, (pDefaultOn) ? XBeePortValue.DigitalOutHigh : XBeePortValue.DigitalOutLow);
            _objTimeSpan = pTimeSpan;
        }

        void _objListener_statusChanged()
        {
            Console.WriteLine("StatusChangedEvent for " + _objXBee.Mac);

            _objXBee.SetPortValue(_objXBeePort, XBeePortValue.DigitalOutHigh);
            if (_objTimeSpan != null)
            {
                _objTimer = new Timer();
                _objTimer.Elapsed += new ElapsedEventHandler(aTimer_Elapsed);
                _objTimer.Interval = _objTimeSpan.Milliseconds;
                _objTimer.Enabled = true;
                _objTimer.Start();
            }

        }

        void aTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _objTimer.Stop();
            _objTimer.Dispose();
            _objTimer = null;
            _objXBee.SetPortValue(_objXBeePort, XBeePortValue.DigitalOutLow);
        }
    }
}
