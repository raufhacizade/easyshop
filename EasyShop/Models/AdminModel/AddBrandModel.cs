using EasyShop.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models
{
    public class AddBrandModel
    {
        [Required]
        public string BrandName { get; set; }
        [Required]

        public IFormFile BrandLogo { get; set; }
    }
}
