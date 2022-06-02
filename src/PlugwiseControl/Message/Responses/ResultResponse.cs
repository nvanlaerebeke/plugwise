namespace PlugwiseControl.Message.Responses;

public class ResultResponse : Response
{
    public ResultResponse()
    {
    }

    public ResultResponse(string data)
    {
        Responses.Add(data);
    }

    public Status Status
    {
        get
        {
            return Responses[0][8..12] switch
            {
                "00C1" => Status.Success,
                "00E1" => Status.TimeOut,
                _ => Status.Failed
            };
        }
    }

    public override void AddData(string data)
    {
        if (Responses.Count.Equals(1))
        {
            Responses[0] += data;
        }
        else
        {
            Responses.Add(data);
        }
    }

    public override bool IsComplete()
    {
        return Responses.Count.Equals(1) && Responses[0].Length.Equals(16);
    }
}