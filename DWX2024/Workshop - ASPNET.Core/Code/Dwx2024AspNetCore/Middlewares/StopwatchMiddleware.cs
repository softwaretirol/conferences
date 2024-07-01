using System.Diagnostics;

namespace Dwx2024AspNetCore.Middlewares;

public class StopwatchMiddleware 
{
    private readonly RequestDelegate _nextMiddleware;

    public StopwatchMiddleware(RequestDelegate nextMiddleware)
    {
        _nextMiddleware = nextMiddleware;
    }

    public async Task InvokeAsync(HttpContext context, 
        ILogger<StopwatchMiddleware> logger,
        IConfiguration configuration)
    {
        var watch = Stopwatch.StartNew();
        // context.Response.StatusCode = 200;
        // await context.Response.WriteAsJsonAsync("Hallo");
        await _nextMiddleware(context);
        logger.LogInformation($"Duration {watch.Elapsed}");
    }
}