using PlugwiseControl.Message.Responses;

namespace PlugwiseControl;

public interface IPlugControl
{
    StickStatusResponse Initialize();
    SwitchOnRequest On(string mac);
    SwitchOffResponse Off(string mac);
    CalibrationResponse Calibrate(string mac);
    double GetUsage(string mac);
    CircleInfoResponse CircleInfo(string mac);
    ResultResponse SetDateTime(string mac, long unixDStamp);
}