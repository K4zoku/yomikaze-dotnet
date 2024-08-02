using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System.Net;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Domain.Entities.Weak;
using Yomikaze.Domain.Models.Search;

namespace Yomikaze.API.Main.Controllers;

public partial class ComicsController
{
    #region Chapters

    private List<SearchFieldMutator<Chapter, ChapterSearchModel>> ChapterSearchFieldMutators { get; } =
    [
        new SearchFieldMutator<Chapter, ChapterSearchModel>(search => search.OrderBy.Length > 0, (query, search) =>
        {
            IOrderedQueryable<Chapter> ordered = search.OrderBy[0] switch
            {
                ChapterOrderBy.Number => query.OrderBy(x => x.Number),
                ChapterOrderBy.NumberDesc => query.OrderByDescending(x => x.Number),
                ChapterOrderBy.Name => query.OrderBy(x => x.Name),
                ChapterOrderBy.NameDesc => query.OrderByDescending(x => x.Name),
                ChapterOrderBy.CreationTime => query.OrderBy(x => x.CreationTime),
                ChapterOrderBy.CreationTimeDesc => query.OrderByDescending(x => x.CreationTime),
                ChapterOrderBy.LastModified => query.OrderBy(x => x.LastModified),
                ChapterOrderBy.LastModifiedDesc => query.OrderByDescending(x => x.LastModified),
                _ => query.OrderBy(x => x.Number)
            };
            return search.OrderBy.Skip(1)
                .Aggregate(ordered, (current, orderBy) => orderBy switch
                {
                    ChapterOrderBy.Number => current.ThenBy(x => x.Number),
                    ChapterOrderBy.NumberDesc => current.ThenByDescending(x => x.Number),
                    ChapterOrderBy.Name => current.ThenBy(x => x.Name),
                    ChapterOrderBy.NameDesc => current.ThenByDescending(x => x.Name),
                    ChapterOrderBy.CreationTime => current.ThenBy(x => x.CreationTime),
                    ChapterOrderBy.CreationTimeDesc => current.ThenByDescending(x => x.CreationTime),
                    ChapterOrderBy.LastModified => current.ThenBy(x => x.LastModified),
                    ChapterOrderBy.LastModifiedDesc => current.ThenByDescending(x => x.LastModified),
                    _ => current.ThenBy(x => x.Number)
                });
        })
    ];

    [HttpGet("{key}/chapters")]
    [AllowAnonymous]
    public ActionResult<PagedList<ChapterModel>> GetChapters(ulong key, [FromQuery] ChapterSearchModel search,
        [FromQuery] PaginationModel pagination)
    {
        Comic? entity = Repository.Get(key);
        if (entity == null)
        {
            return NotFound();
        }

        IQueryable<Chapter> chapters = ChapterRepository.GetAllByComicId(key);

        chapters = ChapterSearchFieldMutators.Aggregate(chapters, (current, mutator) => mutator.Apply(search, current));
        long count = chapters.AsSplitQuery().LongCount();
        if (search.Pagination)
        {
            int skip = (pagination.Page - 1) * pagination.Size;
            chapters = chapters.Skip(skip).Take(pagination.Size);
        }
        else
        {
            pagination.Size = count > 0 ? (int)count : 1;
            pagination.Page = 1;
        }
        List<Chapter> chapterList = chapters.ToList();
        List<ChapterModel>? results = Mapper.Map<List<ChapterModel>>(chapterList);
        PagedList<ChapterModel> paged = new()
        {
            CurrentPage = pagination.Page,
            PageSize = pagination.Size,
            Totals = count,
            TotalPages = (int)Math.Ceiling((double)count / pagination.Size),
            Results = results
        };

        if (!User.TryGetId(out ulong userId))
        {
            return Ok(paged);
        }

        for (int i = 0; i < paged.Results.Count(); i++)
        {
            Chapter chapter = chapterList[i];
            ChapterModel model = paged.Results.ElementAt(i);
            model.IsUnlocked = model.HasLock is false || chapter.Unlocked.Any(u => u.UserId == userId);
        }

        return Ok(paged);
    }

