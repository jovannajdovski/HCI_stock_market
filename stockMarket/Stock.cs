using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockMarket
{
    public class Stock
    {
        public DateTime DateTime { get; set; }
        public double Low { get; set; }
        public double High { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public string Name { get; set; }
        
        public Stock(DateTime dateTime, double min, double  max, double open, double close, string name)
        {
            this.DateTime = dateTime;
            this.Low = min;
            this.High = max;
            this.Open = open;
            this.Close = close;
            this.Name = name;
        }

        public Stock()
        {
        }
    }
}
