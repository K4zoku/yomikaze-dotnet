using Yomikaze.Domain.Database.Entities;
using Yomikaze.Domain.Database.Entities.Identity;
using Yomikaze.WebAPI.Models.Common;

namespace Yomikaze.WebAPI.Helpers;

public static class ModelExtensions
{
    public static UserModel ToModel(this User user)
    {
        return new UserModel(user);
    }

    public static UserModel ToModel(this User user, bool onlyId)
    {
        return onlyId ? new UserModel { Id = user.Id } : user.ToModel();
    }

    public static CommentModel ToModel(this Comment comment)
    {
        return new CommentModel
        {
            Content = comment.Content,
            CreatedAt = comment.CreatedAt,
            CreatedBy = comment.User.ToModel(true),
            Replies = comment.Replies?.Select(ToModel).ToArray(),
        };
    }
}
