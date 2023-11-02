using System;
using PlugwiseControl;
using PlugwiseControl.Message;
using PlugwiseControl.Message.Responses;

namespace Plugwise;

public class PlugService : IPlugService
{
    private readonly PlugControl _plugControl;

    public PlugService()
    {
        var serialPort = Environment.GetEnvironmentVariable("PLUGWISE_SERIAL_PORT");
        if (string.IsNullOrEmpty(serialPort))
        {
            throw new Exception("Plugwise serial port is not set, set environment variable PLUGWISE_SERIAL_PORT");
        }

        _plugControl = new PlugControl(serialPort);
    }

    public bool On(string mac)
    {
        return _plugControl.On(mac).Status == Status.Success;
    }

    public bool Off(string mac)
    {
        return _plugControl.Off(mac).Status == Status.Success;
    }

    public CalibrationResponse Calibrate(string mac)
    {
        return _plugControl.Calibrate(mac);
    }

    public double Usage(string mac)
    {
        return _plugControl.GetUsage(mac);
    }

    public StickStatusResponse Initialize()
    {
        return _plugControl.Initialize();
    }

    public CircleInfoResponse CircleInfo(string mac)
    {
        return _plugControl.CircleInfo(mac);
    }

    public ResultResponse SetDateTime(string mac, long unixDStamp)
    {
        return _plugControl.SetDateTime(mac, unixDStamp);
    }
}