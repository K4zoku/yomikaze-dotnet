namespace Yomikaze.API.Main.Base;

public class PagedList<T>
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int RowCount { get; set; }
    public int PageCount { get; set; }
    public IEnumerable<T> Results { get; set; } = [];
}