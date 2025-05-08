using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProektTSPGlaven.Controllers;
using ProektTSPGlaven.Models.Database;
using ProektTSPGlaven.Models.Login;

namespace ProektTSPGlavenTests.Controllers
{
    public class LoginControllerTest
    {
        [Fact]
        public void Login_InvalidCredentials_ReturnsViewWithError()
        {
            var options = new DbContextOptionsBuilder<FinancesContext>()
                .UseInMemoryDatabase("LoginTestDB")
                .Options;

            using (var context = new FinancesContext(options))
            {
                var logger = new LoggerFactory().CreateLogger<HomeController>();
                var controller = new LoginController(context, logger);

                var model = new LoginModel { username = "user", password = "wrong" };
                var result = controller.Login(model) as ViewResult;

                Assert.NotNull(result);
                Assert.True(controller.ModelState.ErrorCount > 0);
                Assert.Contains(controller.ModelState.Values, v => v.Errors.Any(e => e.ErrorMessage.Contains("Invalid credentials")));
            }
        }
    }
}
