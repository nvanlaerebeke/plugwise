namespace PlugwiseControl.Message.Requests;

internal class PowerUsageRequest : PlugRequest {
    public PowerUsageRequest(string mac) : base("0012", mac) {
    }

    public override string ToString() {
        var crc = new Crc16Ccitt(InitialCrcValue.Zeros).ComputeChecksumString("0012" + Mac + "00");
        return GetStart() + "0012" + Mac + "00" + crc + GetEnd();
    }
}
