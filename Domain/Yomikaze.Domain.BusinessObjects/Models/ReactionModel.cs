using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.Domain.Models;

public class ReactionModel
{
    public ReactionType Type { get; set; } = ReactionType.Like;
}