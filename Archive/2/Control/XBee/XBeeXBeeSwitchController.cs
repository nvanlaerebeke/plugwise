using System;
using Raspberry.IO.GeneralPurpose;
using System.Timers;
using XBee;
using XBee.Frames;

namespace Controller
{

    public class XBeeXBeeSwitchController
    {
        private XBee.XBee _objController;
        private XBeePortController _objXBee;
        private XBeePort _objXBeePort;
        private XBeePort _objSourcePort;
        public XBeePortValue PreviousValue = XBeePortValue.DigitalOutLow;
        private Timer _objLastInputTimer;
        public event EventHandler portValueChanged;

        public XBeeXBeeSwitchController(XBee.XBee pController, string pSourceMac, XBeePort pSourcePort, string pDestinationMac, XBeePort pDestinationPort, SwitchType pType)
        {

            Console.WriteLine("Creating Switch " + pType.ToString() + " for " + pDestinationMac + " from XBee " + pSourceMac);

            //Create the xbee we're going to toggle
            _objXBee = new XBeePortController(pDestinationMac);
            _objXBeePort = pDestinationPort;
            _objSourcePort = pSourcePort;

            _objController = pController;
            _objController.frameReceivedEvent += new FrameReceivedHandler(pController_frameReceivedEvent);

        }

        void pController_frameReceivedEvent(object sender, FrameReceivedArgs args)
        {
            if (args.Response.GetCommandId() != XBeeAPICommandId.EXPLICIT_RX_INDICATOR_RESPONSE)
            {
                return;
            }

            ZigBeeExplicitRXIndicator response = args.Response as ZigBeeExplicitRXIndicator;

            bool blnValue = false;
            switch (_objSourcePort)
            {
                case XBeePort.D0:
                    blnValue = bitSet(response.Data[response.Data.Length - 1], 0);
                    break;
                case XBeePort.D1:
                    blnValue = bitSet(response.Data[response.Data.Length - 1], 1);
                    break;
                case XBeePort.D2:
                    blnValue = bitSet(response.Data[response.Data.Length - 1], 2);
                    break;
                case XBeePort.D3:
                    blnValue = bitSet(response.Data[response.Data.Length - 1], 3);
                    break;
            }
            XBeePortValue objCurrentValue = (blnValue) ? XBeePortValue.DigitalOutHigh : XBeePortValue.DigitalOutLow;

            //timer not expired, reset timer
            if (_objLastInputTimer != null)
            {
                _objLastInputTimer.Stop();
                _objLastInputTimer.Interval = 500;
                _objLastInputTimer.Start();
                return;
            }
            else
            {
                _objLastInputTimer = new Timer(500);
                _objLastInputTimer.Elapsed += new ElapsedEventHandler(_objLastInputTimer_Elapsed);
                _objLastInputTimer.Start();
            }
            if (PreviousValue != objCurrentValue)
            {
                PreviousValue = objCurrentValue;
                _objXBee.SetPortValue(_objXBeePort, objCurrentValue);
                if (portValueChanged != null)
                {
                    portValueChanged(this, null);
                }
            }


            Console.WriteLine("Done");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
        }

        void _objLastInputTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _objLastInputTimer.Elapsed -= new ElapsedEventHandler(_objLastInputTimer_Elapsed);
            _objLastInputTimer.Stop();
            _objLastInputTimer = null;

            PreviousValue = XBeePortValue.DigitalOutLow;
            _objXBee.SetPortValue(_objXBeePort, XBeePortValue.DigitalOutLow);
            if (portValueChanged != null)
            {
                portValueChanged(this, null);
            }
        }

        static bool bitSet(byte pValue, byte pBitNumber)
        {
            return (pValue & (1 << pBitNumber)) != 0;
        }

        public void dispose() {
            _objController.frameReceivedEvent -= new FrameReceivedHandler(pController_frameReceivedEvent);
            _objController = null;
            if(_objLastInputTimer != null) {
                _objLastInputTimer.Elapsed -= new ElapsedEventHandler(_objLastInputTimer_Elapsed);
                _objLastInputTimer.Stop();
                _objLastInputTimer.Dispose();
                portValueChanged = null;
            }
        }
    }
}