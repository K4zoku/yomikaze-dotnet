using System.ComponentModel.DataAnnotations.Schema;
using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

[Table("Page")]
public class Page : BaseEntity, IEntity
{
    public int Index { get; set; }
    
    public short Server { get; set; }
    
    public string Image { get; set; } = default!;
    
    public virtual Chapter Chapter { get; set; } = default!;
}