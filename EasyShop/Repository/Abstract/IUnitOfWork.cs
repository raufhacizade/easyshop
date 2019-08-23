using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Repository.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IBrandRepository Brands { get; }
        ICategoryRepository Categories { get; }
        IParentCategoryRepository ParentCategories { get; }
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }

        int SaveChanges();
    }
}
