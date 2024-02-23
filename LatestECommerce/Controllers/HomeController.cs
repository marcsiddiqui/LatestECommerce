using AutoMapper;
using LatestECommerce.DbConfig;
using LatestECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static LatestECommerce.Models.CategoryWiseModel;

namespace LatestECommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EcommerceContext _context;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, EcommerceContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var model = new ProductListModel();

            var products = _context.Products.Where(x => !x.Deleted).ToList();

            IEnumerable<Product> proList =
                from p in products
                where p.Price > 5000
                select p;


            var groupedList = products.GroupBy(x => x.CategoryId).ToList();

            var categoryWiseModel = new CategoryWiseModel();

            var html = "";

            // 3
            foreach (var group in groupedList)
            {
                var categoryGroupModel = new CategoryGroupModel();

                html += $"<tr style='background-color: #f7444e;'><td>Category Id = {group.Key}</td></tr>";

                // 11       2       3
                foreach (var item in group)
                {
                    html += $"<tr style='background-color: #ffad99;'><td>Product Name = {item.Name}</td></tr>";

                    var proModel = _mapper.Map<ProductModel>(item);

                    categoryGroupModel.ProductModels.Add(proModel);
                }

                categoryWiseModel.CategoryGroupModels.Add(categoryGroupModel);
            }

            model.RawHtml = html;

            var categries = _context.Categories.ToList();

            if (products != null && products.Any())
            {
                foreach (var product in products)
                {
                    var productModel = new ProductModel();
                    productModel.Id = product.Id;
                    productModel.Name = product.Name;
                    productModel.Description = product.Description;
                    productModel.Price = product.Price;
                    productModel.StockQuantity = product.StockQuantity;
                    productModel.CategoryId = product.CategoryId;
                    productModel.Deleted = product.Deleted;

                    if (!string.IsNullOrWhiteSpace(product.ImagePath))
                    {
                        productModel.ImagePath = product.ImagePath;
                    }
                    else
                    {
                        productModel.ImagePath = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png";
                    }

                    productModel.CategoryName = "No-Category";

                    if (categries != null && categries.Any())
                    {
                        var category = categries.FirstOrDefault(x => x.Id == product.CategoryId);
                        if (category != null)
                        {
                            productModel.CategoryName = category.Name;
                        }
                    }

                    model.ProductModels.Add(productModel);
                }
            }

            var customerId = Convert.ToInt32(Request.Cookies["AuthenticatedCustomer"]);
            if (customerId > 0)
                model.CartCount = _context.Carts.Where(x => x.CustomerId == customerId).Count();

            model.IsAdmin = IsCustomerAdmin();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var customerId = Convert.ToInt32(Request.Cookies["AuthenticatedCustomer"]);
            if (customerId == 0)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (productId > 0)
            {
                var cart = new Cart();
                cart.ProductId = productId;
                cart.CustomerId = customerId;

                _context.Carts.Add(cart);
                _context.SaveChanges();

                var totalCount = _context.Carts.Where(x=>x.CustomerId == customerId).Count();

                return Json(new { Success = true, Message = "Product added to cart!", CartCount = totalCount });
            }
            else
            {
                // product not found
                return Json(new { Success = false, Message = "Product not found!" });
            }

            
        }

        // to return true/false
        // true -- all good
        // false -- not allowed
        public bool IsCustomerAdmin()
        {
            if (HttpContext.Request.Cookies["AuthenticatedCustomer"] != null)
            {
                var customerId = Convert.ToInt32(HttpContext.Request.Cookies["AuthenticatedCustomer"]);

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
                if (customerRoles.Select(x => x.Name).Contains("Admin"))
                    return true;
            }

            return false;
        }
    }
}