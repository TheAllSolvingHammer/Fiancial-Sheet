using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProektTSPGlaven.Models.Database;
using ProektTSPGlaven.Models.Session;
using Microsoft.EntityFrameworkCore;

namespace ProektTSPGlaven.Controllers
{
    public class DashboardController : Controller
    {
        private readonly FinancesContext financesContext;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger, FinancesContext financesContext)
        {
            _logger = logger;
            this.financesContext = financesContext;
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
            LoggedUser loggedUser = HttpContext.Session.GetObject<LoggedUser>("LoggedUser");
            if (loggedUser == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var accounts = financesContext.accounts
                    .Where(a => a.userID == loggedUser.user.userID)
                    .ToList();
            return View(accounts);
        }

        [HttpGet]
        public IActionResult AddAccount()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Stats()
        {
            LoggedUser loggedUser = HttpContext.Session.GetObject<LoggedUser>("LoggedUser");
            if (loggedUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var accounts = financesContext.accounts
                .Where(a => a.userID == loggedUser.user.userID)
                .Select(a => new
                {
                    a.accountID,
                    a.name,
                    a.balance
                }).ToList();

            ViewBag.AccountLabels = accounts.Select(a => a.name).ToList();
            ViewBag.AccountBalances = accounts.Select(a => a.balance).ToList();
            ViewBag.AccountIds = accounts.Select(a => a.accountID).ToList();

            return View();
        }
    }
}
