using System;
using Raspberry.IO.GeneralPurpose;

namespace Controller
{
    class PlugSwitchController
    {
        private Listener _objListener;
        private Plug _objPlug;

        public PlugSwitchController(string pMac, ConnectorPin pPin, SwitchType pType)
        {

            Console.WriteLine("Creating Switch " + pType.ToString() + " for " + pMac + " on pin " + pPin.ToString());

            //Create the plug we're going to toggle
            _objPlug = new Plug(pMac);

            //Listen to input pin for controlling the Plug
            _objListener = new Listener(pPin, pType);
            _objListener.statusChanged += new StatusChanged(_objListener_statusChanged);
            _objListener.Start();

        }

        public PlugSwitchController(string pMac, ConnectorPin pPin, SwitchType pType, Boolean pDefaultOn)
            : this(pMac, pPin, pType)
        {
            Console.WriteLine("Initial value for plug should be " + ((pDefaultOn) ? "ON" : "OFF"));
            _objPlug.On = pDefaultOn;
        }

        void _objListener_statusChanged()
        {
            Console.WriteLine("StatusChangedEvent for " + _objPlug.Mac);
            _objPlug.On = !_objPlug.On;
        }
    }
}
