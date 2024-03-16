using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yomikaze.API.Main.Base;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Models;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator")]
public class ComicsController(DbContext dbContext, IMapper mapper)
    : CrudControllerBase<Comic, ComicInputModel, ComicOutputModel>(dbContext, mapper, new ComicRepo(dbContext))
{
    private new ComicRepo Repository => (ComicRepo)base.Repository;
    private ChapterRepo ChapterRepo => new ChapterRepo(DbContext);

    [HttpPost]
    public override ActionResult<ComicOutputModel> Post(ComicInputModel input)
    {
        CheckModelState();

        Comic? entity = Mapper.Map<Comic>(input);
        Repository.Add(entity);
        return Ok(Mapper.Map<ComicOutputModel>(entity));
    }

    [HttpPut("{key}")]
    public override ActionResult<ComicOutputModel> Put(long key, ComicInputModel input)
    {
        CheckModelState();

        Comic? entityToUpdate = Repository.Get(key);
        CheckEntity(entityToUpdate);

        Mapper.Map(input, entityToUpdate);
        Repository.Update(entityToUpdate);

        return Ok(Mapper.Map<ComicOutputModel>(entityToUpdate));
    }


    [HttpDelete("{key}")]
    public override ActionResult Delete(long key)
    {
        Comic? entity = Repository.Get(key);
        CheckEntity(entity);

        Repository.Delete(entity);
        return Ok();
    }

    // get chapter by comic id
    [HttpGet("{comicId}/Chapters")]
    [AllowAnonymous]
    public ActionResult<IEnumerable<ChapterOutputModel>> GetChapters(long comicId)
    {
        var comic = Repository.GetChaptersByComicId(comicId);
        CheckEntity(comic);

        return Ok(Mapper.Map<IEnumerable<ChapterOutputModel>>(comic.Chapters));
    }

    // get chapter by comic id and index
    [HttpGet("{comicId}/Chapters/{index}")]
    [AllowAnonymous]
    public ActionResult<ChapterOutputModel> GetChapter(long comicId, int index)
    {
        var chapter = ChapterRepo.GetByComicIdAndIndex(comicId, index);
        CheckEntity(chapter);

        return Ok(Mapper.Map<ChapterOutputModel>(chapter));
    }
}