﻿using System;
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
        public int PageSize = 8;
        private readonly IProductRepository repository;

        public ProductController(IProductRepository repository) => this.repository = repository;

        public IActionResult Index(int page=1)
        {
            var products = repository.GetAll().Where(p => p.IsHome);
            var count = products.Count();
            products = products.Skip((page - 1) * PageSize).Take(PageSize);

            ViewBag.Action = "Index";
            var model = new ProductPagingModel()
            {
                Products = products,
                PagingDetails = new PagingDetails()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = count
                }
            };
            return View(model);
        }

        public IActionResult GetProducts(string sortType,int page = 1)
        {
            IQueryable<Product> products = SortProducts(sortType);

            var count = products.Count();
            products = products.Skip((page - 1) * PageSize).Take(PageSize);

            ViewBag.Action = "GetProducts";
            ViewBag.SortType = sortType;
            var model = new ProductPagingModel()
            {
                Products = products,
                PagingDetails = new PagingDetails()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = count
                }
            };
            return View("Index", model);
        }

        public IActionResult SearchProduct(string SearchKeyword, int page = 1)
        {
           // var keyWord = "%" + SearchKeyword + "%";

            var products = repository.GetAll().Where(r => r.ProductName.Contains(SearchKeyword));

          //  var products = repository.GetAll().Where(p => EF.Functions.Like(p.ProductName, keyWord));

            var count = products.Count();
            products = products.Skip((page - 1) * PageSize).Take(PageSize);

            ViewBag.Action = "SearchProduct";
            var model = new ProductPagingModel()
            {
                Products = products,
                PagingDetails = new PagingDetails()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = count
                }
            };
            return View("Index", model);
        }

        public IActionResult ListByCategory(int categoryId, int page=1)
        {
            var products = repository.GetAll()
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId));

            var count = products.Count();
            products = products.Skip((page - 1) * PageSize).Take(PageSize);

            ViewBag.Action = "ListByCategory";
            var model = new ProductPagingModel()
            {
                Products = products,
                PagingDetails = new PagingDetails()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = count
                }
            };
            return View("Index", model);
        }

        public IActionResult ListByBrand(int brandId, int page = 1)
        {
            var products = repository.GetAll().Where(p => p.BrandId == brandId);
            var count = products.Count();
            products = products.Skip((page - 1) * PageSize).Take(PageSize);

            ViewBag.Action = "ListByBrand";
            var model = new ProductPagingModel()
            {
                Products = products,
                PagingDetails = new PagingDetails()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = count
                }
            };
            return View("Index", model);
        }

        public IActionResult ProductInfo(int id)
        => View(repository.GetAll()
                          .Where(p => p.ProductId == id)
                          .Include(p => p.Features)
                          .Include(p => p.ProductCategories)
                          .ThenInclude(pc => pc.Category)
                          .Select(p => new ProductInfoModel()
                          {
                              Product = p,
                              Features = p.Features,
                              Categories = p.ProductCategories.Select(pc => pc.Category).ToList()
                          }).FirstOrDefault()
            );

        private IQueryable<Product> SortProducts(string sortType)
        {
            switch (sortType)
            {
                case "lp":
                    return  repository.GetAll().OrderBy(p => p.Price);
                case "hp":
                    return  repository.GetAll().OrderByDescending(p => p.Price);
                case "bs":
                    return repository.GetAll().OrderBy(p => p.SoldAmount);
                case "n":
                    return repository.GetAll().OrderByDescending(p => p.DateAdded);
                case "d":
                    return repository.GetAll().Where(p => p.Discount != 0);
                default:
                    return repository.GetAll();
            }
        }

    }
}