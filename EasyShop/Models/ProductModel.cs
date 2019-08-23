using EasyShop.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models
{
    public class ProductModel
    {
        public Product Product { get; set; }
        public List<ProductFeature> Features { get; set; }
        public List<Category> Categories { get; set; }
    }
}
