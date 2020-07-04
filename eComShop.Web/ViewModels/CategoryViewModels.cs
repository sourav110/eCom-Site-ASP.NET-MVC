using eComShop.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eComShop.Web.ViewModels
{
    public class NewCategoryViewModel
    {
        public int CategoryId { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
        public string ImageURL { get; set; }
    }


    public class CategorySearchViewModel
    {
        public int PageNo { get; set; }
        public List<Category> Categories { get; set; }
        public string SearchText { get; set; }

        public Pager Pager { get; set; }
    }
}