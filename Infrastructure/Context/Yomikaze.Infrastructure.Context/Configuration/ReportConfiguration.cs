using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Infrastructure.Context.Configuration;

public class ReportConfiguration : BaseEntityConfiguration<Report>
{
    public override void Configure(EntityTypeBuilder<Report> builder)
    {
        base.Configure(builder);
        
        builder
            .HasDiscriminator<string>("type")
            .IsComplete(false)
            .HasValue<ChapterReport>("chapter_report")
            .HasValue<ComicReport>("comic_report")
            .HasValue<ProfileReport>("profile_report")
            .HasValue<TranslationReport>("translation_report")
            .HasValue<CommentReport>("comment_report")
            .HasValue<ComicCommentReport>("comic_comment_report")
            .HasValue<ChapterCommentReport>("chapter_comment_report");
        
    }
}