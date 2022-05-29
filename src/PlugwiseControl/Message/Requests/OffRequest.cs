namespace PlugwiseControl.Message.Requests;

internal class Off : PlugRequest
{
    public Off(string mac) : base("0017", mac)
    {
    }

    public override string ToString()
    {
        var crc = new Crc16Ccitt(InitialCrcValue.Zeros).ComputeChecksumString("0017" + Mac + "00");
        return GetStart() + "0017" + Mac + "00" + crc + GetEnd();
    }
}