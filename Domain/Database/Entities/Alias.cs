﻿using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Database.Entities;

public class Alias : BaseEntity, IEntity
{
    public string Name { get; set; } = default!;
    public virtual ICollection<Comic> Comics { get; set; } = new List<Comic>();
}