using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Companies.API.Entities
{
    public class RelationshipType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }


        //CompanyRelationshipTypeId
        // FOREING KEYS IDs - NAVIGATION PROPERTIES
        //public int ArticleId { get; set; }

        [ForeignKey("CompanyRelationshipTypeId")]
        public ICollection<Company> Companies { get; set; }
    }
}
