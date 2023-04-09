using Newtonsoft.Json;
using stockMarket.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace stockMarket.service
{
    class CryptoService
    {
        private readonly String API_KEY = "FVWOXFUCUTDA11L8";
        public CryptoService() { }

        // PREMIUM
        public async void getCryptoForDayInterval(String symbol, int interval, String intoCurrency="USD")
        {
            String intervalStr = interval.ToString() + "min";
            string QUERY_URL = $"https://www.alphavantage.co/query?function=CRYPTO_INTRADAY&symbol={symbol}&market={intoCurrency}&interval={intervalStr}&apikey={API_KEY}";
            Uri queryUri = new Uri(QUERY_URL);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(queryUri);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var jsonString = await response.Content.ReadAsStringAsync();
                        StockTimeSeries timeSeries = JsonConvert.DeserializeObject<StockTimeSeries>(jsonString);
                        break;

                    default:
                        Console.WriteLine("Unexpected status code: {0}", response.StatusCode);
                        break;
                }

            }
        }

        public async void getCryptoForDay(String symbol, String intoCurrency = "USD")
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=DIGITAL_CURRENCY_DAILY&symbol={symbol}&market={intoCurrency}&apikey={API_KEY}";
            Uri queryUri = new Uri(QUERY_URL);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(queryUri);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var jsonString = await response.Content.ReadAsStringAsync();
                        StockTimeSeries timeSeries = JsonConvert.DeserializeObject<StockTimeSeries>(jsonString);
                        break;

                    default:
                        Console.WriteLine("Unexpected status code: {0}", response.StatusCode);
                        break;
                }

            }
        }

        public async void getCryptoForWeek(String symbol, String intoCurrency = "USD")
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=DIGITAL_CURRENCY_WEEKLY&{symbol}=BTC&market={intoCurrency}&apikey={API_KEY}";
            Uri queryUri = new Uri(QUERY_URL);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(queryUri);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var jsonString = await response.Content.ReadAsStringAsync();
                        WeekStockTimeSeries timeSeries = JsonConvert.DeserializeObject<WeekStockTimeSeries>(jsonString);
                        break;

                    default:
                        Console.WriteLine("Unexpected status code: {0}", response.StatusCode);
                        break;
                }

            }
        }

        public async void getCryptoForMonth(String symbol, String intoCurrency = "USD")
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=DIGITAL_CURRENCY_MONTHLY&symbol={symbol}&market={intoCurrency}&apikey={API_KEY}";
            Uri queryUri = new Uri(QUERY_URL);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(queryUri);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var jsonString = await response.Content.ReadAsStringAsync();
                        MonthStockTimeSeries timeSeries = JsonConvert.DeserializeObject<MonthStockTimeSeries>(jsonString);
                        break;

                    default:
                        Console.WriteLine("Unexpected status code: {0}", response.StatusCode);
                        break;
                }

            }
        }
    }
}
