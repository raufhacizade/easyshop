using EasyShop.Entity;
using EasyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Repository.Abstract
{
    public interface IParentCategoryRepository : IGenericRepository<ParentCategory>
    {
        ParentCategory GetByName(string name);
        IEnumerable<ParentCategoryModel> GetAllWithCategories();
    }
}
