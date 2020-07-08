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
    [Authorize]
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
            model.SearchText = searchText;

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

            var totalRecords = productService.GetProductsCount(searchText);

            model.Products = productService.GetProducts(searchText, model.PageNo);

            if (model.Products != null)
            {
                model.Pager = new Pager(totalRecords, pageNo, 5);

                return PartialView(model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            NewProductViewModel model = new NewProductViewModel();

            model.AvailableCategories = categoryService.GetCategories();
            
            return PartialView(model);
            //return View();
        }

        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product();

                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.Category = categoryService.GetCategory(model.CategoryId);
                product.ImageURL = model.ImageURL;

                productService.SaveProduct(product);

                return RedirectToAction("ProductTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            EditProductViewModel model = new EditProductViewModel();

            var product = productService.GetProduct(id);

            model.Id = product.Id;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.CategoryId = product.Category != null ? product.Category.Id : 0;
            model.ImageURL = product.ImageURL;

            model.AvailableCategories = categoryService.GetCategories();

            return PartialView(model);
            //return View();
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            var existingProduct = productService.GetProduct(model.Id);

            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;
            existingProduct.Category = null;
            existingProduct.CategoryId = model.CategoryId;

            if (!string.IsNullOrEmpty(model.ImageURL))
            {
                existingProduct.ImageURL = model.ImageURL;
            }

            productService.UpdateProduct(existingProduct);

            return RedirectToAction("ProductTable");
        }

        //[HttpGet]
        //public ActionResult Edit(int id)
        //{
        //    var product = productService.GetProduct(id);
        //    return PartialView(product);
        //    //return View();
        //}

        //[HttpPost]
        //public ActionResult Edit(Product product)
        //{
        //    productService.UpdateProduct(product);
        //    return RedirectToAction("ProductTable");
        //}

        [HttpGet]
        public ActionResult Details(int id)
        {
            ProductDetailsViewModel model = new ProductDetailsViewModel();

            model.Product = productService.GetProduct(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            productService.DeleteProduct(id);
            return RedirectToAction("ProductTable");
        }
    }
}