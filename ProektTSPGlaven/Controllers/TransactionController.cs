using Microsoft.AspNetCore.Mvc;
using ProektTSPGlaven.Models.Database;

namespace ProektTSPGlaven.Controllers
{
    public class TransactionController : Controller
    {
        private readonly FinancesContext financesContext;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(FinancesContext financesContext, ILogger<TransactionController> logger)
        {
            this.financesContext = financesContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
