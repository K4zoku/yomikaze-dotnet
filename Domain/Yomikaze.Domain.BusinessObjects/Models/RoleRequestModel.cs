using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Domain.Models;

public class RoleRequestModel : BaseModel
{
    [SwaggerIgnore]
    [SwaggerSchema(ReadOnly = true)]
    [ValidateNever]
    public string RoleId { get; set; } = default!;
    
    [SwaggerIgnore]
    [SwaggerSchema(ReadOnly = true)]
    [ValidateNever]
    public string UserId { get; set; } = default!;
    
    [ValidateNever]
    [SwaggerSchema(ReadOnly = true)]
    public string Role { get; set; } = default!;
    
    [ValidateNever]
    [SwaggerSchema(ReadOnly = true)]
    public ProfileModel User { get; set; } = default!;
    
    [Required]
    [StringLength(256)]
    public string Reason { get; set; } = default!;
    
    [SwaggerSchema(ReadOnly = true)]
    public RoleRequestStatus Status { get; set; } = RoleRequestStatus.Pending;
}