using System;
using System.Collections.Generic;
using System.Threading;
using PlugwiseLib.BLL.BC;

namespace Controller;

internal class PlugRequest
{
    private PlugwiseActions _enuAction;
    private Plug _objPlug;
    private PlugwiseMessage _objResponseMessage;
    private EventWaitHandle blockThread;

    public T Request<T>(Plug pPlug, PlugwiseActions pAction)
    {
        _objPlug = pPlug;
        _enuAction = pAction;

        var objConnection = PlugConnectionManager.GetConnection();
        objConnection.DataReceived += objConnection_DataReceived;

        objConnection.Action(pPlug.Mac, pAction);

        blockThread = new AutoResetEvent(false);
        blockThread.WaitOne();

        return (T) Activator.CreateInstance(typeof(T), _objResponseMessage);
    }

    private void objConnection_DataReceived(object sender, EventArgs e, List<PlugwiseMessage> data)
    {
        if (data[0].Owner == _objPlug.Mac && data[0].ResponseCode == PlugResponseCode.GetResponseCode(_enuAction))
        {
            _objResponseMessage = data[0];
            blockThread.Set();
        }

        Console.WriteLine("DATA RECIEVED");
    }
}