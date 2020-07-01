using eComShop.Entities;
using eComShop.Services;
using eComShop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

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

        public ActionResult ProductTable(string searchText, int? pageNo)
        {
            ProductSearchViewModels model = new ProductSearchViewModels();

            //model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            
            if (pageNo.HasValue)
            {
                if(pageNo.Value > 0)
                {
                    model.PageNo = pageNo.Value;
                }
                else
                {
                    model.PageNo = 1;
                }
            }
            else
            {
                model.PageNo = 1;
            }
            
            
            model.Products = productService.GetAllProducts(model.PageNo);

            if (!string.IsNullOrEmpty(searchText))
            {
                model.SearchText = searchText;
                model.Products = model.Products.Where(p => p.Name != null && p.Name.ToLower().Contains(searchText.ToLower())).ToList();
            }
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var categories = categoryService.GetAllCategories();
            return PartialView(categories);
            //return View();
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            Product product = new Product();
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Category = categoryService.GetCategory(model.CategoryId);

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