using Microsoft.AspNetCore.Identity;
using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

public class Role(string name) : IdentityRole<long>(name), IEntity;