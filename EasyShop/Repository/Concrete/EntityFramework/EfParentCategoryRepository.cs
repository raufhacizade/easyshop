using EasyShop.Entity;
using EasyShop.Models;
using EasyShop.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Repository.Concrete.EntityFramework
{
    public class EfParentCategoryRepository : EfGenericRepository<ParentCategory>, IParentCategoryRepository
    {
        private readonly EfCategoryRepository categoryRepository;
        public EfParentCategoryRepository(EasyShopContext context) : base(context)
        {
            categoryRepository = new EfCategoryRepository(context);
        }

        public EasyShopContext EasyShopContext { get => context as EasyShopContext; }

        public IEnumerable<ParentCategoryModel> GetAllWithCategories()
        {
            return GetAll().Select(pC => new ParentCategoryModel()
            {
                ParentCategoryId = pC.ParentCategoryId,
                ParentCategoryName = pC.ParentCategoryName,
                Categories = categoryRepository.GetAll()
                                               .Where(c => c.ParentCategoryId == pC.ParentCategoryId)
                                               .OrderBy(c => c.CategoryName)
                                               .ToList()
            }
            ).OrderBy(pc => pc.ParentCategoryName);
        }

        public ParentCategory GetByName(string name)
            => EasyShopContext.ParentCategories
                .Where(pc => pc.ParentCategoryName == name)
                .FirstOrDefault();
    }
}