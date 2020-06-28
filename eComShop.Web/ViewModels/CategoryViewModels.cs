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
        public decimal Price { get; set; }
    }
}