﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Companies.API.DTOs
{
    public class ContactRoleDTO
    {
        
        public int Id { get; set; }
        public string Description { get; set; }


    }
}
