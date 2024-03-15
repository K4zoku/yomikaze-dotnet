using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yomikaze.Domain.Entities.Identity;
using Yomikaze.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Yomikaze.Application.Data.Models.Common;


public class HistoryRecordInputModel
{
    [Required]
    public long ChapterId { get; set; }

    [Required]
    public long UserId { get; set; }

    public DateTimeOffset LastRead { get; set; }

}

public class HistoryRecordOutputModel
{   
    public long Id { get; set; }

    public ChapterOutputModel Chapter { get; set; } = default!;

    public UserOutputModel User { get; set; } = default!;

    public DateTimeOffset LastRead { get; set; } = DateTimeOffset.Now;
}
