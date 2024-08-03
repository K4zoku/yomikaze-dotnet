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

        builder
            .HasOne(r => r.Reason)
            .WithMany()
            .HasForeignKey(r => r.ReasonId);

        builder
            .Navigation(r => r.Reason)
            .AutoInclude();
    }
}

public class ChapterReportConfiguration : BaseEntityConfiguration<ChapterReport>
{
    public override void Configure(EntityTypeBuilder<ChapterReport> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(r => r.Chapter)
            .WithMany()
            .HasForeignKey(r => r.ChapterId);

        builder
            .Navigation(r => r.Chapter)
            .AutoInclude();

        builder
            .Navigation(r => r.Chapter)
            .AutoInclude();
    }
}

public class ComicReportConfiguration : BaseEntityConfiguration<ComicReport>
{
    public override void Configure(EntityTypeBuilder<ComicReport> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(r => r.Comic)
            .WithMany()
            .HasForeignKey(r => r.ComicId);

        builder
            .Navigation(r => r.Comic)
            .AutoInclude();
    }
}

public class ProfileReportConfiguration : BaseEntityConfiguration<ProfileReport>
{
    public override void Configure(EntityTypeBuilder<ProfileReport> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(r => r.Profile)
            .WithMany()
            .HasForeignKey(r => r.ProfileId);

        builder
            .Navigation(r => r.Profile)
            .AutoInclude();
    }
}

public class TranslationReportConfiguration : BaseEntityConfiguration<TranslationReport>
{
    public override void Configure(EntityTypeBuilder<TranslationReport> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(r => r.Translation)
            .WithMany()
            .HasForeignKey(r => r.TranslationId);

        builder
            .Navigation(r => r.Translation)
            .AutoInclude();
    }
}

public class CommentReportConfiguration : BaseEntityConfiguration<CommentReport>
{
    public override void Configure(EntityTypeBuilder<CommentReport> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(r => r.Comment)
            .WithMany()
            .HasForeignKey(r => r.CommentId);

        builder
            .Navigation(r => r.Comment)
            .AutoInclude();
    }
}

public class ComicCommentReportConfiguration : BaseEntityConfiguration<ComicCommentReport>
{
    public override void Configure(EntityTypeBuilder<ComicCommentReport> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(r => r.Comment)
            .WithMany()
            .HasForeignKey(r => r.CommentId);

        builder
            .Navigation(r => r.Comment)
            .AutoInclude();
    }
}

public class ChapterCommentReportConfiguration : BaseEntityConfiguration<ChapterCommentReport>
{
    public override void Configure(EntityTypeBuilder<ChapterCommentReport> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(r => r.Comment)
            .WithMany()
            .HasForeignKey(r => r.CommentId);

        builder
            .Navigation(r => r.Comment)
            .AutoInclude();
    }
}