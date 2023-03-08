using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPTestProject.Models
{
    internal class Currency
    {
        public string ID { get; set; }
        public int Rank { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public double PriceUsd { get; set; }
    }
}
