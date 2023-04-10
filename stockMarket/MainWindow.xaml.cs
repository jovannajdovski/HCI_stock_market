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
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.Annotations;
using System.Runtime.Intrinsics;

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
        private TextAnnotation errorMessageAnnotation;

        private SolidColorBrush[] colors = { new SolidColorBrush(Color.FromRgb(248,68,85)),
                                             new SolidColorBrush(Color.FromRgb(204,0,17)),
                                             new SolidColorBrush(Color.FromRgb(255,145,0)),
                                             new SolidColorBrush(Color.FromRgb(204,85,0)),
                                             new SolidColorBrush(Color.FromRgb(128,255,204)),
                                             new SolidColorBrush(Color.FromRgb(0,204,102)),
                                             new SolidColorBrush(Color.FromRgb(0,191,255)),
                                             new SolidColorBrush(Color.FromRgb(56,134,181)),
                                             new SolidColorBrush(Color.FromRgb(191,51,255)),
                                             new SolidColorBrush(Color.FromRgb(109,0,179)),
        };

        private List<FastCandlestickRenderableSeries> candlestickSeries;
        private int seriesIdx;
        private int timeInterevalCLicked;
        private int dateIntervalCLicked;

        private StockService stockService;
        private CryptoService cryptoService;

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
            cryptoService = new CryptoService();
            this.Loaded += OnLoaded;

            this.viewModel = new StockViewModel();

            
            this.fileService = new FileService();
            this.CryptoCurrencies = fileService.GetCryptocurrencies();
            this.chips = new List<Chip>();
        }
        
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            BindButtons();
            candlestickSeries = new List<FastCandlestickRenderableSeries>();
            for (int i =0;i<5;i++)
            {
                candlestickSeries.Add(new FastCandlestickRenderableSeries()
                {
                    StrokeUp = colors[2*i].Color,
                    FillUp = colors[2*i],
                    StrokeDown = colors[2 * i + 1].Color,
                    FillDown = colors[2*i+1],
                    AntiAliasing = false,
                    DataPointWidth = 0.5,
                    StrokeThickness = 1,
                });
            }
            
            foreach(FastCandlestickRenderableSeries series in candlestickSeries)
            {
                sciChartSurface.RenderableSeries.Add(series);
            }
            seriesIdx = 0;
        }

        private void BindButtons()
        {
            var btns = this.graphGrid.Children.OfType<Button>().ToList();
            ResetTimeIntervalTags(btns, 5);
            ResetDateIntervalTags(btns, 4);

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

    //        Generate();
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

      //      Generate();
        }

        private async void Generate()
        {
            List<StockUnit> units = new List<StockUnit>();
            String symbol = TextBox1.Text;
            if ((bool)CryptoBtn.IsChecked)
            {
                switch (dateIntervalCLicked)
                {
                    case 5:
                        // TODO: treba greska
                        WriteMessageOnChart("You need premium account to get crypto \ndata for an interval of less than a day");
                        return;
                        //units = await cryptoService.GetCryptoForDayInterval(symbol, timeIntervals[timeInterevalCLicked]);
                        //break;
                    case 6:
                        units = await cryptoService.GetCryptoForDay(symbol);
                        break;
                    case 7:
                        units = await cryptoService.GetCryptoForWeek(symbol);
                        break;
                    case 8:
                        units = await cryptoService.GetCryptoForMonth(symbol);
                        break;
                }
            } else
            {
                switch (dateIntervalCLicked)
                {
                    case 5:
                        units = await stockService.GetStocksForDayInterval(symbol, timeIntervals[timeInterevalCLicked]);
                        break;
                    case 6:
                        // TODO: treba greska 
                        WriteMessageOnChart("You need premium account to get stock \ndata for exactly one day");
                        return;
                        //units = await stockService.GetStocksForDay(symbol);
                        //break;
                    case 7:
                        units = await stockService.GetStocksForWeek(symbol);
                        break;
                    case 8:
                        units = await stockService.GetStocksForMonth(symbol);
                        break;
                }
            }

            errorMessageAnnotation = null;
            sciChartSurface.Annotations.Clear();

            GenerateChart(units, symbol);
            GenerateTable(units);
            CreateChip(symbol);
        }

        private void GenerateTable(List<StockUnit>? units)
        {
            List<Stock> stocks = new List<Stock>();
            String symbol = TextBox1.Text;
            units.ForEach(unit =>
                stocks.Add(new Stock()
                {
                    DateTime = unit.Date,
                    Low = unit.StockValue.Low,
                    High = unit.StockValue.High,
                    Close = unit.StockValue.Close,
                    Open = unit.StockValue.Open,
                    Name = symbol
                }
            ));
            this.viewModel.Stocks = stocks;
            this.DataContext= this.viewModel;
        }

        private void GenerateChart(List<StockUnit>? units, String symbol)
        {
           var ohlcDataSeries = new OhlcDataSeries<DateTime, double>();
           ohlcDataSeries.AcceptsUnsortedData = true;
           ohlcDataSeries.SeriesName = symbol;
           // Open, High, Low, Close
           foreach ( StockUnit unit in units )
           {
                ohlcDataSeries.Append(unit.Date, unit.StockValue.Open, unit.StockValue.High, unit.StockValue.Low, unit.StockValue.Close);
           }
           candlestickSeries[seriesIdx++].DataSeries = ohlcDataSeries;
           seriesIdx %= 5;
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            Generate();
        }

        private void Chip_OnDeleteClick(object sender, RoutedEventArgs e)
        {
            var currentChip = (Chip)sender;
            RemoveChips();
            chips.Remove(currentChip);
            AddChips();

        }

        private void CreateChip(String content)
        {
            var myChip = new MaterialDesignThemes.Wpf.Chip()
            {
                Height = 50,
                Content = content,
                IsDeletable = true,
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
                TextBox1.Text = cryptoCurrency.Code;
                AutocompleteListBox.Visibility = Visibility.Hidden;
            }
        }
        private void window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
            AutocompleteListBox.Visibility = Visibility.Hidden;
            // ILI OVO
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

        private void Refresh(object sender, RoutedEventArgs e)
        {
        }

        private void RemoveAll(object sender, RoutedEventArgs e)
        {
            RemoveChips();
            chips.Clear();
            chipCounter = 0;

            foreach (FastCandlestickRenderableSeries candleStick in candlestickSeries)
            {
                if (candleStick.DataSeries != null)
                candleStick.DataSeries.Clear();
                candleStick.DataSeries = null;
            }
            seriesIdx = 0;

            this.viewModel.Stocks.Clear();
            this.DataContext = this.viewModel;
        }

        private void WriteMessageOnChart(String message)
        {
            RemoveAll(new object(), new RoutedEventArgs());
            // Get the visible range of the X-axis
            var xVisibleRange = sciChartSurface.XAxis.VisibleRange;

            // Get the visible range of the Y-axis
            var yVisibleRange = sciChartSurface.YAxis.VisibleRange;

            // Get the X1 position of the current view
            DateTime x1 = (DateTime)xVisibleRange.Min;
            DateTime x2 = (DateTime)xVisibleRange.Max;
            TimeSpan timeSpan = x2 - x1;
            DateTime middleDateTime = x1.Add(timeSpan / 4);
            // Get the Y1 position of the current view
            var y1 = (double)yVisibleRange.Min;
            var y2 = (double)yVisibleRange.Max;

            errorMessageAnnotation = new TextAnnotation
            {
                X1 = middleDateTime, 
                Y1 = y1+ (y2-y1)/2, 
                Text = message, 
                FontSize = 18, 
                FontWeight = FontWeights.Bold, 
                HorizontalAlignment = HorizontalAlignment.Center, 
                VerticalAlignment = VerticalAlignment.Center, 
                Foreground = Brushes.Red,
                IsEditable = false
            };

            sciChartSurface.Annotations.Add(errorMessageAnnotation);
        }

    }
}
