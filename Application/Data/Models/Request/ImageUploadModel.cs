using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Yomikaze.Domain.Helpers.Attributes;

namespace Yomikaze.Application.Data.Models.Request;

public class ImageUploadModel
{
    [Required]
    [DataType(DataType.Upload)]
    [AllowedContentTypes([MediaTypeNames.Image.Png, MediaTypeNames.Image.Jpeg, MediaTypeNames.Image.Webp, MediaTypeNames.Image.Svg, MediaTypeNames.Image.Gif])]

    public IFormFile File { get; set; } = default!;
}