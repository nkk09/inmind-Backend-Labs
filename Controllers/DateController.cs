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
        
        try
        {
            var formattedDate = _dateService.getFormattedDate(language);
            return Ok(new { date = formattedDate });
        }
        catch (CultureNotFoundException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
        }
        finally
        {
            Console.WriteLine($"Request processed for language: {language}");
            //I don't really get why we would want that here?
            //oh nvm was testing if my isvalid is correct and finally found it useful heh :)
            //I was getting invalid language, even though i was leaving it empty and i discovered thanks to finally
            //that the langague was actually the full en-Us, with a bunch of q's..
            //thats why im gonna split it and remove everything and just take the first language
            
        }
    }
}