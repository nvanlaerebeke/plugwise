using System;
using Raspberry.IO.GeneralPurpose;
using System.Timers;

namespace Controller
{
    class PIXBeeSwitchController
    {
        private Listener _objListener;
        private XBeePortController _objXBee;
        private XBeePort _objXBeePort;
        private TimeSpan _objTimeSpan;
        private Timer _objTimer = new Timer();

        public PIXBeeSwitchController(string pMac, XBeePort pPort, ConnectorPin pPin, SwitchType pType)
        {

            Console.WriteLine("Creating Switch " + pType.ToString() + " for " + pMac + " on pin " + pPin.ToString());

            //Link the siwtch to the port PI IO port
            _objListener = new Listener(pPin, pType);
            _objListener.statusChanged += new StatusChanged(_objListener_statusChanged);
            _objListener.Start();

            Console.WriteLine("Finished Starting the PI IO port Listener");

            Console.WriteLine("Creating the XBEE(" + pMac + ") object on port " + pPort.ToString());
            //Create the xbee we're going to toggle
            _objXBee = new XBeePortController(pMac);
            _objXBeePort = pPort;
            Console.WriteLine("Finished Creating the XBEE object");


        }

        public PIXBeeSwitchController(string pMac, XBeePort pPort, ConnectorPin pPin, SwitchType pType, TimeSpan pTimeSpan, Boolean pDefaultOn)
            : this(pMac, pPort, pPin, pType)
        {
            Console.WriteLine("Initial value should be " + ((pDefaultOn) ? "ON" : "OFF"));
            _objXBee.SetPortValue(pPort, (pDefaultOn) ? XBeePortValue.DigitalOutHigh : XBeePortValue.DigitalOutLow);
            _objTimeSpan = pTimeSpan;
        }

        void _objListener_statusChanged()
        {
            Console.WriteLine("StatusChangedEvent for " + _objXBee.Mac);

            if (_objTimer.Enabled)
            {
                Console.WriteLine("Skipping Event, Timer still running...");
            }
            else
            {
                _objXBee.SetPortValue(_objXBeePort, XBeePortValue.DigitalOutHigh);
                Console.WriteLine("Starting Timer...");
                _objTimer.Elapsed += new ElapsedEventHandler(delegate
                {
                    Console.WriteLine("Timer Expired");
                    _objXBee.SetPortValue(_objXBeePort, XBeePortValue.DigitalOutLow);
                    _objTimer.Stop();
                    _objTimer.Enabled = false;

                });

                _objTimer.Interval = _objTimeSpan.TotalMilliseconds;
                _objTimer.Enabled = true;
                _objTimer.Start();

            }
        }
    }
}
