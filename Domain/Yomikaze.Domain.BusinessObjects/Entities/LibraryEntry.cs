using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.Domain.Entities;

[Table("library_entries")]
[DataContract(Name = "libraryEntry")]
public class LibraryEntry : BaseEntity
{
    #region Fields
    
    private Comic _comic = default!;
    
    #endregion
    
    #region Properties
    
    private Action<object, string>? LazyLoader { get; set; }
    
    [ForeignKey(nameof(Comic))] 
    [StringLength(20)]
    [DataMember(Name = "comicId", Order = 1)]
    [Column("comic_id", Order = 1)]
    public string ComicId { get; set; } = default!;

    [DataMember(Name = "comic")]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Comic Comic { 
        get => LazyLoader.Load(this, ref _comic);
        set => _comic = value;
    }

    [StringLength(20)]
    [DataMember(Name = "userId", Order = 2)]
    [Column("user_id", Order = 2)]
    public string UserId { get; set; } = default!;
    
    #endregion
    
    #region Constructors
    
    public LibraryEntry() { }
    
    public LibraryEntry(Action<object, string>? lazyLoader)
    {
        LazyLoader = lazyLoader;
    }
    
    #endregion
}