using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Companies.API.Entities
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int BusinessTypeId { get; set; }


        [Required]
        public int CompanyRelationshipTypeId { get; set; } // what kind of relationsship we have?

        [Required]
        public string Street { get; set; }

        public string Street2 { get; set; } // for additional info

        [Required]
        public string HouseNumber { get; set; }

        [Required]
        public string City { get; set; }

        public string County { get; set; }
        public string State { get; set; }
        public string Province { get; set; }
        public string Region { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Postcode { get; set; }

        [Required]
        public string CompanySiteUrl1 { get; set; }
       
        public string CompanySiteUrl2 { get; set; }
        public string CompanySiteUrl3 { get; set; }

        public string LinkedIn { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }


        //[ForeignKey("CompanyId")]
        //public ICollection<Contact> Contacts { get; set; }


    }
}
