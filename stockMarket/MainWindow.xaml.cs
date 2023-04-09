using MaterialDesignThemes.Wpf;
using SciChart.Charting.Model.DataSeries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace stockMarket
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Stock> Data { get; set; }
        private readonly StockViewModel viewModel;
        private FileService fileService;
        public List<CryptoCurrency> CryptoCurrencies { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
            this.viewModel = new StockViewModel()
            {
                Stocks= new List<Stock>(){
                    new Stock()
                    {
                        DateTime = new DateTime(2020, 12, 12, 12, 12, 12),
                        Min = 12,
                        Max = 13,
                        Open = 14,
                        Close = 15,
                        Name = "Tesla"
                    },
                    new Stock()
                    {
                        DateTime = new DateTime(2020, 12, 12, 12, 12, 12),
                        Min = 12,
                        Max = 13,
                        Open = 14,
                        Close = 15,
                        Name = "Amazon"
                    },
                    new Stock()
                    {
                        DateTime = new DateTime(2020, 12, 12, 12, 12, 12),
                        Min = 12,
                        Max = 13,
                        Open = 14,
                        Close = 15,
                        Name = "IBM"
                    },
                    new Stock()
                    {
                        DateTime = new DateTime(2020, 12, 12, 12, 12, 12),
                        Min = 12,
                        Max = 13,
                        Open = 14,
                        Close = 15,
                        Name = "Tesla"
                    }
                },
                Currency="RSD"
                
            };
            this.DataContext= this.viewModel;
            this.fileService = new FileService();
            this.CryptoCurrencies = fileService.GetCryptocurrencies();
        }
        
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var ohlcDataSeries = new OhlcDataSeries<DateTime, double>();
              
            ohlcDataSeries.Append(new DateTime(2015, 10, 1), 6061.60, 6172.80, 6053.30, 6072.50);
            ohlcDataSeries.Append(new DateTime(2015, 10, 2), 6072.50, 6176.20, 6051.60, 6130.00);
            ohlcDataSeries.Append(new DateTime(2015, 10, 5), 6130.00, 6301.10, 6130.00, 6298.90);

            var ohlcDataSeries2 = new OhlcDataSeries<DateTime, double>();
              
            ohlcDataSeries2.Append(new DateTime(2015, 10, 1), 6161.60, 6666.80, 5053.30, 6032.50);
            ohlcDataSeries2.Append(new DateTime(2015, 10, 2), 5072.50, 5176.20, 5051.60, 5130.00);
            ohlcDataSeries2.Append(new DateTime(2015, 10, 5), 5130.00, 5301.10, 5130.00, 5298.90);
           

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string QUERY_URL = "https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=IBM&interval=5min&apikey=FVWOXFUCUTDA11L8";
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {
             
                dynamic json_data = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(client.DownloadString(queryUri));
                //kk.Text = string.Join(Environment.NewLine, json_data);

            }
        }
        private void Chip_OnDeleteClick(object sender, RoutedEventArgs e)
        {
            var currentChip = (Chip)sender;
            StockMarket.Children.Remove(currentChip);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var myChip = new MaterialDesignThemes.Wpf.Chip()
            {
                Height = 50,
                Content = "My Chip Content",
                IsDeletable = true,
                ToolTip = "This is my Chip",
                Foreground = Brushes.White,
                Background = new SolidColorBrush(Color.FromArgb(255,94,98,102))
            };
            myChip.DeleteClick += Chip_OnDeleteClick;
            StockMarket.Children.Add(myChip); 
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = TextBox1.Text;
            if (text.Length > 2)
            {
                AutocompleteListBox.Visibility = Visibility.Visible;
                AutocompleteListBox.ItemsSource = CryptoCurrencies.Where(cc => cc.Code.ToLower().Contains(text.ToLower()) || cc.Name.ToLower().Contains(text.ToLower()));
                AutocompleteListBox.SelectedIndex = -1;
            }
            else
                AutocompleteListBox.Visibility = Visibility.Hidden;
            
        }
        private void TextBox_OnFocusLost(object sender, RoutedEventArgs e)
        {
            AutocompleteListBox.Visibility = Visibility.Hidden;
            
        }

        private void AutocompleteListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CryptoCurrency? cryptoCurrency = AutocompleteListBox.SelectedItem as CryptoCurrency;
            if (cryptoCurrency != null)
            {
                TextBox1.Text = cryptoCurrency.Name;
                AutocompleteListBox.Visibility = Visibility.Hidden;
            }
        }
        private void window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
            AutocompleteListBox.Visibility = Visibility.Hidden;
            // ILI OVO
        }
    }
}
