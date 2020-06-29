using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Companies.API.DTOs
{

    //Company Sector
    public class BusinessTypeDTO
    {
       
        public int Id { get; set; }
        public string Description { get; set; }


    }
}
