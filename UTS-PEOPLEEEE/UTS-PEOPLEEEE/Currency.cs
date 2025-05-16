using System.Text.Json.Serialization;

namespace UTS_PEOPLEEEE
{
    public class Currency
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("conversion_rate")]
        public double ConversionRate { get; set; }
    }
}
