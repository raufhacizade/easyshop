using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models
{
    public class OrderModel
    {
        public Cart Cart { get; set; }

        [Required(ErrorMessage = "Please enter  your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please enter a valid Email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a valid Phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter a Country name")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please enter a City name")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a Zip code")]
        public string Zip { get; set; }

    }
}
