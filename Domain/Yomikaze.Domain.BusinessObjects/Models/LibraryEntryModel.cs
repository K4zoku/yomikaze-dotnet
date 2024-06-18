using System.Text.Json.Serialization;

namespace Yomikaze.Domain.Models;

public class LibraryEntryInputModel
{
    [Required] public string UserId { get; set; }

    [Required] public string ComicId { get; set; }
}

public class LibraryEntryOutputModel
{
    public string Id { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ComicOutputModel Comic { get; set; } = default!;

    public DateTimeOffset CreatedAt { get; set; }
}