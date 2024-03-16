using Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yomikaze.Domain.Entities;
public class ComicGenre : BaseEntity
{
    public virtual long ComicId { get; set; }
    public virtual Comic Comic { get; set; } = default!;

    public virtual long GenreId { get; set; }
    public virtual Genre Genre { get; set; } = default!;
}
