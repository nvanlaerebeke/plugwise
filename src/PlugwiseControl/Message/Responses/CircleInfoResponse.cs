using System;

namespace PlugwiseControl.Message.Responses;

public class CircleInfoResponse : Response
{
    public override string Code => Responses[2][..4];
    public override string Sequence => Responses[2][4..8];
    public override string Crc16 => Responses[2][..^4];

    public string CirclePlusMac => Responses[2][8..24];

    public DateTime Date => new DateTime(
            Convert.ToInt32(Responses[2][24..26], 16) + 2000,
            Convert.ToInt32(Responses[2][26..28], 16),
            1)
        .AddMinutes(
            Convert.ToInt32(Responses[2][28..32], 16)
        );

    public string CurrentLog => Responses[2][32..40];
    public SwitchState State => Responses[2][40..42] == "00" ? SwitchState.Off : SwitchState.On;
    public string Hertz => Responses[2][42..44];

    public string HW1 => Responses[2][44..48];
    public string HW2 => Responses[2][48..52];
    public string HW3 => Responses[2][52..56];

    // => d/m/y
    public string Firmware => Responses[2][56..64];

    public string Type => Responses[3][64..66];

    public override bool IsComplete()
    {
        return
            Responses.Count.Equals(3) &&
            Responses[2].Length.Equals(70) &&
            Responses[2].StartsWith("0024");
    }
}