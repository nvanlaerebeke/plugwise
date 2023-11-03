using LanguageExt.Common;
using PlugwiseControl.Cache;
using PlugwiseControl.Message.Requests;
using PlugwiseControl.Message.Responses;

namespace PlugwiseControl;

internal class PlugControl : IPlugControl {
    private readonly IRequestManager _requestManager;
    private readonly IUsageCache _usageCache;

    public PlugControl(IRequestManager requestManager, IUsageCache usageCache) {
        _requestManager = requestManager;
        _usageCache = usageCache;
    }

    public Result<StickStatusResponse> Initialize() {
        return _requestManager.Send<StickStatusResponse>(new InitializeRequest());
    }

    public Result<SwitchOnResponse> On(string mac) {
        return _requestManager.Send<SwitchOnResponse>(new OnRequest(mac));
    }

    public Result<SwitchOffResponse> Off(string mac) {
        return _requestManager.Send<SwitchOffResponse>(new OffRequest(mac));
    }

    public Result<CalibrationResponse> Calibrate(string mac) {
        return _requestManager.Send<CalibrationResponse>(new CalibrationRequest(mac));
    }

    public Result<double> GetUsage(string mac) {
        return _usageCache.Get(mac);
    }

    public Result<CircleInfoResponse> CircleInfo(string mac) {
        return _requestManager.Send<CircleInfoResponse>(new CircleInfoRequest(mac));
    }

    public Result<ResultResponse> SetDateTime(string mac, long unixDStamp) {
        return _requestManager.Send<ResultResponse>(new SetDateTimeRequest(mac, unixDStamp));
    }
}
