using VendLib;

public class VendingMachine
{
    private Dictionary<ProductCode, Product> _products;

    // Existing default constructor
    public VendingMachine()
    {
        _products = new Dictionary<ProductCode, Product>
        {
            { ProductCode.A1, new Product { Name = "Chitato", Price = 10000 } },
            { ProductCode.A2, new Product { Name = "Taro", Price = 12000 } },
            { ProductCode.A3, new Product { Name = "Qtela", Price = 9000 } },
            { ProductCode.A4, new Product { Name = "Lay's", Price = 11000 } },
            { ProductCode.A5, new Product { Name = "Cheetos", Price = 100000 } },
            { ProductCode.A6, new Product { Name = "Tic Tac", Price = 9000 } }
        };
    }

    // Additional constructor for testing
    public VendingMachine(Dictionary<ProductCode, Product> customMap)
    {
        _products = customMap;
    }

    public Product? GetProduct(ProductCode code)
    {
        return _products.TryGetValue(code, out var product) ? product : null;
    }
}
