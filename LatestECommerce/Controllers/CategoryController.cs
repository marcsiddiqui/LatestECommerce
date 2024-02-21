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
    [TypeFilter(typeof(AdminOnlyAttribute))]
    public class CategoryController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EcommerceContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CategoryController(ILogger<HomeController> logger, EcommerceContext context, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var model = new CategoryListModel();

            var categories = _context.Categories.ToList();

            if (categories != null && categories.Any())
            {
                foreach (var category in categories)
                {
                    var categoryModel = new CategoryModel();
                    categoryModel.Id = category.Id;
                    categoryModel.Name = category.Name;
                    categoryModel.IsActive = category.IsActive;

                    model.CategoryModels.Add(categoryModel);
                }
            }

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category();
                category.Name = model.Name;

                _context.Categories.Add(category);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
                return RedirectToAction("Index");

            // object initialization
            var model = new CategoryModel();

            model.Id = category.Id;
            model.Name = category.Name;
            model.IsActive = category.IsActive;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category = _context.Categories.FirstOrDefault(x => x.Id == model.Id);
                if (category == null)
                    return RedirectToAction("Index");

                category.Name = model.Name;
                category.IsActive = model.IsActive;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
                return RedirectToAction("Index");

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}