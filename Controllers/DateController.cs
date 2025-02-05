using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using lab1_nour_kassem.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab1_nour_kassem.Controllers;

[Route("api/date")]
[ApiController]
public class DateController : ControllerBase
{
    /*[HttpGet]
    public ActionResult<DateTime> GetCurrentDate()
    {
        return Ok(DateOnly.FromDateTime(DateTime.Now));

    }*/

    [HttpGet]
    public IActionResult GetFormattedDate()
    {
        var language = Request.Headers["Accept-Language"].ToString();

        if (string.IsNullOrEmpty(language) || language == "*")
        {
            language = "en-US"; // Default language
        }
        
        
        //however if i leave it empty, it autofills with a bunch of languages and q's and so my code will
        //think it is ivalid. so we try to split and take the first language here:
        else
        {
            language = language.Split(',')[0].Trim();
        }

        try
        {
            bool isValid = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .Any(c => c.Name.Equals(language, StringComparison.Ordinal));
            //OH SO THAT'S HOW YOU MAKE IT CASE INSENSITIVE! nice :)

            if (!isValid)
            {
                throw new CultureNotFoundException($"Invalid language code");
            }

            var culture = new CultureInfo(language);
            var formattedDate = DateTime.Now.ToString("D", culture);

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