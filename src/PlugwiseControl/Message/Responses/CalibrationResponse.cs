namespace PlugwiseControl.Message.Responses;

public class CalibrationResponse : Response
{
    public string Mac => Responses[1][8..24];
    public float GainA => ConversionClass.HexStringToFloat(Responses[1][24..32]);
    public float GainB => ConversionClass.HexStringToFloat(Responses[1][32..40]);
    public float OffsetTotal => ConversionClass.HexStringToFloat(Responses[1][40..48]);
    public float OffsetNoise => ConversionClass.HexStringToFloat(Responses[1][48..56]);

    public override bool IsComplete()
    {
        return IsComplete(60, "0027");
    }
}