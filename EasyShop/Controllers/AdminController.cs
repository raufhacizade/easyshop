using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyShop.Entity;
using EasyShop.Models;
using EasyShop.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyShop.Controllers
{
    public class AdminController : Controller
    {
        private IUnitOfWork unitOfWork;

        public AdminController(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;

        public IActionResult Index() => View();

        public IActionResult CategoryList()
        {
            var model = new CategoryListModel()
            {
                ParentCategories = unitOfWork.ParentCategories.GetAll().Include(p => p.Categories).ToList(),
                Categories = unitOfWork.Categories.GetAll().ToList()
            };

            return View(model);
        }

        public IActionResult BrandList()
        {
            List<Brand> brands = unitOfWork.Brands.GetAll()
                .Include(d => d.Products).ToList();

            return View(brands);
        }

        public IActionResult Update()
        {
            List<Brand> brands = unitOfWork.Brands.GetAll()
                .Include(d => d.Products).ToList();

            return RedirectToAction("BrandList");
        }




        public IActionResult ProductList()
        {
            return View(unitOfWork.Products.GetAll()
                          .Include(p => p.Features)
                          .Include(p => p.ProductCategories)
                          .ThenInclude(pc => pc.Category)
                          .Select(p => new ProductModel()
                          {
                              Product = p,
                              Features = p.Features,
                              Categories = p.ProductCategories.Select(pc => pc.Category).ToList()
                          }).ToList());
        }
    }
}