    [HttpPost("{key}/chapters")]
    public ActionResult<ChapterModel> PostChapter(ulong key,
        [Bind($"{nameof(ChapterModel.Number)},{nameof(ChapterModel.Name)},{nameof(ChapterModel.Pages)}")]
        ChapterModel model)
    {
        Comic? comic = Repository.Get(key);
        if (comic == null)
        {
            return NotFound();
        }

        if (comic.PublisherId != User.GetId())
        {
            return Forbid();
        }

        Chapter chapter = Mapper.Map<Chapter>(model);
        chapter.ComicId = key;
        // update chapters number
        chapter.Number = comic.Chapters.Count;

        comic.Chapters.Add(chapter);
        comic.LastModified = DateTimeOffset.UtcNow;
        Repository.Update(comic);

        return CreatedAtAction(nameof(GetChapter), new { key, number = chapter.Number },
            Mapper.Map<ChapterModel>(chapter));
    }

    [HttpGet($"{{{nameof(key)}}}/chapters/{{{nameof(number)}:int}}")]
    [AllowAnonymous]
    public ActionResult<ChapterModel> GetChapter(ulong key, int number)
    {
        Chapter? chapter = ChapterRepository.GetByComicIdAndIndex(key.ToString(), number);
        if (chapter == null)
        {
            return NotFound();
        }

        ChapterModel? model = Mapper.Map<ChapterModel>(chapter);

        try
        {
            if (User.TryGetId(out ulong userId))
            {
                model.IsUnlocked = model.HasLock is false || chapter.Unlocked.Any(u => u.UserId == userId);
                if (model.IsUnlocked is true)
                {
                    model.IsRead = true;
                    chapter.Views++;
                    ChapterRepository.Update(chapter);
                    Logger.LogDebug("Chapter {Id} views increased to {Views}", chapter.Id, chapter.Views);
                    DbContext.ComicViews.Add(new ComicView
                    {
                        ComicId = chapter.ComicId, CreationTime = DateTimeOffset.UtcNow, Views = 1
                    });
                    DbContext.SaveChanges();
                    Logger.LogDebug("Added view record for comic {Id}", chapter.ComicId);
                    HistoryRepository.Add(userId, chapter);
                    Logger.LogDebug("User {Id} read chapter {Chapter}", userId, chapter.Id);
                }
                else
                {
                    return Forbid();
                }
            }
            else
            {
                Logger.LogDebug("User is not authenticated, skipping history record");
                if (model.HasLock is true)
                {
                    Logger.LogDebug("User is not authenticated, chapter {Id} is locked", chapter.Id);
                    return Unauthorized();
                }
            }
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Failed to update chapter {Id} views or add history record", chapter.Id);
        }

        return Ok(model);
    }

    [HttpPut($"{{{nameof(key)}}}/chapters/{{{nameof(number)}:int}}/unlock")]
    [Authorize]
    public ActionResult UnlockChapter(ulong key, int number, [FromServices] UserManager<User> userManager,
        [FromServices] TransactionRepository transactionRepository)
    {
        Chapter? chapter = ChapterRepository.GetByComicIdAndIndex(key.ToString(), number);
        if (chapter == null)
        {
            return NotFound();
        }

        if (chapter.Price == 0)
        {
            ModelState.AddModelError("Chapter", "Chapter is not locked");
            return BadRequest(ModelState);
        }

        ulong userId = User.GetId();

        if (chapter.Unlocked.Any(u => u.UserId == userId))
        {
            ModelState.AddModelError("Chapter", "Chapter is already unlocked");
            return BadRequest(ModelState);
        }

        User currentUser = User.GetUser(userManager);
        if (currentUser.Balance < chapter.Price)
        {
            return Problem(statusCode: (int)HttpStatusCode.PaymentRequired, detail: "Insufficient balance");
        }

        currentUser.Balance -= chapter.Price;
        userManager.UpdateAsync(currentUser).Wait();
        Transaction transaction = new()
        {
            UserId = currentUser.Id,
            Amount = -chapter.Price,
            Type = TransactionType.UnlockChapter,
            Description = $"/comics/{key}/chapters/{number}"
        };
        transactionRepository.Add(transaction);
        var comic = Repository.Get(key);
        var publisher = comic!.Publisher;
        publisher.Balance += chapter.Price;
        userManager.UpdateAsync(publisher).Wait();
        transaction = new Transaction
        {
            UserId = publisher.Id,
            Amount = chapter.Price,
            Type = TransactionType.ReceiveCoin,
            Description = $"Unlock chapter {number} for comic {key}"
        };
        transactionRepository.Add(transaction);
        
        chapter.Unlocked.Add(new UnlockedChapter { UserId = userId, ChapterId = chapter.Id });
        ChapterRepository.Update(chapter);
        return Ok();
    }

