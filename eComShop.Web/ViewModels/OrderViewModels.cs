using eComShop.Entities;
using eComShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eComShop.Web.ViewModels
{
    public class OrderViewModel
    {
        public List<Order> Orders { get; set; }
        public Pager Pager { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
    }


    public class OrderDetailsViewModel
    {
        public Order Order { get; set; }
        public ApplicationUser User { get; set; } 
        public List<string> OrderStatus { get; set; } 
    }
}