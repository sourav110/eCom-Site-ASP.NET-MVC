using eComShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eComShop.Web.ViewModels
{
    public class CheckoutViewModel
    {
        public List<Product> CartProducts { get; set; } 
        public List<int> CartProductIds { get; set; } 
    }

    public class ShopViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> FeaturedCategories { get; set; }
        public int MaximumPrice { get; set; }
        public int? SortBy { get; set; }
        public int? CategoryId { get; set; }

        public Pager Pager { get; set; }
    }

    public class FilteredProductsViewModel
    {
        public List<Product> Products { get; set; }
        public int? SortBy { get; set; }
        public int? CategoryId { get; set; }

        public Pager Pager { get; set; }
    }
}