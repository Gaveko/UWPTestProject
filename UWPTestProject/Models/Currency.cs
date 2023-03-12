using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UWPTestProject.Models
{
    public class Currency
    {
        [JsonPropertyName("id")]
        public string ID { get; set; }
        [JsonPropertyName("rank")]
        public string Rank { get; set; }
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("priceUsd")]
        public string PriceUsd { get; set; }
        [JsonPropertyName("supply")]
        public string Supply { get; set; }
        [JsonPropertyName("maxSupply")]
        public string MaxSupply { get; set; }
        [JsonPropertyName("marketCapUsd")]
        public string MarketCapUsd { get; set; }
        [JsonPropertyName("changePercent24Hr")]
        public string ChangePercent24Hr { get; set; }
    }
}
