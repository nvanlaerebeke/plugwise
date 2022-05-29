using System;
using System.Collections.Generic;

namespace PlugwiseControl.Message.Responses;

public abstract class Response
{
    protected List<string> Responses { get; } = new();

    public virtual string Code => Responses[1][..4];
    public virtual string Sequence => Responses[1][4..8];
    public virtual string Crc16 => Responses[0][..^4];

    public abstract bool IsComplete();

    protected bool IsComplete(int length, string code)
    {
        if (Responses.Count > 2)
        {
            Invalid();
        }

        return
            Responses.Count.Equals(2) &&
            IsAckOk(Responses[0]) &&
            Responses[1].Length.Equals(length) &&
            Responses[1].StartsWith(code)
            ;
    }

    public virtual void AddData(string data)
    {
        Responses.Add(data);
    }

    protected bool IsAckOk(string data)
    {
        var result = new ResultResponse(data);
        return result.IsComplete() && result.Success;
    }

    private void Invalid()
    {
        throw new Exception($"Invalid {GetType().Name}");
    }
}