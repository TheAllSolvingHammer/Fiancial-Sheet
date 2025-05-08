using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ProektTSPGlaven.Controllers;
using ProektTSPGlaven.Models.Database;
using ProektTSPGlaven.Models.Register;

namespace ProektTSPGlavenTests.Controllers
{
    public class RegisterControllerTests
    {
        [Fact]
        public void Register_PasswordMismatch_ReturnsViewWithError()
        {
            var options = new DbContextOptionsBuilder<FinancesContext>()
                .UseInMemoryDatabase("RegisterTestDB")
                .Options;

            using (var context = new FinancesContext(options))
            {
                var logger = new LoggerFactory().CreateLogger<RegisterController>();
                var controller = new RegisterController(context, logger);

                var model = new RegisterModel
                {
                    username = "user1",
                    password = "pass1",
                    passwordRepeat = "pass2"
                };

                var result = controller.Register(model) as ViewResult;
                Assert.NotNull(result);
                Assert.True(controller.ModelState.ErrorCount > 0);
                Assert.Contains(controller.ModelState.Values, v => v.Errors.Any(e => e.ErrorMessage.Contains("Passwords doesn't match")));
            }
        }

    }
}
