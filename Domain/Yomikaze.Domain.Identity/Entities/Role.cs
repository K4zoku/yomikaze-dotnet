using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.Domain.Identity.Entities;

[PrimaryKey(nameof(Id))]
public sealed class Role : IdentityRole, IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Key]
    public override string Id { get; set; }
    
    public Role(string name) : base(name)
    {
        Id = SnowflakeGenerator.Generate(10);
        NormalizedName = name.ToUpperInvariant();
        ConcurrencyStamp = Guid.NewGuid().ToString();
    }
}