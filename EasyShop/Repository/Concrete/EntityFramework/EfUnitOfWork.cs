using EasyShop.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Repository.Concrete.EntityFramework
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly EasyShopContext dbContext;

        public EfUnitOfWork(EasyShopContext dbContext)
            => this.dbContext = dbContext ?? throw new ArgumentNullException("DbContext can not be null");


        private IBrandRepository brands;
        private ICategoryRepository categories;
        private IParentCategoryRepository parentCategories;
        private IProductRepository products;
        private IOrderRepository orders;


        public IBrandRepository Brands
        { get => brands ?? (brands = new EfBrandRepository(dbContext)); }

        public ICategoryRepository Categories
        { get => categories ?? (categories = new EfCategoryRepository(dbContext)); }

        public IParentCategoryRepository ParentCategories
        { get => parentCategories ?? (parentCategories = new EfParentCategoryRepository(dbContext)); }

        public IProductRepository Products
        { get => products ?? (products = new EfProductRepository(dbContext)); }

        public IOrderRepository Orders
        { get => orders ?? (orders = new EfOrderRepository(dbContext)); }

        public void Dispose() => dbContext.Dispose();

        public int SaveChanges()
        {
            try
            {
                return dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
