namespace Yomikaze.Domain.Models;

public class PageInputModel
{
    public int Index { get; set; }

    public string Image { get; set; } = default!;
}

public class PageOutputModel
{
    public string Id { get; set; }

    public int Index { get; set; }

    public string Image { get; set; } = default!;
}