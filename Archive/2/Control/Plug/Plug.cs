using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using PlugwiseLib.BLL.BC;

namespace Controller;

internal class Plug
{
    private bool? _blnOn;

    public Plug(string pMac)
    {
        Console.WriteLine("Added Plug " + pMac);
        Mac = pMac;
    }

    public string Mac { get; }

    public bool? On
    {
        get
        {
            if (_blnOn == null)
            {
                Console.WriteLine("Current plug state is unknown");
                Console.WriteLine("Getting current plug status...");
               
                var objRequest = new PlugRequest();
                var objResponse = objRequest.Request<PlugwiseStatusMessage>(this, PlugwiseActions.Status);
                _blnOn = objResponse.On;

                Console.WriteLine("Current Status is " + (_blnOn == true ? "ON" : "OFF"));
            }

            return _blnOn;
        }
        set
        {
            Console.WriteLine("Setting Plug " + Mac + " to " + value);
            _blnOn = value == null ? false : value;

            var objConnection = PlugConnectionManager.GetConnection();
            Console.WriteLine("Setting plug status of " + Mac + " to " + (_blnOn == true ? "ON" : "OFF"));
            if (value == false)
            {
                objConnection.Action(Mac, PlugwiseActions.Off);
            }
            else
            {
                objConnection.Action(Mac, PlugwiseActions.On);
            }

            Console.WriteLine("Status Changed");
        }
    }

    private void objConnection_DataReceived(object sender, EventArgs e, List<PlugwiseMessage> data)
    {
        throw new NotImplementedException();
    }
}