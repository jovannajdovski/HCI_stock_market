﻿using Newtonsoft.Json;
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
        public async Task<List<StockUnit>?> GetCryptoForDayInterval(String symbol, int interval, String intoCurrency="USD")
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
                        if (jsonString.Contains("5 calls per minute and 500 calls per day"))
                            throw new CannotUnloadAppDomainException();
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
                        CryptoTimeSeries timeSeries = JsonConvert.DeserializeObject<CryptoTimeSeries>(jsonString);
                        List<StockUnit> cryptoUnits = timeSeries.Data.Select(kvp => new StockUnit(kvp.Key, kvp.Value)).ToList();
                        return cryptoUnits;

                    default:
                        Console.WriteLine("Unexpected status code: {0}", response.StatusCode);
                        break;
                }

            }
            return null;
        }

        public async Task<List<StockUnit>?> GetCryptoForDay(String symbol, String intoCurrency = "USD")
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
                        if (jsonString.Contains("5 calls per minute and 500 calls per day"))
                            throw new CannotUnloadAppDomainException();
                        jsonString = jsonString.Replace("Time Series (Digital Currency Daily)", "Time Series");
                        CryptoTimeSeries timeSeries = JsonConvert.DeserializeObject<CryptoTimeSeries>(jsonString);
                        List<StockUnit> cryptoUnits = timeSeries.Data.Select(kvp => new StockUnit(kvp.Key, kvp.Value)).ToList();
                        return cryptoUnits;

                    default:
                        Console.WriteLine("Unexpected status code: {0}", response.StatusCode);
                        break;
                }

            }
            return null;
        }

        public async Task<List<StockUnit>?> GetCryptoForWeek(String symbol, String intoCurrency = "USD")
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=DIGITAL_CURRENCY_WEEKLY&symbol={symbol}&market={intoCurrency}&apikey={API_KEY}";
            Uri queryUri = new Uri(QUERY_URL);

            using (HttpClient client = new HttpClient())
            {

                var response = await client.GetAsync(queryUri);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var jsonString = await response.Content.ReadAsStringAsync();
                        if (jsonString.Contains("5 calls per minute and 500 calls per day"))
                            throw new CannotUnloadAppDomainException();
                        jsonString = jsonString.Replace("Time Series (Digital Currency Weekly)", "Time Series");
                        CryptoTimeSeries timeSeries = JsonConvert.DeserializeObject<CryptoTimeSeries>(jsonString);
                        List<StockUnit> cryptoUnits = timeSeries.Data.Select(kvp => new StockUnit(kvp.Key, kvp.Value)).ToList();
                        return cryptoUnits;
                    default:
                        Console.WriteLine("Unexpected status code: {0}", response.StatusCode);
                        break;
                }

            }
            return null;
        }

        public async Task<List<StockUnit>?> GetCryptoForMonth(String symbol, String intoCurrency = "USD")
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
                        if (jsonString.Contains("5 calls per minute and 500 calls per day"))
                            throw new CannotUnloadAppDomainException();
                        jsonString = jsonString.Replace("Time Series (Digital Currency Monthly)", "Time Series");
                        CryptoTimeSeries timeSeries = JsonConvert.DeserializeObject<CryptoTimeSeries>(jsonString);
                        List<StockUnit> cryptoUnits = timeSeries.Data.Select(kvp => new StockUnit(kvp.Key, kvp.Value)).ToList();
                        return cryptoUnits;

                    default:
                        Console.WriteLine("Unexpected status code: {0}", response.StatusCode);
                        break;
                }

            }
            return null;
        }
    }
}
