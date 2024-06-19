namespace Yomikaze.Domain.Models;

public class HistoryRecordModel : BaseModel
{
    #region ReadWriteProperties

    public int PageNumber { get; set; } = 1;

    #endregion

    #region ReadOnlyProperties

    [SwaggerSchema(ReadOnly = true)] public ChapterModel Chapter { get; set; } = default!;

    [SwaggerSchema(ReadOnly = true)] public ProfileModel User { get; set; } = default!;

    #endregion

    #region WriteOnlyProperties

    [Required]
    [SwaggerSchema(WriteOnly = true)]
    public string? ChapterId { get; set; }

    [Required]
    [SwaggerSchema(WriteOnly = true)]
    public string? UserId { get; set; }

    #endregion
}