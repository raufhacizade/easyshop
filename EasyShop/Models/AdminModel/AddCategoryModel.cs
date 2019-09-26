using EasyShop.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models
{
    public class AddCategoryModel
    {
        public AddCategoryModel()
        {
            ParentCategorySelectList = new List<SelectListItem>();
        }
        [Required]
        public string CategoryName { get; set; }
        [Required(ErrorMessage ="Parent Category Name is required")]
        public int ParentCategoryId { get; set; }
        public List<SelectListItem> ParentCategorySelectList { get; set; }
    }
}
