namespace Yomikaze.Domain.Models;

public class SearchModel : PaginationModel
{
    public string OrderBy { get; set; } = string.Empty;
}