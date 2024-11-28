
using System.Diagnostics;

namespace Restaurants.API.Middlewares;

public class TimeLoggingMiddleware(ILogger<TimeLoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopWatch = Stopwatch.StartNew();
        await next.Invoke(context);
        stopWatch.Stop();
        if (stopWatch.Elapsed.TotalSeconds > 4) 
        {
            logger.LogInformation($"Method: {context.Request.Method}, Path: {context.Request.Path}");
        }
    }
}
