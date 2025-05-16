using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MSTestVend;

[TestClass]
public class UnitTest1
{
    private SmartVendingMachine vendingMachine;

    [TestInitialize]
    public void Setup()
    {
        vendingMachine = new SmartVendingMachine();
    }

    [TestMethod]
    public void TestGetProductByCode_P01_Returns_Fanta()
    {
        // Arrange
        ProductCode code = ProductCode.P01;

        // Act
        Product result = vendingMachine.GetProductByCode(code);

        // Assert
        Assert.AreEqual(ProductName.Fanta, result.Name);
        Assert.AreEqual(10000, result.Price);
        Assert.AreEqual(10, result.Stock);
    }

    [TestMethod]
    public void TestGetProductByCode_P02_Returns_Fruitea()
    {
        // Arrange
        ProductCode code = ProductCode.P02;

        // Act
        Product result = vendingMachine.GetProductByCode(code);

        // Assert
        Assert.AreEqual(ProductName.Fruitea, result.Name);
        Assert.AreEqual(12000, result.Price);
        Assert.AreEqual(15, result.Stock);
    }

    [TestMethod]
    public void TestGetProductByCode_P03_Returns_Sprite()
    {
        // Arrange
        ProductCode code = ProductCode.P03;

        // Act
        Product result = vendingMachine.GetProductByCode(code);

        // Assert
        Assert.AreEqual(ProductName.Sprite, result.Name);
        Assert.AreEqual(10000, result.Price);
        Assert.AreEqual(20, result.Stock);
    }

    [TestMethod]
    public void TestGetProductByCode_P04_Returns_Pepsi()
    {
        // Arrange
        ProductCode code = ProductCode.P04;

        // Act
        Product result = vendingMachine.GetProductByCode(code);

        // Assert
        Assert.AreEqual(ProductName.Pepsi, result.Name);
        Assert.AreEqual(11000, result.Price);
        Assert.AreEqual(0, result.Stock);
    }

    [TestMethod]
    public void TestGetProductByCode_P05_Returns_Aqua()
    {
        // Arrange
        ProductCode code = ProductCode.P05;

        // Act
        Product result = vendingMachine.GetProductByCode(code);

        // Assert
        Assert.AreEqual(ProductName.Aqua, result.Name);
        Assert.AreEqual(5000, result.Price);
        Assert.AreEqual(30, result.Stock);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetProductByCode_InvalidCode()
    {
        // Arrange
        ProductCode invalidCode = (ProductCode)99;

        // Act & Assert
        vendingMachine.GetProductByCode(invalidCode);
    }

    [TestMethod]
    public void TestSelectProduct_ValidProduct()
    {
        // Arrange
        ProductCode code = ProductCode.P01;
        vendingMachine.SelectProduct(code);

        // Act

        // Assert
        Assert.AreEqual(code, vendingMachine.selectedProductCode);
    }

    [TestMethod]
    public void TestGetCurrentState_InitialState()
    {
        // Arrange
        SmartVendingMachine vendingMachine = new SmartVendingMachine();

        // Act
        VendingState currentState = vendingMachine.GetCurrentState();

        // Assert
        Assert.AreEqual(VendingState.Order, currentState);
    }

    [TestMethod]
    public void TestGetCurrentState_AfterCheckout()
    {
        // Arrange
        SmartVendingMachine vendingMachine = new SmartVendingMachine();
        vendingMachine.SelectProduct(ProductCode.P01);
        vendingMachine.Checkout();

        // Act
        VendingState currentState = vendingMachine.GetCurrentState();

        // Assert
        Assert.AreEqual(VendingState.Payment, currentState);
    }
}
