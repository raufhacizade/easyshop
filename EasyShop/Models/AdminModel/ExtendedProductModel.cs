using EasyShop.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models
{
    public class ExtentedProductModel
    {
        public Product Product { get; set; }
        public int BrandId { get; set; }
        public int[] CatgoriesIdArray { get; set; }
        public IFormFile ProfilImageFile { get; set; }
        public List<SelectListItem> BrandSelectList { get; set; }
        public List<SelectListItem> CategorySelectList { get; set; }
    }
}
