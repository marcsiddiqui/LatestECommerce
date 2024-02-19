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

        public IActionResult Login()
        {
            AuthModel model = new AuthModel();

            model.Username = "ma";
            model.Password = "22";

            return View(model);
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
            co.Expires = DateTime.Now.AddMinutes(15);

            Response.Cookies.Append("AuthenticatedCustomer", customer.Id.ToString(), co);

            return RedirectToAction("Index", "Home");
        }
    }
}