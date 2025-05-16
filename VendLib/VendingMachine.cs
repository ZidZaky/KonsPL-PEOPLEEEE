using System;
using System.Collections.Generic;

namespace VendLib
{
    public class VendingMachine
    {
        private readonly Dictionary<ProductCode, Product> _products;

        public VendingMachine()
        {
            _products = GetDefaultProducts();
        }

        public VendingMachine(Dictionary<ProductCode, Product> customMap)
        {
            if (customMap == null || customMap.Count == 0)
                throw new ArgumentException("Product map tidak boleh null atau kosong.");

            _products = customMap;
        }

        public Product? GetProduct(ProductCode code)
        {
            return _products.TryGetValue(code, out var product) ? product : null;
        }

        private static Dictionary<ProductCode, Product> GetDefaultProducts()
        {
            return new Dictionary<ProductCode, Product>
            {
                { ProductCode.A1, new Product("Chitato", 10000) },
                { ProductCode.A2, new Product("Taro", 12000) },
                { ProductCode.A3, new Product("Qtela", 9000) },
                { ProductCode.A4, new Product("Lay's", 11000) },
                { ProductCode.A5, new Product("Cheetos", 100000) },
                { ProductCode.A6, new Product("Tic Tac", 9000) }
            };
        }
    }
}
