using EasyShop.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models
{
    public class CategoryListModel
    {
        public List<ParentCategory> ParentCategories { get; set; }
        public List<Category> Categories { get; set; }
    }
}
