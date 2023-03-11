using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
