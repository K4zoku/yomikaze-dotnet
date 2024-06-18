namespace Yomikaze.Domain.Models;

public class HistoryRecordInputModel
{
    [Required] public string ChapterId { get; set; }

    [Required] public string UserId { get; set; }
}

public class HistoryRecordOutputModel
{
    public string Id { get; set; }

    public ChapterOutputModel Chapter { get; set; } = default!;

    public DateTimeOffset CreatedAt { get; set; }
}