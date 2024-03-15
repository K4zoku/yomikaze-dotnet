using System.ComponentModel.DataAnnotations;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Models.Common;

public class  GenreInputModel
{
    [Required]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }
}

public class GenreOutputModel
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;

    public string? Description { get; set; }
}