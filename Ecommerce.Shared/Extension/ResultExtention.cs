namespace Ecommerce.Shared.Extension;

public static class ResultExtension
{
    public static IActionResult ToProblemDetail<T>(this BaseResult<T> result)
    {
        if (result.Success)
        {
            return new OkResult();
        }

        // Use the ErrorCode from the first error as the status code
        var statusCode =
            result.Errors?.FirstOrDefault()?.ErrorCode ?? StatusCodes.Status400BadRequest;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = result?.Errors?.FirstOrDefault()?.FieldName ?? "Error:",
            Type = $"https://httpstatuses.com/{statusCode}",
            Extensions = new Dictionary<string, object?>
            {
                {
                    "errors",
                    result
                        ?.Errors?.Select(x => new
                        {
                            PropertyName = x.FieldName,
                            ErrorMessage = x.Description
                        })
                        .ToList()
                }
            }
        };

        return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
    }

    public static ProblemDetails ToProblemDetail(this ValidationException validationException)
    {
        var statusCode = StatusCodes.Status400BadRequest;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = "Error:",
            Type = $"https://httpstatuses.com/{statusCode}",
            Extensions = new Dictionary<string, object?>
            {
                {
                    "errors",
                    validationException
                        ?.Errors?.Select(x => new { x.PropertyName, x.ErrorMessage })
                        .ToList()
                }
            }
        };

        return problemDetails;
    }
}
