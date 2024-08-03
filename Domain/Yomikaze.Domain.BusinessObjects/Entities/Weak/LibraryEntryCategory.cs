namespace Yomikaze.Domain.Entities.Weak;

[PrimaryKey(nameof(EntryId), nameof(CategoryId))]
public class LibraryEntryCategory
{
    [ForeignKey(nameof(Entry))] public ulong EntryId { get; set; }

    public LibraryEntry Entry { get; set; } = default!;

    [ForeignKey(nameof(Category))] public ulong CategoryId { get; set; }

    public LibraryCategory Category { get; set; } = default!;
}