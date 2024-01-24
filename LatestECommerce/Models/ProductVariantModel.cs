using Microsoft.AspNetCore.Mvc.Rendering;

namespace LatestECommerce.Models
{
    public class ProductVariantModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string Key { get; set; } = null!;

        public string Value { get; set; } = null!;
    }
}
