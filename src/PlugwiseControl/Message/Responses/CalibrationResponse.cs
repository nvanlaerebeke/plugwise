namespace PlugwiseControl.Message.Responses;

public class CalibrationResponse : Response {
    public string Mac => Responses[0][8..24];
    public float GainA => ConversionClass.HexStringToFloat(Responses[0][24..32]);
    public float GainB => ConversionClass.HexStringToFloat(Responses[0][32..40]);
    public float OffsetTotal => ConversionClass.HexStringToFloat(Responses[0][40..48]);
    public float OffsetNoise => ConversionClass.HexStringToFloat(Responses[0][48..56]);

    public override bool IsComplete() {
        return IsComplete(60, "0027");
    }
}
