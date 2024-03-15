﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Repos;
public class CommentRepo(DbContext dbContext) : BaseRepo<Comment>(new CommentDao(dbContext))
{
}