namespace Yomikaze.API.Main.Base;

public class PagedList<T>
{
    public int CurrentPage { get; init; } = 1;
    public int PageSize { get; init; } = 25;
    public long Totals { get; init; } = 0;
    public int TotalPages { get; init; } = 1;
    public IEnumerable<T> Results { get; init; } = [];
}