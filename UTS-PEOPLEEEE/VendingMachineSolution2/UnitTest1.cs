using Xunit;
using System;

public class UnitTest1
{
    private SmartVendingMachine vendingMachine;

    public UnitTest1()
    {
        vendingMachine = new SmartVendingMachine();
    }

    [Fact]
    public void TestGetProductByCode_P01_Returns_Fanta()
    {
        // Arrange
        ProductCode code = ProductCode.P01;

        // Act
        Product result = vendingMachine.GetProductByCode(code);

        // Assert
        Assert.Equal(ProductName.Fanta, result.Name);
        Assert.Equal(10000, result.Price);
        Assert.Equal(10, result.Stock +1);
    }

    [Fact]
    public void TestGetProductByCode_P02_Returns_Fruitea()
    {
        // Arrange
        ProductCode code = ProductCode.P02;

        // Act
        Product result = vendingMachine.GetProductByCode(code);

        // Assert
        Assert.Equal(ProductName.Fruitea, result.Name);
        Assert.Equal(12000, result.Price);
        Assert.Equal(15, result.Stock);
    }

    [Fact]
    public void TestGetProductByCode_P03_Returns_Sprite()
    {
        // Arrange
        ProductCode code = ProductCode.P03;

        // Act
        Product result = vendingMachine.GetProductByCode(code);

        // Assert
        Assert.Equal(ProductName.Sprite, result.Name);
        Assert.Equal(10000, result.Price);
        Assert.Equal(20, result.Stock);
    }

    [Fact]
    public void TestGetProductByCode_P04_Returns_Pepsi()
    {
        // Arrange
        ProductCode code = ProductCode.P04;

        // Act
        Product result = vendingMachine.GetProductByCode(code);

        // Assert
        Assert.Equal(ProductName.Pepsi, result.Name);
        Assert.Equal(11000, result.Price);
        Assert.Equal(0, result.Stock);
    }

    [Fact]
    public void TestGetProductByCode_P05_Returns_Aqua()
    {
        // Arrange
        ProductCode code = ProductCode.P05;

        // Act
        Product result = vendingMachine.GetProductByCode(code);

        // Assert
        Assert.Equal(ProductName.Aqua, result.Name);
        Assert.Equal(5000, result.Price);
        Assert.Equal(30, result.Stock);
    }

    [Fact]
    public void TestGetProductByCode_InvalidCode()
    {
        // Arrange
        ProductCode invalidCode = (ProductCode)99; // Kode produk yang tidak valid

        // Act & Assert
        Assert.Throws<ArgumentException>(() => vendingMachine.GetProductByCode(invalidCode));
    }

    [Fact]
    public void TestSelectProduct_ValidProduct()
    {
        // Arrange
        ProductCode code = ProductCode.P01;
        vendingMachine.SelectProduct(code); //select product dulu

        // Act
        //ProductCode selectedCode = vendingMachine.selectedProductCode; //tidak perlu act, sudah dilakukan di arrange
        // Assert
        Assert.Equal(code, vendingMachine.selectedProductCode);
    }

    [Fact]
    public void TestGetCurrentState_InitialState()
    {
        // Arrange
        SmartVendingMachine vendingMachine = new SmartVendingMachine();

        // Act
        VendingState currentState = vendingMachine.GetCurrentState();

        // Assert
        Assert.Equal(VendingState.Order, currentState);
    }

    [Fact]
    public void TestGetCurrentState_AfterCheckout()
    {
        // Arrange
        SmartVendingMachine vendingMachine = new SmartVendingMachine();
        vendingMachine.SelectProduct(ProductCode.P01);
        vendingMachine.Checkout();

        // Act
        VendingState currentState = vendingMachine.GetCurrentState();

        // Assert
        Assert.Equal(VendingState.Payment, currentState);
    }
}
