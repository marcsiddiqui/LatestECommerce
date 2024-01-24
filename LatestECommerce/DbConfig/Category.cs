using System;
using System.Collections.Generic;

namespace LatestECommerce.DbConfig;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsActive { get; set; }
}
