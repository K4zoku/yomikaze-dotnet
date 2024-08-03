using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.Application.Data.Repos;

public class ComicCommentRepository(DbContext dbContext) : BaseRepository<ComicComment>(new ComicCommentDao(dbContext))
{
    public override IQueryable<ComicComment> Query()
    {
        return base.Query().Include(x => x.Reactions).Include(x => x.Author).ThenInclude(x => x.Roles);
    }

    public IQueryable<ComicComment> GetByComicId(ulong comicId)
    {
        return Query().Where(x => x.ComicId == comicId);
    }

    public IQueryable<ComicComment> GetRepliesByCommentId(ulong commentId)
    {
        return Query().Where(x => x.ReplyToId == commentId);
    }

    public ICollection<CommentReaction> GetReactionsByCommentId(string commentId)
    {
        return Query().Where(x => x.Id.ToString() == commentId).SelectMany(x => x.Reactions).ToList();
    }
}