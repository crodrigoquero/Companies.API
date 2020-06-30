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
        [TestMethod]
        public void DepartmentController_List_ReturnsDepartmentsList()
        {

            DepartmentController departmentController = new DepartmentController(Helper.GetDatabaseContext());

            OkObjectResult result = (OkObjectResult)departmentController.List();

            List<Department> retrievedList = (List<Department>)result.Value;


            Assert.AreEqual(10, retrievedList.Count());

        }


        //[TestMethod]
        //public void DepartmentController_Redirection_Works()
        //{

        //    DepartmentController departmentController = new DepartmentController(Helper.GetDatabaseContext());

        //    RedirectToRouteResult result = departmentController.Redirection(0) as RedirectToRouteResult;
        //    //OkObjectResult result = (OkObjectResult)departmentController.Redirection(0);
            

        //    Assert.AreEqual("List", result.RouteValues["action"]);
        //    //Assert.AreEqual("Department", result.RouteValues["controller"]);

        //}

        [TestMethod]
        public void DepartmentController_GetOne_Works()
        {

            DepartmentController departmentController = new DepartmentController(Helper.GetDatabaseContext());


            IActionResult actionResult = departmentController.Details(1);
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));

        }


        [TestMethod]
        public void DepartmentController_returns_404_if_NotFound()
        {

            DepartmentController departmentController = new DepartmentController(Helper.GetDatabaseContext());

            IActionResult actionResult = departmentController.Details(10000);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

            //var response = departmentController.Post(department);
            //Assert.AreEqual(200, response.ToString());
        }


        [TestMethod]
        public void DepartmentController_Post_Works()
        {

            DepartmentController departmentController = new DepartmentController(Helper.GetDatabaseContext());

            // prepare user identity with its necessary claims
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "username"),
                new Claim(ClaimTypes.NameIdentifier, "userId"),
                new Claim(ClaimTypes.Email, "userId@cojones.com"),
                new Claim("name", "John Doe"),
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
                Id = 1,
                Description = "New Department"
            };

            // invoke the Post action
            IActionResult actionResult = departmentController.Post(department);

            //ASSERT
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));

        }


    }
}
