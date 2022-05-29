namespace PlugwiseControl.Message.Responses;

public class PowerUsageResponse : Response
{
    public string Seq => Responses[1][4..8];
    public string Mac => Responses[1][8..24];
    public ushort Pulse1 => ConversionClass.HexStringToUInt16(Responses[1][24..28]);
    public ushort Pulse2 => ConversionClass.HexStringToUInt16(Responses[1][28..32]);
    public uint PulseTotal => ConversionClass.HexStringToUInt32(Responses[1][32..40]);
    public uint PulseHour => ConversionClass.HexStringToUInt32(Responses[1][48..56]);

    public override bool IsComplete()
    {
        return IsComplete(56, "0013");
    }
}