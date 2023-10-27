namespace Yomikaze.WebAPI.Services;

public class ImageUploadService
{
    public async Task<string> UploadImageAsync(IFormFile file)
    {
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var dir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        var filePath = Path.Combine(dir, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return "/images/" + fileName;
    }
}
