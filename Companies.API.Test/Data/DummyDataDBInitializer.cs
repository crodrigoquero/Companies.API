using System;
using Companies.API.Data;
using Companies.API.Entities;

namespace Companies.API.Test.Data
{
    public class DummyDataDBInitializer
    {
        public DummyDataDBInitializer()
        {
        }

        public void Seed(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Departments.AddRange(
                new Department() { Description = "Department 1" },
                new Department() { Description = "Department 2" },
                new Department() { Description = "Department 3" },
                new Department() { Description = "Department 4" }
            );

            context.Genders.AddRange(
                new Gender() { Description = "Male" },
                new Gender() { Description = "Female" }
            );

            context.RelationshipTypes.AddRange(
                new RelationshipType() { Description = "Relationship Type 1" },
                new RelationshipType() { Description = "Relationship Type 2" }
            );


            context.ContactRoles.AddRange(
                new ContactRole() { Description = "Contact Role 1" },
                new ContactRole() { Description = "Contact Role 2" }
            );

            context.BusinessTypes.AddRange(
                new BusinessType() { Description = "Business Type 1" },
                new BusinessType() { Description = "Business Type 2" }
            );

            //context.Post.AddRange(
            //    new Post() { Title = "Test Title 1", Description = "Test Description 1", CategoryId = 2, CreatedDate = DateTime.Now },
            //    new Post() { Title = "Test Title 2", Description = "Test Description 2", CategoryId = 3, CreatedDate = DateTime.Now }
            //);

            context.SaveChanges();
        }
    }
}
