namespace Yomikaze.Application.Data.Models.Common;


public class PageInputModel
{
    public int Index { get; set; }

    public string Image { get; set; } = default!;
}

public class PageOutputModel
{   
    public long Id { get; set; }

    public int Index { get; set; }

    public string Image { get; set; } = default!;
}