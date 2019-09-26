using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Entity
{
    public class ProductFeature
    {
        public int ProductFeatureId { get; set; }

        [Required]
        public string FeatureName { get; set; }
        [Required]
        public string Value { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
