using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;

namespace UTS_PEOPLEEEE
{
    public class LanguageConfig
    {
        public string Language { get; set; }

        [JsonPropertyName("messages")]
        public Dictionary<string, Dictionary<string, string>> Messages { get; set; }
            = new Dictionary<string, Dictionary<string, string>>();

        public string GetMessage(string key)
        {
            if (Messages.ContainsKey(Language) && Messages[Language].ContainsKey(key))
                return Messages[Language][key];
            return $"Pesan tidak ditemukan untuk key: {key}";
        }
    }

    public static class RuntimeLanguage
    {
        public static LanguageConfig Load(string path)
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<LanguageConfig>(json);
        }

    }
}
