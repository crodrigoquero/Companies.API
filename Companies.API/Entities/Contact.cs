using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Companies.API.Entities
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        // Foreing Keys
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public int ContactRoleId { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Phome { get; set; }

        public string Email { get; set; }


        // FOREING KEYS IDs - NAVIGATION PROPERTIES

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
    }
}
