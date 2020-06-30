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
        // GET: Shop
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