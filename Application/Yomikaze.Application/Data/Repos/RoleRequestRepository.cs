using Yomikaze.Application.Data.Access;

namespace Yomikaze.Application.Data.Repos;

public class RoleRequestRepository(DbContext dbContext) : BaseRepository<RoleRequest>(new RoleRequestDao(dbContext));