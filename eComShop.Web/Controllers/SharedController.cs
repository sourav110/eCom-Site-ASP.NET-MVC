using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace eComShop.Web.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult UploadImage()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                var file = Request.Files[0];

                //var fileName = file.FileName;
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/content/images/"), fileName);

                file.SaveAs(path);

                //result.Data = new { Success = true, ImageURL = path }; 
                result.Data = new { Success = true, ImageURL = string.Format("/content/images/{0}", fileName) };

                //var newImage = new Image() { Name = fileName }; 

                //if (ImageService.Instance.SaveNewImage(newImage))
                //{
                //    result.Data = new { success = true, Image = fileName, File = newImage.Id, ImageURL = string.Format("{0}{1}", Variables.ImageFolderPath, fileName)};
                //}

                //else
                //{
                //    result.Data = new { success = false, Message = new HttpStatusCodeResult(500) };
                //}
            }

            catch(Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }

            return result;
        }
    }
}