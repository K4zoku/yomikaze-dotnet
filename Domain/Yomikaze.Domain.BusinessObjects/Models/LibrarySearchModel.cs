namespace Yomikaze.Domain.Models;

public class LibrarySearchModel
{
    [SwaggerIgnore] public ulong? CategoryId { get; set; }

    [SwaggerIgnore] public bool? HasNoCategory { get; set; }

    public string? Name { get; set; }

    public LibraryOrderBy[] OrderBy { get; set; } = [];
}

public enum LibraryOrderBy
{
    Name,
    NameDesc,
    CreationTime,
    CreationTimeDesc
}