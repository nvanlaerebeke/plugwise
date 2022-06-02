namespace PlugwiseControl.Message.Responses;

public class PowerUsageResponse : Response
{
    public string Seq => Responses[0][4..8];
    public string Mac => Responses[0][8..24];
    public ushort Pulse1 => ConversionClass.HexStringToUInt16(Responses[0][24..28]);
    public ushort Pulse2 => ConversionClass.HexStringToUInt16(Responses[0][28..32]);
    public uint PulseTotal => ConversionClass.HexStringToUInt32(Responses[0][32..40]);
    public uint PulseHour => ConversionClass.HexStringToUInt32(Responses[0][48..56]);

    public override bool IsComplete()
    {
        return IsComplete(56, "0013");
    }
}