namespace lab1_nour_kassem.Filters;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public class LoggingActionFilter : IActionFilter
{
    private readonly ILogger<LoggingActionFilter> _logger;

    public LoggingActionFilter(ILogger<LoggingActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var actionName = context.ActionDescriptor.DisplayName;
        var parameters = JsonSerializer.Serialize(context.ActionArguments);

        var startime = DateTime.Now;
        
        _logger.LogInformation($"Started executing {actionName} at {startime} with parameters: {parameters}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var actionName = context.ActionDescriptor.DisplayName;
        var response = context.Result as ObjectResult; 
            //without specifying as objectresult, we cant receive the response as an http response and we cant access
            //any part of it
        
        var endtime = DateTime.Now;
        
        _logger.LogInformation($"Finished executing {actionName} at {endtime}" +
                               $" with response: {response.StatusCode}" +
                               $" {JsonSerializer.Serialize(response.Value)}");
    }
}
