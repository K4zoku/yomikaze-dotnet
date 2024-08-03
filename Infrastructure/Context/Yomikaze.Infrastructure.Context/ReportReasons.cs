using Yomikaze.Domain.Entities;

namespace Yomikaze.Infrastructure.Context;

public partial class YomikazeDbContext
{
    public static partial class Default
    {
        public static readonly ChapterReportReason[] ChapterReportReasons =
        [
            new ChapterReportReason(67464207511101440L, "Inappropriate content"),
            new ChapterReportReason(67464207515295744L, "Price is not reasonable"),
            new ChapterReportReason(67464207515295745L, "Wrong chapter"),
            new ChapterReportReason(67464207515295746L, "Other")
        ];

        public static readonly ComicReportReason[] ComicReportReasons =
        [
            new ComicReportReason(67464207511101447L, "Duplicated entry"),
            new ComicReportReason(67464207515295748L, "Incorrect information"),
            new ComicReportReason(67464207515295749L, "Copyright infringement"),
            new ComicReportReason(67464207515295750L, "Other")
        ];

        public static readonly ProfileReportReason[] ProfileReportReasons =
        [
            new ProfileReportReason(67464207511101453L, "Inappropriate content"),
            new ProfileReportReason(67464207515295754L, "Impersonation"),
            new ProfileReportReason(67464207515295755L, "Other")
        ];

        public static readonly TranslationReportReason[] TranslationReportReasons =
        [
            new TranslationReportReason(67464207511101460L, "Inappropriate content"),
            new TranslationReportReason(67464207515295761L, "Incorrect translation"),
            new TranslationReportReason(67464207515295762L, "Other")
        ];

        public static readonly CommentReportReason[] CommentReportReasons =
        [
            new CommentReportReason(67464207511101467L, "Inappropriate content"),
            new CommentReportReason(67464207515295768L, "Spam"),
            new CommentReportReason(67464207515295769L, "Harassment"),
            new CommentReportReason(67464207515295770L, "Other")
        ];
    }
}