namespace PlugwiseControl.Message.Responses;

public class StickStatusResponse : Response
{
    public string StickMac => Responses[0][8..24];
    public bool IsCirclePlus => Responses[0][24..26].Equals("01");
    public string PanIDLong => Responses[0][26..42];
    public string PanID => Responses[0][42..46];

    public override bool IsComplete()
    {
        return IsComplete(54, "0011");
    }
}