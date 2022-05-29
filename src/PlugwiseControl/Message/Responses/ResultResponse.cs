namespace PlugwiseControl.Message.Responses;

public class ResultResponse : Response
{
    public ResultResponse(string data)
    {
        Responses.Add(data);
    }

    public bool Success => Responses[0][8..12].Equals("00C1");

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