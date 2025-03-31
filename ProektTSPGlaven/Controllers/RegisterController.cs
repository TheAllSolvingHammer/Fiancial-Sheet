using Microsoft.AspNetCore.Mvc;
using ProektTSPGlaven.Models.Builder;
using ProektTSPGlaven.Models.Database;
using ProektTSPGlaven.Models.Register;

namespace ProektTSPGlaven.Controllers
{
    public class RegisterController : Controller
    {
        private readonly FinancesContext financesContext;
        private readonly ILogger<HomeController> logger;

        public RegisterController(FinancesContext financesContext, ILogger<HomeController> logger)
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
        public IActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View("Register", "");
            if (!model.password.Equals(model.passwordRepeat))
            {
                ModelState.AddModelError("", "Passwords doesn't match");

                return View("Register", model);
            }
            User user = new UserBuilder().withFirstName(model.firstName)
                .withLastName(model.lastName)
                .withUsername(model.username)
                .withEmail(model.email)
                .withPassword(BCrypt.Net.BCrypt.HashPassword(model.password))
                .build();
            financesContext.users.Add(user);
            financesContext.SaveChanges();
            return RedirectToAction("Login", "Login");

        }

    }
}
