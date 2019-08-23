using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Entity
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }

        public int ParentCategoryId { get; set; }
        public ParentCategory ParentCategory { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
