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
using System.Windows.Documents;

namespace stockMarket.service
{
    public class StockService
    {
        private readonly String API_KEY = "FVWOXFUCUTDA11L8";
        public StockService() { }

        public async Task<List<StockUnit>?> GetStocksForDayInterval(String symbol, int interval)
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
                        switch (interval)
                        {
                            case 1:
                                jsonString = jsonString.Replace("Time Series (1min)", "Time Series");
                                break;
                            case 5:
                                jsonString = jsonString.Replace("Time Series (5min)", "Time Series");
                                break;
                            case 15:
                                jsonString = jsonString.Replace("Time Series (15min)", "Time Series");
                                break;
                            case 30:
                                jsonString = jsonString.Replace("Time Series (30min)", "Time Series");
                                break;
                            case 60:
                                jsonString = jsonString.Replace("Time Series (60min)", "Time Series");
                                break;
                            default:
                                Console.WriteLine("Unexpected interval: {0}", interval);
                                break;

                        }
                        StockTimeSeries timeSeries = JsonConvert.DeserializeObject<StockTimeSeries>(jsonString);
                        List<StockUnit> stockUnits = timeSeries.Data.Select(kvp => new StockUnit(kvp.Key, kvp.Value)).ToList();
                        return stockUnits;
  
                    default:
                        Console.WriteLine("Unexpected status code: {0}", response.StatusCode);
                        break;
                }
                return null;

            }
        }

        // PREMIUM
        public async Task<List<StockUnit>?> GetStocksForDay(String symbol)
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
                        jsonString = jsonString.Replace("Daily Time Series", "Time Series");
                        StockTimeSeries timeSeries = JsonConvert.DeserializeObject<StockTimeSeries>(jsonString);
                        List<StockUnit> stockUnits = timeSeries.Data.Select(kvp => new StockUnit(kvp.Key, kvp.Value)).ToList();
                        return stockUnits;

                    default:
                        Console.WriteLine("Unexpected status code: {0}", response.StatusCode);
                        break;
                }

            }
            return null;
        }

        public async Task<List<StockUnit>?> GetStocksForWeek(String symbol)
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
                        jsonString = jsonString.Replace("Weekly Time Series", "Time Series");
                        StockTimeSeries timeSeries = JsonConvert.DeserializeObject<StockTimeSeries>(jsonString);
                        List<StockUnit> stockUnits = timeSeries.Data.Select(kvp => new StockUnit(kvp.Key, kvp.Value)).ToList();
                        return stockUnits;

                    default:
                        Console.WriteLine("Unexpected status code: {0}", response.StatusCode);
                        break;
                }

            }
            return null;
        }

        public async Task<List<StockUnit>?> GetStocksForMonth(String symbol)
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
                        jsonString = jsonString.Replace("Monthly Time Series", "Time Series");
                        StockTimeSeries timeSeries = JsonConvert.DeserializeObject<StockTimeSeries>(jsonString);
                        List<StockUnit> stockUnits = timeSeries.Data.Select(kvp => new StockUnit(kvp.Key, kvp.Value)).ToList();
                        return stockUnits;

                    default:
                        Console.WriteLine("Unexpected status code: {0}", response.StatusCode);
                        break;
                }

            }
            return null;
        }
    }
}
