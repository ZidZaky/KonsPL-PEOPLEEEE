using VendLib;

class Program
{
    static void Main()
    {
        var vm = new VendingMachine();

        Console.Write("Masukkan kode produk (A1–A6): ");
        string input = Console.ReadLine();

        if (Enum.TryParse(input, out ProductCode code))
        {
            var product = vm.GetProduct(code);
            if (product != null)
            {
                Console.WriteLine($"Produk: {product.Name}, Harga: Rp{product.Price}");
            }
            else
            {   
                Console.WriteLine("Produk tidak ditemukan.");
            }
        }
        else
        {
            Console.WriteLine("Kode tidak valid.");
        }
    }
}
