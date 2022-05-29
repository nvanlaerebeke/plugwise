using System;
using PlugwiseControl.Message.Requests;
using PlugwiseControl.Message.Responses;

namespace PlugwiseControl;

public class PlugControl
{
    private readonly RequestManager _requestManager;

    public PlugControl(string serialPort)
    {
        _requestManager = new RequestManager(serialPort);
        //_requestManager.Send<StickStatusResponse>(new InitializeRequest());
    }

    public StickStatusResponse Initialize()
    {
        return _requestManager.Send<StickStatusResponse>(new InitializeRequest());
    }

    public SwitchOnRequest On(string mac)
    {
        return _requestManager.Send<SwitchOnRequest>(new On(mac));
    }

    public SwitchOffResponse Off(string mac)
    {
        return _requestManager.Send<SwitchOffResponse>(new Off(mac));
    }

    public CalibrationResponse Calibrate(string mac)
    {
        return _requestManager.Send<CalibrationResponse>(new CalibrationRequest(mac));
    }

    public double GetUsage(string mac)
    {
        var usage = _requestManager.Send<PowerUsageResponse>(new PowerUsageRequest(mac));
        var corrected = Correction(usage.Pulse1, Calibrate(mac));
        return 1000 * ConversionClass.PulsesToKws(corrected);
    }

    public CircleInfoResponse CircleInfo(string mac)
    {
        return _requestManager.Send<CircleInfoResponse>(new CircleInfoRequest(mac));
    }

    private double Correction(int pulses, CalibrationResponse calibration)
    {
        return 1 * (
            Math.Pow(pulses + calibration.OffsetNoise, 2) * calibration.GainB +
            (pulses + calibration.OffsetNoise) * calibration.GainA + calibration.OffsetTotal
        );
    }
}