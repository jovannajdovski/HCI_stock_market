using Newtonsoft.Json;
using SciChart.Data.Model;
using stockMarket.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace stockMarket.service
{
    public class StockService
    {
        private readonly String API_KEY = "FVWOXFUCUTDA11L8";
        public StockService() { }

        public async void getStocksForDayInterval(String symbol, int interval)
        {
            String intervalStr = interval.ToString()+"min";
            string QUERY_URL = $"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={symbol}&interval={intervalStr}&apikey={API_KEY}";
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

        // PREMIUM
        public async void getStocksForDay(String symbol)
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={API_KEY}";
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

        public async void getStocksForWeek(String symbol)
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY&symbol={symbol}&apikey={API_KEY}";
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

        public async void getStocksForMonth(String symbol)
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY&symbol={symbol}&apikey={API_KEY}";
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
