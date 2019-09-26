using EasyShop.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models
{
    public class ProductInfoModel
    {
        public Product Product { get; set; }
        public List<ProductFeature> Features { get; set; }
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
    }
}
