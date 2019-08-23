using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.Controllers
{
    public class OrderController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}