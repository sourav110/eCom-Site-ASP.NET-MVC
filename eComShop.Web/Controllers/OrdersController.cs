using eComShop.Services;
using eComShop.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eComShop.Web.Controllers
{
    public class OrdersController : Controller
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public OrdersController()
        {
        }

        public OrdersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        OrderService orderService = new OrderService();

        // GET: Orders
        public ActionResult Index(string userId, string status, int? pageNo)
        {
            OrderViewModel model = new OrderViewModel();
            model.UserId = userId;
            model.Status = status;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            var pageSize = 5;

            model.Orders = orderService.SearchOrders(userId, status, pageNo.Value, pageSize);
            var totalRecords = orderService.SearchOrdersCount(userId, status);

            model.Pager = new Pager(totalRecords, pageNo, pageSize);

            return View(model);
        }

        public ActionResult Details(int id)
        {
            OrderDetailsViewModel model = new OrderDetailsViewModel();

            model.Order = orderService.GetOrderById(id);
            if(model.Order != null)
            {
                model.User = UserManager.FindById(model.Order.UserId);
            }

            model.OrderStatus = new List<string>()
            {
                "Pending", "In Progress", "Delivered"
            };

            return View(model);
        }


        public JsonResult ChangeStatus(string status, int id)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            result.Data = new { Success = orderService.UpdateOrderStatus(status, id) };

            return result;
        }





















        public ActionResult OrderTable()
        {
            return PartialView();
        }
    }
}