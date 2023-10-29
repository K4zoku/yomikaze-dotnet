using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Yomikaze.WebAPI.Models;
using Yomikaze.WebAPI.Services;

namespace Yomikaze.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly ImageUploadService _imageUploadService;

    public UploadController(ImageUploadService imageUploadService)
    {
        _imageUploadService = imageUploadService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImageAsync(IFormFile file)
    {
        if (file.ContentType != "image/png" && file.ContentType != "image/jpeg") return BadRequest(new UploadResponse { Success = false, Message = "Invalid file type" });
        var path = await _imageUploadService.UploadImageAsync(file);
        var host = $"{Request.Scheme}://{Request.Host}";
        path = host + path;
        return Ok(new UploadResponse { Success = true, Message = "Upload success", Url = path });
    }
}

[Controller]
public class ImageController : Controller
{
    [HttpGet]
    [Route("images/{fileName}")]
    public IActionResult GetImage(string fileName)
    {
        var dir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        var filePath = Path.Combine(dir, fileName);
        if (!System.IO.File.Exists(filePath)) return NotFound();
        var file = System.IO.File.OpenRead(filePath);
        var mime = MimeTypes.GetMimeType(fileName);
        if (mime == null || mime != "image/png" && mime != "image/jpeg") return NotFound();
        return File(file, mime);
    }
}
