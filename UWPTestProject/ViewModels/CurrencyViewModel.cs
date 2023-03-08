using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPTestProject.Models;

namespace UWPTestProject.ViewModels
{
    internal class CurrencyViewModel
    {
        public CurrencyViewModel() 
        {
            Currencies = new ObservableCollection<Currency>();
            Currencies.Add(new Currency() { ID = "Bitcoin", Name = "Bitcoin", Rank = 1, Symbol = "bit", PriceUsd = 19.28 });
        }
        public ObservableCollection<Currency> Currencies { get; set; }
    }
}
