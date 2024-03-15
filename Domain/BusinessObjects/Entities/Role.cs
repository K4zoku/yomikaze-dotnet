using Abstracts;
using Microsoft.AspNetCore.Identity;

namespace Yomikaze.Domain.Entities;

public class Role(string name) : IdentityRole<long>(name), IEntity;