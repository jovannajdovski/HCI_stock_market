using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockMarket
{
    internal class StockViewModel : INotifyPropertyChanged
    {
        private List<Stock> stocks;
        public List<Stock> Stocks { 
            get { return stocks; }
            set {
                if (stocks != value)
                {
                    stocks = value;
                    OnPropertyChange("Stocks");
                } 
            }
         }
        public string Currency { get; set; }

        public StockViewModel()
        {
            this.Stocks = new List<Stock>();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
