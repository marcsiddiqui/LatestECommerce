using System;
using System.Collections.Generic;

namespace LatestECommerce.DbConfig;

public partial class CustomerRole
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<CustomerRoleMapping> CustomerRoleMappings { get; set; } = new List<CustomerRoleMapping>();
}
