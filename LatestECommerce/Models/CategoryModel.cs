using Microsoft.AspNetCore.Mvc.Rendering;

namespace LatestECommerce.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public bool? IsActive { get; set; }
    }
}
