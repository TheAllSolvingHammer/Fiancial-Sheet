using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ProektTSPGlaven.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet] 
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpGet]
        public IActionResult Accounts()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddAccount()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Stats()
        {
            return View();
        }
    }
}
