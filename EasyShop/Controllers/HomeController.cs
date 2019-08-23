using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyShop.Models;
using EasyShop.Repository.Concrete.EntityFramework;
using EasyShop.Repository.Abstract;

namespace EasyShop.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;

        public IActionResult Index()
            => View("_Index", unitOfWork.Products.GetAll().Where(p=>p.IsHome).ToList());

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
