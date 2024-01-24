namespace LatestECommerce.Models
{
    public class CategoryListModel
    {
        public CategoryListModel()
        {
            CategoryModels = new List<CategoryModel>();
        }

        public List<CategoryModel> CategoryModels { get; set; }
    }
}
