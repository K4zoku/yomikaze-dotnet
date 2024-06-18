using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Yomikaze.Application.Data.Attributes;

namespace Yomikaze.API.CDN.Images.Models;

public class ImageUploadModel
{
    [Required]
    [DataType(DataType.Upload)]
    [AllowedContentTypes([
        MediaTypeNames.Image.Png, MediaTypeNames.Image.Jpeg, MediaTypeNames.Image.Webp, MediaTypeNames.Image.Svg,
        MediaTypeNames.Image.Gif
    ])]
    public IFormFile File { get; set; } = default!;
    
    [Required]
    [Range(1, ulong.MaxValue)]
    public ulong ComicId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int ChapterIndex  { get; set; }
}