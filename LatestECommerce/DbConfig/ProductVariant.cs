using System;
using System.Collections.Generic;

namespace LatestECommerce.DbConfig;

public partial class ProductVariant
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;
}
