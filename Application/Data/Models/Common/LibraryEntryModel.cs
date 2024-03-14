namespace Yomikaze.Application.Data.Models.Common;

public class LibraryEntryModel
{
    public long Id { get; set; }
    public UserModel User { get; set; }
    public ComicModel Comic { get; set; }
}