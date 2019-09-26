using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyShop.Entity;
using EasyShop.Models;
using EasyShop.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http.Internal;
using EasyShop.Models.AdminModel;
using Microsoft.AspNetCore.Identity;
using EasyShop.IdentityEntity;

namespace EasyShop.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        public int OrderPageSize = 5;
        public int UserPageSize = 5;
        public int ProductPageSize = 5;
        public int BrandPageSize = 2;

        private IUnitOfWork unitOfWork;
        private UserManager<AppUser> userManager;
        private readonly IHostingEnvironment hostingEnvironment;

        public AdminController(IUnitOfWork unitOfWork, IHostingEnvironment hostingEnvironment, UserManager<AppUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
        }

        public IActionResult Index() => View();

        public IActionResult Orders(int page = 1)
        {
            var orders = unitOfWork.Orders.GetAll().Where(o => o.OrderState == OrderState.Waiting);
            var count = orders.Count();
            orders = orders.Skip((page - 1) * OrderPageSize).Take(OrderPageSize);

            ViewBag.Action = "Orders";
            var model = new OrderPagingModel()
            {
                Orders = orders,
                PagingDetails = new PagingDetails()
                {
                    CurrentPage = page,
                    ItemsPerPage = OrderPageSize,
                    TotalItems = count
                }
            };
            return View(model);
        }

        public IActionResult ComplatedOrders(int page = 1)
        {
            var orders = unitOfWork.Orders.GetAll().Where(o => o.OrderState == OrderState.Completed);
            var count = orders.Count();
            orders = orders.Skip((page - 1) * OrderPageSize).Take(OrderPageSize);

            ViewBag.Action = "ComplatedOrders";
            var model = new OrderPagingModel()
            {
                Orders = orders,
                PagingDetails = new PagingDetails()
                {
                    CurrentPage = page,
                    ItemsPerPage = OrderPageSize,
                    TotalItems = count
                }
            };
            return View(model);
        }

        public IActionResult OrderComplate(int orderId)
        {
            var order = unitOfWork.Orders.Get(orderId);

            if (order != null)
            {
                order.OrderState = OrderState.Completed;
                unitOfWork.SaveChanges();
            }

            return RedirectToAction("Orders");
        }

        public IActionResult OrderDetails(int orderId)
        {
            var order = unitOfWork.Orders.GetAll()
                                         .Include(o => o.OrderLines)
                                         .ThenInclude(oL => oL.Product)
                                         .Where(o => o.OrderId == orderId).FirstOrDefault();

            return View(order);
        }

        public IActionResult BrandList()
        {
            List<Brand> brands = unitOfWork.Brands.GetAll()
                .Include(d => d.Products).ToList();

            return View(brands);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBrand(AddBrandModel model)
        {
            if (ModelState.IsValid)
            {
                Brand brand = new Brand()
                {
                    BrandName = model.BrandName
                };

                if (model.BrandLogo != null)
                {
                    var fileName = Path.Combine(hostingEnvironment.WebRootPath, "images/BrandLogo", Path.GetFileName(model.BrandLogo.FileName));
                    model.BrandLogo.CopyTo(new FileStream(fileName, FileMode.Create));
                    brand.BrandLogo = Path.GetFileName(model.BrandLogo.FileName);
                    unitOfWork.Brands.Add(brand);
                    unitOfWork.SaveChanges();

                    List<Brand> brands = unitOfWork.Brands.GetAll()
                                                          .Include(d => d.Products).ToList();

                    return View("BrandList", brands);
                }
            }
            ModelState.AddModelError("", "Brand adding is unsuccess");
            return RedirectToAction("BrandList");
        }


        #region OldWithAjaxToAddBrand
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AddBrand(AddBrandModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Brand brand = new Brand()
        //        {
        //            BrandName = model.BrandName
        //        };

        //        if (model.BrandLogo != null)
        //        {
        //            var fileName = Path.Combine(hostingEnvironment.WebRootPath, "images/BrandLogo", Path.GetFileName(model.BrandLogo.FileName));
        //            model.BrandLogo.CopyTo(new FileStream(fileName, FileMode.Create));
        //            brand.BrandLogo = Path.GetFileName(model.BrandLogo.FileName);
        //            unitOfWork.Brands.Add(brand);
        //            unitOfWork.SaveChanges();

        //            var data = new string[] {unitOfWork.Brands.GetAll().ToList().Count.ToString(), brand.BrandId.ToString(), brand.BrandName, brand.BrandLogo };
        //            return Ok(data);
        //        }
        //    }
        //    return BadRequest();
        //}
        #endregion

        public IActionResult UpdateBrand(Brand brand, IFormFile brandLogo)
        {
            var updatedbrand = unitOfWork.Brands.Get(brand.BrandId);

            if (brand != null)
            {
                updatedbrand.BrandName = brand.BrandName;

                if (brandLogo != null)
                {
                    var fileName = Path.Combine(hostingEnvironment.WebRootPath, "images/BrandLogo", Path.GetFileName(brandLogo.FileName));
                    brandLogo.CopyTo(new FileStream(fileName, FileMode.Create));
                    updatedbrand.BrandLogo = Path.GetFileName(brandLogo.FileName);
                }

                unitOfWork.SaveChanges();
            }
            return RedirectToAction("BrandList");
        }

        [HttpPost]
        public IActionResult UpdateParentCategory(ParentCategory model)
        {
            if (ModelState.IsValid)
            {
                int id = model.ParentCategoryId;
                var parentCategory = unitOfWork.ParentCategories.Get(id);

                if (parentCategory != null)
                {
                    parentCategory.ParentCategoryName = model.ParentCategoryName;
                    unitOfWork.SaveChanges();
                }
                else ModelState.AddModelError("", "Updated parent category was not found");
            }
            else ModelState.AddModelError("", "Model error in parent category update");

            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public IActionResult UpdatetCategory(int id, string categoryName, int parentCategoryId)
        {
            if (ModelState.IsValid)
            {
                var category = unitOfWork.Categories.Get(id);

                if (category != null)
                {
                    category.CategoryName = categoryName;
                    category.ParentCategoryId = parentCategoryId;
                    unitOfWork.SaveChanges();
                }
                else ModelState.AddModelError("", "Updated category was not found");
            }
            else ModelState.AddModelError("", "Model error in category update");

            return RedirectToAction("CategoryList");
        }

        public IActionResult DeleteBrand(int id)
        {

            Brand brand = unitOfWork.Brands.Get(id);
            var products = unitOfWork.Products.GetAll().Where(p => p.BrandId == id).ToList();

            foreach (var product in products)
                unitOfWork.Products.Delete(product);

            unitOfWork.Brands.Delete(brand);

            unitOfWork.SaveChanges();
            return RedirectToAction("BrandList");
        }

        public IActionResult CategoryList()
        {
            var model = new CategoryListModel()
            {
                ParentCategories = unitOfWork.ParentCategories.GetAll().Include(p => p.Categories).ToList(),
                Categories = unitOfWork.Categories.GetAll()
                                                    .Include(p => p.ProductCategories)
                                                    .ThenInclude(pc => pc.Product)
                                                    .ToList()
            };

            return View(model);
        }

        public IActionResult DeleteParentCategory(int id)
        {
            var parentCategory = unitOfWork.ParentCategories.Get(id);
            var categories = unitOfWork.Categories.GetAll().Where(c => c.ParentCategoryId == id).ToList();
            List<List<Product>> productsOfParent = new List<List<Product>>();

            foreach (var category in categories)
            {
                var productsOfCategory = unitOfWork.Products.GetAll()
                                                  .Include(p => p.ProductCategories)
                                                  .ThenInclude(pc => pc.Category)
                                                  .Where(p => p.ProductCategories.Any(pc => pc.CategoryId == category.CategoryId)).ToList();

                productsOfParent.Add(productsOfCategory);
            }

            foreach (var categoryProductsList in productsOfParent)
                foreach (var product in categoryProductsList)
                    unitOfWork.Products.Delete(product);

            foreach (var category in categories)
                unitOfWork.Categories.Delete(category);

            unitOfWork.ParentCategories.Delete(parentCategory);
            unitOfWork.SaveChanges();
            return RedirectToAction("CategoryList");
        }

        public IActionResult DeleteCategory(int id)
        {

            var category = unitOfWork.Categories.Get(id);
            var products = unitOfWork.Products.GetAll()
                                                  .Include(p => p.ProductCategories)
                                                  .ThenInclude(pc => pc.Category)
                                                  .Where(p => p.ProductCategories.Any(pc => pc.CategoryId == category.CategoryId)).ToList();

            foreach (var product in products)
                unitOfWork.Products.Delete(product);

            unitOfWork.Categories.Delete(category);

            unitOfWork.SaveChanges();
            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddParentCategory(ParentCategory entity)
        {
            if (ModelState.IsValid)
            {
                entity.Categories = new List<Category>();
                unitOfWork.ParentCategories.Add(entity);
                unitOfWork.SaveChanges();

                return Ok(entity);
            }
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategory(AddCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                ParentCategory parentCateogory = unitOfWork.ParentCategories.Get(model.ParentCategoryId);

                if (parentCateogory != null)
                {
                    Category category = new Category()
                    {
                        CategoryName = model.CategoryName,
                        ParentCategory = parentCateogory,
                        ProductCategories = new List<ProductCategory>()
                    };
                    unitOfWork.Categories.Add(category);
                    unitOfWork.SaveChanges();
                    var data = new string[] { category.CategoryId.ToString(), category.CategoryName, category.ParentCategory.ParentCategoryName };
                    //var data = JsonConvert.SerializeObject(category);
                    return Ok(data);
                }
                else ModelState.AddModelError("", "ParentCategory was not found!");

            }
            return BadRequest();
        }

        public IActionResult ProductList(int page=1)
        {
            List<AdminProductModel> productModels = new List<AdminProductModel>();
            var products = unitOfWork.Products.GetAll();

            var count = products.Count();
            products = products.Skip((page - 1) * ProductPageSize).Take(ProductPageSize);

            products = products.Include(p => p.ProductCategories).ThenInclude(pC => pC.Category);

            foreach (var product in products)
            {
                var categoriesName = product.ProductCategories.Select(i => i.Category.CategoryName).ToList();

                AdminProductModel productModel = new AdminProductModel()
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    ProfilImage = product.ProfilImage,
                    AvailableAmount = product.AvailableAmount,
                    IsHome = product.IsHome,
                    Discount = product.Discount,
                    Categories = String.Join(",", categoriesName)
                };
                productModels.Add(productModel);
            }
            ViewBag.Action = "Orders";
            var model = new AdminPagingProductModel()
            {
                Products = productModels,
                PagingDetails = new PagingDetails()
                {
                    CurrentPage = page,
                    ItemsPerPage = OrderPageSize,
                    TotalItems = count
                }
            };
            return View(model);
        }


        public IActionResult UpdateProduct(int id)
        {
            Product product = unitOfWork.Products.GetAll()
                                                 .Include(p => p.ProductCategories)
                                                 .ThenInclude(pc => pc.Category)
                                                 .Where(p => p.ProductId == id).FirstOrDefault();

            ExtentedProductModel model = new ExtentedProductModel()
            {
                Product = product,
                BrandId = product.BrandId,
                ProfilImageFile = new FormFile(null, 0, 0, null, product.ProfilImage),
                BrandSelectList = new List<SelectListItem>(),
                CategorySelectList = new List<SelectListItem>()
            };

            FillSelectListsForUpdateProduct(product, model.BrandSelectList, model.CategorySelectList);

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateProduct(ExtentedProductModel model)
        {
            var product = unitOfWork.Products.GetAll()
                                             .Include(p => p.ProductCategories)
                                             .ThenInclude(pc => pc.Category)
                                             .Where(p => p.ProductId == model.Product.ProductId)
                                             .FirstOrDefault();

            if (product != null)
            {
                product.ProductName = model.Product.ProductName;
                product.Price = model.Product.Price;
                product.SoldAmount = model.Product.SoldAmount;
                product.AvailableAmount = model.Product.AvailableAmount;
                product.Gender = model.Product.Gender;
                product.ShortDescription = model.Product.ShortDescription;
                product.Description = model.Product.Description;
                product.DateAdded = model.Product.DateAdded;
                product.IsHome = model.Product.IsHome;
                product.BrandId = model.BrandId;

                //foreach (var item in product.ProductCategories)
                //    unitOfWork.RemoveFromProductCategory(product.ProductId, item.CategoryId);

                product.ProductCategories.RemoveAll(pc => pc.ProductId == product.ProductId);

                List<ProductCategory> newProductCategories = new List<ProductCategory>();
                foreach (var id in model.CatgoriesIdArray)
                    newProductCategories.Add(new ProductCategory()
                    {
                        Product = product,
                        CategoryId = id
                    });

                product.ProductCategories = newProductCategories;


                if (model.ProfilImageFile != null)
                {
                    var fileName = Path.Combine(hostingEnvironment.WebRootPath, "images/products/profil", Path.GetFileName(model.ProfilImageFile.FileName));
                    model.ProfilImageFile.CopyTo(new FileStream(fileName, FileMode.Create));
                    product.ProfilImage = Path.GetFileName(model.ProfilImageFile.FileName);
                }

                unitOfWork.SaveChanges();

                return RedirectToAction("ProductList");
            }
            else ModelState.AddModelError("", "Product was not found");

            FillSelectListsForUpdateProduct(model.Product, model.BrandSelectList, model.CategorySelectList);

            return View(model);
        }

        public IActionResult AddProduct()
        {
            ExtentedProductModel model = new ExtentedProductModel();
            model.Product = new Product();
            model.Product.ProductCategories = new List<ProductCategory>();

            model.BrandSelectList = new List<SelectListItem>();
            model.CategorySelectList = new List<SelectListItem>();

            FillSelectListsForUpdateProduct(model.Product, model.BrandSelectList, model.CategorySelectList);

            return View(model);
        }

        [HttpPost]
        public IActionResult AddProduct(ExtentedProductModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ProfilImageFile != null)
                {
                    var fileName = Path.Combine(hostingEnvironment.WebRootPath, "images/products/profil", Path.GetFileName(model.ProfilImageFile.FileName));
                    model.ProfilImageFile.CopyTo(new FileStream(fileName, FileMode.Create));
                    model.Product.ProfilImage = Path.GetFileName(model.ProfilImageFile.FileName);

                    model.Product.BrandId = model.BrandId;
                    model.Product.ProductCategories = new List<ProductCategory>();
                    model.Product.DateAdded = DateTime.Now;

                    unitOfWork.Products.Add(model.Product);
                    unitOfWork.Products.Save();

                    List<ProductCategory> productCategories = new List<ProductCategory>();
                    foreach (var categoryId in model.CatgoriesIdArray)
                        productCategories.Add(new ProductCategory()
                        {
                            Product = model.Product,
                            CategoryId = categoryId
                        });

                    model.Product.ProductCategories = productCategories;

                    model.Product.Features = new List<ProductFeature>();
                    unitOfWork.SaveChanges();

                    ViewBag.ProductId = model.Product.ProductId;
                    ViewBag.ProductName = model.Product.ProductName;
                    return RedirectToAction("ProductList");

                }
                else ModelState.AddModelError("", "Profil image is required");
            }
            return View(model);
        }

        public IActionResult DeleteProduct(int id)
        {
            var product = unitOfWork.Products.Get(id);

            if (product != null)
            {
                unitOfWork.Products.Delete(product);
                unitOfWork.SaveChanges();
            }

            return RedirectToAction("ProductList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProductFeature(ProductFeature feature)
        {
            var product = unitOfWork.Products.GetAll()
                                             .Include(p => p.Features)
                                             .Where(p => p.ProductId == feature.ProductId)
                                             .FirstOrDefault();

            if (ModelState.IsValid)
            {
                product.Features.Add(feature);
                unitOfWork.SaveChanges();

                var json = new string[] {feature.ProductFeatureId.ToString(), feature.FeatureName, feature.Value };

                return Ok(json);
            }

            return BadRequest();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult UpdateProductFeature(int ProductId)
        {
            var product = unitOfWork.Products.GetAll()
                                                 .Include(p => p.Features)
                                                 .Where(p => p.ProductId == ProductId).FirstOrDefault();
            ViewBag.ProductId = product.ProductId;
            ViewBag.ProductName = product.ProductName;
            return View(product.Features);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProductFeatureItem(ProductFeature model)
        {
            var product = unitOfWork.Products.GetAll()
                                                 .Include(p => p.Features)
                                                 .Where(p => p.ProductId == model.ProductId).FirstOrDefault();
            ViewBag.ProductId = product.ProductId;
            ViewBag.ProductName = product.ProductName;

            var feature = product.Features.Where(f => f.ProductFeatureId == model.ProductFeatureId).FirstOrDefault();

            feature.FeatureName = model.FeatureName;
            feature.Value = model.Value;

            unitOfWork.SaveChanges();

            return View("UpdateProductFeature",product.Features);
        }

        public IActionResult DeleteProductFeatureItem(int FeatureIndex, int ProductId)
        {
            var product = unitOfWork.Products.GetAll()
                                             .Include(p => p.Features)
                                             .Where(p => p.ProductId == ProductId)
                                             .FirstOrDefault();
            product.Features.RemoveAt(FeatureIndex);
            //unitOfWork.RemoveFeatureFromProductByFeatureId(FeatureId);
            unitOfWork.SaveChanges();

            ViewBag.ProductId = product.ProductId;
            ViewBag.ProductName = product.ProductName;

            return View("UpdateProductFeature", product.Features);
        }

        public IActionResult Users(int page=1)
        {
            var users = userManager.Users;

            var count = users.Count();
            users = users.Skip((page - 1) * UserPageSize).Take(UserPageSize);

            var model = new UsersModel()
            {
                Users = users,
                PagingDetails = new PagingDetails()
                {
                    CurrentPage = page,
                    ItemsPerPage = OrderPageSize,
                    TotalItems = count
                }
            };

            return View(model);
        }

        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = userManager.Users.Where(u => u.Email == email).FirstOrDefault();
            await userManager.DeleteAsync(user);

            return RedirectToAction("Users");
        }

        private void FillSelectListsForUpdateProduct(Product product, List<SelectListItem> BrandSelectList, List<SelectListItem> CategorySelectList)
        {
            List<SelectListItem> selectedCategories = new List<SelectListItem>();

            if (product.ProductCategories.Count != 0)
                foreach (var productCategory in product.ProductCategories)
                    selectedCategories.Add(new SelectListItem()
                    {
                        Text = productCategory.Category.CategoryName,
                        Value = productCategory.Category.CategoryId.ToString()
                    });


            foreach (var category in unitOfWork.Categories.GetAll().ToList())
            {
                SelectListItem item = new SelectListItem()
                {
                    Text = category.CategoryName,
                    Value = category.CategoryId.ToString()
                };

                if (selectedCategories.Count != 0)
                    foreach (var selected in selectedCategories)
                        if (item.Value == selected.Value) item.Selected = true;


                CategorySelectList.Add(item);
            }

            foreach (var brand in unitOfWork.Brands.GetAll().ToList())
            {
                if (brand.BrandId == product.BrandId)
                    BrandSelectList.Add(new SelectListItem()
                    {
                        Text = brand.BrandName,
                        Value = brand.BrandId.ToString(),
                        Selected = true
                    });
                else
                    BrandSelectList.Add(new SelectListItem()
                    {
                        Text = brand.BrandName,
                        Value = brand.BrandId.ToString()
                    });
            }
        }
    }
}