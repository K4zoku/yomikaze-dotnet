using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.Domain.Entities;

[Table("chapters")]
[DataContract(Name = "chapter")]
public class Chapter : BaseEntity
{
    #region Fields

    private Comic _comic = default!;
    private ICollection<Page> _pages = [];

    #endregion
    
    #region Properties

    private Action<object, string>? LazyLoader { get; set; }
    
    [DataMember(Name = "index")]
    [Column("index", Order = 1)]
    public int Index { get; set; }
    
    [StringLength(256)]
    [DataMember(Name = "title")]
    [Column("title", Order = 2)]
    public string Title { get; set; } = default!;
    
    [StringLength(512)]
    [DataMember(Name = "description")]
    [Column("description", Order = 3)]
    public string? Description { get; set; }
    
    [StringLength(20)]
    [ForeignKey(nameof(Comic))]
    [DataMember(Name = "comicId")]
    [Column("comic_id", Order = 4)]
    public string ComicId { get; set; } = default!;
    
    [DeleteBehavior(DeleteBehavior.Cascade)]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public Comic Comic { 
        get => LazyLoader.Load(this, ref _comic);
        set => _comic = value;
    }

    [DataMember(Name = "pages")]
    [InverseProperty(nameof(Page.Chapter))]
    public ICollection<Page> Pages
    {
        get => LazyLoader.Load(this, ref _pages);
        set => _pages = value;
    }

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