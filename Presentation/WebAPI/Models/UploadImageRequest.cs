using System.ComponentModel.DataAnnotations;
using Yomikaze.WebAPI.Helpers.Attributes;

namespace Yomikaze.WebAPI.Models;

public class UploadImageRequest
{
    [Required]
    [DataType(DataType.Upload)]
    [AllowedContentTypes(new[] { "image/png", "image/jpeg", "image/webp" })]

    public IFormFile File { get; set; } = default!;
}