    [HttpPut($"{{{nameof(key)}}}/chapters/unlock")]
    [Authorize]
    public ActionResult<IEnumerable<int>> UnlockChapters(ulong key, [FromBody] int[] chapterNumbers,
        [FromServices] UserManager<User> userManager, [FromServices] TransactionRepository transactionRepository)
    {
        IQueryable<Chapter> chapters = ChapterRepository.GetByComicIdAndIndexes(key.ToString(), chapterNumbers);
        if (!chapters.Any())
        {
            return NotFound();
        }

        List<Chapter> locked = chapters
            .Where(c => c.Price > 0)
            .Where(c => c.Unlocked.All(u => u.UserId != User.GetId()))
            .ToList();

        if (locked.Count == 0)
        {
            ModelState.AddModelError("Chapters", "There are no locked chapters");
            return BadRequest(ModelState);
        }

        int price = locked.Sum(c => c.Price);

        User currentUser = User.GetUser(userManager);

        if (currentUser.Balance < price)
        {
            return Problem(statusCode: (int)HttpStatusCode.PaymentRequired, detail: "Insufficient balance");
        }

        currentUser.Balance -= price;
        userManager.UpdateAsync(currentUser).Wait();
        Transaction transaction = new()
        {
            UserId = currentUser.Id,
            Amount = -price,
            Type = TransactionType.UnlockChapters,
            Description =
                JsonConvert.SerializeObject(chapters.Select(c => $"/comics/{key}/chapters/{c.Number}").ToArray())
        };
        transactionRepository.Add(transaction);
        var comic = Repository.Get(key);
        var publisher = comic!.Publisher;
        publisher.Balance += price;
        userManager.UpdateAsync(publisher).Wait();
        transaction = new Transaction
        {
            UserId = publisher.Id,
            Amount = price,
            Type = TransactionType.ReceiveCoin,
            Description = $"Unlock chapters for comic {key}"
        };
        transactionRepository.Add(transaction);
        
        List<int> unlockedChapters = [];
        foreach (Chapter chapter in chapters)
        {
            chapter.Unlocked.Add(new UnlockedChapter { UserId = currentUser.Id, ChapterId = chapter.Id });
            ChapterRepository.Update(chapter);
            unlockedChapters.Add(chapter.Number);
        }

        return Ok(unlockedChapters);
    }

    [HttpPatch("{key}/chapters/{number:int}")]
    public ActionResult<ChapterModel> UpdateChapter(ulong key, int number,
        JsonPatchDocument<ChapterModel> patchDocument)
    {
        Comic? comic = Repository.Get(key);

        if (comic == null)
        {
            return NotFound();
        }

        if (comic.PublisherId != User.GetId())
        {
            return Forbid();
        }

        Chapter? chapter = ChapterRepository.GetByComicIdAndIndex(key.ToString(), number);

        if (chapter == null)
        {
            return NotFound();
        }

        ChapterModel model = Mapper.Map<ChapterModel>(chapter);
        patchDocument.ApplyTo(model);
        Mapper.Map(model, chapter);
        ChapterRepository.Update(chapter);
        return Ok(Mapper.Map<ChapterModel>(chapter));
    }

    [HttpDelete("{key}/chapters/{number:int}")]
    public ActionResult<ChapterModel> DeleteChapter(ulong key, int number)
    {
        Comic? comic = Repository.Get(key);

        if (comic == null)
        {
            return NotFound();
        }

        if (comic.PublisherId != User.GetId())
        {
            return Forbid();
        }

        Chapter? chapter = ChapterRepository.GetByComicIdAndIndex(key.ToString(), number);

        if (chapter == null)
        {
            return NotFound();
        }

        ChapterRepository.Delete(chapter);
        return NoContent();
    }

    #endregion
}