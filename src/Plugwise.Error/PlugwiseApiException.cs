using System.Net;

namespace Plugwise.Error;

public class PlugwiseApiException : ApiException<ApiErrorCode> {
    private PlugwiseApiException(IApiError<ApiErrorCode> error) : base(error) { }

    public static PlugwiseApiException from(ApiErrorCode error,
        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError) {
        return from(error, ApiErrorMessage.GetMessageForCode(error), httpStatusCode);
    }

    public static PlugwiseApiException from(ApiErrorCode error, string message) {
        return from(error, message, HttpStatusCode.InternalServerError);
    }

    public static PlugwiseApiException from(ApiErrorCode error, string message, HttpStatusCode httpStatusCode) {
        return new PlugwiseApiException(new ApiError(error, httpStatusCode) {
            Message = message
        });
    }
}