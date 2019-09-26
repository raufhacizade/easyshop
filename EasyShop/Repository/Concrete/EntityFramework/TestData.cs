using EasyShop.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Repository.Concrete.EntityFramework
{
    public static class TestData
    {
        public static void AddTestData(IApplicationBuilder app)
        {
            var context = app.ApplicationServices
                .GetRequiredService<EasyShopContext>();

            context.Database.Migrate();

            if (!context.Products.Any())
            {
                var parentCategories = new[]
                {
                    new ParentCategory(){ParentCategoryName="Electronics"},
                    new ParentCategory(){ParentCategoryName="Furnitures"},
                    new ParentCategory(){ParentCategoryName="Women’s Clothing"},
                    new ParentCategory(){ParentCategoryName="Men’s Clothing"}
                };
                context.ParentCategories.AddRange(parentCategories);

                var categories = new[]
                {
                    new Category(){ CategoryName = "SmartPhones",ParentCategory=parentCategories[0]},
                    new Category(){ CategoryName = "Tablets", ParentCategory=parentCategories[0]},
                    new Category(){ CategoryName = "Sofa", ParentCategory = parentCategories[1]},
                    new Category(){ CategoryName = "Women’s sneakers", ParentCategory = parentCategories[2]},
                    new Category(){ CategoryName = "Men’s sneakers", ParentCategory = parentCategories[3]},
                };
                context.Categories.AddRange(categories);

                var brands = new[]
                {
                    new Brand(){BrandName="Apple"},
                    new Brand(){BrandName="Baker"},
                    new Brand(){BrandName="Samsung"},
                    new Brand(){BrandName="Nike"},
                    new Brand(){BrandName="Terranova"}
                };
                context.Brands.AddRange(brands);

                var products = new[]
                {
                    new Product(){ProductName="IPhone 8 Plus Latest Ver.",Price=460,SoldAmount=10,AvailableAmount=252,
                                  ProfilImage ="iphone8_plus.jpg", DateAdded=DateTime.Now.AddDays(-10),
                                  Brand =brands[0],IsHome=true,ShortDescription="This is latest version of IPhone 8",
                                  Gender ="Both",Discount=0,Description="This is Iphone 8 This is Iphone 8 This is Iphone 8 This is Iphone 8 This is Iphone 8" },

                    new Product(){ProductName="IPhone XS",Price=670,SoldAmount=23,AvailableAmount=452,
                                  ProfilImage ="iphoneXS.jpg", DateAdded=DateTime.Now,Brand=brands[0],
                                  IsHome=false,ShortDescription="This is new Phone",
                                  Gender ="Both",Discount=10,Description="This is Iphone 8 This is Iphone 8 This is Iphone 8 This is Iphone 8 This is Iphone 8" },
                    
                    new Product(){ProductName="Samsung Galaxy Note 10",Price=670,SoldAmount=23,AvailableAmount=452,
                                  ProfilImage ="Samsung_Galaxy_Note_10.jpg", DateAdded=DateTime.Now,Brand=brands[2],
                                  IsHome=true,ShortDescription="This is new Phone",
                                  Gender ="Both",Discount=10,Description="Samsung Galaxy Note 10 This is Iphone 8 This is Iphone 8 This is Iphone 8" },

                    new Product(){ProductName="NIKE AIR MAX 97",Price=380,SoldAmount=30,AvailableAmount=10,
                                  ProfilImage ="NIKE_AIR_MAX_97.jpg", DateAdded=DateTime.Now,Brand=brands[3],
                                  IsHome=true,ShortDescription="This is new Men's black sneaker",
                                  Gender ="Both",Discount=30,Description="Men's black sneaker NIKE AIR MAX 97" },

                    new Product(){ProductName="Samsung Galaxy Tab A 10.1",Price=1270,SoldAmount=23,AvailableAmount=452,
                                  ProfilImage ="Samsung Galaxy Tab A 10.1.jpg", DateAdded=DateTime.Now,Brand=brands[2],
                                  IsHome=true,ShortDescription="This is new Samsung Galaxy Tab A 10.1",
                                  Gender ="Both",Discount=10,Description="Samsung Galaxy Tab A 10.1is Iphone 8 This is Iphone 8 This is Iphone 8" }

                };
                context.Products.AddRange(products);

                var productcategories = new[]
                {
                    new ProductCategory(){Category=categories[0],Product=products[0]},
                    new ProductCategory(){Category=categories[0],Product=products[1]},
                    new ProductCategory(){Category=categories[0],Product=products[2]},
                    new ProductCategory(){Category=categories[4],Product=products[3]},
                    new ProductCategory(){Category=categories[1],Product=products[4]}
                };
                context.AddRange(productcategories);

                var productFeatures = new[]
                {
                    new ProductFeature(){ FeatureName="Screan",Value="5.5-inch (diagonal) widescreen LCD Multi-Touch",Product=products[0]},
                    new ProductFeature(){ FeatureName="Resolution ",Value="1920-by-1080-pixel",Product=products[0]},
                    new ProductFeature(){ FeatureName="battery ",Value="4000MA",Product=products[0]},

                    new ProductFeature(){ FeatureName="Screan",Value="5.5-inch (diagonal) widescreen LCD Multi-Touch",Product=products[1]},
                    new ProductFeature(){ FeatureName="Resolution ",Value="1920-by-1080-pixel",Product=products[1]},
                    new ProductFeature(){ FeatureName="battery ",Value="4000MA",Product=products[1]},

                    new ProductFeature(){ FeatureName="Screan",Value="5.5-inch (diagonal) widescreen LCD Multi-Touch",Product=products[2]},
                    new ProductFeature(){ FeatureName="Resolution ",Value="1920-by-1080-pixel",Product=products[2]},
                    new ProductFeature(){ FeatureName="battery ",Value="4000MA",Product=products[2]},

                    new ProductFeature(){ FeatureName="Available sizes",Value="39, 40, 41, 42, 43, 44, 45",Product=products[3]},
                    new ProductFeature(){ FeatureName="Available Colors",Value="Black, Red, Green, Yellow, Blue",Product=products[3]},

                    new ProductFeature(){ FeatureName="Screan",Value="5.5-inch (diagonal) widescreen LCD Multi-Touch",Product=products[3]},
                    new ProductFeature(){ FeatureName="Resolution ",Value="1920-by-1080-pixel",Product=products[3]},
                    new ProductFeature(){ FeatureName="battery ",Value="4000MA",Product=products[3]}
                };
                context.ProductFeatures.AddRange(productFeatures);

                context.SaveChanges();
            }
        }
    }
}
