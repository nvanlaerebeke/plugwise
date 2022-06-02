using System.Collections.Generic;

namespace PlugwiseControl.Message.Responses;

public abstract class Response
{
    protected List<string> Responses { get; } = new();
    public Status Status { get; set; }
    public virtual string Code => Responses[0][..4];
    public virtual string Sequence => Responses[0][4..8];
    public virtual string Crc16 => Responses[0][..^4];

    public abstract bool IsComplete();

    protected bool IsComplete(int length, string code)
    {
        if (Responses.Count > 1)
        {
            Status = Status.Failed;
        }

        return
            Responses.Count.Equals(1) &&
            Responses[0].Length.Equals(length) &&
            Responses[0].StartsWith(code)
            ;
    }

    public virtual void AddData(string data)
    {
        Responses.Add(data);
    }
}