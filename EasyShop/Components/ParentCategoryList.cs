using EasyShop.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Components
{
    public class ParentCategoryList : ViewComponent
    {
        private IParentCategoryRepository repository;

        public ParentCategoryList(IParentCategoryRepository repository)
            => this.repository = repository;

        public IViewComponentResult Invoke() => View(repository.GetAllWithCategories());
    }
}
