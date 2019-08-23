using EasyShop.Infrastructure;
using EasyShop.Models;
using EasyShop.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Components
{
    public class CartModel : ViewComponent
    {
        private IProductRepository repository;

        public CartModel(IProductRepository repository)
        => this.repository = repository;

        public IViewComponentResult Invoke() => View(GetCart());
        

        //public IActionResult AddToCart(int id, int quantity = 1)
        //{
        //    var product = repository.Get(id);

        //    if (product != null)
        //    {
        //        var cart = GetCart();
        //        cart.AddProduct(product, quantity);
        //        SaveCart(cart);
        //    }
        //    return RedirectToAction("Index");
        //}

        //public IActionResult RemoveFromCart(int ProductId)
        //{
        //    var product = repository.Get(ProductId);

        //    if (product != null)
        //    {
        //        GetCart().RemoveProduct(product);
        //        SaveCart(GetCart());
        //    }
        //    return RedirectToAction("Index");
        //}

        //public IActionResult ClearCart()
        //{
        //    var cart = GetCart();
        //    cart.CartLines.Clear();
        //    SaveCart(cart);
        //    return RedirectToAction("Index");
        //}

        //private void SaveCart(Cart cart)
        //    => HttpContext.Session.SetJson("Cart", cart);

        private Cart GetCart()
            => HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
    }
}
