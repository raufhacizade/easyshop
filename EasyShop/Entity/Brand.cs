using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Entity
{
    public class Brand
    {
        public int BrandId { get; set; }
        [Required]
        public string BrandName { get; set; }
        public string BrandLogo { get; set; }
        
        public List<Product> Products { get; set; }
    }
}

