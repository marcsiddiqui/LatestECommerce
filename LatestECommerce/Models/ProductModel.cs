using Microsoft.AspNetCore.Mvc.Rendering;

namespace LatestECommerce.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }

        public bool Deleted { get; set; }
    }
}
