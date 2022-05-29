namespace PlugwiseControl.Message.Responses;

public class SwitchOffResponse : Response
{
    public SwitchState State => Responses[1][8..12] == "00D8" ? SwitchState.On : SwitchState.Off;
    public string Mac => Responses[1][8..28];

    public override bool IsComplete()
    {
        return IsComplete(32, "0000");
    }
}