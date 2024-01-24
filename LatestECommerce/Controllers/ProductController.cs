using LatestECommerce.DbConfig;
using LatestECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace LatestECommerce.Controllers
{
    [LoginOnly]
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EcommerceContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(ILogger<HomeController> logger, EcommerceContext context, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var model = new ProductListModel();

            var products = _context.Products.ToList();

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

                    model.ProductModels.Add(productModel);
                }
            }

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product();
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.StockQuantity = model.StockQuantity;
                product.CategoryId = model.CategoryId;

                _context.Products.Add(product);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
                return RedirectToAction("Index");

            // object initialization
            var model = new ProductModel();

            model.Id = product.Id;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.StockQuantity = product.StockQuantity;
            model.CategoryId = product.CategoryId;
            model.Deleted = product.Deleted;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _context.Products.FirstOrDefault(x => x.Id == model.Id);
                if (product == null)
                    return RedirectToAction("Index");

                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.StockQuantity = model.StockQuantity;
                product.StockQuantity = model.StockQuantity;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
                return RedirectToAction("Index");

            product.Deleted = true;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}