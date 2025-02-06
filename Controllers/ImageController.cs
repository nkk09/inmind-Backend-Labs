using lab1_nour_kassem.Services;

namespace lab1_nour_kassem.Controllers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly ImageService _imageService;

    public ImageController(ImageService imageService)
    {
        _imageService = imageService;
    }


    [HttpPost("upload-image")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        try
        {
            var filePath = await _imageService.upload_image(file);
            return Ok(new { message = "File uploaded successfully.", filePath });
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }

        
    }
}