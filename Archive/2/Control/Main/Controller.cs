using System.Collections.Generic;

namespace Controller;

internal static class Controller
{
    private static List<PIPlugSwitchController> _listSwitches = new();

    public static void Start()
    {
        _listSwitches.Add(new PIPlugSwitchController("000D6F0001A5A3B6", true));

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