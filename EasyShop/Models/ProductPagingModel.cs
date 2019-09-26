using EasyShop.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models
{
    public class ProductPagingModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingDetails PagingDetails { get; set; }
    }
}
