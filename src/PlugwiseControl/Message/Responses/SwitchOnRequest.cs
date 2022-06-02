namespace PlugwiseControl.Message.Responses;

public class SwitchOnRequest : Response
{
    public override bool IsComplete()
    {
        return true;
    }
}