using System;
using System.Collections.Generic;

namespace LatestECommerce.DbConfig;

public partial class CustomerRoleMapping
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int CustomerRoleId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual CustomerRole CustomerRole { get; set; } = null!;
}
