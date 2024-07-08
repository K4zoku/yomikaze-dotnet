using Microsoft.AspNetCore.JsonPatch;
using Yomikaze.Application.Helpers.API;

namespace Yomikaze.API.Main.Controllers;

// TODO)) Check comics ownership for creating, updating and deleting chapters
// TODO)) Return isUnlocked & isRead for chapters, only if user is authenticated
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

        ChapterRepository.Add(chapter);

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

        if (User.Identity?.IsAuthenticated ?? false)
        {
            HistoryRepository.Add(new HistoryRecord { ChapterId = chapter.Id, UserId = User.GetId() });
        }

        return Ok(Mapper.Map<ChapterModel>(chapter));
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