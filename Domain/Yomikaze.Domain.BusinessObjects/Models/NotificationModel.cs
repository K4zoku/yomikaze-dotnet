namespace Yomikaze.Domain.Models;

public class NotificationModel
{
    #region ReadWriteProperties

    [Required]
    public string? Title { get; set; }
    
    public string? Content { get; set; }
    
    public bool Read { get; set; }

    #endregion
    
    #region WriteOnlyProperties
    
    [Required]  
    [SwaggerSchema(WriteOnly = true)]
    public string UserId { get; set; } = default!; 
    
    #endregion
}