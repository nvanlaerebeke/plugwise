using PlugwiseControl.Message.Responses;

namespace PlugwiseControl;

internal interface IRequestManager
{
    T Send<T>(Message.Request request) where T : Response, new();
}