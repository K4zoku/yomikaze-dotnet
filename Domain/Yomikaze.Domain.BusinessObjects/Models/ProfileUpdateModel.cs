namespace Yomikaze.Domain.Models;

public class ProfileUpdateModel
{
    public string? Name { get; set; }
    
    public string? Bio { get; set; }     
    
    public string? Avatar { get; set; }
    
    public string? Banner { get; set; }
    
    public DateTime? Birthday { get; set; }
}