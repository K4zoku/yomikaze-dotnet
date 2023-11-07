using Microsoft.AspNetCore.Mvc;
using Yomikaze.Application.Data.Models;
using Yomikaze.Application.Data.Models.Common;
using Yomikaze.Application.Data.Models.Response;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Constants;
using Yomikaze.Domain.Database.Entities;

namespace Yomikaze.WebAPI.Controllers;

[Route($"API/{Api.Version}/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IDao<Genre> _genreDao;

    public GenresController(IDao<Genre> genreDao)
    {
        _genreDao = genreDao;
    }

    [HttpGet]
    public async Task<ActionResult<ResponseModel<IEnumerable<GenreModel>>>> GetGenres()
    {
        var genres = await _genreDao.GetAllAsync();
        return Ok(ResponseModel.CreateSuccess(genres.Select(g => g.ToModel())));
    }

    [HttpGet]
    [Route("Create")]
    public async Task<ActionResult<ResponseModel<GenreModel>>> CreateGenre([FromBody, Bind($"{nameof(GenreModel.Name)},{nameof(GenreModel.Description)}")] GenreModel model)
    {
        var genre = new Genre
        {
            Name = model.Name,
            Description = model.Description
        };
        await _genreDao.AddAsync(genre);
        return Ok(ResponseModel.CreateSuccess("Create genre successfully", genre.ToModel()));
    }


}
