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

    [SwaggerParameter("The page number to retrieve.")]
    public int Page { get; set; }

    [SwaggerParameter("The number of items per page.")]
    public int Size { get; set; }
}