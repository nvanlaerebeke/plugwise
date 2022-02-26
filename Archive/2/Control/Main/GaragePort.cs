using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBee;

namespace Controller
{
    class GaragePort
    {
        private static List<XBeeXBeeSwitchController> _lstXBeeXBee = new List<XBeeXBeeSwitchController>(); 

        public static void init() {
            
            //bee.SetConnection(new SerialConnection("COM5", 9600));

            /*XBee.XBee objGarageXBee = new XBee.XBee { ApiType = ApiTypeValue.Enabled };
            objGarageXBee.SetConnection(new SerialConnection("COM5", 9600));/*
            
            //garage port open command
            _lstXBeeXBee.Add(new XBeeXBeeSwitchController(objGarageXBee, "0013A20040B18A4D", XBeePort.D0, "0013A20040B18A4D", XBeePort.D2, SwitchType.HighAndLow));

            //garage port alarm

            /*XBeeXBeeSwitchController objAlarm = new XBeeXBeeSwitchController(objGarageXBee, "0013A20040B18ABF", XBeePort.D1, "0013A20040B18A4D", XBeePort.D3, SwitchType.HighAndLow);
            objAlarm.PreviousValue = XBeePortValue.DigitalOutHigh;
            objAlarm.portValueChanged += new EventHandler(objAlarm_portValueChanged);
            _lstXBeeXBee.Add(objAlarm);*/


        }

        static void objAlarm_portValueChanged(object sender, EventArgs e)
        {
            /**
             * Stop listening
             */
            for (int i = 0; i < _lstXBeeXBee.Count; i++)
            {
                _lstXBeeXBee[i].dispose();
            }
            _lstXBeeXBee.Clear();
        }
    }
}
