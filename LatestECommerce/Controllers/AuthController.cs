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
    public class AuthController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EcommerceContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AuthController(ILogger<HomeController> logger, EcommerceContext context, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var model = new CustomerListModel();

            var customers = _context.Customers.Where(x => !x.Deleted).ToList();

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

                    if (!string.IsNullOrWhiteSpace(customer.ImagePath))
                    {
                        customerModel.ImagePath = customer.ImagePath;
                    }
                    else
                    {
                        customerModel.ImagePath = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png";
                    }


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
        public IActionResult Create(CustomerModel model, IFormFile photo)
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

                if (photo != null)
                {
                    var fileName = "Content\\Customer\\" + customer.Id.ToString() + "_" + customer.FullName + "_" + photo.FileName;

                    UploadImage(fileName, photo);

                    customer.ImagePath = fileName;
                    _context.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Id == id && !x.Deleted);
            if (customer == null)
                return RedirectToAction("Index");

            // object initialization
            var model = new CustomerModel();

            model.Id = customer.Id;
            model.FullName = customer.FullName;
            model.Email = customer.Email;
            model.PhoneNumber1 = customer.PhoneNumber1;
            model.PhoneNumber2 = customer.PhoneNumber2;
            model.Username = customer.Username;
            model.Password = customer.Password;
            model.ImagePath = "https://localhost:7176/" + customer.ImagePath;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CustomerModel model, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                var customer = _context.Customers.FirstOrDefault(x => x.Id == model.Id && !x.Deleted);
                if (customer == null)
                    return RedirectToAction("Index");

                customer.FullName = model.FullName;
                customer.Email = model.Email;
                customer.PhoneNumber1 = model.PhoneNumber1;
                customer.PhoneNumber2 = model.PhoneNumber2;
                customer.Username = model.Username;
                customer.Password = model.Password;

                _context.SaveChanges();

                if (photo != null)
                {
                    var fileName = "Content\\Customer\\" + customer.Id.ToString() + "_" + customer.FullName + "_" + photo.FileName;

                    UploadImage(fileName, photo, true, customer.ImagePath);

                    customer.ImagePath = fileName;
                    _context.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Id == id && !x.Deleted);
            if (customer == null)
                return RedirectToAction("Index");

            customer.Deleted = true;
            _context.SaveChanges();

            return RedirectToAction("Index");
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AuthModel model)
        {
            if (model == null || !ModelState.IsValid || string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
                return View(model);

            var customer = _context.Customers.FirstOrDefault(x => 
            (x.Username == model.Username || x.Email == model.Username) 
            &&
            x.Password == model.Password && !x.Deleted);

            if (customer == null)
                return View(model);

            CookieOptions co = new CookieOptions();
            co.Expires = DateTime.Now.AddMinutes(5);

            Response.Cookies.Append("AuthenticatedCustomer", customer.Id.ToString(), co);

            return RedirectToAction("Index", "Home");
        }
    }
}