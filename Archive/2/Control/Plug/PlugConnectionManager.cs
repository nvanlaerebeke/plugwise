using System;
using PlugwiseLib;

namespace Controller;

internal class PlugConnectionManager
{
    private static plugwiseControl _objConnection;
    private static readonly object objLock = new();

    public static plugwiseControl GetConnection()
    {
        lock (objLock)
        {
            if (_objConnection == null)
            {
                Console.WriteLine("Creating connection to Plugwise serial adapter");
                _objConnection = new plugwiseControl(ComPort());
                _objConnection.Open();
            }
        }

        return _objConnection;
    }

    private static string ComPort()
    {
        return "/dev/ttyUSB0";
    }
}