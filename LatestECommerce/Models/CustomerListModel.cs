namespace LatestECommerce.Models
{
    public class CustomerListModel
    {
        public CustomerListModel()
        {
            CustomerModels = new List<CustomerModel>();
        }

        public List<CustomerModel> CustomerModels { get; set; }
    }
}
