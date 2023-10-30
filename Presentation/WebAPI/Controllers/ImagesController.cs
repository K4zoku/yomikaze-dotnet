using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net;
using Yomikaze.Domain.Constants;
using Yomikaze.WebAPI.Models;
using Yomikaze.WebAPI.Services;

namespace Yomikaze.WebAPI.Controllers;


[ApiController]
public class ImagesController : ControllerBase
{

    private readonly ImageUploadService _imageUploadService;

    public ImagesController(ImageUploadService imageUploadService)
    {
        _imageUploadService = imageUploadService;
    }

    [HttpPost]
    [Route($"/API/{Api.Version}/Images/Upload")]
    public async Task<IActionResult> UploadImageAsync([FromForm] UploadImageRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new Response
            {
                Success = false,
                Message = "Invalid request",
                Data = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
            });
        }
        var path = await _imageUploadService.UploadImageAsync(request.File);
        var fileName = Path.GetFileName(path);
        var controllerName = ControllerContext.ActionDescriptor.ControllerName;
        var url = Url.Action(nameof(GetImage), controllerName, new { fileName }, Request.Scheme);
        if (url == null) return StatusCode((int)HttpStatusCode.InternalServerError, new Response
        {
            Success = false,
            Message = "Image uploaded successfully but failed to generate URL"
        });
        return Ok(new UploadResponse
        {
            Success = true,
            Message = "Upload success",
            Url = url
        });
    }

    [HttpGet]
    [Route("/CDN/[controller]/{fileName}")]
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
