using eComShop.Database;
using eComShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComShop.Services
{
    public class CustomConfigService 
    {
        public List<CustomConfig> GetAllCustomConfigs()
        {
            using (var context = new ShopDbContext())
            {
                return context.CustomConfigs.ToList();
            }
        }


        public CustomConfig GetCustomConfig(string key)
        {
            using (var context = new ShopDbContext())
            {
                var customConfig = context.CustomConfigs.Find(key);
                return customConfig;
            }
        }

        public void SaveCustomConfig(CustomConfig customConfig)
        {
            using (var context = new ShopDbContext())
            {
                context.CustomConfigs.Add(customConfig);
                context.SaveChanges();
            }
        }

        public void UpdateCustomConfig(CustomConfig customConfig)
        {
            using (var context = new ShopDbContext())
            {
                context.Entry(customConfig).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteCustomConfig(string key)
        {
            using (var context = new ShopDbContext())
            {
                var customConfig = context.CustomConfigs.Find(key);
                context.CustomConfigs.Remove(customConfig);
                context.SaveChanges();
            }
        }
    }
}
