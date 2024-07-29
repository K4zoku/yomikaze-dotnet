using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using Yomikaze.API.Main.Helpers;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Domain.Entities.Weak;
using Yomikaze.Domain.Models.Search;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("comics/{comicId}/comments")]
public class ComicCommentController(
    ComicCommentRepository repository,
    IMapper mapper,
    ILogger<ComicCommentController> logger)
    : SearchControllerBase<ComicComment, ComicCommentModel, ComicCommentRepository, ComicCommentSearchModel>(repository,
        mapper, logger)
{
    protected override IList<SearchFieldMutator<ComicComment, ComicCommentSearchModel>> SearchFieldMutators { get; } =
    [
        new SearchFieldMutator<ComicComment, ComicCommentSearchModel>(search => search.ComicId is not null,
            (query, search) => query.Where(comment => comment.ComicId == search.ComicId)),
        new SearchFieldMutator<ComicComment, ComicCommentSearchModel>(search => search.ReplyToId is not null,
            (query, search) => query.Where(comment => comment.ReplyToId == search.ReplyToId)),
        new SearchFieldMutator<ComicComment, ComicCommentSearchModel>(search => search.ReplyToId is null,
            (query, search) => query.Where(comment => comment.ReplyToId == null)),
        new SearchFieldMutator<ComicComment, ComicCommentSearchModel>(search => search.OrderBy is not { Length: 0 },
            (query, search) =>
            {
                IOrderedQueryable<ComicComment> orderedQuery = search.OrderBy![0] switch
                {
                    ComicCommentOrderBy.CreationTime => query.OrderBy(comment => comment.CreationTime),
                    ComicCommentOrderBy.CreationTimeDesc => query.OrderByDescending(comment => comment.CreationTime),
                    ComicCommentOrderBy.LastModified => query.OrderBy(comment => comment.LastModified),
                    ComicCommentOrderBy.LastModifiedDesc => query.OrderByDescending(comment => comment.LastModified),
                    _ => query.OrderBy(comment => comment.CreationTime)
                };

                return search.OrderBy!.Skip(1)
                    .Aggregate(orderedQuery, (current, orderBy) => orderBy switch
                    {
                        ComicCommentOrderBy.CreationTime => current.ThenBy(comment => comment.CreationTime),
                        ComicCommentOrderBy.CreationTimeDesc => current.ThenByDescending(
                            comment => comment.CreationTime),
                        ComicCommentOrderBy.LastModified => current.ThenBy(comment => comment.LastModified),
                        ComicCommentOrderBy.LastModifiedDesc => current.ThenByDescending(
                            comment => comment.LastModified),
                        _ => current.ThenBy(comment => comment.CreationTime)
                    });
            })
    ];

    [NonAction]
    public override ActionResult<PagedList<ComicCommentModel>> List(ComicCommentSearchModel search,
        PaginationModel pagination)
    {
        return base.List(search, pagination);
    }

    [HttpGet]
    public ActionResult<PagedList<ComicCommentModel>> List([FromRoute] ulong comicId,
        [FromQuery] ComicCommentSearchModel search, [FromQuery] PaginationModel pagination)
    {
        search.ComicId = comicId;
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        string keyName =
            $"{CacheKeyPrefix}{nameof(List)}:{JsonConvert.SerializeObject(search)}:[{pagination.Page},{pagination.Size}]";

        return Cache.GetOrSet(keyName, () =>
        {
            IQueryable<ComicComment> query = ListQuery();
            query = ApplySearch(query, search);
            PagedList<ComicCommentModel> result = GetPaged(query, pagination);
            if (!User.TryGetId(out ulong userId))
            {
                return result;
            }

            foreach (ComicCommentModel item in result.Results)
            {
                if (item.Id is null)
                {
                    continue;
                }
                CommentReaction? reaction = Repository.GetReactionsByCommentId(item.Id)
                    .FirstOrDefault(x => x.UserId == userId);
                item.IsReacted = reaction != null;
                item.MyReaction = reaction?.ReactionType;
            }

            return result;
        }, logger: Logger);
    }

    [HttpGet("{key}/replies")]
    public ActionResult<PagedList<ComicCommentModel>> ListReplies([FromRoute] ulong comicId, [FromRoute] ulong key,
        [FromQuery] ComicCommentSearchModel search, [FromQuery] PaginationModel pagination)
    {
        search.ComicId = comicId;
        search.ReplyToId = key;
        return List(comicId, search, pagination);
    }

    [HttpPost("{key}/replies")]
    public ActionResult<ComicCommentModel> PostReplies([FromRoute] ulong comicId, [FromRoute] ulong key,
        [FromBody] ComicCommentModel input)
    {
        ComicComment? comment = Repository.Get(key);
        if (comment == null)
        {
            return NotFound();
        }

        // Find the root comment
        while (comment.ReplyToId is not null)
        {
            comment = Repository.Get(comment.ReplyToId.Value);
            if (comment == null)
            {
                return NotFound();
            }
        }

        input.ReplyToId = comment.Id.ToString(); // Set the root comment as the reply to id

        return Post(comicId, input);
    }

    [NonAction]
    public override ActionResult<ComicCommentModel> Post(ComicCommentModel input)
    {
        return base.Post(input);
    }

    [HttpPost]
    [Authorize]
    public ActionResult<ComicCommentModel> Post([FromRoute] ulong comicId, ComicCommentModel input)
    {
        input.ComicId = comicId.ToString();
        input.AuthorId = User.GetIdString();
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        ComicComment entity = Mapper.Map<ComicComment>(input);
        Logger.LogDebug("After mapped {Entity}", JsonConvert.SerializeObject(entity));
        try
        {
            Repository.Add(entity);
        }
        catch (DbUpdateException e)
        {
            Logger.LogWarning(e, "Error when adding entity");
            return Conflict();
        }
        catch (Exception e)
        {
            Logger.LogCritical(e, "Critical error when adding entity");
            return Problem();
        }

        RemoveListCache();
        if (Equals(entity.Id, default(ulong)))
        {
            return Problem("Id is null");
        }

        entity = Repository.Get(entity.Id) ?? entity;
        ComicCommentModel? model = Mapper.Map<ComicCommentModel>(entity);
        ModelWriteOnlyProperties.ForEach(x => x.SetValue(model, default));

        return CreatedAtAction("GetComment", new { comicId, key = entity.Id }, model);
    }

    [NonAction]
    public override ActionResult<ComicCommentModel> Get(ulong key)
    {
        return base.Get(key);
    }

    [HttpGet("{key}")]
    public ActionResult<ComicCommentModel> GetComment([FromRoute] ulong comicId, [FromRoute] ulong key)
    {
        ComicComment? comment = Repository.Get(key);
        if (comment == null)
        {
            return NotFound();
        }

        ComicCommentModel? model = Mapper.Map<ComicCommentModel>(comment);
        if (!User.TryGetId(out ulong userId))
        {
            return model;
        }

        CommentReaction? reaction = comment.Reactions.FirstOrDefault(x => x.UserId == userId);
        model.IsReacted = reaction != null;
        model.MyReaction = reaction?.ReactionType;
        return model;
    }

    [NonAction]
    public override ActionResult<ComicCommentModel> Patch(ulong key, JsonPatchDocument<ComicCommentModel> patch)
    {
        return base.Patch(key, patch);
    }

    [HttpPatch("{key}")]
    public ActionResult<ComicCommentModel> Patch(ulong comicId, ulong key, JsonPatchDocument<ComicCommentModel> patch)
    {
        return base.Patch(key, patch);
    }

    [NonAction]
    public override ActionResult Delete(ulong key)
    {
        return base.Delete(key);
    }

    [HttpDelete("{key}")]
    public ActionResult Delete(ulong comicId, ulong key)
    {
        return base.Delete(key);
    }

    [HttpPost("{key}/react")]
    public ActionResult React(ulong comicId, ulong key, [FromBody] ReactionModel input)
    {
        ComicComment? comment = Repository.Get(key);
        if (comment == null)
        {
            return NotFound();
        }

        ulong userId = User.GetId();

        CommentReaction? existingReaction = comment.Reactions
            .FirstOrDefault(x => x.UserId == userId);
        if (existingReaction != null)
        {
            if (existingReaction.ReactionType == input.Type)
            {
                comment.Reactions.Remove(existingReaction);
                Repository.Update(comment);
                return NoContent();
            }

            existingReaction.ReactionType = input.Type;
        }
        else
        {
            comment.Reactions.Add(new CommentReaction { UserId = userId, ReactionType = input.Type });
        }

        Repository.Update(comment);
        return Ok();
    }
}