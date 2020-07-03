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
    public class CategoryService
    {
        public List<Category> GetCategories()
        {
            using (var context = new ShopDbContext())
            {
                return context.Categories.ToList();
            }
        }

        public List<Category> GetAllCategories(int pageNo)
        {
            var pageSize = 3;

            using (var context = new ShopDbContext())
            {
                return context.Categories.OrderBy(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Products).ToList();
            }
        }

        public List<Category> GetFeaturedCategories()
        {
            using (var context = new ShopDbContext())
            {
                return context.Categories.Where(x=> x.IsFeatured && x.ImageURL != null).ToList();
            }
        }

        public Category GetCategory(int id)
        {
            using (var context = new ShopDbContext())
            {
                var category = context.Categories.Find(id);
                return category;
            }
        }

        public void SaveCategory(Category category)
        {
            using (var context = new ShopDbContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public void UpdateCategory(Category category)
        {
            using (var context = new ShopDbContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteCategory(int id)
        {
            using (var context = new ShopDbContext())
            {
                var category = context.Categories.Find(id);
                //context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
