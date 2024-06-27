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
    
    public ComicStatus? Status { get; set; }
    
    public DateTimeOffset? FromPublicationDate { get; set; }
    
    public DateTimeOffset? ToPublicationDate { get; set; }
    
    public int? FromTotalChapters { get; set; }
    
    public int? ToTotalChapters { get; set; }
    
    public int? FromTotalViews { get; set; }
    
    public int? ToTotalViews { get; set; }
    
    public double? FromAverageRating { get; set; }
    
    public double? ToAverageRating { get; set; }
    
    public int? FromTotalFollows { get; set; }
    
    public int? ToTotalFollows { get; set; }
    
    public string[]? IncludeTags { get; set; }
    
    public string[]? ExcludeTags { get; set; }

    public LogicalOperator? InclusionMode { get; set; }

    public LogicalOperator? ExclusionMode { get; set; }
    
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