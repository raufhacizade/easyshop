using EasyShop.Entity;
using EasyShop.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Repository.Concrete.EntityFramework
{
    public class EfOrderRepository: EfGenericRepository<Order>, IOrderRepository
    {
        public EfOrderRepository(EasyShopContext context):base(context)
        {
                
        }
    }
}
