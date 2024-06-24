namespace Yomikaze.Domain.Models;

public class PaginationModel
{
    public PaginationModel()
    {
        Page = 1;
        Size = 25;
    }
    
    public PaginationModel(int page, int pageSize)
    {
        Page = page < 1 ? 1 : page;
        Size = pageSize is < 1 or > 1000 ? 25 : pageSize;
    }

    public int Page { get; set; }

    public int Size { get; set; }
}