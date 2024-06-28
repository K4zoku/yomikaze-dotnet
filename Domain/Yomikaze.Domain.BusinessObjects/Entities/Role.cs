namespace Yomikaze.Domain.Entities;

[PrimaryKey(nameof(Id))]
public sealed class Role : IdentityRole<ulong>, IEntity
{
    public Role(string name) : base(name)
    {
        Id = SnowflakeGenerator.Generate(WorkerId);
        NormalizedName = name.ToUpperInvariant();
        ConcurrencyStamp = Guid.NewGuid().ToString();
    }

    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Key]
    public override ulong Id { get; set; }

    [NotMapped] public int WorkerId => 10;
}