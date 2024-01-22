using LatestECommerce.DbConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LatestECommerce
{
    public class LoginOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!IsCustomerLoggedIn(context))
            {
                var values = new RouteValueDictionary(new
                {
                    action = "Login",
                    controller = "Auth"
                });
                context.Result = new RedirectToRouteResult(values);
            }
        }

        public bool IsCustomerLoggedIn(ActionExecutingContext context)
        {
            if (context == null)
                return false;

            if (context.HttpContext.Request.Cookies["AuthenticatedCustomer"] != null)
            {
                var customerId = Convert.ToInt32(context.HttpContext.Request.Cookies["AuthenticatedCustomer"]);

                if (customerId == 0)
                    return false;

                //EcommerceContext _dbContext = new EcommerceContext();

                //var customer = _dbContext.Customers.FirstOrDefault(x => x.Id == customerId);
                //if (customer == null)
                //    return false;

                return true;
            }

            return false;
        }
    }
}
