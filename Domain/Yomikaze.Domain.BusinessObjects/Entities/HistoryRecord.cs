using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.Domain.Entities;

[Table("history_records")]
[DataContract(Name = "historyRecord")]
public class HistoryRecord : BaseEntity
{
    #region Fields

    private Chapter _chapter = default!;

    #endregion
    
    #region Properties
    
    private Action<object, string>? LazyLoader { get; set; }
    
    [StringLength(20)] 
    [ForeignKey(nameof(Chapter))]
    [DataMember(Name = "chapterId")]
    [Column("chapter_id", Order = 1)]
    public string ChapterId { get; set; } = default!;

    [DataMember(Name = "chapter")]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Chapter Chapter { 
        get => LazyLoader.Load(this, ref _chapter);
        set => _chapter = value;
    }

    [StringLength(20)]
    [DataMember(Name = "userId")]   
    [Column("user_id", Order = 2)]
    public string UserId { get; set; } = default!;
    
    #endregion
    
    #region Constructors
    
    public HistoryRecord() { }
    
    public HistoryRecord(Action<object, string>? lazyLoader)
    {
        LazyLoader = lazyLoader;
    }
    
    #endregion
}