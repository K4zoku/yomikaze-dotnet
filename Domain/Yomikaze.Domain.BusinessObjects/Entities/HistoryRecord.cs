namespace Yomikaze.Domain.Entities;

[Table("history_records")]
[DataContract(Name = "historyRecord")]
[Index(nameof(ChapterId), nameof(UserId), IsUnique = true)]
public class HistoryRecord : BaseEntity
{
    #region Fields

    private Chapter _chapter = default!;
    private UserProfile _user = default!;

    #endregion
    
    #region Properties
    
    private Action<object, string>? LazyLoader { get; set; }
    
    [ForeignKey(nameof(Chapter))]
    [DataMember(Name = "chapterId")]
    [Column("chapter_id", Order = 1)]
    public ulong ChapterId { get; set; }

    [DataMember(Name = "chapter")]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Chapter Chapter { 
        get => LazyLoader.Load(this, ref _chapter);
        set => _chapter = value;
    }
    
    [DataMember(Name = "userId")]   
    [Column("user_id", Order = 2)]
    [ForeignKey(nameof(User))]
    public ulong UserId { get; set; }
    
    
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public UserProfile User { 
        get => LazyLoader.Load(this, ref _user);
        set => _user = value;
    }

    [DataMember(Name = "views")]
    [Column("views", Order = 3)]
    public long Views { get; set; } = 1;

    #endregion
    
    #region Constructors

    public HistoryRecord()
    {
        LastModified = DateTime.UtcNow;
    }
    
    public HistoryRecord(Action<object, string>? lazyLoader)
    {
        LazyLoader = lazyLoader;
        LastModified = DateTime.UtcNow;
    }
    
    #endregion
}