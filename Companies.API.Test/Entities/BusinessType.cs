using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Companies.API.Test.Entities
{

    //Company Sector
    public class BusinessType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }


        // FOREING KEYS IDs - NAVIGATION PROPERTIES
        //public int ArticleId { get; set; }

        [ForeignKey("BusinessTypeId")]
        public ICollection<Company> Companies { get; set; }

    }
}
