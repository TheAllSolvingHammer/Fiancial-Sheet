using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ProektTSPGlaven.Models.Builder;
using ProektTSPGlaven.Models.Database;
using ProektTSPGlaven.Models.Session;
using ProektTSPGlaven.Models.TransactionModel;

namespace ProektTSPGlaven.Controllers
{
    public class TransactionController : Controller
    {
        private readonly FinancesContext financesContext;
        private readonly ILogger<TransactionController> logger;
        private CustomTransaction custom;
        public TransactionController(FinancesContext financesContext, ILogger<TransactionController> logger)
        {
            this.financesContext = financesContext;
            this.logger = logger;
            custom= CustomTransaction.getInstance();
        }

        [HttpGet]
        public IActionResult Create(int accountId)
        {
            custom.accountId = accountId;
            return View();
        }

        [HttpPost]
        public IActionResult Create(TransactionModel model)
        {
            
            if (model.amount < 0)
            {
                ModelState.AddModelError("", "Amount can not be less than zero, even if it is expense");
                return RedirectToAction("Accounts","Dashboard");
            }
            try
            {
                LoggedUser loggedUser = HttpContext.Session.GetObject<LoggedUser>("LoggedUser");
                if (loggedUser == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                var trackedUser = financesContext.users.Find(loggedUser.user.userID);
                if (trackedUser == null)
                {
                    ModelState.AddModelError("", "User not found");
                    return RedirectToAction("Accounts", "Dashboard");
                }

                var account = financesContext.accounts.FirstOrDefault(a=>a.accountID==custom.accountId);
                if(account == null)
                {
                    ModelState.AddModelError("", "No account was found with such id");
                    return RedirectToAction("Accounts", "Dashboard");
                }
                Console.WriteLine("My account is %d\n", custom.accountId);
                if (model.type.Equals(TransactionType.Expense) && model.amount>account.balance)
                {
                    ModelState.AddModelError("", "Can not create expense that is bigger than accounts balance");
                    return RedirectToAction("Accounts", "Dashboard");
                }
                TransactionEntity t=new TransactionBuilder()
                    .withAmount(model.amount)
                    .withAccount(account)
                    .withCategory(model.category)
                    .withDescription(model.description)
                    .withType(model.type)
                    .build();


                financesContext.transactions.Add(t);
                if (t.type.Equals(TransactionType.Expense))
                {
                    account.balance = account.balance - t.amount;
                }
                else
                {
                    account.balance = account.balance + t.amount;
                }
                financesContext.SaveChanges();

                

                TempData["SuccessMessage"] = $"Transaction created successfully!";

                return RedirectToAction("Accounts", "Dashboard");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating transaction");
                ModelState.AddModelError("", "An error occurred while creating the transaction");
                return RedirectToAction("Accounts", "Dashboard");
            }
        }

        [HttpGet]
        public IActionResult History(int accountId)
        {
            ViewBag.AccountId = accountId;
            LoggedUser loggedUser = HttpContext.Session.GetObject<LoggedUser>("LoggedUser");
            if (loggedUser == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var transactions = financesContext.transactions
                    .Where(t => t.accountID == accountId)
                    .Where(t=>t.account.userID==loggedUser.user.userID)
                    .ToList();
            return View(transactions);
   
        }

        [HttpGet]
        public IActionResult BalanceHistory(int accountId)
        {
            var account = financesContext.accounts
                .Include(a => a.Transactions)
                .FirstOrDefault(a => a.accountID == accountId);

            if (account == null)
                return NotFound();

            var sortedTransactions = account.Transactions
                .OrderByDescending(t => t.createdAt)
                .ToList();

            var dailyBalances = new Dictionary<string, decimal>();
            decimal currentBalance = account.balance;

            for (int i = 0; i < 10; i++)
            {
                var day = DateTime.Today.AddDays(-i);
                var dayKey = day.ToString("yyyy-MM-dd");
                dailyBalances[dayKey] = currentBalance;

                foreach (var tx in sortedTransactions.Where(t => t.createdAt.Date == day))
                {
                    if (tx.type == TransactionType.Income)
                        currentBalance -= tx.amount;
                    else
                        currentBalance += tx.amount;
                }
            }

            dailyBalances = dailyBalances
                .OrderBy(kv => DateTime.Parse(kv.Key))
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            ViewBag.Dates = dailyBalances.Keys.ToList();
            ViewBag.Balances = dailyBalances.Values.ToList();
            ViewBag.AccountName = account.name;

            return View("Stats");
        }


    }
}
