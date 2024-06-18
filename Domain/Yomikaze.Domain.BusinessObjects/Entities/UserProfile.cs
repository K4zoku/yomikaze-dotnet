namespace Yomikaze.Domain.Entities;

[Table("profiles")]
[DataContract(Name = "Profile")]
public class UserProfile : BaseEntity
{
    [StringLength(255)]
    public string? Avatar { get; set; }

    [StringLength(255)]
    public string? Banner { get; set; }

    [StringLength(255)]
    public string? Bio { get; set; }

    [StringLength(128)]
    public string Name { get; set; }
    
    public DateTimeOffset? Birthday { get; set; }
}