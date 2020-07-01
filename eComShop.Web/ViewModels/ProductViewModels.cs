using eComShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eComShop.Web.ViewModels
{
    public class ProductSearchViewModels
    {
        public int PageNo { get; set; }
        public List<Product> Products { get; set; }
        public string SearchText { get; set; }
    }
}