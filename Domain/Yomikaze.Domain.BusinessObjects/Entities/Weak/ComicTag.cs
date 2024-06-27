namespace Yomikaze.Domain.Entities.Weak;

[Index(nameof(ComicId), nameof(TagId), IsUnique = true)]
public class ComicTag
{
    [ForeignKey(nameof(Comic))] public ulong ComicId { get; init; }

    [ForeignKey(nameof(Tag))] public ulong TagId { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj is not ComicTag other)
        {
            return false;
        }

        return ComicId == other.ComicId && TagId == other.TagId;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(ComicId, TagId);
    }
}