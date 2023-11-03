namespace PlugwiseControl.Message;

internal abstract class PlugRequest : Request {
    protected PlugRequest(string code, string mac) {
        Code = code;
        Mac = mac;
    }

    public string Mac { get; }
    public string Code { get; }

    public override string ToString() {
        return GetStart() + Code + Mac + new Crc16Ccitt(InitialCrcValue.Zeros).ComputeChecksumString(Code + Mac) +
               GetEnd();
    }
}
