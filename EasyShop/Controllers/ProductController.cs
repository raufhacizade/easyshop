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
    public class ProductController : Controller
    {
        private readonly IProductRepository repository;

        public ProductController(IProductRepository repository) => this.repository = repository;

        public IActionResult AllProduct() => View("_Index", repository.GetAll().ToList());

        public IActionResult ListByCategory(int categoryId)
        {
            return View("_Index", repository.GetAll()
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId)).ToList());
        }

        public IActionResult ListByBrand(int brandId)
             => View("_Index", repository.GetAll().Where(p => p.BrandId == brandId).ToList());

        public IActionResult ProductInfo(int id)
        => View(repository.GetAll()
                          .Where(p => p.ProductId == id)
                          .Include(p => p.Features)
                          .Include(p => p.ProductCategories)
                          .ThenInclude(pc => pc.Category)
                          .Select(p => new ProductModel()
                          {
                              Product = p,
                              Features = p.Features,
                              Categories = p.ProductCategories.Select(pc => pc.Category).ToList()
                          }).FirstOrDefault()
            );

    }
}