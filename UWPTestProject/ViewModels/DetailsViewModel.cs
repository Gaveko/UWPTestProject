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
        public DetailsViewModel(Currency currency) 
        {
            Currency = currency;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
