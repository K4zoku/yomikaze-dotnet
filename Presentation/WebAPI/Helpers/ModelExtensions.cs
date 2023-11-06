using Yomikaze.Domain.Database.Entities;
using Yomikaze.Domain.Database.Entities.Identity;
using Yomikaze.WebAPI.Models.Common;

namespace Yomikaze.WebAPI.Helpers;

public static class ModelExtensions
{
    public static UserModel ToModel(this User user)
    {
        return (UserModel)user;
    }

    public static CommentModel ToModel(this Comment comment)
    {
        return new CommentModel
        {
            Content = comment.Content,
            CreatedAt = comment.CreatedAt,
            CreatedBy = new UserModel { Id = comment.UserId },
            Replies = comment.Replies?.Select(ToModel).ToArray(),
        };
    }

    public static GenreModel ToModel(this Genre genre)
    {
        return (GenreModel)genre;
    }

    public static ChapterModel ToModel(this Chapter chapter)
    {
        return (ChapterModel)chapter;
    }

    public static ChapterModel ToModel(this Chapter chapter, bool includePages)
    {
        var chapterModel = (ChapterModel)chapter;
        chapterModel.Pages = includePages ? chapter.Pages?.Select(ToModel).ToArray() : null;
        return chapterModel;
    }

    public static PageModel ToModel(this Page page)
    {
        return (PageModel)page;
    }

    public static ComicModel ToModel(this Comic comic)
    {
        return (ComicModel)comic;
    }

    public static ComicModel ToModel(this Comic comic, bool includeChapters)
    {
        var comicModel = (ComicModel)comic;
        comicModel.Chapters = includeChapters ? comic.Chapters?.Select(ToModel).ToArray() : null;
        return comicModel;
    }
}
