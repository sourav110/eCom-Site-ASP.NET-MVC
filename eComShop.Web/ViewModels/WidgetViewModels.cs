using eComShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eComShop.Web.ViewModels
{
    public class ProductsWidgetViewModel
    {
        public List<Product> Products { get; set; }
        public bool IsLatest { get; set; }
        public int? CategoryId { get; set; }
    }
}