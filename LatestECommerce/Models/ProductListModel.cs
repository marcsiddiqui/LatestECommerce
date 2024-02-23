namespace LatestECommerce.Models
{
    public class ProductListModel
    {
        public ProductListModel()
        {
            ProductModels = new List<ProductModel>();
        }

        public List<ProductModel> ProductModels { get; set; }

        public int CartCount { get; set; }

        public bool IsAdmin { get; set; }

        public string RawHtml { get; set; }
    }
}
