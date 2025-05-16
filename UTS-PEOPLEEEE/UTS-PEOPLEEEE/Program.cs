<<<<<<< HEAD
﻿using AutoVending;
using AutoVending.Models;
using System.Diagnostics;
using UTS_PEOPLEEEE;

class Program
{
    static void Main()
    {
        List<Transaction> history = new List<Transaction>();
        LanguageConfig langConfig = null;
        string[] supportedLanguages = { "ID", "EN", "JV" };

        // Pemilihan Bahasa
        while (true)
        {
            //Console.Clear();
            Console.Write("Select Language (ID/EN/JV): ");
            string inputLang = Console.ReadLine()?.ToUpper();
            //Console.Write(inputLang);

            //DBC
            Debug.Assert(!string.IsNullOrEmpty(inputLang), "Input bahasa tidak boleh kosong");
            Debug.Assert(inputLang == "ID" || inputLang == "EN" || inputLang == "JV", "Bahasa tidak dikenali");


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
        //var manager = new VendingManager();
        //SeedDummyProducts(manager);
        var manager = new GenericRepository<Product>();
        SeedDummyProducts(manager);

        // Menu Utama
        while (true)
        {
            Console.Clear();
            Console.WriteLine(langConfig.GetMessage("welcome"));
            Console.WriteLine(langConfig.GetMessage("choose_instruction"));
            Console.WriteLine(langConfig.GetMessage("exit_instruction"));
            Console.WriteLine("====================================");

            foreach (var p in manager.LihatSemua())
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
                Console.WriteLine("====================================");

                foreach (var p in manager.LihatSemua())
                {
                    Console.WriteLine($"[{p.Id}] {p.Name} - Rp {p.Price} x {p.Quantity}");
                }
                Console.WriteLine(langConfig.GetMessage("total_products") + manager.LihatSemua().Count);
                Console.WriteLine(langConfig.GetMessage("total_value") + manager.HitungTotal(p => p.Price * p.Quantity));
                Console.WriteLine("99 - View Transaction History");
                Console.WriteLine(langConfig.GetMessage("press_enter_return"));

                var adminInput = Console.ReadLine()?.Trim();

                if (adminInput == "99")
                {
                    Console.Clear();
                    Console.WriteLine("=== TRANSACTION HISTORY ===");
                    if (history.Count == 0)
                    {
                        Console.WriteLine("No transactions yet.");
                    }
                    else
                    {
                        foreach (var t in history)
                        {
                            Console.WriteLine($"{t.Timestamp:g} - {t.ProductName} x{t.Quantity} = Rp {t.TotalPrice}");
                        }
                    }
                    Console.WriteLine(langConfig.GetMessage("press_enter_return"));
                    Console.ReadLine();
                    Console.Clear();
                }

                continue;
            }


            //var selected = manager.DetilProduk(input);
            var selected = manager.Detil(input);
            if (selected != null)
            {
                Console.WriteLine($"{langConfig.GetMessage("product_selection")}{selected.Name}");
                Console.Write(langConfig.GetMessage("input_quantity"));
                if (int.TryParse(Console.ReadLine(), out int qty) && qty > 0)
                {
                    var total = selected.Price * qty;
                    Console.WriteLine($"Total: Rp {total}");
                    Console.WriteLine(langConfig.GetMessage("checkout_success"));

                    // Tambah ke riwayat transaksi
                    history.Add(new Transaction
                    {
                        ProductId = selected.Id,
                        ProductName = selected.Name,
                        Quantity = qty,
                        TotalPrice = (double)total,
                        Timestamp = DateTime.Now
                    });
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
                Console.Clear();
            }
        }

        Console.WriteLine(langConfig.GetMessage("thank_you"));
    }

    static void SeedDummyProducts(GenericRepository<Product> manager)
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
            manager.Tambah(product);
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;

// Enum untuk merepresentasikan status mesin
public enum VendingState
{
    Order,
    Payment,
    Finish,
    Error
}

// Enum untuk merepresentasikan kode produk
public enum ProductCode
{
    P01, P02, P03, P04, P05
}

// Enum untuk merepresentasikan nama produk
public enum ProductName
{
Fanta, Fruitea, Sprite, Pepsi, Aqua
}

// Struktur data untuk merepresentasikan produk
public struct Product
{
    public ProductCode Code;
    public ProductName Name;
    public int Price;
    public int Stock;
}

public class SmartVendingMachine
{
    // Teknik Automata
    public static Dictionary<VendingState, Dictionary<string, VendingState>> stateTable =
        new Dictionary<VendingState, Dictionary<string, VendingState>>
        {
            {
                VendingState.Order, new Dictionary<string, VendingState>
                {
                    { "checkout", VendingState.Payment },
                    { "error", VendingState.Error }
                }
            },
            {
                VendingState.Payment, new Dictionary<string, VendingState> // state bayar
                {
                    { "processPayment", VendingState.Finish },
                    { "error", VendingState.Error }
                }
            },
            {
                VendingState.Finish, new Dictionary<string, VendingState>
                {
                    { "finishTransaction", VendingState.Order },
                    { "error", VendingState.Error }
                }
            },
            {
                VendingState.Error, new Dictionary<string, VendingState> // State kalau error
                {
                    { "reset", VendingState.Order }
                }
            }
        };

    // Teknik Table Driven
    public static Product[] productTable = new Product[]
    {
        new Product { Code = ProductCode.P01, Name = ProductName.Fanta, Price = 10000, Stock = 10 },
        new Product { Code = ProductCode.P02, Name = ProductName.Fruitea, Price = 12000, Stock = 15 },
        new Product { Code = ProductCode.P03, Name = ProductName.Sprite, Price = 10000, Stock = 20 },
        new Product { Code = ProductCode.P04, Name = ProductName.Pepsi, Price = 11000, Stock = 0 },
        new Product { Code = ProductCode.P05, Name = ProductName.Aqua, Price = 5000, Stock = 30 },
    };

    public VendingState currentState;
    public int totalAmount;
    public ProductCode selectedProductCode;
    public int quantity;

    public SmartVendingMachine()
    {
        currentState = VendingState.Order;
        totalAmount = 0;
        selectedProductCode = ProductCode.P01;
        quantity = 0;
    }

    // Metode untuk mendapatkan produk dari kode produk
    public Product GetProductByCode(ProductCode code)
    {
        foreach (var product in productTable)
        {
            if (product.Code == code)
            {
                return product;
            }
        }
        throw new ArgumentException("Kode produk tidak valid.");
    }

    // Metode untuk menampilkan menu produk
    public void DisplayProducts()
    {
        Console.WriteLine("Daftar Produk:");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("Kode\tNama\t\tHarga\tStok");
        Console.WriteLine("--------------------------------------------------");
        foreach (var product in productTable)
        {
            Console.WriteLine($"{product.Code}\t{product.Name}\t\t{product.Price}\t{product.Stock}");
        }
        Console.WriteLine("--------------------------------------------------");
    }

    // Metode untuk memilih produk
    public void SelectProduct(ProductCode code)
    {
        try
        {
            if (currentState != VendingState.Order)
            {
                throw new InvalidOperationException("Tidak dapat memilih produk saat ini.");
            }

            Product selectedProduct = GetProductByCode(code);

            if (selectedProduct.Stock < 1) // kalau stok nol, gabisa di checkout
            {
                throw new InvalidOperationException($"Stok produk {selectedProduct.Name} tidak mencukupi.");
            }

            // Update Stock
            for (int i = 0; i < productTable.Length; i++)
            {
                if (productTable[i].Code == code)
                {
                    productTable[i].Stock -= 1; // Kurangi stok 1 tiap checkout berhasil
                    break;
                }
            }
            totalAmount = selectedProduct.Price;
            selectedProductCode = code; 
            quantity = 1; 

            // Update State
            Console.WriteLine($"1 {selectedProduct.Name} berhasil ditambahkan ke pesanan.");
            Console.WriteLine($"Total Harga: {totalAmount}");
        }
        catch (ArgumentException ex)
        {
            TransitionState("error");
            Console.WriteLine($"Error: {ex.Message}");
            Environment.Exit(1);
        }
        catch (InvalidOperationException ex)
        {
            TransitionState("error");
            Console.WriteLine($"Error: {ex.Message}");
            Environment.Exit(1);
        }
    }

    // Metode untuk menampilkan total pesanan
    public void Checkout()
    {
        if (currentState != VendingState.Order)
        {
            throw new InvalidOperationException("Tidak dapat melakukan checkout saat ini.");
        }

        Console.WriteLine("Pesanan Anda:");
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("Nama Produk\tJumlah\tHarga");
        Console.WriteLine("-----------------------------------");
        Product product = GetProductByCode(selectedProductCode);
        Console.WriteLine($"{product.Name}\t\t1\t{totalAmount}");
        Console.WriteLine("-----------------------------------");
        Console.WriteLine($"Total\t\t\t{totalAmount}");
        Console.WriteLine("-----------------------------------");

        TransitionState("checkout");
    }

    // Metode untuk memproses pembayaran
    public void ProcessPayment(int payment)
    {
        try
        {
            if (currentState != VendingState.Payment)
            {
                throw new InvalidOperationException("Tidak dapat melakukan pembayaran saat ini.");
            }

            if (payment < totalAmount)
            {
                throw new ArgumentException("Pembayaran kurang.");
            }

            int change = payment - totalAmount;
            Console.WriteLine($"Pembayaran berhasil. Kembalian Anda: {change}");
            totalAmount = 0;
            TransitionState("processPayment");
            Console.WriteLine("Transaksi selesai. Terima kasih!");
            System.Threading.Thread.Sleep(2000);
            TransitionState("finishTransaction");
            Console.Clear();
        }
        catch (ArgumentException ex)
        {
            TransitionState("error");
            Console.WriteLine($"Error: {ex.Message}");
            Environment.Exit(1); 
        }
        catch (InvalidOperationException ex)
        {
            TransitionState("error");
            Console.WriteLine($"Error: {ex.Message}");
            Environment.Exit(1); 
        }
    }

    // Metode transisi state
    public void TransitionState(string eventName)
    {
        if (stateTable.ContainsKey(currentState) && stateTable[currentState].ContainsKey(eventName))
        {
            currentState = stateTable[currentState][eventName];
        }
        else
        {
            Console.WriteLine($"Invalid transition: {currentState} - {eventName}");
            currentState = VendingState.Error;
        }
    }

    public VendingState GetCurrentState()
    {
        return currentState;
    }

    public static void Main(string[] args)
    {
        SmartVendingMachine vendingMachine = new SmartVendingMachine();
        string input;
        int payment;

        while (true)
        {
            Console.WriteLine("\n=== Smart Vending Machine ===");
            Console.WriteLine($"Status: {vendingMachine.GetCurrentState()}");
            vendingMachine.DisplayProducts();

            if (vendingMachine.GetCurrentState() == VendingState.Order)
            {
                Console.WriteLine("Pilih produk (contoh: P01):");
                input = Console.ReadLine();
                try
                {
                    ProductCode code = (ProductCode)Enum.Parse(typeof(ProductCode), input.ToUpper()); // Convert input agar bisa lowercase juga
                    vendingMachine.SelectProduct(code);
                    vendingMachine.Checkout();
                    Console.WriteLine("Masukkan pembayaran:");
                    input = Console.ReadLine();
                    payment = int.Parse(input);
                    vendingMachine.ProcessPayment(payment);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Terjadi kesalahan: {ex.Message}");
                    Environment.Exit(1); 
                }
                catch (FormatException)
                {
                    Console.WriteLine("Format pembayaran tidak valid.");
                    Environment.Exit(1); 
                }
            }
            else if (vendingMachine.GetCurrentState() == VendingState.Payment)
            {

            }
            else if (vendingMachine.GetCurrentState() == VendingState.Finish)
            {
                Console.WriteLine("Transaksi selesai. Terima kasih!");
                System.Threading.Thread.Sleep(1500);
                vendingMachine.TransitionState("finishTransaction");
            }
            else if (vendingMachine.GetCurrentState() == VendingState.Error)
            {
                Console.WriteLine("Terjadi kesalahan. Mesin sedang direset.");
                
                // Kembalikan stok
                Product product = vendingMachine.GetProductByCode(vendingMachine.selectedProductCode);
                for (int i = 0; i < productTable.Length; i++)
                {
                    if (productTable[i].Code == vendingMachine.selectedProductCode)
                    {
                        productTable[i].Stock += vendingMachine.quantity; // Kembalikan jumlah yang dibeli
                        break;
                    }
                }
                vendingMachine.TransitionState("reset");
            }
        }
    }
}
>>>>>>> origin/1201220449_FarhanNugraha
