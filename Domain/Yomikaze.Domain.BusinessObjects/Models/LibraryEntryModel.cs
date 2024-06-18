using System.Text.Json.Serialization;

namespace Yomikaze.Domain.Models;

public class LibraryEntryInputModel
{
    [Required] public string UserId { get; set; }

    [Required] public string ComicId { get; set; }
}

public class LibraryEntryOutputModel
{
    public ulong Id { get; set; }
    public string IdStr { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ComicOutputModel Comic { get; set; } = default!;

    public DateTimeOffset CreatedAt { get; set; }
}