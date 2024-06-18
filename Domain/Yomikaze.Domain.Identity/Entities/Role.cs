using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.Domain.Identity.Entities;

[PrimaryKey(nameof(Id))]
public sealed class Role : IdentityRole<ulong>, IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Key]
    public override ulong Id { get; set; }

    [NotMapped]
    public int WorkerId => 10;

    public Role(string name) : base(name)
    {
        Id = SnowflakeGenerator.Generate(WorkerId);
        NormalizedName = name.ToUpperInvariant();
        ConcurrencyStamp = Guid.NewGuid().ToString();
    }
}