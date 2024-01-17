using LatestECommerce.DbConfig;
using LatestECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LatestECommerce.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EcommerceContext _context;

        public CustomerController(ILogger<HomeController> logger, EcommerceContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new CustomerListModel();

            var customers = _context.Customers.ToList();

            if (customers != null && customers.Any())
            {
                foreach (var customer in customers)
                {
                    var customerModel = new CustomerModel();
                    customerModel.Id = customer.Id;
                    customerModel.FullName = customer.FullName;
                    customerModel.Email = customer.Email;
                    customerModel.PhoneNumber1 = customer.PhoneNumber1;
                    customerModel.PhoneNumber2 = customer.PhoneNumber2;
                    customerModel.Username = customer.Username;
                    customerModel.Password = customer.Password;
                    customerModel.Deleted = customer.Deleted;

                    model.CustomerModels.Add(customerModel);
                }
            }

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer();
                customer.FullName = model.FullName;
                customer.Email = model.Email;
                customer.PhoneNumber1 = model.PhoneNumber1;
                customer.PhoneNumber2 = model.PhoneNumber2;
                customer.Username = model.Username;
                customer.Password = model.Password;

                _context.Customers.Add(customer);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}