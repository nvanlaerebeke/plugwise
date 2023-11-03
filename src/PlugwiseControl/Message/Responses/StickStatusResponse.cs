namespace PlugwiseControl.Message.Responses;

public class StickStatusResponse : Response {
    public string StickMac => Responses[0][8..24];
    public bool IsCirclePlus => Responses[0][24..26].Equals("01");
    public string PanIdLong => Responses[0][26..42];
    public string PanId => Responses[0][42..46];

    public override bool IsComplete() {
        return IsComplete(54, "0011");
    }
}
