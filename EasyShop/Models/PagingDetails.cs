using EasyShop.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models
{
    public class PagingDetails
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages()=> (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        
    }
}
