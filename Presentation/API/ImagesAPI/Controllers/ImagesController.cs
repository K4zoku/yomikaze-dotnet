using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Models;
using static System.IO.File;
using static System.IO.Path;
using ImageUploadModel = Yomikaze.API.CDN.Images.Models.ImageUploadModel;

namespace Yomikaze.API.CDN.Images.Controllers;

[ApiController]
[Route("API/[controller]")]
public class ImagesController(PhysicalFileProvider fileProvider) : ControllerBase
{
    private PhysicalFileProvider FileProvider => fileProvider;
    
    [HttpPost]
    public async Task<IActionResult> UploadImageAsync([FromForm] ImageUploadModel request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        IFormFile file = request.File;
        string ext = GetExtension(file.FileName);
        string fileName = SnowflakeGenerator.Generate(30) + ext;
        string comicPath = Combine(FileProvider.Root, request.ComicId.ToString());
        string chapterPath = Combine(comicPath, request.ChapterIndex.ToString());
        Directory.CreateDirectory(chapterPath);
        string filePath = Combine(chapterPath, fileName);

        // Save file to disk
        await using FileStream stream = new(filePath, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);

        // Generate URL
        string url = Url.Content($"~/Images/{request.ComicId}/{request.ChapterIndex}/{fileName}");
        return Created(url, new { Url = url });
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
    public ActionResult<ResponseModel> GetStatistics()
    {
        IFileInfo[] files = fileProvider.GetDirectoryContents("").Where(f => !f.IsDirectory).ToArray();
        string[] suffixes = ["B", "KB", "MB", "GB", "TB", "PB", "EB"];
        long bytes = files.Sum(f => f.Length);
        int i = (int)Math.Floor(Math.Log(bytes, 1024));
        double value = bytes / Math.Pow(1024, i);
        string size = $"{value:0.##} {suffixes[i]}";
        return Ok(ResponseModel.CreateSuccess("OK", new { TotalFiles = files.Length, TotalSize = size }));
    }
}