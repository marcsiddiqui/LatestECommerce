using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LatestECommerce.Models
{
    public class ProductModel
    {
        public ProductModel()
        {
            AvailableCategories = new List<SelectListItem>();
            AvailableVariantKeys = new List<SelectListItem>();
            AvailableSizes = new List<SelectListItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }

        public bool Deleted { get; set; }

        public string? ImagePath { get; set; }

        public string CategoryName { get; set; } = null!;

        public List<SelectListItem> AvailableCategories { get; set; }

        public List<SelectListItem> AvailableVariantKeys { get; set; }

        public List<SelectListItem> AvailableSizes { get; set; }

        public string VariantKey { get; set; }
        public string VariantValue { get; set; }

        public string VariantSize { get; set; }
    }
}
