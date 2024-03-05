using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net;
using Yomikaze.Application.Data.Models.Request;
using Yomikaze.Application.Data.Models.Response;
using Yomikaze.Domain.Constants;
using Yomikaze.ImageApi.Services;


namespace Yomikaze.ImageApi.Controllers;


[ApiController]
[Route("[controller]")]
public class ImagesController : ControllerBase
{

    private readonly ImageUploadService _imageUploadService;

    public ImagesController(ImageUploadService imageUploadService)
    {
        _imageUploadService = imageUploadService;
    }

    [HttpPost]
    [Route($"Upload")]
    public async Task<ActionResult<ResponseModel<ImageUploadResponse>>> UploadImageAsync([FromForm] ImageUploadModel request)
    {
        var path = await _imageUploadService.UploadImageAsync(request.File);
        var fileName = Path.GetFileName(path);
        var controllerName = ControllerContext.ActionDescriptor.ControllerName;
        var url = Url.Action(nameof(GetImage), controllerName, new { fileName }, Request.Scheme);
        if (url == null) return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    ResponseModel.CreateError("Image uploaded successfully but failed to generate URL")
                );
        var Uri = new Uri(url);
        url = Uri.PathAndQuery;
        return Ok(ResponseModel.CreateSuccess(new ImageUploadResponse { Url = url }));
    }

    [HttpGet]
    [Route("{fileName}")]
    public IActionResult GetImage(string fileName)
    {
        var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
        var filePath = Path.Combine(dir, fileName);
        if (!System.IO.File.Exists(filePath)) return NotFound();
        var file = System.IO.File.OpenRead(filePath);
        var mime = MimeTypes.GetMimeType(fileName);
        if (mime == null || !Array.Exists(Api.AllowedImageTypes, t => t.Contains(mime))) return NotFound();
        return File(file, mime);
    }
}
