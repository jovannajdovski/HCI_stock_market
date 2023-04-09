using SciChart.Charting.Model.DataSeries;
using stockMarket.model;
using stockMarket.service;
﻿using MaterialDesignThemes.Wpf;
using SciChart.Charting.Model.DataSeries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public class IntervalButtonTag
    {

        public String? Type { get; set; }
        public int Position { get; set; }
    }


    public partial class MainWindow : Window
    {
        private int timeInterevalCLicked;
        private int dateIntervalCLicked;

        private StockService stockService;

        private int[] timeIntervals = {1,5,15,30,60};
        private int[] dateIntervals = { 1, 7, 30, 365};
        public List<Stock> Data { get; set; }
        private readonly StockViewModel viewModel;
        private FileService fileService;
        private List<MaterialDesignThemes.Wpf.Chip> chips;
        private int chipCounter = 0;
        public List<CryptoCurrency> CryptoCurrencies { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            stockService = new StockService();
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
            this.chips = new List<Chip>();
        }
        
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            BindButtons();
            var ohlcDataSeries = new OhlcDataSeries<DateTime, double>();

            ohlcDataSeries.Append(new DateTime(2015, 10, 1), 6061.60, 6172.80, 6053.30, 6072.50);
            ohlcDataSeries.Append(new DateTime(2015, 10, 2), 6072.50, 6176.20, 6051.60, 6130.00);
            ohlcDataSeries.Append(new DateTime(2015, 10, 5), 6130.00, 6301.10, 6130.00, 6298.90);

            var ohlcDataSeries2 = new OhlcDataSeries<DateTime, double>();

            ohlcDataSeries2.Append(new DateTime(2015, 10, 1), 6161.60, 6666.80, 5053.30, 6032.50);
            ohlcDataSeries2.Append(new DateTime(2015, 10, 2), 5072.50, 5176.20, 5051.60, 5130.00);
            ohlcDataSeries2.Append(new DateTime(2015, 10, 5), 5130.00, 5301.10, 5130.00, 5298.90);


            
        }

        private void BindButtons()
        {
            var btns = this.graphGrid.Children.OfType<Button>().ToList();
            ResetTimeIntervalTags(btns, 5);
            ResetDateIntervalTags(btns, 3);

            btns[1].Background = new SolidColorBrush(Color.FromRgb(121, 128, 134));
            btns[1].Foreground = new SolidColorBrush(Colors.White);
            timeInterevalCLicked = 1;

            btns[5].Background = new SolidColorBrush(Color.FromRgb(121, 128, 134));
            btns[5].Foreground = new SolidColorBrush(Colors.White);
            dateIntervalCLicked = 5;
        }

        private void ResetTimeIntervalTags(List<Button> btns, int timeBtns=5)
        {
            
            for (int i = 0; i < timeBtns; i++)
            {
                btns[i].Tag = new IntervalButtonTag()
                {
                    Type = "time",
                    Position = i
                };
                btns[i].Background = new SolidColorBrush(Color.FromRgb(33, 33, 33));
                btns[i].Foreground = new SolidColorBrush(Color.FromRgb(184, 186, 194));
            }
        }

        private void ResetDateIntervalTags(List<Button> btns, int dateBtns = 3)
        {
            int btnsLen = btns.Count;
            for (int i = btnsLen - dateBtns; i < btnsLen; i++)
            {
                btns[i].Tag = new IntervalButtonTag()
                {
                    Type = "date",
                    Position = i
                };
                btns[i].Background = new SolidColorBrush(Color.FromRgb(33, 33, 33));
                btns[i].Foreground = new SolidColorBrush(Color.FromRgb(184, 186, 194));
            }
        }

        private void DateIntervalChanged(object sender, RoutedEventArgs e)
        {
            Button? clickedBtn = sender as Button;
            IntervalButtonTag? clickedBtnTag = (IntervalButtonTag)clickedBtn.Tag;
            if (clickedBtnTag.Position == dateIntervalCLicked)
            {
                return;
            }

            var btns = this.graphGrid.Children.OfType<Button>().ToList();
            btns[dateIntervalCLicked].Background = new SolidColorBrush(Color.FromRgb(33, 33, 33));
            btns[dateIntervalCLicked].Foreground = new SolidColorBrush(Color.FromRgb(184, 186, 194));

            dateIntervalCLicked = clickedBtnTag.Position;
            clickedBtn.Background = new SolidColorBrush(Color.FromRgb(121, 128, 134));
            clickedBtn.Foreground = new SolidColorBrush(Colors.White);

            if (clickedBtnTag.Position != 5)
            {
                EnableTimeButtons(false);
            } else
            {
                EnableTimeButtons(true);
            }

            ChangeChartForInterval();
        }

        private void EnableTimeButtons(bool enable)
        {
            var btns = this.graphGrid.Children.OfType<Button>().ToList();
            List<Button> timeBtns = btns.Where(btn => (btn.Tag as IntervalButtonTag).Type.Equals("time")).ToList();
            foreach (Button btn in timeBtns)
            {
                btn.IsEnabled = enable;
            }

            if (!enable)
            {
                btns[timeInterevalCLicked].Background = new SolidColorBrush(Color.FromRgb(33, 33, 33));
                btns[timeInterevalCLicked].Foreground = new SolidColorBrush(Color.FromRgb(184, 186, 194));
            }
            
        }

        private void TimeIntervalChanged(object sender, RoutedEventArgs e)
        {
            Button? clickedBtn = sender as Button;
            IntervalButtonTag? clickedBtnTag = (IntervalButtonTag)clickedBtn.Tag;

            var btns = this.graphGrid.Children.OfType<Button>().ToList();
            btns[timeInterevalCLicked].Background = new SolidColorBrush(Color.FromRgb(33, 33, 33));
            btns[timeInterevalCLicked].Foreground = new SolidColorBrush(Color.FromRgb(184, 186, 194));

            timeInterevalCLicked = clickedBtnTag.Position;
            clickedBtn.Background = new SolidColorBrush(Color.FromRgb(121, 128, 134));
            clickedBtn.Foreground = new SolidColorBrush(Colors.White);

            ChangeChartForInterval();
        }

        private async void ChangeChartForInterval()
        {
            switch (dateIntervalCLicked) { 
                case 5:
                    //List<StockUnit> units  = await stockService.GetStocksForDayInterval("IBM", timeInterevalCLicked);
                    break;
                case 6:
                    break;
                case 7:
                    break;
            }

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
            RemoveChips();
            chips.Remove(currentChip);
            AddChips();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateChip();
        }

        private void CreateChip()
        {
            var myChip = new MaterialDesignThemes.Wpf.Chip()
            {
                Height = 50,
                Content = chipCounter.ToString(),
                IsDeletable = true,
                ToolTip = "This is my Chip",
                Foreground = Brushes.White,
                Background = new SolidColorBrush(Color.FromArgb(255, 94, 98, 102)),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            AddChipToGrid(myChip);
            chips.Add(myChip);
        }

        private void AddChipToGrid(Chip myChip)
        {
            myChip.SetValue(Grid.RowProperty, 3 + chipCounter / 3);
            myChip.SetValue(Grid.ColumnProperty,   (chipCounter % 3) * 3);
            myChip.SetValue(Grid.ColumnSpanProperty, 3);
            myChip.DeleteClick += Chip_OnDeleteClick;

            StockMarket.Children.Add(myChip);
            chipCounter++;
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

        private void OnAutoGeneratingColumn(object? sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as System.Windows.Controls.DataGridTextColumn).Binding.StringFormat = "0:HH:mm:ss dd.MM.yyyy.";
        }

        private void RemoveChips()
        {
            foreach (var chip in this.chips)
            {
                StockMarket.Children.Remove(chip);
            }
        }

        private void AddChips()
        {
            chipCounter = 0;
            foreach (var chip in this.chips)
            {
                AddChipToGrid(chip);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            RemoveChips();
            chips.Clear();
            chipCounter = 0;

        }
    }
}
