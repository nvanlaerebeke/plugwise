using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using Plugwise.Error;

namespace Plugwise.Api.ExtensionMethods;

public static class ControllerExtensions {
    public static IActionResult ToOk<TResult, TContract>(
        this Result<TResult> result, Func<TResult, TContract> mapper
    ) {
        return result.Match<IActionResult>(obj => {
            var response = mapper(obj);
            return new OkObjectResult(response);
        }, exception => {
            if (exception is PlugwiseApiException) {
                var error = (exception as PlugwiseApiException)!.GetError();
                switch (error.Code) {
                    case ApiErrorCode.InvalidValue:
                        return new BadRequestObjectResult(exception);
                    case ApiErrorCode.NotFound:
                        return new NotFoundResult();
                    case ApiErrorCode.Exists:
                        return new ConflictResult();
                    default:
                        return new JsonResult(error) { StatusCode = (int)error.HttpStatusCode };
                }
            }

            throw exception;
        });
    }
}
