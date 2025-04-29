using AutoVending;
using UTS_PEOPLEEEE;

class Program
{
    static void Main()
    {
        LanguageConfig langConfig = null;
        string[] supportedLanguages = { "ID", "EN", "JV" };

        // Pemilihan Bahasa
        while (true)
        {
            Console.Clear();
            Console.Write("Select Language (ID/EN/JV): ");
            string inputLang = Console.ReadLine()?.ToUpper();
            //Console.Write(inputLang);


            if (supportedLanguages.Contains(inputLang))
            {
                //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "language_config.json");
                langConfig = RuntimeLanguage.Load("languageConfig.json");
                //Console.Write("ini lnag " + langConfig);
                langConfig.Language = inputLang;
                break;
            }
            else
            {
                Console.WriteLine("Language not available. Press Enter to try again...");
                Console.ReadLine();
            }
        }

        //if (langConfig.Messages.ContainsKey(langConfig.Language))
        //{
        //    Console.WriteLine("Pesan tersedia untuk bahasa " + langConfig.Language);
        //}
        //else
        //{
        //    Console.WriteLine("Pesan untuk bahasa " + langConfig.Language + " tidak ditemukan.");
        //}



        //Console.WriteLine("Bahasa dipilih: " + langConfig.Language);
        //Console.WriteLine("Total bahasa: " + langConfig.Messages.Count);
        var manager = new VendingManager();
        SeedDummyProducts(manager);

        // Menu Utama
        while (true)
        {
            //Console.Clear();
            Console.WriteLine(langConfig.GetMessage("welcome"));
            Console.WriteLine(langConfig.GetMessage("choose_instruction"));
            Console.WriteLine(langConfig.GetMessage("exit_instruction"));
            Console.WriteLine("====================================");

            foreach (var p in manager.LihatSemuaProduk())
            {
                Console.WriteLine($"[{p.Id}] {p.Name} - Rp {p.Price} x {p.Quantity}");
            }

            Console.Write("Input: ");
            var input = Console.ReadLine()?.Trim().ToUpper();

            if (input == "EXIT")
                break;

            if (input == "00")
            {
                Console.Clear();
                Console.WriteLine(langConfig.GetMessage("admin_dashboard"));
                Console.WriteLine(langConfig.GetMessage("total_products") + manager.LihatSemuaProduk().Count);
                Console.WriteLine(langConfig.GetMessage("total_value") + manager.HitungTotal());
                Console.WriteLine(langConfig.GetMessage("press_enter_return"));
                Console.ReadLine();
                continue;
            }

            var selected = manager.DetilProduk(input);
            if (selected != null)
            {
                Console.WriteLine($"{langConfig.GetMessage("product_selection")}{selected.Name}");
                Console.Write(langConfig.GetMessage("input_quantity"));
                if (int.TryParse(Console.ReadLine(), out int qty) && qty > 0)
                {
                    var total = selected.Price * qty;
                    Console.WriteLine($"Total: Rp {total}");
                    Console.WriteLine(langConfig.GetMessage("checkout_success"));
                }
                else
                {
                    Console.WriteLine(langConfig.GetMessage("invalid_quantity"));
                }

                Console.WriteLine(langConfig.GetMessage("press_enter_return"));
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine(langConfig.GetMessage("no_product"));
                Console.WriteLine(langConfig.GetMessage("press_enter_return"));
                Console.ReadLine();
            }
        }

        Console.WriteLine(langConfig.GetMessage("thank_you"));
    }

    static void SeedDummyProducts(VendingManager manager)
    {
        var dummyProducts = new List<Product>
        {
            new Product { Id = "P01", Name = "Air Mineral", Price = 5000, Quantity = 10 },
            new Product { Id = "P02", Name = "Teh Botol", Price = 7000, Quantity = 8 },
            new Product { Id = "P03", Name = "Kopi Hitam", Price = 8000, Quantity = 5 },
            new Product { Id = "P04", Name = "Coklat Dingin", Price = 9000, Quantity = 6 },
            new Product { Id = "P05", Name = "Susu Murni", Price = 8500, Quantity = 4 },
            new Product { Id = "P06", Name = "Mie Gelas", Price = 6000, Quantity = 9 },
            new Product { Id = "P07", Name = "Roti Bakar", Price = 7000, Quantity = 3 },
            new Product { Id = "P08", Name = "Nasi Instan", Price = 12000, Quantity = 2 },
            new Product { Id = "P09", Name = "Snack Kentang", Price = 6500, Quantity = 7 },
            new Product { Id = "P10", Name = "Biskuit", Price = 4000, Quantity = 11 },
            new Product { Id = "P11", Name = "Coklat Batang", Price = 9500, Quantity = 5 },
            new Product { Id = "P12", Name = "Permen Mint", Price = 3000, Quantity = 15 },
            new Product { Id = "P13", Name = "Vitamin C", Price = 10000, Quantity = 6 },
            new Product { Id = "P14", Name = "Minuman Energi", Price = 11000, Quantity = 4 },
            new Product { Id = "P15", Name = "Keripik Singkong", Price = 7000, Quantity = 8 },
            new Product { Id = "P16", Name = "Kacang Campur", Price = 7500, Quantity = 5 },
        };

        foreach (var product in dummyProducts)
        {
            manager.TambahProduk(product);
        }
    }
}
