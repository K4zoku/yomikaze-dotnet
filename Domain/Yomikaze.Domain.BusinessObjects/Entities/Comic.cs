using EntityFrameworkCore.Projectables;
using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.Domain.Entities;

public class Comic : BaseEntity
{

    #region Properties
    
    [NotMapped] public override int WorkerId => HashCode.Combine(GetType());

    [StringLength(256)] public string Name { get; set; } = default!;

    public string[] Aliases { get; set; } = [];

    [StringLength(1024)] public string? Description { get; set; }

    [StringLength(512)] public string? Cover { get; set; }

    [StringLength(512)] public string? Banner { get; set; }

    public DateTimeOffset? PublicationDate { get; set; }

    public ISet<Tag> Tags { get; set; } = new HashSet<Tag>();

    public ISet<ComicTag> ComicTags { get; set; } = new HashSet<ComicTag>();
    
    public ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();
    
    [Projectable]
    public int TotalChapters => Chapters.Count;
    
    public ICollection<ComicView> Views { get; set; } = new List<ComicView>();

    [Projectable]
    public int ViewsByDate => Views.Sum(view => view.Views);
    
    [Projectable]
    public int TotalViews => Chapters.Sum(chapter => chapter.Views);
    
    public ICollection<ComicRating> Ratings { get; set; } = new List<ComicRating>();
    
    [Projectable]
    public int TotalRatings => Ratings.Count;
    
    [Projectable]
    public double AverageRating => TotalRatings > 0 ? Ratings.Average(rating => rating.Rating) : 0;
    
    public ICollection<LibraryEntry> Follows { get; set; } = new List<LibraryEntry>();
    
    [Projectable]
    public int TotalFollows => Follows.Count;
    
    public ICollection<ComicComment> Comments { get; set; } = new List<ComicComment>();
    
    [Projectable]
    public int TotalComments => Comments.Count;

    public string[] Authors { get; set; } = [];

    [ForeignKey(nameof(Publisher))] 
    public ulong PublisherId { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public User Publisher { get; set; } = default!;

    public ComicStatus Status { get; set; }

    #endregion
}

public enum ComicStatus
{
    OnGoing = 0,
    Completed = 1,
    Hiatus = 2,
    Cancelled = 3
}