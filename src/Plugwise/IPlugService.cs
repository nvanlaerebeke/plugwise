using PlugwiseControl.Message.Responses;

namespace Plugwise;

public interface IPlugService
{
    StickStatusResponse Initialize();
    bool On(string mac);
    bool Off(string mac);
    CalibrationResponse Calibrate(string mac);
    double Usage(string mac);
    CircleInfoResponse CircleInfo(string mac);
}