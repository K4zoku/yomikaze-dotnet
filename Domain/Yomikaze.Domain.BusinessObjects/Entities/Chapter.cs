using EntityFrameworkCore.Projectables;
using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.Domain.Entities;

public class Chapter : BaseEntity
{

    #region Properties

    private Action<object, string>? LazyLoader { get; }

    public int Number { get; set; }

    [StringLength(256)] public string Name { get; set; } = default!;

    [StringLength(20)]
    [ForeignKey(nameof(Comic))]
    public ulong ComicId { get; set; }

    public int Views { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Comic Comic { get; set; } = default!;

    [InverseProperty(nameof(Page.Chapter))]
    public IList<Page> Pages { get; set; } = new List<Page>();

    public ICollection<ChapterComment> Comments { get; set; } = new List<ChapterComment>();

    public int Price { get; set; } = 0;
    
    [Projectable]
    public bool HasLock => Price > 0;
    
    public ICollection<UnlockedChapter> Unlocked { get; set; } = new List<UnlockedChapter>();
    
    [NotMapped]
    [Projectable]
    public int TotalComments => Comments.Count;

    #endregion

    #region Constructor

    public Chapter()
    {
    }

    public Chapter(Action<object, string> lazyLoader)
    {
        LazyLoader = lazyLoader;
    }

    #endregion
}