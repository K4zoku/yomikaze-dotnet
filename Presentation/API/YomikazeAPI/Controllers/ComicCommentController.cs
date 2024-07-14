using Microsoft.AspNetCore.JsonPatch;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
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
    
    [Route("comics/{comicId}/comments")]
    [HttpGet]
    public ActionResult<PagedList<ComicCommentModel>> List([FromRoute] ulong comicId, [FromQuery] ComicCommentSearchModel search, [FromQuery] PaginationModel pagination)
    {
        search.ComicId = comicId;
        return base.List(search, pagination);
    }

    [NonAction]
    public override ActionResult<ComicCommentModel> Post(ComicCommentModel input)
    {
        return base.Post(input);
    }
    
    [Route("comics/{comicId}/comments")]
    [HttpPost]
    public ActionResult<ComicCommentModel> Post([FromRoute] ulong comicId, ComicCommentModel input)
    {
        input.ComicId = comicId.ToString();
        return base.Post(input);
    }
    
    [Route("comics/comments/{key}")]
    [HttpGet]
    public override ActionResult<ComicCommentModel> Get(ulong key)
    {
        return base.Get(key);
    }

    [Route("comics/comments/{key}")]
    [HttpPatch]
    public override ActionResult<ComicCommentModel> Patch(ulong key, JsonPatchDocument<ComicCommentModel> patch)
    {
        return base.Patch(key, patch);
    }

    [Route("comics/comments/{key}")]
    [HttpDelete]
    public override ActionResult Delete(ulong key)
    {
        return base.Delete(key);
    }
}