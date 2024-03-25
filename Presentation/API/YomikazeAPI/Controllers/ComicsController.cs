using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Yomikaze.API.Main.Base;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Application.Helpers;
using Yomikaze.Application.Helpers.API;
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
    private ChapterRepo ChapterRepo => new(DbContext);
    private HistoryRepo HistoryRepo => new(DbContext);

    [HttpPost]
    public override ActionResult<ComicOutputModel> Post(ComicInputModel input)
    {
        CheckModelState();

        Comic? entity = Mapper.Map<Comic>(input);
        Repository.Add(entity);
        return Ok(Mapper.Map<ComicOutputModel>(entity));
    }

    [HttpPut("{key}")]
    public override ActionResult<ComicOutputModel> Put(string key, ComicInputModel input)
    {
        CheckModelState();
        // update only index
        foreach (var t in input.Chapters)
        {
            var chapter = ChapterRepo.Get(t.Id);
            if (chapter == null)
            {
                continue;
            }
            chapter.Index = t.Index;
            ChapterRepo.Update(chapter);
        }
        input.Chapters = null;
        
        Comic? entityToUpdate = Repository.Get(key);
        CheckEntity(entityToUpdate);
        Mapper.Map(input, entityToUpdate);
        Repository.Update(entityToUpdate);

        return Ok(Mapper.Map<ComicOutputModel>(entityToUpdate));
    }


    [HttpDelete("{key}")]
    public override ActionResult Delete(string key)
    {
        Comic? entity = Repository.Get(key);
        CheckEntity(entity);

        Repository.Delete(entity);
        return Ok();
    }

    // get chapter by comic id
    [HttpGet("{comicId}/Chapters")]
    [AllowAnonymous]
    public ActionResult<IEnumerable<ChapterOutputModel>> GetChapters(string comicId)
    {
        Comic? comic = Repository.GetChaptersByComicId(comicId);
        CheckEntity(comic);

        return Ok(Mapper.Map<IEnumerable<ChapterOutputModel>>(comic.Chapters));
    }

    // get chapter by comic id and index
    [HttpGet("{comicId}/Chapters/{index}")]
    [AllowAnonymous]
    public ActionResult<ChapterOutputModel> GetChapter(string comicId, int index)
    {
        Chapter? chapter = ChapterRepo.GetByComicIdAndIndex(comicId, index);
        CheckEntity(chapter);

        // check if user is logged in then add history
        if (User.Identity?.IsAuthenticated == true)
        {
            string id = User.GetId();
            HistoryRecord history = new HistoryRecord { UserId = id, ChapterId = chapter.Id };
            HistoryRepo.Add(history);
        }

        return Ok(Mapper.Map<ChapterOutputModel>(chapter));
    }
    
    [HttpGet("{key}")]
    public override ActionResult<ComicOutputModel> Get(string key)
    {
        Comic? entity = Repository.Get(key);

        if (entity == null)
        {
            throw new HttpResponseException(HttpStatusCode.NotFound, ResponseModel.CreateError("Not found"));
        }
        
        _ = entity.Chapters; // lazy load
        _ = entity.ComicGenres; // lazy load

        return Ok(Mapper.Map<ComicOutputModel>(entity));
    }

}