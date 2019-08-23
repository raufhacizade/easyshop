using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Entity
{
    public enum OrderState
    {
        [Display(Name = "Pending Approval")]
        Waiting,

        [Display(Name = "Order Completed")]
        Completed
    }
}
