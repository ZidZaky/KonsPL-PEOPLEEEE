using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UTS_PEOPLEEEE
{
    public class CurrencyConfig
    {
        [JsonPropertyName("default_currency")]
        public string DefaultCurrency { get; set; }

        [JsonPropertyName("currencies")]
        public Dictionary<string, Currency> Currencies { get; set; }

        public class Currency
        {
            [JsonPropertyName("symbol")]
            public string Symbol { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("conversion_rate")]
            public double ConversionRate { get; set; }
        }

        // Fungsi untuk memuat data dari file JSON
        public static CurrencyConfig Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File tidak ditemukan: " + filePath);
                return null;
            }

            string json = File.ReadAllText(filePath); // Membaca file JSON
            var config = JsonSerializer.Deserialize<CurrencyConfig>(json); // Deserialize ke objek C#

            if (config == null)
            {
                Console.WriteLine("Deserialisasi JSON gagal.");
                return null;
            }

            return config;
        }
    }
}
