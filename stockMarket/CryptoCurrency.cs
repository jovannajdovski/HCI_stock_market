using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace stockMarket
{
    public class CryptoCurrency
    {
        [Index(0)]
        public string Code { get; set; }
        [Index(1)]
        public string Name { get; set; }

        public override string ToString()
        {
            return  Code+" : "+Name;
        }
    }
}
