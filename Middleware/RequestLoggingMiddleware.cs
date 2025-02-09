using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace LearnWebAPI.Middlewares;

//want to log request method, path, query params, headers, body, timsetamp, response status code

//timestamp meaning when request was made: datetime.utcnow

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        
//            $"\n Body: {context.Request.Body}");
// turns out body is a stream and it should be read through await. because once its read its lost forever
// so we have to enable the stream buffer thing according to the documentation of .net

        string requestbody = "No body";
        if (context.Request.ContentLength != null)
        {
            context.Request.EnableBuffering();
            using var buffer = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
            requestbody= await buffer.ReadToEndAsync();
            context.Request.Body.Position = 0;
        }

        _logger.LogInformation(
            $"**Request** " +
            $"\n Method: {context.Request.Method} " +
            $"\n Path: {context.Request.Path} " +
            $"\n QueryString: {context.Request.QueryString} " +
            $"\n Headers: {JsonSerializer.Serialize(context.Request.Headers)}" +
            $"\n Body: {requestbody}" +
            $"\n Timestamp: {DateTime.Now}");
        
        await _next(context);
        
        _logger.LogInformation($"**Response** \n StatusCode: {context.Response.StatusCode}");
    }
}