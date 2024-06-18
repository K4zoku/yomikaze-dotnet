using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.Domain.Identity.Entities;

[PrimaryKey(nameof(Id))]
public sealed class User : IdentityUser<ulong>, IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Key]
    public override ulong Id { get; set; }
    
    [NotMapped]
    public int WorkerId => 7;

    public User()
    {
        Id = SnowflakeGenerator.Generate(WorkerId);
        SecurityStamp = Guid.NewGuid().ToString();
    }
    
    public User(string userName) : this()
    {
        UserName = userName;
    }
}