using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Yomikaze.Domain.Entities.Weak;

[Table("comic_author")]
[DataContract(Name = "comicAuthor")]
[PrimaryKey(nameof(ComicId), nameof(AuthorId))]
public class ComicAuthor
{
    [StringLength(20)]
    [DataMember(Name = "comicId")]
    [Column("comic_id", Order = 1)]
    public string ComicId { get; set; } = default!;
    
    [StringLength(20)]
    [DataMember(Name = "authorId")]
    [Column("author_id", Order = 2)]
    public string AuthorId { get; set; } = default!;
}