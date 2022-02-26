using System;
using System.Collections.Generic;
using Raspberry.IO.GeneralPurpose;
using XBee;

namespace Controller
{
    static class Controller
    {
        private static List<PIPlugSwitchController> _listSwitches = new List<PIPlugSwitchController>();
        private static List<PIXBeeSwitchController> _listXBees = new List<PIXBeeSwitchController>();
        private static List<XBeeXBeeSwitchController> _lstXBeeXBee = new List<XBeeXBeeSwitchController>(); 
        public static void Start()
        {
            /**
             * XBee Mac Addresses:
             *  - Controller: 0013A20040B18A50
             *  - Bel: 0013A20040B18ABF
             *  - Garage: 0013A20040B18A4D
             */


            /**
             * PI Port Legend
             * 1 => P1Pin07 sometimes malfunctions on P1Pin08
             * 2 => P1Pin11
             * 3 => P1Pin12
             * 4 => P1Pin13
             * 5 => P1Pin15
             * 6 => P1Pin16
             */
            //GaragePort.init();
            //Add Switch 1

            //Add Switch 2
            //Console.WriteLine("Adding Switch 2");

            /**
             * Remote 220v Plug 
             */
            _listXBees.Add(new PIXBeeSwitchController("0013A20040C335B2", XBeePort.D0, ConnectorPin.P1Pin11, SwitchType.HighAndLow, new TimeSpan(0, 0, 0, 2, 0), false));

            /**
             * Garage port Remote 
             */
            _listXBees.Add(new PIXBeeSwitchController("0013A20040B18ABF", XBeePort.D0, ConnectorPin.P1Pin07, SwitchType.HighAndLow, new TimeSpan(0,0, 0, 2, 0), false));

            /*
            _listSwitches.Add(new PIPlugSwitchController("000D6F0001A5A3B6", ConnectorPin.P1Pin03, SwitchType.HighAndLow, false));
            _listSwitches.Add(new PIPlugSwitchController("000D6F0001A5A3B6", ConnectorPin.P1Pin05, SwitchType.HighAndLow, false));
            _listSwitches.Add(new PIPlugSwitchController("000D6F0001A5A3B6", ConnectorPin.P1Pin07, SwitchType.HighAndLow, false));
            _listSwitches.Add(new PIPlugSwitchController("000D6F0001A5A3B6", ConnectorPin.P1Pin08, SwitchType.HighAndLow, false));
            _listSwitches.Add(new PIPlugSwitchController("000D6F0001A5A3B6", ConnectorPin.P1Pin10, SwitchType.HighAndLow, false));
            _listSwitches.Add(new PIPlugSwitchController("000D6F0001A5A3B6", ConnectorPin.P1Pin12, SwitchType.HighAndLow, false));
            _listSwitches.Add(new PIPlugSwitchController("000D6F0001A5A3B6", ConnectorPin.P1Pin13, SwitchType.HighAndLow, false));
            _listSwitches.Add(new PIPlugSwitchController("000D6F0001A5A3B6", ConnectorPin.P1Pin15, SwitchType.HighAndLow, false));
            _listSwitches.Add(new PIPlugSwitchController("000D6F0001A5A3B6", ConnectorPin.P1Pin16, SwitchType.HighAndLow, false));
            _listSwitches.Add(new PIPlugSwitchController("000D6F0001A5A3B6", ConnectorPin.P1Pin18, SwitchType.HighAndLow, false));
            _listSwitches.Add(new PIPlugSwitchController("000D6F0001A5A3B6", ConnectorPin.P1Pin19, SwitchType.HighAndLow, false));
            _listSwitches.Add(new PIPlugSwitchController("000D6F0001A5A3B6", ConnectorPin.P1Pin21, SwitchType.HighAndLow, false));
            _listSwitches.Add(new PIPlugSwitchController("000D6F0001A5A3B6", ConnectorPin.P1Pin22, SwitchType.HighAndLow, false));
            _listSwitches.Add(new PIPlugSwitchController("000D6F0001A5A3B6", ConnectorPin.P1Pin23, SwitchType.HighAndLow, false));
            _listSwitches.Add(new PIPlugSwitchController("000D6F0001A5A3B6", ConnectorPin.P1Pin24, SwitchType.HighAndLow, false));*/
            //_listSwitches.Add(new PIPlugSwitchController("000D6F0001A5A3B6", ConnectorPin.P1Pin26, SwitchType.HighAndLow, false));
            //_listSwitches.Add(new PIPlugSwitchController("000D6F00004BC20A", ConnectorPin.P1Pin11, SwitchType.HighAndLow, false));

            //Adding XBee 1

        }
    }
}
