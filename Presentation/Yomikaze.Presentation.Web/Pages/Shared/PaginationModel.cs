using Microsoft.AspNetCore.Mvc;

namespace Yomikaze.Presentation.Web.Pages.Shared;

public class PaginationModel
{
    [FromQuery]
    public int Page { get; set; } = 1;
    
    [FromQuery]
    public int Size { get; set; } = 10;
}