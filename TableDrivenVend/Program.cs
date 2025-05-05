using System;
using System.Collections.Generic;

namespace SmartVendingMachine
{
    enum ProductCode
    {
        A1, A2, A3, A4, A5, A6
    }

    class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Table-driven: mapping kode ke produk
            Dictionary<ProductCode, Product> productMap = new Dictionary<ProductCode, Product>
            {
                { ProductCode.A1, new Product { Name = "Chitato", Price = 10000 } },
                { ProductCode.A2, new Product { Name = "Taro", Price = 12000 } },
                { ProductCode.A3, new Product { Name = "Qtela", Price = 9000 } },
                { ProductCode.A4, new Product { Name = "Lays", Price = 11000 } },
                { ProductCode.A5, new Product { Name = "Cheetos", Price = 100000 } },
                { ProductCode.A6, new Product { Name = "Tic Tac", Price = 9000 } }
            };


            // Input dari user
            Console.Write("Masukkan kode produk (A1-A6): ");
            string input = Console.ReadLine();

            // Validasi dan lookup produk
            if (Enum.TryParse(input, out ProductCode code) && productMap.ContainsKey(code))
            {
                var selected = productMap[code];
                Console.WriteLine($"Produk: {selected.Name}, Harga: Rp{selected.Price}");
            }
            else
            {
                Console.WriteLine("Kode produk tidak valid.");
            }

            Console.ReadKey();
        }
    }
}
