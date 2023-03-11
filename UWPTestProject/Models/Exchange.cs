using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UWPTestProject.Models
{
    public class Exchange
    {
        [JsonPropertyName("exchangeId")]
        public string ID { get; set; }

        [JsonPropertyName("rank")]
        public string Rank { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
