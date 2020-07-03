using eComShop.Entities;
using eComShop.Services;
using eComShop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

            if (pageNo.HasValue)
            {
                if (pageNo.Value > 0)
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


            model.Categories = categoryService.GetAllCategories(model.PageNo);

            if (!string.IsNullOrEmpty(searchText))
            {
                model.SearchText = searchText;
                model.Categories = model.Categories.Where(p => p.Name != null && p.Name.ToLower().Contains(searchText.ToLower())).ToList();
            }
            return PartialView(model);
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
            Category category = new Category();

            category.Name = model.Name;
            category.Description = model.Description;
            category.ImageURL = model.ImageURL;

            categoryService.SaveCategory(category);
            return RedirectToAction("CategoryTable");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = categoryService.GetCategory(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            categoryService.UpdateCategory(category);
            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }
    }
}