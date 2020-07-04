using eComShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eComShop.Web.ViewModels
{
    public class NewCategoryViewModel
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }
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