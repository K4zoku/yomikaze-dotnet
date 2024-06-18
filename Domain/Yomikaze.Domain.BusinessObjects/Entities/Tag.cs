namespace Yomikaze.Domain.Entities;

[Table("Tags")]
[DataContract(Name = "Tag")]
[Index(nameof(Name), IsUnique = true)]
public class Tag : BaseEntity
{
    #region Fields

    private ICollection<Comic> _comics = [];

    #endregion

    #region Properties

    private Action<object, string>? LazyLoader { get; set; }
    
    [StringLength(64)]
    [DataMember(Name = "name")]
    [Column("name", Order = 1)]
    public string Name { get; set; } = default!;
    
    [StringLength(512)]
    [DataMember(Name = "description")]
    [Column("description", Order = 2)]
    public string? Description { get; set; }

    public ICollection<Comic> Comics
    {
        get => LazyLoader.Load(this, ref _comics);
        set => _comics = value;
    }
    
    #endregion

    #region Constructors

    public Tag() { }

    public Tag(Action<object, string>? lazyLoader)
    {
        LazyLoader = lazyLoader;
    }

    #endregion

}