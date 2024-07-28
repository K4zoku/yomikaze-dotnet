using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.API.Main.Controllers;

// TODO)) copy code from ComicCommentController
[ApiController]
[Route("comics/{comicId}/chapters/{number:int}/comments")]
public class ChapterCommentController(
    ChapterCommentRepository repository,
    ChapterRepository chapterRepository,
    IMapper mapper,
    ILogger<ChapterCommentController> logger)
    : SearchControllerBase<ChapterComment, ChapterCommentModel, ChapterCommentRepository, ChapterCommentSearchModel>(repository,
        mapper, logger)
{
    
    private ChapterRepository ChapterRepository { get; } = chapterRepository;
    
    protected override IList<SearchFieldMutator<ChapterComment, ChapterCommentSearchModel>> SearchFieldMutators { get; } =
    [
        new SearchFieldMutator<ChapterComment, ChapterCommentSearchModel>(search => search.ComicId is not null,
            (query, search) => query.Where(comment => comment.Chapter.ComicId == search.ComicId)),
        new SearchFieldMutator<ChapterComment, ChapterCommentSearchModel>(search => search.ChapterNumber is not null,
            (query, search) => query.Where(comment => comment.Chapter.Number == search.ChapterNumber)),
        new SearchFieldMutator<ChapterComment, ChapterCommentSearchModel>(search => search.ReplyToId is not null,
            (query, search) => query.Where(comment => comment.ReplyToId == search.ReplyToId)),
        new SearchFieldMutator<ChapterComment, ChapterCommentSearchModel>(search => search.ReplyToId is null,
            (query, search) => query.Where(comment => comment.ReplyToId == null)),
        new SearchFieldMutator<ChapterComment, ChapterCommentSearchModel>(search => search.OrderBy is not { Length: 0 },
            (query, search) =>
            {
                IOrderedQueryable<ChapterComment> orderedQuery = search.OrderBy![0] switch
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
    public override ActionResult<PagedList<ChapterCommentModel>> List(ChapterCommentSearchModel search, PaginationModel pagination)
    {
        return base.List(search, pagination);
    }
    
    [HttpGet]
    public ActionResult<PagedList<ChapterCommentModel>> List([FromRoute] ulong comicId, [FromRoute] int number, [FromQuery] ChapterCommentSearchModel search, [FromQuery] PaginationModel pagination)
    {
        search.ComicId = comicId;
        search.ChapterNumber = number;
        return base.List(search, pagination);
    }
    
    [HttpGet("{key}/replies")]
    public ActionResult<PagedList<ChapterCommentModel>> ListReplies([FromRoute] ulong comicId, [FromRoute] int number, [FromRoute] ulong key, [FromQuery] ChapterCommentSearchModel search, [FromQuery] PaginationModel pagination)
    {
        search.ComicId = comicId;
        search.ReplyToId = key;
        search.ChapterNumber = number;
        return base.List(search, pagination);
    }
    
    [HttpPost("{key}/replies")]
    public ActionResult<ChapterCommentModel> PostReplies([FromRoute] ulong comicId, [FromRoute] int number, [FromRoute] ulong key, [FromBody] ChapterCommentModel input)
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
        input.ChapterId = comment.ChapterId.ToString();

        return Post(comicId, number, input);
    }

    [NonAction]
    public override ActionResult<ChapterCommentModel> Post(ChapterCommentModel input)
    {
        return base.Post(input);
    }
    
    [HttpPost]
    [Authorize]
    public ActionResult<ChapterCommentModel> Post([FromRoute] ulong comicId, [FromRoute] int number, ChapterCommentModel input)
    {
        input.AuthorId = User.GetIdString();
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }
        
        Chapter? chapter = input.ChapterId == null ? ChapterRepository.GetByComicIdAndIndex(comicId.ToString(), number) : ChapterRepository.Get(input.ChapterId);
        
        if (chapter == null)
        {
            return NotFound();
        }

        ChapterComment entity = Mapper.Map<ChapterComment>(input);
        entity.ChapterId = chapter.Id;
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
    public override ActionResult<ChapterCommentModel> Get(ulong key)
    {
        return base.Get(key);
    }
    
    [HttpGet("{key}")]
    public ActionResult<ChapterCommentModel> GetComment([FromRoute] ulong comicId, [FromRoute] ulong key)
    {
        return base.Get(key);
    }
    
    [NonAction]
    public override ActionResult<ChapterCommentModel> Patch(ulong key, JsonPatchDocument<ChapterCommentModel> patch)
    {
        return base.Patch(key, patch);
    }
    
    [HttpPatch("{key}")]
    public ActionResult<ChapterCommentModel> Patch(ulong comicId, int number, ulong key, JsonPatchDocument<ChapterCommentModel> patch)
    {
        return base.Patch(key, patch);
    }
    
    [NonAction]
    public override ActionResult Delete(ulong key)
    {
        return base.Delete(key);
    }
    
    [HttpDelete("{key}")]
    public ActionResult Delete(ulong comicId, int number, ulong key)
    {
        return base.Delete(key);
    }
    
    [HttpPost("{key}/react")]
    public ActionResult React(ulong comicId, int number, ulong key, [FromBody] ReactionModel input)
    {
        var comment = Repository.Get(key);
        if (comment == null)
        {
            return NotFound();
        }
        var userId = User.GetId();

        var existingReaction = comment.Reactions
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