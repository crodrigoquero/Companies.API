using System;
namespace Companies.API.DTOs
{
    public class ContactDTO
    {

        public int Id { get; set; }

        // Foreing Keys
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public int ContactRoleId { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Phome { get; set; }

        public string Email { get; set; }
    }
}
