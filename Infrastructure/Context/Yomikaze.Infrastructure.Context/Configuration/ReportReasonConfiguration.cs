using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Infrastructure.Context.Configuration;

public class ReportReasonConfiguration : BaseEntityConfiguration<ReportReason>
{
    public override void Configure(EntityTypeBuilder<ReportReason> builder)
    {
        base.Configure(builder);
        builder
            .HasDiscriminator<string>("type")
            .IsComplete(false)
            .HasValue<ReportReason>("base")
            .HasValue<ChapterReportReason>("chapter")
            .HasValue<ComicReportReason>("comic")
            .HasValue<ProfileReportReason>("profile")
            .HasValue<TranslationReportReason>("translation");
    }
}