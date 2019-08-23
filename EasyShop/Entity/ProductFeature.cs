using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Entity
{
    public class ProductFeature
    {
        public int ProductFeatureId { get; set; }

        public string FeatureName { get; set; }
        public string Value { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
