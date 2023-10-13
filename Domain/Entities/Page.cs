using System.ComponentModel.DataAnnotations.Schema;
using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

[Table("Page")]
public class Page : BaseEntity<long>
{
    public int Index { get; set; }
    
    public short Server { get; set; } = 0;
    
    public string Image { get; set; } = default!;
    
    [ForeignKey(nameof(Chapter))]
    public long ChapterId { get; set; }
    public Chapter Chapter { get; set; } = default!;
}