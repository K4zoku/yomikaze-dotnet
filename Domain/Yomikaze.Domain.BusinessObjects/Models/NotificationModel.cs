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
    public string UserId { get; set; }
    
    #endregion
}