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

    public class NewProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public string ImageURL { get; set; }
        public List<Category> AvailableCategories { get; set; }
    }

    public class EditProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public string ImageURL { get; set; }
        public List<Category> AvailableCategories { get; set; }
    }
}