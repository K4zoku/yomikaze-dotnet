namespace Yomikaze.Domain.Entities.Weak;

[Index(nameof(ComicId), nameof(TagId), IsUnique = true)]
public class ComicTag
{
    [ForeignKey(nameof(Comic))] public ulong ComicId { get; set; }

    [ForeignKey(nameof(Tag))] public ulong TagId { get; set; }
}