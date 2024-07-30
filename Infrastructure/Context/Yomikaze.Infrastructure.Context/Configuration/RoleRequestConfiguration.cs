using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Infrastructure.Context.Configuration;

public class RoleRequestConfiguration : BaseEntityConfiguration<RoleRequest>
{
    public override void Configure(EntityTypeBuilder<RoleRequest> builder)
    {
        base.Configure(builder);
        builder
            .Navigation(e => e.User)
            .AutoInclude();
        builder
            .Navigation(e => e.Role)
            .AutoInclude();
    }
}