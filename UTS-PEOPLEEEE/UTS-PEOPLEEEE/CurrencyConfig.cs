using System.Text.Json;
using System.Text.Json.Serialization;

public class CurrencyConfig<TCurrency> where TCurrency : class
{
    [JsonPropertyName("default_currency")]
    public string DefaultCurrency { get; set; }

    [JsonPropertyName("currencies")]
    public Dictionary<string, TCurrency> Currencies { get; set; }

    public static CurrencyConfig<TCurrency> Load(string filePath)
    {
        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<CurrencyConfig<TCurrency>>(json);
    }
}
