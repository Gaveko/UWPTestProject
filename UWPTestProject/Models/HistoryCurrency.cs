using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UWPTestProject.Models
{
    public class HistoryCurrency
    {
        [JsonPropertyName("priceUsd")]
        public string PriceUsd { get; set; }

        [JsonPropertyName("time")]
        public long Time { get; set; }
    }
}
