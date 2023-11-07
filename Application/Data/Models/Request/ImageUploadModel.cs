using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Yomikaze.Application.Data.Attributes;

namespace Yomikaze.Application.Data.Models.Request;

public class ImageUploadModel
{
    [Required]
    [DataType(DataType.Upload)]
    [AllowedContentTypes(new[] { "image/png", "image/jpeg", "image/webp" })]

    public IFormFile File { get; set; } = default!;
}
