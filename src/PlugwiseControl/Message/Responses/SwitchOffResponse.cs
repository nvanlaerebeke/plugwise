namespace PlugwiseControl.Message.Responses;

public class SwitchOffResponse : Response
{
    public override bool IsComplete()
    {
        return true;
    }
}