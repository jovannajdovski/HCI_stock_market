using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockMarket
{
    internal class StockViewModel
    {
        public List<Stock> Stocks { get; set; }
        public string Currency { get; set; }

        public StockViewModel()
        {
            this.Stocks = new List<Stock>();
        }

    }
}
