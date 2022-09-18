
namespace Account.WebApi.Middlewares;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    //public async Task Invoke(HttpContext httpContext)
    //{

    //    try
    //    {
    //        await _next(httpContext);
    //        //throw on exception when model is invalid

    //        if (httpContext.Response.StatusCode == 400)
    //        {
    //            //httpContext.Response
    //            //    response.StatusCode = (int)HttpStatusCode.BadRequest;

    //            // throw new Exception("model is invalid");
    //            _logger.LogInformation("model is invalid");
    //            _logger.LogError("model is invalid - ERROR!!!");
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            
            await _next(httpContext);
            if (httpContext.Response.StatusCode >= 400 && httpContext.Response.StatusCode < 500)
                await httpContext.Response.WriteAsJsonAsync("returned with status code between 400 and 500");
        }
        catch (Exception ex)
        {
            var response = httpContext.Response;
            response.ContentType = "application/json";
            _logger.LogError(ex.Message);
            switch (ex)
            {
                case ArgumentNullException e:
                    await response.WriteAsync($"Oppps... \n the argument {e.Message} is null!");
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException e:
                    await response.WriteAsync("Oppps... \n Page Not Found!");
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    await response.WriteAsync("Oppps... \n we are trying to fix the problem!");
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
        }
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class ErrorHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}
