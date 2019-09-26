using EasyShop.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models
{
    public class Cart
    {
        private List<CartLine> cartLines = new List<CartLine>();
        public List<CartLine> CartLines => cartLines;

        public string Test => "this is just test";

        public void AddProduct(Product product, int quantity = 1)
        {
            var wantedProduct = CartLines
                    .Where(cl => cl.Product.ProductId == product.ProductId)
                    .FirstOrDefault();

            if (wantedProduct == null)
                CartLines.Add(new CartLine()
                {
                    Product = product,
                    Quantity = quantity,
                });
            else wantedProduct.Quantity += quantity;
        }

        public void UpdateProductQuantity(int cartLineIndex, int quantity)
        {
            CartLine cartLine = cartLines[cartLineIndex];

            if (-cartLine.Quantity == quantity)
                CartLines.Remove(cartLine);
            else
                cartLine.Quantity += quantity;

        }

        public void RemoveProduct(Product product)
            => cartLines.RemoveAll(cl => cl.Product.ProductId == product.ProductId);

        public double GetBill()
            => cartLines.Sum(cl => cl.CartLinePrice);

        public void ClearAll()
            => cartLines.Clear();
    }
}
