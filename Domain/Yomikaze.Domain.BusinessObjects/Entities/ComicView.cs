namespace Yomikaze.Domain.Entities;

[PrimaryKey(nameof(ComicId), nameof(CreationTime))]
public class ComicView
{
    [ForeignKey(nameof(Comic))]
    public ulong ComicId { get; set; }
    
    public DateTimeOffset? CreationTime { get; set; }

    public int Views { get; set; } = 1;
}