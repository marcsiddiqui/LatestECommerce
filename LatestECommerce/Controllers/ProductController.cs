using LatestECommerce.DbConfig;
using LatestECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

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

            return View(model);
        }

        public IActionResult Create()
        {
            // object initialization
            var model = new ProductModel();

            model.CategoryName = "";

            PrepareAvailableCategories(model);

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ProductModel model, IFormFile photo)
        {
            if (true)
            {
                var product = new Product();
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.StockQuantity = model.StockQuantity;
                product.CategoryId = model.CategoryId;

                _context.Products.Add(product);
                _context.SaveChanges();

                if (photo != null)
                {
                    var fileName = "Content\\Product\\" + product.Id.ToString() + "_" + product.Name + "_" + photo.FileName;

                    UploadImage(fileName, photo);

                    product.ImagePath = fileName;
                    _context.SaveChanges();
                }

                return RedirectToAction("Edit", new { id = product.Id });
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
            model.ImagePath = "https://localhost:7176/" + product.ImagePath;

            model.CategoryName = "";

            PrepareAvailableCategories(model);
            PrepareAvailableVariantKeys(model);
            PrepareAvailableVariantSizes(model);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ProductModel model)
        {
            
            if (true)
            {
                var product = _context.Products.FirstOrDefault(x => x.Id == model.Id);
                if (product == null)
                    return RedirectToAction("Index");

                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.StockQuantity = model.StockQuantity;
                product.StockQuantity = model.StockQuantity;
                product.CategoryId = model.CategoryId;

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

        public void PrepareAvailableCategories(ProductModel model)
        {
            var categories = _context.Categories.Where(x => x.IsActive.Value).ToList();
            if (categories != null && categories.Any())
            {
                model.AvailableCategories.Add(new SelectListItem { Text = "Select Category", Value = "0" });

                foreach (var cat in categories)
                {
                    model.AvailableCategories.Add(new SelectListItem { Text = cat.Name, Value = cat.Id.ToString(), Selected = model.CategoryId == cat.Id });
                }
            }
        }

        public void PrepareAvailableVariantKeys(ProductModel model)
        {
            model.AvailableVariantKeys.Add(new SelectListItem { Text = "Select Variant Key", Value = "0" });
            model.AvailableVariantKeys.Add(new SelectListItem { Text = "Color", Value = "Color" });
            model.AvailableVariantKeys.Add(new SelectListItem { Text = "Size", Value = "Size" });
        }

        public void PrepareAvailableVariantSizes(ProductModel model)
        {
            model.AvailableSizes.Add(new SelectListItem { Text = "X-Small", Value = "X-Small" });
            model.AvailableSizes.Add(new SelectListItem { Text = "Small", Value = "Small" });
            model.AvailableSizes.Add(new SelectListItem { Text = "Medium", Value = "Medium" });
            model.AvailableSizes.Add(new SelectListItem { Text = "Large", Value = "Large" });
            model.AvailableSizes.Add(new SelectListItem { Text = "X-Large", Value = "X-Large" });
            model.AvailableSizes.Add(new SelectListItem { Text = "XX-Large", Value = "XX-Large" });
        }

        [HttpPost]
        public IActionResult SaveVariant(int productId, string VariantKey, string VariantValue)
        {
            if (!string.IsNullOrWhiteSpace(VariantKey) && !string.IsNullOrWhiteSpace(VariantValue))
            {
                var existingVariant = _context.ProductVariants.Where(x => x.Key == VariantKey && x.Value == VariantValue).ToList();
                if (existingVariant != null && existingVariant.Any())
                    return Json(new { Success = false, Message = "Already Exists!" });

                var productVariant = new ProductVariant();
                productVariant.ProductId = productId;
                productVariant.Key = VariantKey;
                productVariant.Value = VariantValue;

                _context.ProductVariants.Add(productVariant);
                _context.SaveChanges();
            }

            List<ProductVariantModel> productVariantModels = new List<ProductVariantModel>();

            var allVariants = _context.ProductVariants.Where(x => x.ProductId == productId).ToList();
            if (allVariants != null && allVariants.Any())
            {
                foreach (var variant in allVariants)
                {
                    ProductVariantModel productVariantModel = new ProductVariantModel();
                    productVariantModel.Key = variant.Key;
                    productVariantModel.Value = variant.Value;
                    productVariantModel.Id = variant.Id;
                    productVariantModel.ProductId = variant.ProductId;
                    
                    productVariantModels.Add(productVariantModel);
                }
            }

            return Json(new { Success = true, VariantList = productVariantModels });
        }

        public async Task UploadImage(string fileName, IFormFile photo, bool editMode = false, string oldFileName = "")
        {
            if (editMode)
            {
                var oldPath = Path.Combine(_hostEnvironment.WebRootPath, oldFileName);
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }

            var path = Path.Combine(_hostEnvironment.WebRootPath, fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
                stream.Close();
            }
        }
    }
}