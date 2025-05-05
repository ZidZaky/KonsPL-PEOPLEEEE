using Xunit;
using VendLib;
using System.Collections.Generic;

namespace UnitTestVend
{
    public class UnitTest1
    {
        [Fact]
        public void VendingMachine_ShouldReturnCorrectProduct_ForEachCode()
        {
            // Arrange
            var productMap = new Dictionary<ProductCode, Product>
            {
                { ProductCode.A1, new Product { Name = "Chitato", Price = 10000 } },
                { ProductCode.A2, new Product { Name = "Taro", Price = 12000 } },
                { ProductCode.A3, new Product { Name = "Qtela", Price = 9000 } },
                { ProductCode.A4, new Product { Name = "Lay's", Price = 11000 } },
                { ProductCode.A5, new Product { Name = "Cheetos", Price = 100000 } },
                { ProductCode.A6, new Product { Name = "Tic Tac", Price = 9000 } }
            };

            var vendingMachine = new VendingMachine(productMap);

            // Act & Assert
            foreach (var entry in productMap)
            {
                var actual = vendingMachine.GetProduct(entry.Key);
                Assert.Same(entry.Value, actual);
                Assert.Equal(entry.Value.Name, actual.Name);
                Assert.Equal(entry.Value.Price, actual.Price);
            }
        }

        [Theory]
        [InlineData(ProductCode.A1, "Chitato", 10000)]
        [InlineData(ProductCode.A2, "Taro", 12000)]
        [InlineData(ProductCode.A3, "Qtela", 9000)]
        [InlineData(ProductCode.A4, "Lay's", 11000)]
        [InlineData(ProductCode.A5, "Cheetos", 100000)]
        [InlineData(ProductCode.A6, "Tic Tac", 9000)]
        public void GetProduct_ShouldReturnExpectedValues(ProductCode code, string expectedName, decimal expectedPrice)
        {
            // Arrange
            var vendingMachine = new VendingMachine();

            // Act
            var product = vendingMachine.GetProduct(code);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(expectedName, product.Name);
            Assert.Equal(expectedPrice, product.Price);
        }
    }
}
