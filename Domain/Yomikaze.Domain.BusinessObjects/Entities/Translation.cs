namespace Yomikaze.Domain.Entities;

public class Translation : BaseEntity
{
    #region Fields
    
    private User _user = default!;
    
    private Page _page = default!;
    
    #endregion
    
    #region Properties
    private Action<object, string>? LazyLoader { get; set; }
    
    [ForeignKey(nameof(User))]
    public ulong UserId { get; set; }
    
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public User User { 
        get => LazyLoader.Load(this, ref _user);
        set => _user = value;
    }
    
    [ForeignKey(nameof(Page))]
    public ulong PageId { get; set; }
    
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Page Page { 
        get => LazyLoader.Load(this, ref _page);
        set => _page = value;
    }
    
    public int X { get; set; } = default!;
    
    public int Y { get; set; } = default!;
    
    public int Width { get; set; } = default!;
    
    public int Height { get; set; } = default!;
    
    public string Content { get; set; } = default!;
    
    public string Language { get; set; } = default!;
    
    public string Alignment { get; set; } = default!;
    
    #endregion
    
    #region Constructors

    public Translation()
    {
    }
    
    public Translation(Action<object, string>? lazyLoader)
    {
        LazyLoader = lazyLoader;
    }
    
    #endregion
}