using EasyShop.Entity;
using EasyShop.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Repository.Concrete.EntityFramework
{
    public class EfCategoryRepository : EfGenericRepository<Category>,ICategoryRepository
    {
        public EfCategoryRepository(EasyShopContext context) : base(context) { }

        public EasyShopContext EasyShopContext { get => context as EasyShopContext; }

        public Category GetByName(string name)
            => EasyShopContext.Categories
                .Where(c => c.CategoryName == name)
                .FirstOrDefault();
    }
}
