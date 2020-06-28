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
    public class ProductService
    {
        public List<Product> GetAllProducts()
        {
            //var context = new ShopDbContext();
            //return context.Products.ToList();

            using (var context = new ShopDbContext())
            {
                return context.Products.Include(x=> x.Category).ToList();
            }
        }

        public Product GetProduct(int id)
        {
            using (var context = new ShopDbContext())
            {
                var product = context.Products.Find(id);
                return product;
            }
        }

        public void SaveProduct(Product product)
        {
            using (var context = new ShopDbContext())
            {
                context.Entry(product.Category).State = System.Data.Entity.EntityState.Unchanged;

                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var context = new ShopDbContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            using (var context = new ShopDbContext())
            {
                var product = context.Products.Find(id);
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
    }
}
