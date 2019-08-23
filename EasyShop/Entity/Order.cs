using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Entity
{
    public class Order
    {
        public Order() => OrderLines = new List<OrderLine>();

        public int OrderId { get; set; }

        public double Bill { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderState OrderState { get; set; }

        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }

        public virtual List<OrderLine> OrderLines { get; set; }
    }
}
