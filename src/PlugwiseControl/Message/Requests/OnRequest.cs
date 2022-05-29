namespace PlugwiseControl.Message.Requests;

internal class On : PlugRequest
{
    public On(string mac) : base("0017", mac)
    {
    }

    public override string ToString()
    {
        var crc = new Crc16Ccitt(InitialCrcValue.Zeros).ComputeChecksumString("0017" + Mac + "01");
        return GetStart() + "0017" + Mac + "01" + crc + GetEnd();
    }
}