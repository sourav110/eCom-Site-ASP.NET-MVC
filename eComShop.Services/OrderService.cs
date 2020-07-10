using eComShop.Database;
using eComShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace eComShop.Services
{
    public class OrderService
    {
        public List<Order> SearchOrders(string userId, string status, int pageNo, int pageSize)
        {
            using (var context = new ShopDbContext())
            {
                var orders = context.Orders.ToList();

                if (!string.IsNullOrEmpty(userId))
                {
                    orders = orders.Where(x => x.UserId.ToLower().Contains(userId.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(status))
                {
                    orders = orders.Where(x => x.Status.ToLower().Contains(status.ToLower())).ToList();
                }

                return orders.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int SearchOrdersCount(string userId, string status)
        {
            using (var context = new ShopDbContext())
            {
                var orders = context.Orders.ToList();

                if (!string.IsNullOrEmpty(userId))
                {
                    orders = orders.Where(x => x.UserId.ToLower().Contains(userId.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(status))
                {
                    orders = orders.Where(x => x.Status.ToLower().Contains(status.ToLower())).ToList();
                }

                return orders.Count();
            }
        }

        public Order GetOrderById(int id)
        {
            using (var context = new ShopDbContext())
            {
                var order = context.Orders.Where(x => x.Id == id).Include(x => x.OrderItems).Include("OrderItems.Product").FirstOrDefault();                                                                    

                return order;
            }
        }

        public bool UpdateOrderStatus(string status, int id)
        {
            using (var context = new ShopDbContext())
            {
                var order = context.Orders.Find(id);
                order.Status = status;

                context.Entry(order).State = EntityState.Modified;

                return context.SaveChanges() > 0;
            }
        }

    }
}
