using EasyShop.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models.AdminModel
{
    public class OrderPagingModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public PagingDetails PagingDetails { get; set; }
    }
}
