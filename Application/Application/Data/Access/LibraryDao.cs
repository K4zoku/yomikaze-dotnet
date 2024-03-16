using Abstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Access;
public class LibraryDao(DbContext dbContext) : BaseDao<LibraryEntry>(dbContext)
{
    public override IQueryable<LibraryEntry> Query()
    {
        return base.Query()
            .Include(library => library.Comic);
    }
}
