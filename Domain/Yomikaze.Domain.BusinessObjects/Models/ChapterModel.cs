﻿namespace Yomikaze.Domain.Models;

public class ChapterModel : BaseModel
{
    #region ReadOnlyProperties

    [SwaggerSchema(ReadOnly = true)]
    public int? Views { get; set; }

    #endregion

    #region WriteOnlyProperties

    [WriteOnly]
    public string? ComicId { get; set; }

    #endregion

    #region CommonProperties

    [Required] public int? Number { get; set; }

    [Required]
    [Length(0, 50, ErrorMessage = "Chapter name must from 0 to 50 characters")]
    public string? Name { get; set; }

    [MinLength(1, ErrorMessage = "Chapter must have at least 1 page")]
    public IList<string>? Pages { get; set; } = new List<string>();

    #endregion
}