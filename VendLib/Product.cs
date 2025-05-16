namespace VendLib
{
    public class Product
    {
        public string Name { get; }
        public decimal Price { get; }

        public Product(string name, decimal price)
        {
            // Preconditions
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name tidak boleh kosong.");
            if (price < 0)
                throw new ArgumentOutOfRangeException("Price harus >= 0.");

            Name = name;
            Price = price;
        }
    }
}
