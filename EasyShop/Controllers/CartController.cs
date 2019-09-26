using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyShop.Entity;
using EasyShop.IdentityEntity;
using EasyShop.Infrastructure;
using EasyShop.Models;
using EasyShop.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.Controllers
{
    public class CartController : Controller
    {
        private UserManager<AppUser> userManager;
        private IUnitOfWork unitOfWork;

        public CartController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public IActionResult Index() => View(GetCart());

        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var product = unitOfWork.Products.Get(id);

            if (product != null)
            {
                var cart = GetCart();
                cart.AddProduct(product, quantity);
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int ProductId)
        {
            var product = unitOfWork.Products.Get(ProductId);

            if (product != null)
            {
                GetCart().RemoveProduct(product);
                SaveCart(GetCart());
            }
            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            var cart = GetCart();
            cart.CartLines.Clear();
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var order = new OrderModel();

            if (User.Identity.IsAuthenticated)
            {
                var userId = userManager.GetUserId(HttpContext.User);
                AppUser user = userManager.FindByIdAsync(userId).Result;

                order.Name = user.Name;
                order.Surname = user.Surname;
                order.Email = user.Email;
                order.PhoneNumber = user.PhoneNumber;
                order.Country = user.Country;
                order.City = user.City;
                order.Zip = user.Zip;
            }

            order.Cart = GetCart();
            return View(order);
        }

        [HttpPost]
        public IActionResult Checkout(OrderModel model)
        {
            var cart = GetCart();

            if (cart.CartLines.Count == 0)
                ModelState.AddModelError("", "There is no any product in your Cart");
            else
            {
                if (ModelState.IsValid)
                {
                    SaveOrder(cart, model);
                    cart.ClearAll();
                    SaveCart(cart);
                    return View("Complated");
                }
            }

            model.Cart = cart;
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int cartLineIndex, int quantity)
        {
            var cart = GetCart();

            cart.UpdateProductQuantity(cartLineIndex, quantity);
            SaveCart(cart);

            return RedirectToAction("Index");
        }

        private void SaveOrder(Cart cart, OrderModel model)
        {
            var order = new Order();

            order.Bill = cart.GetBill();
            order.OrderDate = DateTime.Now;
            order.OrderState = OrderState.Waiting;



            order.UserName = model.Name;
            order.UserSurname = model.Surname;
            order.UserEmail = model.Email;
            order.UserPhone = model.PhoneNumber;


            order.Country = model.Country;
            order.City = model.City;
            order.Zip = model.Zip;

            foreach (var cartline in cart.CartLines)
            {
                var orderline = new OrderLine();
                orderline.Quantity = cartline.Quantity;
                orderline.ProductPrice = cartline.CurrentProductPrice;
                orderline.ProductId = cartline.Product.ProductId;

                order.OrderLines.Add(orderline);
            }
            unitOfWork.Orders.Add(order);
            unitOfWork.SaveChanges();
        }

        private void SaveCart(Cart cart)
           => HttpContext.Session.SetJson("Cart", cart);

        private Cart GetCart()
            => HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
    }
}