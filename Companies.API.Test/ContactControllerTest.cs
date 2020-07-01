using System;
using System.Linq;
using System.Threading.Tasks;
using Companies.API.Controllers;
using Companies.API.Data;
using Companies.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Companies.API.Test.Data;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;



namespace Companies.API.Test
{
    [TestClass]
    public class ComtactControllerTest
    {


        private ApplicationDbContext context;
        public static DbContextOptions<ApplicationDbContext> dbContextOptions { get; }
        public static string connectionString = "server=localhost;userid=root;pwd=noolvidar+1;port=3306;database=companies;allowPublicKeyRetrieval=true;sslmode=none;";

        static ComtactControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySQL(connectionString)
                .Options;
        }

        public ComtactControllerTest()
        {
            context = new ApplicationDbContext(dbContextOptions);
            //DummyDataDBInitializer db = new DummyDataDBInitializer();
            //db.Seed(context);

            //repository = new ContactsRepository(context);

        }


        [TestMethod]
        public void Details_Return_OkResult()
        {
            //Arrange  
            ContactController contactController = new ContactController(context);

            var Id = 1;

            // invoke the get action
            IActionResult actionResult = contactController.Details(Id);

            //ASSERT
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        }



        [TestMethod]
        public void Post_Database_FK_Check_Works()
        {

            ContactController contactController = new ContactController(context);

            // prepare user identity with its necessary claims
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "username"),
                new Claim(ClaimTypes.NameIdentifier, "userId"),
                new Claim(ClaimTypes.Email, "userId@cojones.com"),

            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            // apply the claims
            contactController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = claimsPrincipal
                }
            };

            // create a mock object
            Contact contact = new Contact
            {
                //Id = 1,
                CompanyId = 10000, /// bad foreing key value: refencial integrity role break
                DepartmentId = 2000, /// bad foreing key value: refencial integrity role break
                ContactRoleId = 1000, /// bad data: refencial integrity role break
                FirstName = "Pepe",
                LastName = "Gotera",
                Email = "pepe.gotera@hotmail.com"

            };

            // invoke the Post action
            IActionResult actionResult = contactController.Post(contact);

            //ASSERT
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));

        }

    }
}
