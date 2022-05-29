using PlugwiseControl.Message.Responses;

namespace PlugwiseControl;

internal class Request
{
    private readonly Response _response;

    public Request(Response response)
    {
        _response = response;
    }

    public void AddData(string data)
    {
        _response.AddData(data);
    }

    public bool IsComplete()
    {
        return _response.IsComplete();
    }

    public T GetResponse<T>() where T : Response
    {
        return (T) _response;
    }
}