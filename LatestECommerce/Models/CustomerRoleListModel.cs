namespace LatestECommerce.Models
{
    public class CustomerRoleListModel
    {
        public CustomerRoleListModel()
        {
            CustomerRoleModels = new List<CustomerRoleModel>();
        }

        public List<CustomerRoleModel> CustomerRoleModels { get; set; }
    }
}
