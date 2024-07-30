using Yomikaze.Domain.Entities;

namespace Yomikaze.Domain.Models;

public class ComicSearchModel
{
    
    private string? _name;
    private string? _publisher;

    public string? Name
    {
        get => _name;
        set => _name = value?.Trim().ToLower();
    }
    
    public string[]? Authors { get; set; }
    
    public string? Publisher { get => _publisher; set => _publisher = value?.Trim().ToLower(); }
    
    [SwaggerIgnore]
    [SwaggerSchema(ReadOnly = true)]
    public ulong? PublisherId { get; set; }
    
    public ComicStatus? Status { get; set; }
    
    public DateTimeOffset? FromPublicationDate { get; set; }
    
    public DateTimeOffset? ToPublicationDate { get; set; }
    
    public int? FromTotalChapters { get; set; }
    
    public int? ToTotalChapters { get; set; }
    
    public int? FromTotalViews { get; set; }
    
    public int? ToTotalViews { get; set; }
    
    public DateTimeOffset? ViewsByDateTo { get; set; }
    
    public DateTimeOffset? ViewsByDateFrom { get; set; }
    
    public double? FromAverageRating { get; set; }
    
    public double? ToAverageRating { get; set; }
    
    public int? FromTotalFollows { get; set; }
    
    public int? ToTotalFollows { get; set; }

    public ISet<string> IncludeTags { get; set; } = new HashSet<string>();

    public ISet<string> ExcludeTags { get; set; } = new HashSet<string>();

    public LogicalOperator InclusionMode { get; set; } = LogicalOperator.Or;

    public LogicalOperator ExclusionMode { get; set; } = LogicalOperator.And;
    
    public ComicOrderBy[]? OrderBy { get; set; }
}

public enum ComicOrderBy
{
    Name,
    NameDesc,
    PublicationDate,
    PublicationDateDesc,
    CreationTime,
    CreationTimeDesc,
    LastModified,
    LastModifiedDesc,
    TotalChapters,
    TotalChaptersDesc,
    TotalViews,
    TotalViewsDesc,
    AverageRating,
    AverageRatingDesc,
    TotalRatings,
    TotalRatingsDesc,
    TotalFollows,
    TotalFollowsDesc,
    TotalComments,
    TotalCommentsDesc
}

public enum LogicalOperator
{
    And,
    Or
}