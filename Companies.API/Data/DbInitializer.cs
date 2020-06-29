//using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Companies.API.Entities;
//using Companies.API.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationPlugin;

namespace Companies.API.Data

{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _Db;


        public DbInitializer(ApplicationDbContext Db)
        {
            _Db = Db;

        }

        public void Initialize()
        {
            try
            {
                if (_Db.Database.GetPendingMigrations().Count() > 0)
                {
                    _Db.Database.Migrate();
                }
            }
            catch 
            {
                //dummy
            }

            //IF WE ALREADY HAVE AT LEAST ONE ADMIN IN THE DATABASE, 
            //MAKES NO SENSE GO FORWARD HERE...
            //if (_Db.Roles.Any(r => r.Description == SD.AdminEndUser)) return;

            //CREATE ROLES "Admin" and others
            //Role _adminRole = new Role
            //{
            //    Description = "Admin"
            //};

            //_Db.Roles.Add(_adminRole);
            //_Db.SaveChanges();

            //Create first user with admin role

            //User _firstUser = new User
            //{
            //    Name = "seedAdmin@gmail.com",
            //    Email = "seedAdmin@gmail.com",
            //    Phone = "11111111111",
            //    Password = SecurePasswordHasherHelper.Hash("password+123")  // hash the password

            //};  // WARNING: if the password doesn't not acomplish the PASSWORD SECURITY POLICIES,
                // th user will not be created, and no errors will raise to make you aware about that.

            //Category _categotyCar = new Category()
            //{
            //    Description = "Car"
            //};


            //Category _categotyVan = new Category()
            //{
            //    Description = "Van"
            //};

            //Category _categotyBus = new Category()
            //{
            //    Description = "Bus"
            //};

            //Category _categotyMotorbike = new Category()
            //{
            //    Description = "Motorbike"
            //};

            //_Db.Categories.Add(_categotyCar);
            //_Db.Categories.Add(_categotyVan);
            //_Db.Categories.Add(_categotyBus);
            //_Db.Categories.Add(_categotyMotorbike);


            //_Db.Users.Add(_firstUser);


            //_Db.SaveChanges();
        }


    }
}
