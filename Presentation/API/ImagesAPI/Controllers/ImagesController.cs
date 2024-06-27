using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
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
    public async Task<IActionResult> UploadImageAsync([FromForm] ImageUploadModel request,
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        IFormFile file = request.File;
        string ext = GetExtension(file.FileName);
        string fileName = SnowflakeGenerator.Generate(30) + ext;
        string filePath = FileProvider.Root;
        if (request.ComicId != null)
        {
            filePath = Combine(filePath, "Comics", request.ComicId.Value.ToString());
            if (request.ChapterIndex != null)
            {
                filePath = Combine(filePath, "Chapters", request.ChapterIndex.Value.ToString());
            }
        }
        else if (request.UserId != null)
        {
            filePath = Combine(filePath, "Users", request.UserId.Value.ToString());
        }
        else
        {
            filePath = Combine(filePath, "Misc");
        }

        Directory.CreateDirectory(filePath);
        filePath = Combine(filePath, fileName);

        // Save file to disk
        await using FileStream stream = new(filePath, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);

        try
        {
            using var image = await Image.LoadAsync(filePath, cancellationToken);
            await image.SaveAsWebpAsync(ChangeExtension(filePath, "webp"), cancellationToken);
        }
        catch (Exception exception) when(exception is NotSupportedException or InvalidImageContentException)
        {
            Delete(filePath);
            return BadRequest();
        } 
        catch (Exception exception)
        {
            Delete(filePath);
            return StatusCode(500, exception.Message);
        }

        // Generate URL
        string url = Url.Content($"~/Images/{GetRelativePath(FileProvider.Root, filePath)}");
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