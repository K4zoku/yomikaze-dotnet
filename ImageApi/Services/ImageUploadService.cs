namespace Yomikaze.ImageApi.Services;

public class ImageUploadService
{
    private string StoragePath { get; }

    public ImageUploadService()
    {
        StoragePath = Path.Combine(Directory.GetCurrentDirectory(), "images");
    }
    
    public ImageUploadService(string storagePath)
    {
        StoragePath = storagePath;
    }
    
    public async Task<string> UploadImageAsync(IFormFile file)
    {
        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        if (!Directory.Exists(StoragePath)) Directory.CreateDirectory(StoragePath);
        string filePath = Path.Combine(StoragePath, fileName);

        await using FileStream stream = new(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
        return "/images/" + fileName;
    }
}
