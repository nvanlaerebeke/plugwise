using System;

namespace Controller;

internal class PIPlugSwitchController
{
    private readonly Plug _objPlug;

    public PIPlugSwitchController(string pMac)
    {
        Console.WriteLine("Creating Switch for " + pMac);

        //Create the plug we're going to toggle
        _objPlug = new Plug(pMac);
    }

    public PIPlugSwitchController(string pMac, bool pDefaultOn)
        : this(pMac)
    {
        Console.WriteLine("Initial value for plug should be " + (pDefaultOn ? "ON" : "OFF"));
        _objPlug.On = pDefaultOn;
    }
}