using eComShop.Services;
using eComShop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eComShop.Web.Controllers
{
    public class WidgetsController : Controller
    {
        // GET: Widgets

        WidgetService widgetService = new WidgetService();

        public ActionResult Products(bool isLatest, int? categoryId = 0)
        {
            ProductsWidgetViewModel model = new ProductsWidgetViewModel();
            model.IsLatest = isLatest;

            if (isLatest)
            {
                model.Products = widgetService.GetLatestProducts(4);
            }

            else if (categoryId.HasValue && categoryId.Value > 0)
            {
                model.Products = widgetService.GetProductsByCategory(categoryId.Value, 4);
            }
            else
            {
                model.Products = widgetService.GetAllProducts(1, 8);
            }

            return PartialView(model);
        }
    }
}