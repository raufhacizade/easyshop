using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyShop.IdentityEntity;
using EasyShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View(new LoginModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    if (result.Succeeded) return Redirect(returnUrl ?? "/");
                    ModelState.AddModelError(nameof(model.Email), "User was not found");
                }
                ModelState.AddModelError(nameof(model.Email), "Invalid email or password");
            }
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult SignUp(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View(new LoginModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {

                    AppUser newUser = new AppUser()
                    {

                        UserName = model.Email,
                        Email = model.Email,
                        Name = model.Name,
                        Surname = model.Surname,
                        PhoneNumber = model.PhoneNumber,
                        Country = model.Country,
                        City = model.City,
                        Zip = model.Zip
                    };

                    var createUserResult = await userManager.CreateAsync(newUser, model.Password);
                    //var errors = createUserResult.Errors.ToArray();

                    if (createUserResult.Succeeded)
                    {
                        var addRoleResult = await userManager.AddToRoleAsync(newUser, "customer");
                        if (addRoleResult.Succeeded)
                        {
                            ViewBag.Success = "Sign Up was successful.You can log in this account";
                            return View("Login", model);
                        }
                        else ModelState.AddModelError("", "System has some problem, Please, Go to Sign Up and try again!");
                    }
                    else ModelState.AddModelError("", "There is some propblem! Please, Go to Sign Up and try again!");

                }
                else
                    ModelState.AddModelError("", "This email has already taken! Please, Go to Sign Up and use another Email");
            }
            else
                ModelState.AddModelError("", "Please, Go to SignUp and Fill all required fields");

            return View("Login", model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Product");
        }
    }
}