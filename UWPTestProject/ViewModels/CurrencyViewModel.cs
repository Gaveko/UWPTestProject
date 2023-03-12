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
using static System.Net.WebRequestMethods;


namespace UWPTestProject.ViewModels
{
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
        private string searchBoxText;

        public string SearchBoxText
        {
            get
            {
                return searchBoxText;
            }
            set
            {
                searchBoxText = value;
                OnPropertyChanged();
            }
        }
        
        private string loadingDataStatus = "Loading";

        public string LoadingDataStatus
        {
            get
            {
                return loadingDataStatus;
            }
            set
            {
                loadingDataStatus = value;
                if (loadingDataStatus == "Loading" || loadingDataStatus == "Failed")
                {
                    LoadingVisibility = Visibility.Visible;
                    ListViewVisibility = Visibility.Collapsed;
                }
                else
                {
                    LoadingVisibility = Visibility.Collapsed;
                    ListViewVisibility = Visibility.Visible;
                }
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged; 

        public CurrencyViewModel() 
        {
            LoadCurrencies();
        }

        private async Task LoadCurrencies(string currencyName = null)
        {
            LoadingDataStatus = "Loading";
            HttpClient httpClient;
            Uri requestUri;
            httpClient = new HttpClient();
            string baseUri = "https://api.coincap.io/v2/assets";
            if (currencyName == null || currencyName == "") 
            {
                baseUri = string.Concat(baseUri, "?limit=20");
            }
            else
            {
                baseUri = string.Concat(baseUri, $"/{currencyName}");
            }
            requestUri = new Uri(baseUri);
            try
            {
                HttpResponseMessage httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                string httpResponseBody = await httpResponse.Content.ReadAsStringAsync();

                JsonDocument jsonDocument = JsonDocument.Parse(httpResponseBody);
                JsonElement jsonElement = jsonDocument.RootElement.GetProperty("data");

                if (currencyName == null || currencyName == "")
                {
                    Currencies = JsonSerializer.Deserialize<ObservableCollection<Currency>>(jsonElement);
                }
                else
                {
                    Currencies.Clear();
                    Currencies.Add(JsonSerializer.Deserialize<Currency>(jsonElement));

                }

                LoadingDataStatus = "Success";
            }
            catch (Exception ex)
            {
                LoadingDataStatus = "Failed";
            }

            
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

        public void CurrencySearch_Click()
        {
            LoadCurrencies(SearchBoxText);
        }
    }
}
