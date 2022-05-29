using System;

namespace PlugwiseControl.Message.Responses;

public class CircleInfoResponse : Response
{
    public override string Code => Responses[3][..4];
    public override string Sequence => Responses[3][4..8];
    public override string Crc16 => Responses[3][..^4];
    
    public string CirclePlusMac => Responses[3][8..24];
    public string Year => Responses[3][24..26];
    public string Month => Responses[3][26..28];
    public string Min => Responses[3][28..32];
    public string CurrentLog => Responses[3][32..40];
    public SwitchState State => Responses[3][40..42] == "00" ? SwitchState.Off : SwitchState.On;
    public string Hertz => Responses[3][42..44];
    
    public string HW1 => Responses[3][44..48];
    public string HW2 => Responses[3][48..52];
    public string HW3 => Responses[3][52..56];
    
    // => d/m/y
    public string Firmware => Responses[3][56..64];
    
    public string Type => Responses[3][64..66];
    
    public override bool IsComplete()
    {
        return
            Responses.Count.Equals(4) &&
            IsAckOk(Responses[0]) &&
            Responses[3].Length.Equals(70) &&
            Responses[3].StartsWith("0024");
    }
}