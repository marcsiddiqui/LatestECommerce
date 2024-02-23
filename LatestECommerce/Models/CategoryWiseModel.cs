namespace LatestECommerce.Models
{
    public class CategoryWiseModel
    {
        public CategoryWiseModel()
        {
            CategoryGroupModels = new List<CategoryGroupModel>();
        }

        public class CategoryGroupModel
        {
            public CategoryGroupModel()
            {
                ProductModels = new List<ProductModel>();
            }
            public List<ProductModel> ProductModels { get; set; }
        }

        public List<CategoryGroupModel> CategoryGroupModels { get; set; }
    }
}
