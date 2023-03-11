using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using UWPTestProject.Models;
using UWPTestProject.Views;
using Windows.Services.Maps;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.Web.Http;


namespace UWPTestProject.ViewModels
{
    public delegate void ContentLoadedEventHandler(object sender, EventArgs e);
    public class CurrencyViewModel : INotifyPropertyChanged
    {
        private Visibility loadingVisibility = Visibility.Visible;

        public Visibility LoadingVisibility 
        {
            get
            {
                return loadingVisibility;
            }
            set
            {
                loadingVisibility = value;
                OnPropertyChanged();
            }
        }
        private Visibility listViewVisibility = Visibility.Collapsed;
        public Visibility ListViewVisibility 
        { 
            get
            {
                return listViewVisibility;
            }
            set
            {
                listViewVisibility = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        //public event ContentLoadedEventHandler ContentLoaded;

        public CurrencyViewModel() 
        {
            LoadCurrencies();
            
            //LoadExchanges();
        }

        private async Task ContentLoadedHandle()
        {
            LoadingVisibility = Visibility.Collapsed;
            ListViewVisibility = Visibility.Visible;
        }
        private async Task LoadCurrencies()
        {
            HttpClient httpClient;
            Uri requestUri;
            httpClient = new HttpClient();
            requestUri = new Uri("https://api.coincap.io/v2/assets?limit=20");
            try
            {
                HttpResponseMessage httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                string httpResponseBody = await httpResponse.Content.ReadAsStringAsync();

                JsonDocument jsonDocument = JsonDocument.Parse(httpResponseBody);
                JsonElement jsonElement = jsonDocument.RootElement.GetProperty("data");

                Currencies = JsonSerializer.Deserialize<ObservableCollection<Currency>>(jsonElement);
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            await ContentLoadedHandle();
        }
        private ObservableCollection<Currency> currencies;
        public ObservableCollection<Currency> Currencies
        { 
            get
            {
                return currencies;
            }
            set
            {
                currencies = value;
                OnPropertyChanged();
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void CurrencyItem_Click(object sender, ItemClickEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(DetailsPage), (Currency)e.ClickedItem);
        }
    }
}
