using EasyShop.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Repository.Abstract
{
    public interface IBrandRepository:IGenericRepository<Brand>
    {
        Brand GetByName(string name);
    }
}
