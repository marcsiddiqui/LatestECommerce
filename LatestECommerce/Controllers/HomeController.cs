using LatestECommerce.DbConfig;
using LatestECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LatestECommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EcommerceContext _context;

        public HomeController(ILogger<HomeController> logger, EcommerceContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new ProductListModel();

            var products = _context.Products.Where(x => !x.Deleted).ToList();

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
    }
}