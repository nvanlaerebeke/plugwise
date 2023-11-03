namespace PlugwiseControl.Message;

public abstract class Request {
    public new abstract string ToString();

    protected static string GetStart() {
        return "" + (char)5 + (char)5 + (char)3 + (char)3;
    }

    protected static string GetEnd() {
        return "" + (char)13 + (char)10;
    }
}
