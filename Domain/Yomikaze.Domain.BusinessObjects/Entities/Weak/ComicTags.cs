namespace Yomikaze.Domain.Entities.Weak;

[Table("comic_genre")]
[DataContract(Name = "comicGenre")]
[PrimaryKey(nameof(ComicId), nameof(GenreId))]
public class ComicTags
{
    [DataMember(Name = "comicId")]
    [Column("comic_id", Order = 1)]
    public ulong ComicId { get; set; }
    
    [DataMember(Name = "genreId")]
    [Column("genre_id", Order = 2)]
    public ulong GenreId { get; set; }
}