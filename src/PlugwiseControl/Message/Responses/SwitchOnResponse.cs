namespace PlugwiseControl.Message.Responses;

public class SwitchOnResponse : Response {
    public override bool IsComplete() {
        return true;
    }
}
