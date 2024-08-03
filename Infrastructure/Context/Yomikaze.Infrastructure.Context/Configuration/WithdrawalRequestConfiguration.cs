using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Infrastructure.Context.Configuration;

public class WithdrawalRequestConfiguration : BaseEntityConfiguration<WithdrawalRequest>
{
    public override void Configure(EntityTypeBuilder<WithdrawalRequest> builder)
    {
        base.Configure(builder);
        builder
            .Navigation(e => e.User)
            .AutoInclude();
        builder
            .HasOne<User>(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId);
    }
}