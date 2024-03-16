using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yomikaze.Domain.Models;
public class ComicGenreInputModel
{
    public long GenreId { get; set; }
}

public class ComicGenreOutputModel
{
    public GenreOutputModel Genres { get; set; } = default!;
}
