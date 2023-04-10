using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace stockMarket.model
{
    public abstract class Value
    {
        public abstract double Open { get; set; }

        public abstract double High { get; set; }

        public abstract double Low { get; set; }

        public abstract double Close { get; set; }

        public abstract double Volume { get; set; }
    }

    public class StockValue : Value
    {
        [JsonProperty("1. open")]
        public override double Open { get; set; }

        [JsonProperty("2. high")]
        public override double High { get; set; }

        [JsonProperty("3. low")]
        public override double Low { get; set; }

        [JsonProperty("4. close")]
        public override double Close { get; set; }

        [JsonProperty("5. volume")]
        public override double Volume { get; set; }
    }

    public class MetaData
    {
        [JsonProperty("1. Information")]
        public string? Information { get; set; }

        [JsonProperty("2. Symbol")]
        public string? Symbol { get; set; }

        [JsonProperty("3. Last Refreshed")]
        public DateTime LastRefreshed { get; set; }

        [JsonProperty("4. Interval")]
        public string? Interval { get; set; }

        [JsonProperty("5. Output Size")]
        public string? OutputSize { get; set; }

        [JsonProperty("6. Time Zone")]
        public string? TimeZone { get; set; }
    }

    public class StockTimeSeries
    {
        [JsonProperty("Meta Data")]
        public MetaData MetaData { get; set; }

        [JsonProperty("Time Series")]
        public Dictionary<DateTime, StockValue> Data { get; set; }
    }

    public class StockUnit
    {
        public DateTime Date { get; set; }
        public Value StockValue { get; set; }

        public StockUnit(DateTime dateTime, Value stockValue) {
            Date = dateTime;
            StockValue = stockValue;
        }
    }

}
