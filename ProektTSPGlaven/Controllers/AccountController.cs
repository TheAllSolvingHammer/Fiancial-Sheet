using Microsoft.AspNetCore.Mvc;
using ProektTSPGlaven.Models.Database;
using ProektTSPGlaven.Models.Builder;

using ProektTSPGlaven.Models.Account;
using ProektTSPGlaven.Models.Session;

namespace ProektTSPGlaven.Controllers
{
    public class AccountController : Controller
    {
        private readonly FinancesContext financesContext;
        private readonly ILogger<AccountController> logger;
       

        public AccountController(FinancesContext financesContext, ILogger<AccountController> logger)
        {
            this.financesContext = financesContext;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(AccountModel model)
        {
            if (!ModelState.IsValid)
                return View("Account", model);
            if (model.balance<0)
            {
                ModelState.AddModelError("", "Balance can not be less than zero");
                return View("Account", model);
            }
            try
            {
                LoggedUser loggedUser = HttpContext.Session.GetObject<LoggedUser>("LoggedUser");
                if (loggedUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                User u = loggedUser.getUser();
                Console.WriteLine(u);
                Account a = new AccountBuilder()
                    .withName(model.name)
                    .withBalance(model.balance)
                    .withUser(u)
                    .withUserId(u.userID)
                    .build();

                financesContext.accounts.Add(a);
                financesContext.SaveChanges();

                TempData["SuccessMessage"] = $"Account '{model.name}' created successfully!";

                return RedirectToAction("Dashboard", "Dashboard");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating account");
                ModelState.AddModelError("", "An error occurred while creating the account");
                return View("Account", model);
            }
        }
    }
}
