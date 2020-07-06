using eComShop.Database;
using eComShop.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComShop.Services
{
    public class WidgetService
    {
        public List<Product> GetLatestProducts(int numberOfProducts)
        {
            using (var context = new ShopDbContext())
            {
                return context.Products.OrderByDescending(x => x.Id).Take(numberOfProducts).Include(x => x.Category).ToList();
            }
        }

        public List<Product> GetAllProducts(int pageNo, int pageSize)
        {
            using (var context = new ShopDbContext())
            {
                return context.Products.OrderByDescending(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();
            }
        }

        public List<Product> GetProductsByCategory(int categoryId, int pageSize)
        {
            using (var context = new ShopDbContext())
            {
                return context.Products.Where(x=> x.Category.Id == categoryId).OrderByDescending(x => x.Id).Take(pageSize).Include(x => x.Category).ToList();
            }
        }
    }
}
