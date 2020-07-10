using eComShop.Database;
using eComShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComShop.Services
{
    public class ShopService
    {
        public int SaveOrder(Order order)
        {
            using (var context = new ShopDbContext())
            {
                context.Orders.Add(order);
                return context.SaveChanges(); 
            }
        }
    }
}
