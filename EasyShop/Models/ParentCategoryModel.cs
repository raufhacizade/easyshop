using EasyShop.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models
{
    public class ParentCategoryModel
    {
        public int ParentCategoryId { get; set; }
        public string ParentCategoryName { get; set; }
        public List<Category> Categories { get; set; }
    }
}
