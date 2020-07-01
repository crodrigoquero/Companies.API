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
    public class CompanyControllerTest
    {


        private ApplicationDbContext context;
        public static DbContextOptions<ApplicationDbContext> dbContextOptions { get; }
        public static string connectionString = "server=localhost;userid=root;pwd=noolvidar+1;port=3306;database=companies;allowPublicKeyRetrieval=true;sslmode=none;";

        static CompanyControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySQL(connectionString)
                .Options;
        }

        public CompanyControllerTest()
        {
            context = new ApplicationDbContext(dbContextOptions);
            //DummyDataDBInitializer db = new DummyDataDBInitializer();
            //db.Seed(context);

            //repository = new PostRepository(context);

        }



        [TestMethod]
        public void Details_Return_OkResult()
        {
            //Arrange  
            CompanyController companyController = new CompanyController(context);

            var Id = 1;

            // invoke the get action
            IActionResult actionResult = companyController.Details(Id);

            //ASSERT
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        }


        [TestMethod]
        public void Post_Database_FK_Check_Works()
        {

            CompanyController companyController = new CompanyController(context);

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
            companyController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = claimsPrincipal
                }
            };

            // create a mock object
            Company company = new Company
            {
                //Id = 1,
                Name = "name",                        // here we go!
                Street = "street",
                Street2 = "street 2",
                HouseNumber = "12",
                City = "city",
                County = "county",
                State = "state",
                Province = "province",
                Region = "region",
                Country = "country",
                Postcode = "postcode",
                CompanySiteUrl1 = "site url",
                CompanySiteUrl2 = "site url 2",
                CompanySiteUrl3 = "site url 3",
                LinkedIn = "linked in",
                Twitter = "twitter",
                Facebook = "facebook",
                CompanyRelationshipTypeId = 1, // foreing key
                BusinessTypeId = 1 // foreing key

            };

            // invoke the Post action
            IActionResult actionResult = companyController.Post(company);

            //ASSERT
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));

        }




    }
}
