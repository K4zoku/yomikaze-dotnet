using Abstracts;
using Microsoft.AspNetCore.Identity;

namespace Yomikaze.Domain.Entities;

public sealed class Role : IdentityRole<long>, IEntity
{
    public Role(string name) : base(name)
    {
        NormalizedName = name.ToUpperInvariant();
    }
}