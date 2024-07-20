using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.API.Main.Controllers;

// TODO)) Add isReacted and reactionType to comment model, those fields only need to be set when the user is logged in
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
                    _ => query.OrderBy(comment => comment.CreationTime),
                };
                
                return search.OrderBy!.Skip(1)
                    .Aggregate(orderedQuery, (current, orderBy) => orderBy switch
                    {
                        ComicCommentOrderBy.CreationTime => current.ThenBy(comment => comment.CreationTime),
                        ComicCommentOrderBy.CreationTimeDesc => current.ThenByDescending(comment => comment.CreationTime),
                        ComicCommentOrderBy.LastModified => current.ThenBy(comment => comment.LastModified),
                        ComicCommentOrderBy.LastModifiedDesc => current.ThenByDescending(comment => comment.LastModified),
                        _ => current.ThenBy(comment => comment.CreationTime),
                    });
            }),
    ];

    [NonAction]
    public override ActionResult<PagedList<ComicCommentModel>> List(ComicCommentSearchModel search, PaginationModel pagination)
    {
        return base.List(search, pagination);
    }
    
    [HttpGet]
    public ActionResult<PagedList<ComicCommentModel>> List([FromRoute] ulong comicId, [FromQuery] ComicCommentSearchModel search, [FromQuery] PaginationModel pagination)
    {
        search.ComicId = comicId;
        return base.List(search, pagination);
    }
    
    [HttpGet("{key}/replies")]
    public ActionResult<PagedList<ComicCommentModel>> ListReplies([FromRoute] ulong comicId, [FromRoute] ulong key, [FromQuery] ComicCommentSearchModel search, [FromQuery] PaginationModel pagination)
    {
        search.ComicId = comicId;
        search.ReplyToId = key;
        return base.List(search, pagination);
    }
    
    [HttpPost("{key}/replies")]
    public ActionResult<ComicCommentModel> PostReplies([FromRoute] ulong comicId, [FromRoute] ulong key, [FromBody] ComicCommentModel input)
    {
        var comment = Repository.Get(key);
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
        } catch (DbUpdateException e)
        {
            Logger.LogWarning(e, "Error when adding entity");
            return Conflict();
        } catch (Exception e)
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
        var model = Mapper.Map<ComicCommentModel>(entity);
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
        return base.Get(key);
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
    public ActionResult React(ulong comicId, ulong key)
    {
        var comment = Repository.Get(key);
        if (comment == null)
        {
            return NotFound();
        }
        // TODO)) Check if the user has already reacted to the comment
        // TODO)) Add reaction model for user to choose reaction type
        comment.Reactions.Add(new CommentReaction { UserId = User.GetId(), ReactionType = ReactionType.Like });
        Repository.Update(comment);
        return Ok();
    }
}