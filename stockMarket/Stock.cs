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
        public double Min { get; set; }
        public double Max { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public string Name { get; set; }
        
        public Stock(DateTime dateTime, double min, double  max, double open, double close, string name)
        {
            this.DateTime = dateTime;
            this.Min = min;
            this.Max = max;
            this.Open = open;
            this.Close = close;
            this.Name = name;
        }

        public Stock()
        {
        }
    }
}
