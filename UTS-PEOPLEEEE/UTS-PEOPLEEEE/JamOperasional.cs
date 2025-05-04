using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UTS_PEOPLEEEE
{
    public class JamOperationalConfig
    {
        [JsonPropertyName("modes")]
        public Dictionary<string, Mode> Modes { get; set; }

        [JsonPropertyName("default_mode")]
        public string DefaultMode { get; set; }

        public class Mode
        {
            [JsonPropertyName("open_hour")]
            public int OpenHour { get; set; }

            [JsonPropertyName("close_hour")]
            public int CloseHour { get; set; }
        }

        // Fungsi untuk memuat data dari file JSON
        public static JamOperationalConfig Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File tidak ditemukan: " + filePath);
                return null;
            }

            string json = File.ReadAllText(filePath); // Membaca file JSON
            var config = JsonSerializer.Deserialize<JamOperationalConfig>(json); // Deserialize ke objek C#

            if (config == null)
            {
                Console.WriteLine("Deserialisasi JSON gagal.");
                return null;
            }

            return config;
        }
    }
}
