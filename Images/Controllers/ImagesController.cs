using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using MimeKit;
using System.Net.Mime;
using Yomikaze.Application.Data.Models.Request;
using Yomikaze.Application.Data.Models.Response;
using static System.IO.File;
using static System.IO.Path;

namespace Yomikaze.API.CDN.Images.Controllers;

[ApiController]
[Route("API/[controller]")]
public class ImagesController(PhysicalFileProvider fileProvider) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ResponseModel>> UploadImageAsync([FromForm] ImageUploadModel request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        IFormFile file = request.File;
        string ext = GetExtension(file.FileName);
        string fileName = Guid.NewGuid() + ext;
        string filePath = Combine(fileProvider.Root, fileName);

        // Save file to disk
        await using FileStream stream = new(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        // Generate URL
        string url = Url.Content($"~/Images/{fileName}");
        return Created(url, ResponseModel.CreateSuccess("Image Uploaded"));
    }

    [HttpDelete("{file}")]
    public IActionResult DeleteImage(string file)
    {
        IFileInfo info = fileProvider.GetFileInfo(file);
        if (!info.Exists || info.PhysicalPath == null)
        {
            return NotFound();
        }

        Delete(info.PhysicalPath);
        return NoContent();
    }
    
    [HttpGet("Statistics")]
    public IActionResult GetStatistics()
    {
        var files = fileProvider.GetDirectoryContents("").Where(f => !f.IsDirectory).ToArray();
        double bytes = files.Sum(f => f.Length);
        int i;
        for (i = 0; bytes >= 1024; i++) bytes /= 1024;
        string[] suffixes = ["B", "KB", "MB", "GB", "TB", "PB", "EB"];
        string size = $"{bytes:0.##} {suffixes[i]}";
        return Ok(new { TotalFiles = files.Length, TotalSize = size });
    }
}