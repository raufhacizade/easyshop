using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Entity
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string BrandLogo { get; set; }
        
        public List<Product> Products { get; set; }
    }
}
