using eComShop.Entities;
using eComShop.Services;
using eComShop.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eComShop.Web.Controllers
{
    public class ShopController : Controller 
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ShopController()
        {
        }

        public ShopController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }




        ProductService productService = new ProductService();
        CategoryService categoryService = new CategoryService();

        public ActionResult Index(string searchTerm, int? minPrice, int? maxPrice, int? categoryId, int? sortBy, int? pageNo)
        {
            ShopViewModel model = new ShopViewModel();
            
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            model.FeaturedCategories = categoryService.GetFeaturedCategories();
            model.MaximumPrice = productService.GetMaximumPrice();
            
            var totalCount = productService.SearchProductsCount(searchTerm, minPrice, maxPrice, categoryId, sortBy);
            model.Products = productService.SearchProducts(searchTerm, minPrice, maxPrice, categoryId, sortBy, pageNo.Value, 12);
            
            model.SortBy = sortBy;
            model.CategoryId = categoryId;

            model.Pager = new Pager(totalCount, pageNo, 12);

            return View(model);
        }

        public ActionResult FilteredProducts(string searchTerm, int? minPrice, int? maxPrice, int? categoryId, int? sortBy, int? pageNo)
        {
            FilteredProductsViewModel model = new FilteredProductsViewModel();
            
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalCount = productService.SearchProductsCount(searchTerm, minPrice, maxPrice, categoryId, sortBy);
            model.Products = productService.SearchProducts(searchTerm, minPrice, maxPrice, categoryId, sortBy, pageNo.Value, 12);

            model.SortBy = sortBy;
            model.CategoryId = categoryId;

            model.Pager = new Pager(totalCount, pageNo, 12);

            return PartialView(model);
        }


        [Authorize]
        public ActionResult CheckOut()
        {
            CheckoutViewModel model = new CheckoutViewModel();

            var CartProductsCookie = Request.Cookies["CartProducts"];

            if(CartProductsCookie != null && !string.IsNullOrEmpty(CartProductsCookie.Value))
            {
                //var productIds = CartProductsCookie.Value;
                //var ids = productIds.Split('-'); // string
                //List<int> pIds = ids.Select(x => int.Parse(x)).ToList(); // int 

                // in one line
                model.CartProductIds = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();
                model.CartProducts = productService.GetCartProducts(model.CartProductIds);
                
                model.User = UserManager.FindById(User.Identity.GetUserId());
            }
            return View(model);
        }

        // product id formate : 1-9-2-3
        public JsonResult PlaceOrder(string productIds)
        {
            ShopService shopService = new ShopService();

            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (!string.IsNullOrEmpty(productIds))
            {
                var productQuantities = productIds.Split('-').Select(x => int.Parse(x)).ToList();
                var boughtProducts = productService.GetCartProducts(productQuantities.Distinct().ToList());

                Order newOrder = new Order();

                newOrder.UserId = User.Identity.GetUserId();
                newOrder.OrderedAt = DateTime.Now;
                newOrder.Status = "pending";
                newOrder.TotalAmount = boughtProducts.Sum(x => x.Price * productQuantities.Where(productId => productId == x.Id).Count());

                newOrder.OrderItems = new List<OrderItem>();
                newOrder.OrderItems.AddRange(boughtProducts.Select(x => new OrderItem() { ProductId = x.Id, Quantity = productQuantities.Where(productId => productId == x.Id).Count() }));

                var rowsAffected = shopService.SaveOrder(newOrder);

                result.Data = new { Success = true, Rows = rowsAffected };
            }

            else
            {
                result.Data = new { Success = false };
            }

            return result;
        }
    }
}