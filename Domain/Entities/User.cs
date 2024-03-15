using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

public class User : IdentityUser<long>, IEntity
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
}