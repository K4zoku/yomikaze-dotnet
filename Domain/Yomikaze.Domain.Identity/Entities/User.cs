namespace Yomikaze.Domain.Identity.Entities;

[PrimaryKey(nameof(Id))]
public sealed class User : IdentityUser<ulong>, IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Key]
    public override ulong Id { get; set; }
    
    [StringLength(255)]
    public string? Avatar { get; set; }

    [StringLength(255)]
    public string? Banner { get; set; }

    [StringLength(255)]
    public string? Bio { get; set; }

    [StringLength(128)]
    public string Name { get; set; } = default!;
    
    public DateTimeOffset? Birthday { get; set; }
    
    public long Balance { get; set; }
    
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