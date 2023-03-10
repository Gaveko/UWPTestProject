using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPTestProject.Models;

namespace UWPTestProject.ViewModels
{
    internal class DetailsViewModel : INotifyPropertyChanged
    {
        public Currency Currency { get; set; }
        public DetailsViewModel() 
        {
            Currency = new Currency() { ID = "asd", Name = "asdfa", PriceUsd = "134", Rank = "1", Symbol = "dasf"};
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
