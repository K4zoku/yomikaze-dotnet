using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.Domain.Entities;

[Table("genres")]
[DataContract(Name = "genre")]
[Index(nameof(Name), IsUnique = true)]
public class Genre : BaseEntity
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

    public Genre() { }

    public Genre(Action<object, string>? lazyLoader)
    {
        LazyLoader = lazyLoader;
    }

    #endregion

}