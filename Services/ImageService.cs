namespace lab1_nour_kassem.Services;

public class ImageService
{
    //I want to limit this to images only, so we are going to set the allowed extensions:
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp"];
    

    public async Task<string>  upload_image(IFormFile file)
    {
        if (file.Length == 0)
            throw new ArgumentNullException(nameof(file));
        
        var extension = Path.GetExtension(file.FileName).ToLower();
        if (!AllowedExtensions.Contains(extension))
            throw new ArgumentException("Invalid file extension. Please choose an image.");
        
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
        return filePath;
    }
}