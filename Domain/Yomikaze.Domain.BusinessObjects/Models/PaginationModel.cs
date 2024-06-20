namespace Yomikaze.Domain.Models;

public class PaginationModel
{
    public int Page { get; set; } = 1;

    public int Size { get; set; } = 10;

    public PaginationModel()
    {
    }
    
    public PaginationModel(int page, int pageSize)
    {
        Page = page < 1 ? 1 : page;
        Size = pageSize is < 1 or > 1000 ? 10 : pageSize;
    }
}