﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace eComShop.Entities
{
    public class CustomConfig
    {
        [Key]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
