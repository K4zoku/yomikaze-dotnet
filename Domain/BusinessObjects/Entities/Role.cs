using Microsoft.AspNetCore.Identity;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.Domain.Entities;

public sealed class Role : IdentityRole, IEntity
{
    public Role(string name) : base(name)
    {
        Id = SnowflakeGenerator.Generate(10);
        NormalizedName = name.ToUpperInvariant();
    }
}