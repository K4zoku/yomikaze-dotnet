namespace Yomikaze.Domain.Entities;

public class Report : BaseEntity
{
    #region Fields

    private ReportCategory _category = default!;
    
    private User _reporter = default!;

    #endregion

    #region Properties
    
    protected Action<object, string>? LazyLoader { get; }

    [ForeignKey(nameof(Category))]
    public ulong CategoryId { get; set; }
    
    public ReportCategory Category { 
        get => LazyLoader.Load(this, ref _category);
        set => _category = value;
    }
    
    [ForeignKey(nameof(Reporter))]
    public ulong ReporterId { get; set; }
    
    public User Reporter { 
        get => LazyLoader.Load(this, ref _reporter);
        set => _reporter = value;
    }
    
    public string? Description { get; set; } = default!;
    
    public string[]? Images { get; set; } = default!;
    
    public string? DismissalReason { get; set; }
    
    public ReportStatus Status { get; set; }

    #endregion

    #region Constructors

    protected Report()
    {
    }
    
    protected Report(Action<object, string> lazyLoader)
    {
        LazyLoader = lazyLoader;
    }

    #endregion
    
}

public enum ReportStatus
{
    Pending = 0,
    Resolved = 1,
    Dismissed = 2
}