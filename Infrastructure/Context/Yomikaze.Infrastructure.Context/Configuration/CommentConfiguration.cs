using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Infrastructure.Context.Configuration;

public class CommentConfiguration : BaseEntityConfiguration<Comment>
{
    public override void Configure(EntityTypeBuilder<Comment> builder)
    {
        base.Configure(builder);
        builder
            .HasDiscriminator<string>("type")
            .IsComplete(false)
            .HasValue<Comment>("comment_base")
            .HasValue<ChapterComment>("chapter_comment")
            .HasValue<ComicComment>("comic_comment")
            .HasValue<ProfileComment>("profile_comment");
    }
}