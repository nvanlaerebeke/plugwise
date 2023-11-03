using LanguageExt.Common;
using PlugwiseControl.Message.Responses;

namespace PlugwiseControl;

public interface IPlugControl {
    Result<StickStatusResponse> Initialize();
    Result<SwitchOnResponse> On(string mac);
    Result<SwitchOffResponse> Off(string mac);
    Result<CalibrationResponse> Calibrate(string mac);
    Result<double> GetUsage(string mac);
    Result<CircleInfoResponse> CircleInfo(string mac);
    Result<ResultResponse> SetDateTime(string mac, long unixDStamp);
}
