using Yomikaze.Domain.Database.Entities;

namespace Yomikaze.Application.Data.Models.Common;
public class LibraryEntryModel
{
    public long Id { get; set; }
    public UserModel User { get; set; }
    public ComicModel Comic { get; set; }

    public LibraryEntryModel(LibraryEntry libraryEntry)
    {
        Id = libraryEntry.Id;
        User = libraryEntry.User.ToModel();
        Comic = libraryEntry.Comic.ToModel();
    }
}
