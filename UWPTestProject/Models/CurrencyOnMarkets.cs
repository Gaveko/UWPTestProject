using System.Text.Json.Serialization;

namespace UWPTestProject.Models
{
    public class CurrencyOnMarkets
    {
        [JsonPropertyName("exchangeId")]
        public string Exchange { get; set; }

        [JsonPropertyName("baseSymbol")]
        public string BaseSymbol { get; set; }

        [JsonPropertyName("priceUsd")]
        public string PriceUsd { get; set; }
    }
}
