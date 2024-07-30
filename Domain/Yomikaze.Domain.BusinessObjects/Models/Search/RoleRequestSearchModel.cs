using Yomikaze.Domain.Entities;

namespace Yomikaze.Domain.Models.Search;

public class RoleRequestSearchModel
{
    public RoleRequestStatus? Status { get; set; }
    
    public RoleRequestOrderBy OrderBy { get; set; } = RoleRequestOrderBy.CreationTime;
}

public enum RoleRequestOrderBy
{
    CreationTime,
    CreationTimeDesc,
}