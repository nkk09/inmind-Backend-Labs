namespace lab1_nour_kassem.Controllers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]



public class ImageController : ControllerBase
{
    //I want to limit this to images only, so we are going to set the allowed extensions:
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp"];
    
    [HttpPost("upload-image")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        
        if (file.Length == 0)
        {
            return BadRequest(new { message = "No file uploaded." });
        }
        
        var extension = Path.GetExtension(file.FileName).ToLower();
        if (!AllowedExtensions.Contains(extension))
        {
            return BadRequest(new { message = "Invalid file extension. Please choose an image." });
        }
        
        
        var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        if (!Directory.Exists(uploadsDirectory))
        {
            Directory.CreateDirectory(uploadsDirectory);
        }

        var filePath = Path.Combine(uploadsDirectory, file.FileName);

        await using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return Ok(new { message = "File uploaded successfully.", filePath });
    }
}
