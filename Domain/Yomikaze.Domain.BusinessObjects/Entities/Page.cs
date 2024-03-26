using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.Domain.Entities;

[Table("pages")]
[DataContract(Name = "page")]
public class Page : BaseEntity
{
    #region Fields

    private Chapter _chapter = default!;
    
    #endregion
    
    #region Properties
    
    private Action<object, string>? LazyLoader { get; set; }

    [DataMember(Name = "index")]
    [Column("index", Order = 1)]
    public int Index { get; set; }
    
    [StringLength(20)]
    [DataMember(Name = "server")]
    [Column("server", Order = 2)]
    public string? Server { get; set; } = default!;

    [StringLength(512)]
    [DataMember(Name = "image")]
    [Column("image", Order = 3)]
    public string Image { get; set; } = default!;
    
    [StringLength(20)]
    [ForeignKey(nameof(Chapter))]
    [Column("chapter_id", Order = 4)]
    public string ChapterId { get; set; } = default!;
    
    [DeleteBehavior(DeleteBehavior.Cascade)]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public Chapter Chapter { 
        get => LazyLoader.Load(this, ref _chapter);
        set => _chapter = value;
    }

    #endregion
    
    #region Constructors

    public Page()
    {
    }
    
    public Page(Action<object, string>? lazyLoader)
    {
        LazyLoader = lazyLoader;
    }

    #endregion
}