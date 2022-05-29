namespace PlugwiseControl.Message.Requests;

internal class CircleInfoRequest : PlugRequest
{
    public CircleInfoRequest(string mac) : base("0023", mac)
    {
    }
}