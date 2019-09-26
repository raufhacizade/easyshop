using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Entity
{

    public static class CountryList
    {
        public static List<SelectListItem> CountrySelectList()
        {
            var list = new List<SelectListItem>();

            foreach (var eCountry in Enum.GetValues(typeof(EnumCountries)))
                list.Add(new SelectListItem { Text = eCountry.ToString(), Value = eCountry.ToString() });

            return list;
        }

    }

    public enum EnumCountries
    {
        Afghanistan, Albania, Algeria, Andorra, Angola, Australia, Austria, Azerbaijan,
        Brazil, Canada, Chile, Denmark, Estonia, Georgia, Germany, Turkey, UK
    }
}
