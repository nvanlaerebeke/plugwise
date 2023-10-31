using PlugwiseControl.Cache;
using PlugwiseControl.Calibration;
using PlugwiseControl.Message.Requests;
using PlugwiseControl.Message.Responses;

namespace PlugwiseControl;

public class PlugControl : IPlugControl
{
    private readonly Calibrator _calibrator;
    private readonly RequestManager _requestManager;
    private readonly UsageCache _usageCache;
    
    public PlugControl(string serialPort)
    {
        _requestManager = new RequestManager(serialPort);
        _calibrator = new Calibrator(_requestManager);
        _usageCache = new UsageCache(_requestManager, _calibrator);
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
        return _usageCache.Get(mac);
    }

    public CircleInfoResponse CircleInfo(string mac)
    {
        return _requestManager.Send<CircleInfoResponse>(new CircleInfoRequest(mac));
    }

    public ResultResponse SetDateTime(string mac, long unixDStamp)
    {
        return _requestManager.Send<ResultResponse>(new SetDateTimeRequest(mac, unixDStamp));
    }
}