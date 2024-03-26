using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Yomikaze.Domain.Entities.Weak;

[Table("comic_genre")]
[DataContract(Name = "comicGenre")]
[PrimaryKey(nameof(ComicId), nameof(GenreId))]
public class ComicGenre
{
    [StringLength(20)]
    [DataMember(Name = "comicId")]
    [Column("comic_id", Order = 1)]
    public string ComicId { get; set; } = default!;
    
    [StringLength(20)]
    [DataMember(Name = "genreId")]
    [Column("genre_id", Order = 2)]
    public string GenreId { get; set; } = default!;
}