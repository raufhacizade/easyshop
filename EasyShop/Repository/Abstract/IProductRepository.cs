using EasyShop.Entity;

namespace EasyShop.Repository.Abstract
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Product GetByName(string name);
    }
}
