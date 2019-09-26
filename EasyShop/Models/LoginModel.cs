using EasyShop.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="You must enter valid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Your password must contain at least 8 characters.")]
        [UIHint("password")]
        public string Password { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
    }
}
