using Yomikaze.Domain.Database.Entities;

namespace Yomikaze.Application.Data.Models.Common;

public class PageModel
{
    public int Index { get; set; }

    public short Server { get; set; }

    public string Image { get; set; } = default!;

    public static explicit operator PageModel(Page page)
    {
        return new()
        {
            Index = page.Index,
            Server = page.Server,
            Image = page.Image
        };
    }

}
