using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComShop.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MinLength(3), MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
        public List<Product> Products { get; set; }

        public string ImageURL { get; set; } 
        public bool IsFeatured { get; set; }
    }
}
