using System.ComponentModel.DataAnnotations;

namespace Yomikaze.Domain.Models;

public class HistoryRecordInputModel
{
    [Required] public long ChapterId { get; set; }

    [Required] public long UserId { get; set; }

    public DateTimeOffset LastRead { get; set; }
}

public class HistoryRecordOutputModel
{
    public long Id { get; set; }

    public ChapterOutputModel Chapter { get; set; } = default!;

    public UserOutputModel User { get; set; } = default!;

    public DateTimeOffset LastRead { get; set; } = DateTimeOffset.Now;
}