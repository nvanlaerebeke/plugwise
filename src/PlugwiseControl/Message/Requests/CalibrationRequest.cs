namespace PlugwiseControl.Message.Requests;

internal class CalibrationRequest : PlugRequest
{
    public CalibrationRequest(string mac) : base("0026", mac)
    {
    }
}