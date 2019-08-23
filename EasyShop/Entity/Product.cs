using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Entity
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double Price { get; set; }

        public int SoldAmount { get; set; }
        [Required]
        public int AvailableAmount { get; set; }
        public int Sale { get; set; }
        public string ProfilImage { get; set; }

        public string Gender { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }

        public bool IsHome { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }
        public List<ProductFeature> Features { get; set; }
    }
}
