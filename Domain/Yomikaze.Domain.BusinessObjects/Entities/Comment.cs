using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.Domain.Entities;

[Table("comments")]
[DataContract(Name = "comment")]
public class Comment : BaseEntity
{
    #region Fields

    private Comic _comic = default!;
    private Chapter? _chapter;
    private Comment? _replyTo;
    private ICollection<Comment> _replies = [];

    #endregion

    #region Properties

    private Action<object, string>? LazyLoader { get; set; }

    [StringLength(256)]
    [DataMember(Name = "content")]
    [Column("content", Order = 1)]
    public string Content { get; set; } = default!;

    [StringLength(20)]
    [DataMember(Name = "userId")]
    [Column("user_id", Order = 2)]
    public string UserId { get; set; } = default!;

    [StringLength(20)] 
    [ForeignKey(nameof(Comic))]
    [DataMember(Name = "comicId")]
    [Column("comic_id", Order = 3)]
    public string ComicId { get; set; } = default!;
    
    [DeleteBehavior(DeleteBehavior.ClientCascade)]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public Comic Comic { 
        get => LazyLoader.Load(this, ref _comic);
        set => _comic = value;
    }
    
    [ForeignKey(nameof(Chapter))]
    [StringLength(20)]
    [DataMember(Name = "chapterId")]
    [Column("chapter_id", Order = 4)]
    public string? ChapterId { get; set; }
    
    
    [DeleteBehavior(DeleteBehavior.Cascade)]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public Chapter? Chapter { 
        get => LazyLoader.LoadNullable(this, ref _chapter);
        set => _chapter = value;
    }
    
    [ForeignKey(nameof(ReplyTo))]
    [StringLength(20)]
    [DataMember(Name = "replyToId")]
    [Column("reply_to_id", Order = 5)]
    public string? ReplyToId { get; set; }
    
    [DataMember(Name = "replyTo")]
    public Comment? ReplyTo { 
        get => LazyLoader.LoadNullable(this, ref _replyTo);
        set => _replyTo = value;
    }

    [DataMember(Name = "replies")]
    [InverseProperty(nameof(ReplyTo))] 
    public ICollection<Comment> Replies { 
        get => LazyLoader.Load(this, ref _replies);
        set => _replies = value;
    }
    
    #endregion
    
    #region Constructors
    
    public Comment()
    {
    }
    
    public Comment(Action<object, string> lazyLoader)
    {
        LazyLoader = lazyLoader;
    }
    
    #endregion

}