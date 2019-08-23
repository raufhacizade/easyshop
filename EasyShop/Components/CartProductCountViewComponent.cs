using EasyShop.Infrastructure;
using EasyShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Components
{
    public class CartProductCountViewComponent : ViewComponent
    {
        public string Invoke() => HttpContext.Session
            .GetJson<Cart>("Cart")?.CartLines.Count.ToString() ?? "0";
    }
}
