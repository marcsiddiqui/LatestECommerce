using Microsoft.AspNetCore.Mvc.Rendering;

namespace LatestECommerce.Models
{
    public class AuthModel
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
