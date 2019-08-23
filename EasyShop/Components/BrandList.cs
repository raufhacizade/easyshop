using EasyShop.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Components
{
    public class BrandList : ViewComponent
    {
        private IBrandRepository repository;

        public BrandList(IBrandRepository repository)
            => this.repository = repository;

        public IViewComponentResult Invoke() => View(repository.GetAll());
    }
}
