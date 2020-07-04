using eComShop.Entities;
using eComShop.Services;
using eComShop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace eComShop.Web.Controllers
{
    public class CategoriesController : Controller
    {
        CategoryService categoryService = new CategoryService();
        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryTable(string searchText, int? pageNo)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();
            
            model.SearchText = searchText;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = categoryService.GetCategoriesCount(searchText);

            model.Categories = categoryService.GetAllCategories(searchText, pageNo.Value);

            if(model.Categories != null)
            {
                model.Pager = new Pager(totalRecords, pageNo, 3);

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
            NewCategoryViewModel model = new NewCategoryViewModel();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category();

                category.Name = model.Name;
                category.Description = model.Description;
                category.ImageURL = model.ImageURL;

                categoryService.SaveCategory(category);
                return RedirectToAction("CategoryTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
            
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = categoryService.GetCategory(id);
            return PartialView(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            categoryService.UpdateCategory(category);
            return RedirectToAction("CategoryTable");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var category = categoryService.GetCategory(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            //category = categoryService.GetCategory(category.Id);

            categoryService.DeleteCategory(category.Id);
            return RedirectToAction("CategoryTable");
        }
    }
}