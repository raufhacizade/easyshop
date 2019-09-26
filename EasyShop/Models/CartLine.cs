using EasyShop.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models
{
    public class CartLine
    {
        public int CartLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public double CurrentProductPrice
           => (Product.Price - ((Product.Price * Product.Discount) / 100));

        public double CartLinePrice
            => CurrentProductPrice * Quantity;




    }
}
