using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using UWPTestProject.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;

namespace UWPTestProject.ViewModels
{
    public class DetailsViewModel : INotifyPropertyChanged
    {
        public Currency Currency { get; set; }
        private ObservableCollection<CurrencyOnMarkets> currencyOnMarkets;
        public ObservableCollection<CurrencyOnMarkets> CurrencyOnMarkets 
        {
            get
            {
                return currencyOnMarkets;
            }
            set
            {
                currencyOnMarkets = value;
                OnPropertyChanged();
            }
        }
        public DetailsViewModel(Currency currency) 
        {
            Currency = currency;
            LoadCurrencyOnMarkets();
        }

        private async void LoadCurrencyOnMarkets()
        {
            HttpClient httpClient;
            Uri requestUri;
            httpClient = new HttpClient();
            requestUri = new Uri($"https://api.coincap.io/v2/assets/{Currency.ID}/markets");
            try
            {
                HttpResponseMessage httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                string httpResponseBody = await httpResponse.Content.ReadAsStringAsync();

                JsonDocument jsonDocument = JsonDocument.Parse(httpResponseBody);
                JsonElement jsonElement = jsonDocument.RootElement.GetProperty("data");

                CurrencyOnMarkets = JsonSerializer.Deserialize<ObservableCollection<CurrencyOnMarkets>>(jsonElement);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void NavigateBack()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.GoBack();
        }
    }
}
