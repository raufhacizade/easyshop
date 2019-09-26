using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models
{
    public class AdminProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int AvailableAmount { get; set; }
        public int Discount { get; set; }
        public string ProfilImage { get; set; }
        public bool IsHome { get; set; }
        public string Categories { get; set; }
    }
}
