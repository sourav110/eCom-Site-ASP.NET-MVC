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
        public int GetMaximumPrice()
        {
            using (var context = new ShopDbContext())
            {
                var maximumPrice = (int)(context.Products.Max(x => x.Price));
                return maximumPrice;
            }
        }


        public List<Product> SearchProducts(string searchTerm, int? minPrice, int? maxPrice, int? categoryId, int? sortBy)
        {
            using (var context = new ShopDbContext())
            {
                var products = context.Products.ToList();

                if (categoryId.HasValue)
                {
                    products = products.Where(x => x.Category.Id == categoryId).ToList();
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }

                if (minPrice.HasValue) 
                {
                    products = products.Where(x => x.Price >= minPrice.Value).ToList();
                }

                if (maxPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= maxPrice.Value).ToList();
                }

                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            products = products.OrderByDescending(x => x.Id).ToList();
                            break; 

                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;

                        case 4:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;

                        default:
                            products = products.OrderByDescending(x => x.Id).ToList();
                            break;
                    }
                }


                return products;
            }
        }

        public List<Product> GetAllProducts(int pageNo)
        {
            var pageSize = 3;

            using (var context = new ShopDbContext())
            {
                //return context.Products.Include(x=> x.Category).ToList();
                return context.Products.OrderBy(x=> x.Id).Skip((pageNo-1) * pageSize).Take(pageSize).Include(x=> x.Category).ToList();
            }
        }

        public Product GetProduct(int id)
        {
            //using (var context = new ShopDbContext())
            //{
            //    var product = context.Products.Find(id);
            //    return product;
            //}

            var context = new ShopDbContext();
            var product = context.Products.Find(id);
            return product;
        }

        public List<Product> GetCartProducts(List<int> ids)
        {
            using (var context = new ShopDbContext())
            {
                var products = context.Products.Where(product => ids.Contains(product.Id)).ToList();
                return products;
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
                //context.Products.Attach(product);
                context.SaveChanges();
            }

        }

        //public void UpdateProduct(Product product)
        //{
        //    using (var context = new ShopDbContext())
        //    {
        //        context.Entry(product).State = System.Data.Entity.EntityState.Modified;
        //        context.SaveChanges();
        //    }
        //}

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
