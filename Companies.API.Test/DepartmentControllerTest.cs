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
    public class DepartmentControllerTest
    {

        private ApplicationDbContext context;
        public static DbContextOptions<ApplicationDbContext> dbContextOptions { get; }
        public static string connectionString = "server=localhost;userid=root;pwd=noolvidar+1;port=3306;database=companiesTest;allowPublicKeyRetrieval=true;sslmode=none;";


        // CONSTRUCTORS
        static DepartmentControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySQL(connectionString)
                .Options;
        }

        public DepartmentControllerTest()
        {
            context = new ApplicationDbContext(dbContextOptions);
            DBInitializer _db = new DBInitializer();
            _db.Seed(context);

        }

        /// <summary>
        /// Must return a single object
        /// </summary>
        [TestMethod]
        public void Details_Works()
        {
            //Arrange  
            DepartmentController departmentController = new DepartmentController(context);

            var Id = 1;

            // invoke the get action
            IActionResult actionResult = departmentController.Details(Id);

            //ASSERT
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        }


        [TestMethod]
        public void Post_Works()
        {

            DepartmentController departmentController = new DepartmentController(context);

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
            departmentController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = claimsPrincipal
                }
            };

            // create a mock object
            Department department = new Department
            {
                //Id = 1,
                Description = "Test department"

            };

            // invoke the Post action
            IActionResult actionResult = departmentController.Post(department);

            //ASSERT
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));

        }

        //[TestMethod]
        //public void Post_Model_Check_Works()
        //{

        //    DepartmentController departmentController = new DepartmentController(context);

        //    // prepare user identity with its necessary claims
        //    var claims = new List<Claim>()
        //    {
        //        new Claim(ClaimTypes.Name, "username"),
        //        new Claim(ClaimTypes.NameIdentifier, "userId"),
        //        new Claim(ClaimTypes.Email, "userId@cojones.com"),

        //    };
        //    var identity = new ClaimsIdentity(claims, "TestAuthType");
        //    var claimsPrincipal = new ClaimsPrincipal(identity);

        //    // apply the claims
        //    departmentController.ControllerContext = new ControllerContext
        //    {
        //        HttpContext = new DefaultHttpContext
        //        {
        //            User = claimsPrincipal
        //        }
        //    };

        //    // create a mock object
        //    Department department = new Department
        //    {
        //        //Id = 1,
        //        Description = "" // to make it fail (required property)

        //    };

        //    // invoke the Post action
        //    IActionResult actionResult = departmentController.Post(department);

        //    //ASSERT
        //    Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));

        //}
    }
}