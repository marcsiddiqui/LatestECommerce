using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LatestECommerce.DbConfig;

public partial class Customer
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber1 { get; set; } = null!;

    public string? PhoneNumber2 { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool Deleted { get; set; }
}
