using Microsoft.AspNetCore.Mvc.Rendering;

namespace LatestECommerce.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber1 { get; set; } = null!;

        public string? PhoneNumber2 { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool Deleted { get; set; }

        public string? ImagePath { get; set; }
    }
}
