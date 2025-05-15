using System;
using System.Diagnostics;
using System.IO;
using UTS_PEOPLEEEE;

class Program
{
    static void Main()
    {
        string currencyFilePath = "currency_config.json";
        string modeFilePath = "operational_config.json";

        if (!File.Exists(currencyFilePath))
        {
            Console.WriteLine($"❌ File tidak ditemukan: {Path.GetFullPath(currencyFilePath)}");
            return;
        }

        if (!File.Exists(modeFilePath))
        {
            Console.WriteLine($"❌ File tidak ditemukan: {Path.GetFullPath(currencyFilePath)}");
            return;
        }

        var currencyConfig = CurrencyConfig<Currency>.Load(currencyFilePath);
        var jamConfig = JamOperationalConfig.Load(modeFilePath);

        //Implementasi Design by Contract
        Debug.Assert(currencyConfig != null, "currencyConfig should not be null");
        Debug.Assert(currencyConfig.Currencies != null, "Currencies dictionary should not be null");
        Debug.Assert(currencyConfig.Currencies.Count > 0, "Currencies dictionary should not be empty");

        Debug.Assert(jamConfig != null, "jamConfig should not be null");
        Debug.Assert(jamConfig.Modes != null, "Modes dictionary should not be null");
        Debug.Assert(jamConfig.Modes.Count > 0, "Modes dictionary should not be empty");

        if (currencyConfig.Currencies.Count == 0)
        {
            Console.WriteLine("Gagal memuat data mata uang.");
            return;
        }

        if (jamConfig.Modes.Count == 0)
        {
            Console.WriteLine("Gagal memuat data jam operasional.");
            return;
        }

        Console.WriteLine("Daftar Mata Uang:");
        foreach (var entry in currencyConfig.Currencies)
        {
            var code = entry.Key;
            var currency = entry.Value;
            Console.WriteLine($"- {code}: {currency.Name} ({currency.Symbol}) | Rate: {currency.ConversionRate}");
        }

        Console.Write("\nMasukkan jumlah dalam IDR: ");
        if (double.TryParse(Console.ReadLine(), out double amountIdr))
        {
            var handler = new PaymentHandler<Currency>(currencyConfig.Currencies);
            Console.Write("\nMasukkan kode mata uang (contoh: USD): ");
            string kode = Console.ReadLine();
            handler.Pay(kode, amountIdr);
        }
        else
        {
            Console.WriteLine("Input tidak valid.");
        }

        Console.WriteLine("\n=== Jam Operasional ===");
        foreach (var entry in jamConfig.Modes)
        {
            var mode = entry.Key;
            var data = entry.Value;
            Console.WriteLine($"{mode}: Buka {data.OpenHour}:00 - Tutup {data.CloseHour}:00");
        }

        Console.WriteLine($"\nMode default: {jamConfig.DefaultMode}");
    }
}
