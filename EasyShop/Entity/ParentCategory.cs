using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Entity
{
    public class ParentCategory
    {
        public int ParentCategoryId { get; set; }
        [Required]
        public string ParentCategoryName { get; set; }

        public List<Category> Categories { get; set; }
    }
}
