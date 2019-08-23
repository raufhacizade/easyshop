using EasyShop.Entity;
using EasyShop.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Repository.Concrete.EntityFramework
{
    public class EfBrandRepository : EfGenericRepository<Brand>, IBrandRepository
    {
        public EfBrandRepository(EasyShopContext context) : base(context) { }

        public EasyShopContext EasyShopContext { get => context as EasyShopContext; }

        public Brand GetByName(string name)
            => EasyShopContext.Brands
               .Where(b => b.BrandName == name)
               .FirstOrDefault();
    }
}
