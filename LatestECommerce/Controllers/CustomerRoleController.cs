using LatestECommerce.DbConfig;
using LatestECommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace LatestECommerce.Controllers
{
    [LoginOnly]
    public class CustomerRoleController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EcommerceContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CustomerRoleController(ILogger<HomeController> logger, EcommerceContext context, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var model = new CustomerRoleListModel();

            var customerRoles = _context.CustomerRoles.ToList();

            if (customerRoles != null && customerRoles.Any())
            {
                foreach (var customerRole in customerRoles)
                {
                    var customerRoleModel = new CustomerRoleModel();
                    customerRoleModel.Id = customerRole.Id;
                    customerRoleModel.Name = customerRole.Name;
                    customerRoleModel.IsActive = customerRole.IsActive;

                    model.CustomerRoleModels.Add(customerRoleModel);
                }
            }

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerRoleModel model)
        {
            if (ModelState.IsValid)
            {
                var customerRole = new CustomerRole();
                customerRole.Name = model.Name;
                customerRole.IsActive = true;

                _context.CustomerRoles.Add(customerRole);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var customerRole = _context.CustomerRoles.FirstOrDefault(x => x.Id == id);
            if (customerRole == null)
                return RedirectToAction("Index");

            // object initialization
            var model = new CustomerRoleModel();

            model.Id = customerRole.Id;
            model.Name = customerRole.Name;
            model.IsActive = customerRole.IsActive;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CustomerRoleModel model)
        {
            if (ModelState.IsValid)
            {
                var customerRole = _context.CustomerRoles.FirstOrDefault(x => x.Id == model.Id);
                if (customerRole == null)
                    return RedirectToAction("Index");

                customerRole.Name = model.Name;
                customerRole.IsActive = model.IsActive;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var customerRole = _context.CustomerRoles.FirstOrDefault(x => x.Id == id);
            if (customerRole == null)
                return RedirectToAction("Index");

            _context.CustomerRoles.Remove(customerRole);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}