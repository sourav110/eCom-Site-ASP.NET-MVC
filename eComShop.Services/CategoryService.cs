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

        public int GetCategoriesCount(string searchText)
        {
            using (var context = new ShopDbContext())
            {
                if (!string.IsNullOrEmpty(searchText))
                {
                    return context.Categories.Where(c => c.Name != null && c.Name.ToLower()
                        .Contains(searchText.ToLower())).Count();
                }
                else
                {
                    return context.Categories.Count();
                }
            }
        }

        public List<Category> GetAllCategories(string searchText, int pageNo)
        {
            CustomConfigService customConfigService = new CustomConfigService();

            var pageSize = 3; //int.Parse(customConfigService.GetCustomConfig("ListingPageSize").Value);

            using (var context = new ShopDbContext())
            {
                if (!string.IsNullOrEmpty(searchText))
                {
                    return context.Categories.Where(c => c.Name != null && c.Name.ToLower()
                        .Contains(searchText.ToLower()))
                        .OrderBy(x => x.Id)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Products)
                        .ToList();
                }
                else
                {
                    return context.Categories
                        .OrderBy(x => x.Id)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Products)
                        .ToList();
                }
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
                //in case of foreign key
                var category = context.Categories.Where(x => x.Id == id).Include(x => x.Products).FirstOrDefault();

                context.Products.RemoveRange(category.Products); // engaged products to be deleted first
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
