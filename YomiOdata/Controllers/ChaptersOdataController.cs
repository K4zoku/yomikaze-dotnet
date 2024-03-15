﻿using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Domain.Entities;
using YomiOdata.Base;

namespace YomiOdata.Controllers;

public class ChaptersOdataController(DbContext dbContext) : ODataControllerBase<Chapter>(dbContext, new ChapterRepo(dbContext))
{
}
