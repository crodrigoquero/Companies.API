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

            // populate data into DEPARTMENTS entity
            if (databaseContext.Departments.Count() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Departments.Add(new Department()
                    {

                        //Id = 1,
                        Description = "Department " + 1.ToString(),


                    });
                    databaseContext.SaveChanges();
                }
            }

            // populate data into GENDER entity
            if (databaseContext.Genders.Count() <= 0)
            {

                databaseContext.Genders.Add(new Gender()
                {

                    //Id = 1,
                    Description = "Male",
                });
                databaseContext.Genders.Add(new Gender()
                {

                    //Id = 1,
                    Description = "Female",
                });

                databaseContext.SaveChanges();
            }

            // populate data into RelationshipType entity
            if (databaseContext.RelationshipTypes.Count() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.RelationshipTypes.Add(new RelationshipType()
                    {

                        //Id = 1,
                        Description = "Relationship Type " + i.ToString(),
                    });


                    databaseContext.SaveChanges();
                }
            }

            // populate data into ContactRole entity
            if (databaseContext.ContactRoles.Count() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.ContactRoles.Add(new ContactRole()
                    {

                        //Id = 1,
                        Description = "Contact Role " + i.ToString(),
                    });


                    databaseContext.SaveChanges();
                }
            }

            return databaseContext;
        }

    }
}
