namespace PlugwiseControl.Message.Requests;

internal class InitializeRequest : Request
{
    public override string ToString()
    {
        return GetStart() + "000AB43C" + GetEnd();
    }
}