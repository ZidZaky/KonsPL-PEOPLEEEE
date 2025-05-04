using System;
using UTS_PEOPLEEEE;

class Program
{
    static void Main()
    {
        // Tentukan path file JSON eksternal
        string currencyFilePath = "currency_config.json"; // Ganti dengan path file JSON mata uang
        string modeFilePath = "operational_config.json"; // Ganti dengan path file JSON jam operasional

        // Load data dari file JSON eksternal
        CurrencyConfig currencyConfig = CurrencyConfig.Load(currencyFilePath);
        JamOperationalConfig jamOperationalConfig = JamOperationalConfig.Load(modeFilePath);

        // Pastikan currencyConfig dan jamOperationalConfig tidak null
        if (currencyConfig == null || currencyConfig.Currencies == null || currencyConfig.Currencies.Count == 0)
        {
            Console.WriteLine("Terjadi kesalahan saat memuat konfigurasi mata uang atau data mata uang tidak ada.");
            return;
        }

        if (jamOperationalConfig == null || jamOperationalConfig.Modes == null || jamOperationalConfig.Modes.Count == 0)
        {
            Console.WriteLine("Terjadi kesalahan saat memuat konfigurasi jam operasional atau data jam operasional tidak ada.");
            return;
        }

        // Tampilkan daftar mata uang
        Console.WriteLine("Daftar Mata Uang:");
        foreach (var entry in currencyConfig.Currencies)
        {
            var code = entry.Key;
            var currency = entry.Value;
            Console.WriteLine($"- {code}: {currency.Name} ({currency.Symbol}) | Rate: {currency.ConversionRate}");
        }

        // Input jumlah dalam IDR
        Console.Write("\nMasukkan jumlah dalam IDR: ");
        if (double.TryParse(Console.ReadLine(), out double amountIdr))
        {
            Console.WriteLine("\nKonversi:");
            foreach (var entry in currencyConfig.Currencies)
            {
                var code = entry.Key;
                var currency = entry.Value;
                double converted = amountIdr * currency.ConversionRate;
                Console.WriteLine($"{currency.Symbol} {converted:F2} ({code})");
            }
        }
        else
        {
            Console.WriteLine("Input tidak valid.");
        }

        // Tampilkan jam operasional
        Console.WriteLine("\n=== Jam Operasional ===");
        foreach (var entry in jamOperationalConfig.Modes)
        {
            var mode = entry.Key;
            var data = entry.Value;
            Console.WriteLine($"{mode}: Buka {data.OpenHour}:00 - Tutup {data.CloseHour}:00");
        }

        Console.WriteLine($"\nMode default: {jamOperationalConfig.DefaultMode}");
    }
}
