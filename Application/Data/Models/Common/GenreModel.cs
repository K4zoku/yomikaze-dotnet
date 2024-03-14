﻿using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Models.Common;

public class GenreModel
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public static explicit operator GenreModel(Genre genre)
    {
        return new GenreModel { Id = genre.Id, Name = genre.Name, Description = genre.Description };
    }
}