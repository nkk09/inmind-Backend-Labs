using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using lab1_nour_kassem.Services;

namespace lab1_nour_kassem.Controllers;

[Route("api/date")]
[ApiController]
public class DateController : ControllerBase
{
    private readonly DateService _dateService;
    /*[HttpGet]
    public ActionResult<DateTime> GetCurrentDate()
    {
        return Ok(DateOnly.FromDateTime(DateTime.Now));

    }*/
    public DateController(DateService dateService)
    {
        _dateService = dateService;
    }

    [HttpGet]
    public IActionResult GetFormattedDate()
    {
        var language = Request.Headers["Accept-Language"].ToString();
        
        var formattedDate = _dateService.getFormattedDate(language);
        return Ok(new { date = formattedDate });
    }
}