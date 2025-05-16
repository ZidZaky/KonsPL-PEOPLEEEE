using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendLib;
using System.Collections.Generic;

namespace MSTestVend
{
    [TestClass]
    public class UnitTest1
    {
        // Gunakan constructor baru Product(name, price)
        private Dictionary<ProductCode, Product> GetSampleProductMap()
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

        [TestMethod]
        public void GetProduct_ShouldReturnCorrectProduct_A1()
        {
            var map = GetSampleProductMap();
            var vm = new VendingMachine(map);

            var result = vm.GetProduct(ProductCode.A1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Chitato", result.Name);
            Assert.AreEqual(10000, result.Price);
        }

        [TestMethod]
        public void GetProduct_ShouldReturnCorrectProduct_A6()
        {
            var map = GetSampleProductMap();
            var vm = new VendingMachine(map);

            var result = vm.GetProduct(ProductCode.A6);

            Assert.IsNotNull(result);
            Assert.AreEqual("Tic Tac", result.Name);
            Assert.AreEqual(9000, result.Price);
        }

        [TestMethod]
        public void GetProduct_InvalidCode_ShouldReturnNull()
        {
            var map = GetSampleProductMap();
            var vm = new VendingMachine(map);

            var result = vm.GetProduct((ProductCode)999); // kode tidak valid

            Assert.IsNull(result);
        }
    }
}
