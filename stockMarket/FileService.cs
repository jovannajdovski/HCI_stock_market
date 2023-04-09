using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockMarket
{
    class FileService
    {
        public FileService() { }
        public List<CryptoCurrency> GetCryptocurrencies()
        {
            using (var reader = new StreamReader("../../../digital_currency_list.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<CryptoCurrency>();
                return records.ToList();
            }
        }
    }
}
