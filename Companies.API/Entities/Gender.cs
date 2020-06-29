using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Companies.API.Entities
{
    public class Gender
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        // FOREING KEYS IDs - NAVIGATION PROPERTIES

        [ForeignKey("GenderId")]
        public ICollection<Contact> Contacts { get; set; }

    }
}
