namespace Yomikaze.Domain.Entities;

public class Notification : BaseEntity
{
    #region Properties

    [StringLength(256)] public string Title { get; set; } = default!;

    [StringLength(512)] public string? Content { get; set; }

    public bool Read { get; set; }

    [ForeignKey(nameof(User))] public ulong UserId { get; set; }

    public User User { get; set; } = default!;

    #endregion
}