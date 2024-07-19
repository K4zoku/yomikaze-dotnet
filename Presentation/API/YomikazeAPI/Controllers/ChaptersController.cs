using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using System.Net;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.API.Main.Controllers;

public partial class ComicsController
{
     #region Chapters

    [HttpGet("{key}/chapters")]
    [AllowAnonymous]
    public ActionResult<ICollection<ChapterModel>> GetChapters(ulong key)
    {
        Comic? entity = Repository.Query().Include(c => c.Chapters).FirstOrDefault(c => c.Id == key);
        if (entity == null)
        {
            return NotFound();
        }
        

        return Ok(Mapper.Map<ICollection<ChapterModel>>(entity.Chapters));
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
        
        var model = Mapper.Map<ChapterModel>(chapter);

        try
        {
            model.IsUnlocked = chapter.Price == 0;
            if (User.TryGetId(out var userId))
            {
                 model.IsUnlocked = model.IsUnlocked is true || chapter.Unlocked.Any(u => u.UserId == userId);
                if (model.IsUnlocked is true)
                {
                    model.IsRead = true;
                    chapter.Views++;
                    ChapterRepository.Update(chapter);
                    Logger.LogDebug("Chapter {Id} views increased to {Views}", chapter.Id, chapter.Views);
                    DbContext.ComicViews.Add(new ComicView() { ComicId = chapter.ComicId, CreationTime = DateTimeOffset.UtcNow, Views = 1});
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
                if (model.IsUnlocked is false)
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
    public ActionResult UnlockChapter(ulong key, int number, [FromServices] UserManager<User> userManager)
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

        var userId = User.GetId();
        
        if (chapter.Unlocked.Any(u => u.UserId == userId))
        {
            ModelState.AddModelError("Chapter", "Chapter is already unlocked");
            return BadRequest(ModelState);
        }

        var currentUser = User.GetUser(userManager);
        if (currentUser.Balance < chapter.Price)
        {
            return Problem(statusCode: (int)HttpStatusCode.PaymentRequired, detail: "Insufficient balance");
        }
        
        currentUser.Balance -= chapter.Price;
        userManager.UpdateAsync(currentUser).Wait();  
        
        chapter.Unlocked.Add(new UnlockedChapter() { UserId = userId, ChapterId = chapter.Id });
        
        ChapterRepository.Update(chapter);
        return Ok();
    }

    [HttpPut($"{{{nameof(key)}}}/chapters/unlock")]
    [Authorize]
    public ActionResult<IEnumerable<int>> UnlockChapters(ulong key, [FromBody] int[] chapterNumbers, [FromServices] UserManager<User> userManager)
    {
        var chapters = ChapterRepository.GetByComicIdAndIndexes(key.ToString(), chapterNumbers);
        if (chapters.Count == 0)
        {
            return NotFound();
        }
        var price = chapters.Sum(c => c.Price);

        if (price == 0)
        {
            ModelState.AddModelError("Chapters", "There are no locked chapters");
            return BadRequest(ModelState);
        }

        var currentUser = User.GetUser(userManager);
        
        if (currentUser.Balance < price)
        {
            return Problem(statusCode: (int)HttpStatusCode.PaymentRequired, detail: "Insufficient balance");
        }
        currentUser.Balance -= price;
        userManager.UpdateAsync(currentUser).Wait();
        var unlockedChapters = new List<int>();
        foreach (var chapter in chapters)
        {
            if (chapter.Unlocked.Any(u => u.UserId == currentUser.Id))
            {
                continue;
            }
            chapter.Unlocked.Add(new UnlockedChapter() { UserId = currentUser.Id, ChapterId = chapter.Id });
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