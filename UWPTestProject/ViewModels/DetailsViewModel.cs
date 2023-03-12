using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using UWPTestProject.Models;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
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
            HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri($"https://api.coincap.io/v2/assets/{Currency.ID}/markets");
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

        public async Task DrawGraphic(Canvas currencyHistoryCanvas)
        {
            List<HistoryCurrency> historyCurrency = await LoadHistoryAssets();

            double max = -100000d;
            double min = 100000d;
            double middle;
            foreach (HistoryCurrency item in historyCurrency)
            {
                double value = double.Parse(item.PriceUsd, NumberStyles.Float, CultureInfo.InvariantCulture);
                if (value < min)
                {
                    min = value;
                }
                if (value > max)
                {
                    max = value;
                }
            }
            middle = (max + min) / 2;
            double proportion = (max - middle) / 150;
            int xStart = 5;
            int xStep = 25;
            int steps = 29;
            
            for (int i = 0; i < steps; i++)
            {
                double firstItemPrice = double.Parse(historyCurrency[i].PriceUsd, NumberStyles.Float, CultureInfo.InvariantCulture);
                double secondItemPrice = double.Parse(historyCurrency[i + 1].PriceUsd, NumberStyles.Float, CultureInfo.InvariantCulture);

                Line line = new Line();
                line.X1 = xStart + xStep;
                line.Y1 = (firstItemPrice - middle) / proportion + 250;
                line.X2 = line.X1 + xStep;
                line.Y2 = (secondItemPrice - middle) / proportion + 250;

                xStart += xStep;

                line.Stroke = new SolidColorBrush(Colors.Red);
                currencyHistoryCanvas.Children.Add(line);
            }
            AddTextBlockToGraphic(currencyHistoryCanvas, middle.ToString(), 30, 250);
            AddTextBlockToGraphic(currencyHistoryCanvas, max.ToString(), 30, 100);
            AddTextBlockToGraphic(currencyHistoryCanvas, min.ToString(), 30, 400);
        }

        private void AddTextBlockToGraphic(Canvas parent, string price, int setLeft, int setTop)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = price;
            textBlock.Foreground = new SolidColorBrush(Colors.Red);
            Canvas.SetLeft(textBlock, setLeft);
            Canvas.SetTop(textBlock, setTop);
            parent.Children.Add(textBlock);
        }

        private async Task<List<HistoryCurrency>> LoadHistoryAssets()
        {
            List<HistoryCurrency> historyCurrency = new List<HistoryCurrency>();
            HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri($"https://api.coincap.io/v2/assets/{Currency.ID}/history?interval=h1");
            try
            {
                HttpResponseMessage httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                string httpResponseBody = await httpResponse.Content.ReadAsStringAsync();

                JsonDocument jsonDocument = JsonDocument.Parse(httpResponseBody);
                JsonElement jsonElement = jsonDocument.RootElement.GetProperty("data");

                historyCurrency = JsonSerializer.Deserialize<List<HistoryCurrency>>(jsonElement);
                historyCurrency.Reverse();
                historyCurrency = historyCurrency.GetRange(0, 30);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return historyCurrency;
        }
    }
}
