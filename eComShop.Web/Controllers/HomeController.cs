﻿using eComShop.Services;
using eComShop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eComShop.Web.Controllers
{
    public class HomeController : Controller
    {
        CategoryService categoryService = new CategoryService();

        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.Categories = categoryService.GetAllCategories();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}