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

        private int[] timeIntervals = {1,5,15,30,60};
        private int[] dateIntervals = { 1, 7, 30, 365};
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
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


            rSeries.DataSeries = ohlcDataSeries;
            r2Series.DataSeries = ohlcDataSeries2;

            // Create XyDataSeries to host data for our charts
            //var scatterData = new XyDataSeries<double, double>();
            //var lineData = new XyDataSeries<double, double>();

            //scatterData.SeriesName = "Cos(x)";
            //lineData.SeriesName = "Sin(x)";

            //for (int i = 0; i < 1000; i++)
            //{
            //    lineData.Append(i, Math.Sin(i * 0.1));
            //    scatterData.Append(i, Math.Cos(i * 0.1));
            //}
            //// Assign dataseries to RenderSeries
            //LineSeries.DataSeries = lineData;
            //ScatterSeries.DataSeries = scatterData;

            //double phase = 0.0;
            //var timer = new DispatcherTimer(DispatcherPriority.Render);
            //timer.Interval = TimeSpan.FromMilliseconds(10);
            //timer.Tick += (s, e) =>
            //{
            //    // SuspendUpdates() ensures the chart is frozen
            //    // while you do updates. This ensures best performance
            //    using (lineData.SuspendUpdates())
            //    using (scatterData.SuspendUpdates())
            //    {
            //        for (int i = 0; i < 1000; i++)
            //        {
            //            // Updates the Y value at index i
            //            lineData.Update(i, Math.Sin(i * 0.1 + phase));
            //            scatterData.Update(i, Math.Cos(i * 0.1 + phase));
            //        }
            //    }
            //    phase += 0.01;
            //};
            //timer.Start();
        }

        private void BindButtons()
        {
            var btns = this.graphGrid.Children.OfType<Button>().ToList();
            ResetTimeIntervalTags(btns, 5);
            ResetDateIntervalTags(btns, 5);

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

        private void ResetDateIntervalTags(List<Button> btns, int dateBtns = 5)
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
        }

        private void ChangeChartForInterval()
        {

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

    }
}
