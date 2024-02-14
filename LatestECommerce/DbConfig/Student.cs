using System;
using System.Collections.Generic;

namespace LatestECommerce.DbConfig;

public partial class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;
}
