using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.Domain.Identity.Entities;

[PrimaryKey(nameof(Id))]
public sealed class User : IdentityUser, IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Key]
    public override string Id { get; set; }

    [StringLength(255)]
    public string? Avatar { get; set; }

    [StringLength(255)]
    public string? Banner { get; set; }

    [StringLength(255)]
    public string? Bio { get; set; }

    [StringLength(48)]
    public string? Fullname { get; set; }

    [PersonalData] 
    public DateTimeOffset? Birthday { get; set; }

    public User()
    {
        Id = SnowflakeGenerator.Generate(10);
        SecurityStamp = Guid.NewGuid().ToString();
    }
    
    public User(string userName) : this()
    {
        UserName = userName;
    }
}