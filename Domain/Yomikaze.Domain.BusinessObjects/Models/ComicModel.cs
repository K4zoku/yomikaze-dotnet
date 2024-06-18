﻿namespace Yomikaze.Domain.Models;

public class ComicInputModel
{
    [Required]
    [Length(0, 150, ErrorMessage = "Comic's name must from 0 to 150 characters")]
    public string Name { get; set; } = default!;

    [Length(0, 500, ErrorMessage = "Comic's description must from 0 to 500 characters")]
    public string? Description { get; set; }

    public string? Cover { get; set; }

    public string? Banner { get; set; }

    public DateTimeOffset? Published { get; set; }

    public DateTimeOffset? Ended { get; set; }

    [Length(0, 150, ErrorMessage = "Comic's aliases must from 0 to 150 characters")]
    public string? Aliases { get; set; }

    [Length(0, 70, ErrorMessage = "Comic's authors must from 0 to 150 characters")]
    public string? Authors { get; set; }
    public ICollection<ComicGenreInputModel> ComicGenres { get; set; } = new List<ComicGenreInputModel>();
    
    public IList<ChapterIndexInputModel> Chapters { get; set; } = new List<ChapterIndexInputModel>();
}

public class ComicOutputModel
{
    public string Id { get; set; }

    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public string? Cover { get; set; }

    public string? Banner { get; set; }

    public DateTimeOffset? Published { get; set; }

    public DateTimeOffset? Ended { get; set; }

    public string? Aliases { get; set; }

    public string? Authors { get; set; }
    
    public ICollection<ComicGenreOutputModel> ComicGenres { get; set; } = new List<ComicGenreOutputModel>();
    
    public ICollection<ChapterOutputModel> Chapters { get; set; } = new List<ChapterOutputModel>();
    
    public DateTimeOffset LastUpdated { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}