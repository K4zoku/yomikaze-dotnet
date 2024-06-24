using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.Domain.Entities;

public sealed class Comic : BaseEntity
{

    #region Properties
    
    [NotMapped] public override int WorkerId => HashCode.Combine(GetType());

    [StringLength(256)] public string Name { get; set; } = default!;

    public string[] Aliases { get; set; } = [];

    [StringLength(512)] public string? Description { get; set; }

    [StringLength(512)] public string? Cover { get; set; }

    [StringLength(512)] public string? Banner { get; set; }

    public DateTimeOffset? PublicationDate { get; set; }

    public ICollection<Tag> Tags { get; set; }

    public ICollection<ComicTag> ComicTags { get; set; }
    
    public ICollection<Chapter> Chapters { get; set; }
    
    [NotMapped]
    public int TotalChapters => Chapters.Count;
    
    [NotMapped]
    public int TotalViews => Chapters.Sum(chapter => chapter.Views);
    
    public ICollection<ComicRating> Ratings { get; set; }
    
    [NotMapped]
    public int TotalRatings => Ratings.Count;
    
    [NotMapped]
    public double AverageRating => TotalRatings > 0 ? Ratings.Average(rating => rating.Rating) : 0;
    
    public ICollection<LibraryEntry> Follows { get; set; }
    
    [NotMapped]
    public int TotalFollows => Follows.Count;
    
    public ICollection<ComicComment> Comments { get; set; }
    
    [NotMapped]
    public int TotalComments => Comments.Count;

    public string[] Authors { get; set; } = [];

    [ForeignKey(nameof(Publisher))] public ulong? PublisherId { get; set; }

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