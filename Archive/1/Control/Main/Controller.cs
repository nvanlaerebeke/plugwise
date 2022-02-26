using System;
using System.Collections.Generic;
using Raspberry.IO.GeneralPurpose;

namespace Controller
{
    static class Controller
    {
        private static List<PlugSwitchController> _listSwitches = new List<PlugSwitchController>();
        private static List<XBee.XBeeSwitchController> _listXBees = new List<XBee.XBeeSwitchController>();

        public static void Start() {
            //Add Switch 1
            Console.WriteLine("Adding Switch 1");
            _listXBees.Add(new XBee.XBeeSwitchController("0013A20040B18ABF", XBee.XBeePort.D0, ConnectorPin.P1Pin07, SwitchType.HighAndLow, new TimeSpan(0,0,0,0, 500), false));
            
            //Add Switch 2
            //Console.WriteLine("Adding Switch 2");
            _listSwitches.Add(new PlugSwitchController("000D6F0001A5A3B6", ConnectorPin.P1Pin11, SwitchType.HighAndLow, false));


            //_listSwitches.Add(new PlugSwitchController("000D6F00004BC20A", ConnectorPin.P1Pin11, SwitchType.HighAndLow, false));

            //Adding XBee 1
            
        }
    }
}
