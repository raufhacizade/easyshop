using EasyShop.Entity;
using EasyShop.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Repository.Concrete.EntityFramework
{
    public class EfProductRepository : EfGenericRepository<Product>, IProductRepository
    {
        public EfProductRepository(EasyShopContext context) : base(context) { }

        public EasyShopContext EasyShopContext { get=> context as EasyShopContext; }

        public Product GetByName(string name)
            => EasyShopContext.Products
                .Where(p=>p.ProductName == name)
                .FirstOrDefault();
    }
}
