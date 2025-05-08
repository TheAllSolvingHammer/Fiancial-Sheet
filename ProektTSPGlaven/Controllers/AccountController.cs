using Microsoft.AspNetCore.Mvc;
using ProektTSPGlaven.Models.Database;
using ProektTSPGlaven.Models.Builder;
using ProektTSPGlaven.Models.Session;
using Microsoft.EntityFrameworkCore;
using ProektTSPGlaven.Models.AccountModel;

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

                var trackedUser = financesContext.users.Find(loggedUser.user.userID);
                if (trackedUser == null)
                {
                    ModelState.AddModelError("", "User not found");
                    return View("Account", model);
                }

                Account a = new AccountBuilder()
                    .withName(model.name)
                    .withBalance(model.balance)
                    .withUser(trackedUser)
                    .build();

                financesContext.accounts.Add(a);
                financesContext.SaveChanges();

                TempData["SuccessMessage"] = $"Account '{model.name}' created successfully!";

                return RedirectToAction("Accounts", "Dashboard");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating account");
                ModelState.AddModelError("", "An error occurred while creating the account");
                return View("Account", model);
            }
        }

        [HttpPost]
        public IActionResult Delete(int accountId)
        {
            Console.WriteLine("We eneterd");
            try
            {
                LoggedUser loggedUser = HttpContext.Session.GetObject<LoggedUser>("LoggedUser");
                if (loggedUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                var trackedUser = financesContext.users.Find(loggedUser.user.userID);
                if (trackedUser == null)
                {
                    ModelState.AddModelError("", "User not found");
                }
                var acc=financesContext.accounts.Find(accountId);

                financesContext.accounts.Remove(acc);
                financesContext.SaveChanges();

                TempData["SuccessMessage"] = $"Account '{acc.name}' deleted successfully!";
                return RedirectToAction("Accounts", "Dashboard");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting account");
                ModelState.AddModelError("", "An error occurred while deleting the account");
                return RedirectToAction("Accounts", "Dashboard");
            }
        }
    }
}
