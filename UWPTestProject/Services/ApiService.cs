using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UWPTestProject.Models;
using Windows.Web.Http;

namespace UWPTestProject.Services
{
    internal class ApiService
    {
        private HttpClient httpClient;
        private Uri requestUri;
        public ApiService() 
        {
            httpClient = new HttpClient();
            requestUri = new Uri("https://api.coincap.io/v2/assets");
        }

        public ObservableCollection<Currency> GetCurrencies()
        {
            ObservableCollection<Currency> currencies = new ObservableCollection<Currency>();
            //try
            //{
            //    HttpResponseMessage httpResponse;
            //    httpResponse = httpClient(requestUri);
            //    httpResponse.EnsureSuccessStatusCode();
            //    string httpResponseBody = httpResponse.Content.ToString();

            //    Dictionary<string, object> jsonDict = JsonSerializer.Deserialize<Dictionary<string, object>>(httpResponseBody);



            //}
            //catch (Exception ex)
            //{

            //}

            return currencies;
        }
    }
}
