using System;
using Companies.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Companies.API.Data

{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // console cmd to add migrations on mac: dotnet ef migrations add <migrationName>
        // dotnet ef database update
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactRole> ContactRoles { get; set; }
        public DbSet<BusinessType> BusinessTypes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<RelationshipType> RelationshipTypes { get; set; }

    }
}