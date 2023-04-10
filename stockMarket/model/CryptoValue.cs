using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockMarket.model
{
    public class CryptoValue : Value
    {
        [JsonProperty("1b. open (USD)")]
        public override double Open { get; set; }

        [JsonProperty("2b. high (USD)")]
        public override double High { get; set; }

        [JsonProperty("3b. low (USD)")]
        public override double Low { get; set; }

        [JsonProperty("4b. close (USD)")]
        public override double Close { get; set; }

        [JsonProperty("5. volume")]
        public override double Volume { get; set; }
    }

    public class CryptoTimeSeries
    {
        [JsonProperty("Meta Data")]
        public MetaData MetaData { get; set; }

        [JsonProperty("Time Series")]
        public Dictionary<DateTime, CryptoValue> Data { get; set; }
    }

    public class CryptoUnit
    {
        public DateTime Date { get; set; }
        public Value CryptoValue { get; set; }

        public CryptoUnit(DateTime dateTime, Value stockValue)
        {
            Date = dateTime;
            CryptoValue = stockValue;
        }
    }
}
