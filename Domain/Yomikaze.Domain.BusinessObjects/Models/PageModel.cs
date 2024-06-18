namespace Yomikaze.Domain.Models;

public class PageInputModel
{
    public int Index { get; set; }

    public string Image { get; set; } = default!;
    
    public string ChapterId { get; set; } = default!;
}

public class PageOutputModel
{
    public ulong Id { get; set; }
    public string IdStr { get; set; }

    public int Index { get; set; }

    public string Image { get; set; } = default!;
}