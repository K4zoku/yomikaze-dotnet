using System.ComponentModel.DataAnnotations.Schema;
using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

public class History : BaseEntity, IEntity
{
    public virtual Chapter Chapter { get; set; } = default!;
    
    public virtual Profile Profile { get; set; } = default!;
    
    public DateTimeOffset ReadAt { get; set; }
    
}