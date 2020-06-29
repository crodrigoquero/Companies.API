using System;
using System.Linq;
using Companies.API.Data;
using Companies.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Companies.API.Test.Data
{
    public static class Helper
    {
        public static ApplicationDbContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (databaseContext.Companies.Count() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Departments.Add(new Department()
                    {

                        //Id = 1,
                        Description = "nombre dep",


                    });
                    databaseContext.SaveChanges();
                }
            }
            return databaseContext;
        }

    }
}
