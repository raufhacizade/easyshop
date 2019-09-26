using EasyShop.IdentityEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models.AdminModel
{
    public class UsersModel
    {
       public IEnumerable<AppUser> Users;
       public PagingDetails PagingDetails;
    }
}
