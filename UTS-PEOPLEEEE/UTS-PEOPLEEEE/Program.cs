using System;
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