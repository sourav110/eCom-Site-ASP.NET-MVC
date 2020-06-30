using eComShop.Entities;
using eComShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eComShop.Web.Controllers
{
    public class CustomConfigController : Controller 
    {
        CustomConfigService customConfigService = new CustomConfigService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CustomConfigTable(string searchText)
        {
            var customConfigs = customConfigService.GetAllCustomConfigs();

            if (!string.IsNullOrEmpty(searchText))
            {
                customConfigs = customConfigs.Where(cc => cc.Key != null && cc.Key.ToLower().Contains(searchText.ToLower())).ToList();
            }
            return PartialView(customConfigs);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(CustomConfig customConfig)
        {
            customConfigService.SaveCustomConfig(customConfig);
            return RedirectToAction("CustomConfigTable");
        }


        [HttpGet]
        public ActionResult Edit(string key)
        {
            var customConfig = customConfigService.GetCustomConfig(key); 
            return PartialView(customConfig);
        }

        [HttpPost]
        public ActionResult Edit(CustomConfig customConfig)
        {
            customConfigService.UpdateCustomConfig(customConfig);
            return RedirectToAction("CustomConfigTable");
        }

        [HttpGet]
        public ActionResult Delete(string key)
        {
            var customConfig = customConfigService.GetCustomConfig(key);
            return View(customConfig);
        }

        [HttpPost]
        public ActionResult Delete(CustomConfig customConfig)
        {
            customConfigService.DeleteCustomConfig(customConfig.Key);
            return RedirectToAction("CustomConfigTable");
        }
    }
}