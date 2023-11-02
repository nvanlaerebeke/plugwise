using LanguageExt.Common;
using PlugwiseControl.Message.Responses;

namespace PlugwiseControl;

internal interface IRequestManager
{
    Result<T> Send<T>(Message.Request request) where T : Response, new();
}