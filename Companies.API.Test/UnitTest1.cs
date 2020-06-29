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

namespace Companies.API.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DepartmentController_ReturnsDepartmentList()
        {

            DepartmentController departmentController = new DepartmentController(Helper.GetDatabaseContext());

            OkObjectResult result = (OkObjectResult)departmentController.List();

            List<Department> retrievedList = (List<Department>)result.Value;

            ////Assert.IsNotNull(result);

            Assert.AreEqual(10, retrievedList.Count());

        }


    }
}
