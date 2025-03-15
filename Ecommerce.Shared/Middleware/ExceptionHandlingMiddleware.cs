namespace Ecommerce.Shared.Middleware;

public class ExceptionHandlingMiddleware(
    RequestDelegate requestDelegate,
    ILogger<ExceptionHandlingMiddleware> logger
)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await requestDelegate(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured : {message}", ex.Message);
            var problemDetails = new ProblemDetails
            {
                Status = (int)StatusCodes.Status500InternalServerError,
                Title = "Server Error",
                Type = "https://httpstatuses.com/500",
                Extensions = new Dictionary<string, object?>
                {
                    {
                        "errors",
                        new
                        {
                            fieldName = "Error",
                            descriptions = "An unexpected error occurred. Please try again later."
                        }
                    }
                }
            };
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
