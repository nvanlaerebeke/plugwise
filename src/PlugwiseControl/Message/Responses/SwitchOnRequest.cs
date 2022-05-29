namespace PlugwiseControl.Message.Responses;

public class SwitchOnRequest : Response
{
    public SwitchState State => Responses[1][8..12] == "00DE" ? SwitchState.Off : SwitchState.On;
    public string Mac => Responses[1][8..28];

    public override bool IsComplete()
    {
        return IsComplete(32, "0000");
    }
}