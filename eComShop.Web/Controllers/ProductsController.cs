using eComShop.Entities;
using eComShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eComShop.Web.Controllers
{
    public class ProductsController : Controller
    {
        ProductService productService = new ProductService();
        CategoryService categoryService = new CategoryService();

        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string searchText)
        {
            var products = productService.GetAllProducts();

            if (!string.IsNullOrEmpty(searchText))
            {
                products = products.Where(p => p.Name != null && p.Name.ToLower().Contains(searchText.ToLower())).ToList();
            }
            return PartialView(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var categories = categoryService.GetAllCategories();
            return PartialView(categories);
            //return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            productService.SaveProduct(product);
            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = productService.GetProduct(id);
            return PartialView(product);
            //return View();
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            productService.UpdateProduct(product);
            return RedirectToAction("ProductTable");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            productService.DeleteProduct(id);
            return RedirectToAction("ProductTable");
        }
    }
}