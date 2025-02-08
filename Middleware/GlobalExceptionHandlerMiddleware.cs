using System.Globalization;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;

//middleware is called on every request, while filters are called based on action. for global exceptions, it would be cleaner
//to use a middleware. it would catch all exceptions from all services, and deal with them instead of having the "dealing"
//in the controllers.

//from my controllers i can get all types of exceptions im using and list them from most specific to most general
//argumentnullexception, culturenotfoundexception, argumentexception, keynotfoundexception

namespace LearnWebAPI.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private Task customHTTPResponse(HttpContext context, Exception ex, HttpStatusCode statusCode)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var errorResponse = new { error = statusCode.ToString(), message = ex.Message };
            return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
        
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ArgumentNullException ex)
            {
                await customHTTPResponse(context, ex, HttpStatusCode.BadRequest);
            }
            catch (CultureNotFoundException ex)
            {
                await customHTTPResponse(context, ex, HttpStatusCode.BadRequest);
            }
            catch (ArgumentException ex)
            {
                await customHTTPResponse(context, ex, HttpStatusCode.BadRequest);
            }
            catch (KeyNotFoundException ex)
            {
                await customHTTPResponse(context, ex, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                await customHTTPResponse(context, ex, HttpStatusCode.InternalServerError);
            }
        }
    }
}