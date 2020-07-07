using eComShop.Services;
using eComShop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eComShop.Web.Controllers
{
    public class ShopController : Controller 
    {
        ProductService productService = new ProductService();
        CategoryService categoryService = new CategoryService();

        public ActionResult Index(string searchTerm, int? minPrice, int? maxPrice, int? categoryId, int? sortBy, int? pageNo)
        {
            ShopViewModel model = new ShopViewModel();
            
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            model.FeaturedCategories = categoryService.GetFeaturedCategories();
            model.MaximumPrice = productService.GetMaximumPrice();
            model.Products = productService.SearchProducts(searchTerm, minPrice, maxPrice, categoryId, sortBy, pageNo.Value, 12);
            model.SortBy = sortBy;
            model.CategoryId = categoryId;

            var totalCount = productService.SearchProductsCount(searchTerm, minPrice, maxPrice, categoryId, sortBy);
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            
            model.Pager = new Pager(totalCount, pageNo);

            return View(model);
        }

        public ActionResult FilteredProducts(string searchTerm, int? minPrice, int? maxPrice, int? categoryId, int? sortBy, int? pageNo)
        {
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            FilteredProductsViewModel model = new FilteredProductsViewModel();
            
            var totalCount = productService.SearchProductsCount(searchTerm, minPrice, maxPrice, categoryId, sortBy);

            model.Pager = new Pager(totalCount, pageNo);
            model.Products = productService.SearchProducts(searchTerm, minPrice, maxPrice, categoryId, sortBy, pageNo.Value, 12);

            return PartialView(model);
        }

        public ActionResult CheckOut()
        {
            CheckoutViewModel model = new CheckoutViewModel();

            var CartProductsCookie = Request.Cookies["CartProducts"];

            if(CartProductsCookie != null)
            {
                //var productIds = CartProductsCookie.Value;
                //var ids = productIds.Split('-'); // string
                //List<int> pIds = ids.Select(x => int.Parse(x)).ToList(); // int 

                // in one line
                model.CartProductIds = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();
                model.CartProducts = productService.GetCartProducts(model.CartProductIds); 

            }
            return View(model);
        }
    }
}