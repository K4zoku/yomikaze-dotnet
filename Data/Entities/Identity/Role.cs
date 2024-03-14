using Microsoft.AspNetCore.Identity;
using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities.Identity;

public class Role(string name) : IdentityRole<long>(name), IEntity;