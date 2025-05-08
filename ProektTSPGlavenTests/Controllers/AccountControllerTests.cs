using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ProektTSPGlaven.Controllers;
using ProektTSPGlaven.Models.AccountModel;
using ProektTSPGlaven.Models.Database;
using ProektTSPGlaven.Models.Session;

namespace ProektTSPGlavenTests.Controllers
{
    public class AccountControllerTests
    {
        [Fact]
        public void CreateAccount_InvalidModel_ReturnsToViewWithErrors()
        {
   
            var options = new DbContextOptionsBuilder<FinancesContext>()
                .UseInMemoryDatabase("TestDB")
                .Options;

            using (var context = new FinancesContext(options))
            {
                var logger = new LoggerFactory().CreateLogger<AccountController>();
                var controller = new AccountController(context, logger);
                var model = new AccountModel
                {
                    name = "Invalid Account",
                    balance = -500
                };
                var result = controller.CreateAccount(model) as ViewResult;
                Assert.NotNull(result);
                Assert.Equal("Account", result.ViewName);
                Assert.True(controller.ModelState.ErrorCount > 0);
                Assert.Contains(controller.ModelState.Values, v => v.Errors.Any(e => e.ErrorMessage.Contains("Balance can not be less than zero")));
            }
        }
   
    }
}
