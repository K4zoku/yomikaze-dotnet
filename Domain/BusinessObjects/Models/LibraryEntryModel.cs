﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Yomikaze.Domain.Models;

public class LibraryEntryInputModel
{
    [Required] public long UserId { get; set; }

    [Required] public long ComicId { get; set; }

    public DateTimeOffset DateAdded { get; set; } = DateTimeOffset.Now;
}

public class LibraryEntryOutputModel
{
    public long Id { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ComicOutputModel Comic { get; set; } = default!;

    public DateTimeOffset DateAdded { get; set; }
}