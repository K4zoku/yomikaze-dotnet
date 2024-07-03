namespace Yomikaze.Domain.Models;

public class LibrarySearchModel
{
    public string? Category { get; set; }
    
    public string? Name { get; set; }

    public LibraryOrderBy[] OrderBy { get; set; } = [];
}

public enum LibraryOrderBy
{
    Name,
    NameDesc,
    CreationTime,
    CreationTimeDesc,
}