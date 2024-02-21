using LatestECommerce.DbConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace LatestECommerce
{
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        private readonly EcommerceContext _context;

        public AdminOnlyAttribute(EcommerceContext context)
        {
            _context = context;
        }

        // will this be run before action or after action
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!IsCustomerAdmin(context))
            {
                var values = new RouteValueDictionary(new
                {
                    action = "Index",
                    controller = "Home"
                });
                context.Result = new RedirectToRouteResult(values);
            }
        }

        // to return true/false
        // true -- all good
        // false -- not allowed
        public bool IsCustomerAdmin(ActionExecutingContext context)
        {
            if (context == null)
                return false;

            if (context.HttpContext.Request.Cookies["AuthenticatedCustomer"] != null)
            {
                var customerId = Convert.ToInt32(context.HttpContext.Request.Cookies["AuthenticatedCustomer"]);

                if (customerId == 0)
                    return false;

                // all the roles which are aissnged to the logged in customer
                var customerRoleMapper = _context.CustomerRoleMappings.Where(x => x.CustomerId == customerId).ToList();
                if (customerRoleMapper == null || !customerRoleMapper.Any())
                    return false;

                // all the roles ids that are assigned to logged in customer
                var roleIds = customerRoleMapper.Select(y => y.CustomerRoleId);

                // checking if any of the assigned role is like admin role
                var customerRoles = _context.CustomerRoles.Where(x => roleIds.Contains(x.Id)).ToList();
                if (customerRoles.Select(x => x.Name).Contains("Admin") || customerRoles.Select(x => x.Name).Contains("Manager"))
                    return true;
            }

            return false;
        }
    }
}
