using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.Domain.Entities;

[PrimaryKey(nameof(Id))]
public class User : IdentityUser<ulong>, IEntity
{
    public string? Avatar { get; set; }

    public string? Banner { get; set; }

    public string? Bio { get; set; }

    public string? Fullname { get; set; }

    public DateTimeOffset Birthday { get; set; }

    [InverseProperty(nameof(LibraryEntry.User))]

    public virtual ICollection<LibraryEntry> Library { get; set; } = new List<LibraryEntry>();


    [InverseProperty(nameof(HistoryRecord.User))]

    public virtual ICollection<HistoryRecord> History { get; set; } = new List<HistoryRecord>();

    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Key]
    public override ulong Id { get; set; } = SnowflakeGenerator.Generate(10);
}