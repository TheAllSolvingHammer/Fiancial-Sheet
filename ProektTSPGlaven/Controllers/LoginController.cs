using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ProektTSPGlaven.Models.Login;
using ProektTSPGlaven.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ProektTSPGlaven.Models.Session;
using ProektTSPGlaven.Models.Register;
using ProektTSPGlaven.Models.Builder;

namespace ProektTSPGlaven.Controllers
{
    public class LoginController : Controller
    {
        private readonly FinancesContext financesContext;
        private readonly ILogger<HomeController> logger;
 
        public LoginController(FinancesContext financesContext, ILogger<HomeController> logger)
        {
            this.financesContext = financesContext;
            this.logger = logger;   
            
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View("Login",model);
            //
            //
            //
            //
            User user = financesContext.users.FirstOrDefault(u => u.username == model.username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.password, user.hashedPassword))
            {
                ModelState.AddModelError("", "Invalid credentials");
               
                return View("Login",model);
            }
            

            HttpContext.Session.SetObject("LoggedUser", new LoggedUser(user));
            var user2 = HttpContext.Session.GetObject<User>("LoggedUser");
            Console.WriteLine(user2);
            return RedirectToAction("Dashboard","Dashboard");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
    }
}
