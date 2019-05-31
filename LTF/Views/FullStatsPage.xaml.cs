using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;

using LTF.ViewModels;
using LTF.Models;
using LTF.Services;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media;
using Windows.UI.Popups;
using Windows.UI;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

namespace LTF.Views
{
    public sealed partial class FullStatsPage : Page
    {
        List<StatsService> Stats = new List<StatsService>();
        private long ID { get; set; }

        private FullStatsViewModel ViewModel
        {
            get { return ViewModelLocator.Current.FullStatsViewModel; }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ID = Convert.ToInt64(e.Parameter);

            LoadSumStats();
            LoadIncorrectStats();
            LoadAvgStats();
            LoadMarkStats();
            LoadPercentageStats();
        }

        public FullStatsPage()
        {
            InitializeComponent();
        }

        private void LoadSumStats()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\LTF" + "\\userdata" + ID + ".json";

            Stats = StatsService.FromJson((path));
            
            List<long> Value = new List<long>();
            Value = Stats.Select(i => i.CorrectAnsCount).ToList();

            List<DateTimeOffset> Category = new List<DateTimeOffset>();
            Category = Stats.Select(j => j.Date).ToList();

            List<MainChartModel> listdata = new List<MainChartModel>();

            int Count = Category.Count;
            int index = 0;
            while (Count - index >= 10)
            {
                Value.RemoveAt(0);
                Category.RemoveAt(0);
                index++;
                Debug.WriteLine("Dátum: " + Category[0]);
                Debug.WriteLine("Index: " + index);
                Debug.WriteLine("Počet položiek: " + Count);
            }

            int LoopCount = Count - index;
            for (int i = 0; i < LoopCount; i++)
            {
                var newValue = (double)Value[i];
                var newCategory = Category[i].ToString("dd/MM HH:mm");

                listdata.Add(new MainChartModel() { Category = newCategory, Value = newValue });
            }

            (this.chartseries.Series[0] as LineSeries).ItemsSource = listdata;
        }

        private void LoadIncorrectStats()
        {
            List<long> Value = new List<long>();
            Value = Stats.Select(i => i.IncorrectAnsCount).ToList();

            List<DateTimeOffset> Category = new List<DateTimeOffset>();
            Category = Stats.Select(j => j.Date).ToList();

            List<MainChartModel> listdata = new List<MainChartModel>();

            int Count = Category.Count;
            int index = 0;
            while (Count - index >= 10)
            {
                Value.RemoveAt(0);
                Category.RemoveAt(0);
                index++;
                Debug.WriteLine("Dátum: " + Category[0]);
                Debug.WriteLine("Index: " + index);
                Debug.WriteLine("Počet položiek: " + Count);
            }

            int LoopCount = Count - index;
            for (int i = 0; i < LoopCount; i++)
            {
                var newValue = (double)Value[i];
                var newCategory = Category[i].ToString("dd/MM HH:mm");

                listdata.Add(new MainChartModel() { CategoryInc = newCategory, ValueInc = newValue });
            }

            (this.chartseries.Series[1] as LineSeries).ItemsSource = listdata;
        }

        private void LoadAvgStats()
        {
            List<double> Value = new List<double>();
            Value = Stats.Select(i => i.AvgTime).ToList();

            List<DateTimeOffset> Category = new List<DateTimeOffset>();
            Category = Stats.Select(j => j.Date).ToList();

            List<MainChartModel> listdata = new List<MainChartModel>();

            int Count = Category.Count;
            int index = 0;
            while (Count - index >= 10)
            {
                Value.RemoveAt(0);
                Category.RemoveAt(0);
                index++;
                Debug.WriteLine("Dátum: " + Category[0]);
                Debug.WriteLine("Index: " + index);
                Debug.WriteLine("Počet položiek: " + Count);
            }

            int LoopCount = Count - index;
            for (int i = 0; i < LoopCount; i++)
            {
                var newValue = Value[i];
                var newCategory = Category[i].ToString("dd/MM HH:mm");

                listdata.Add(new MainChartModel() { CategoryAvg = newCategory, ValueAvg = newValue });
            }

            (this.chartavgseries.Series[0] as LineSeries).ItemsSource = listdata;
        }

        private void LoadMarkStats()
        {
            List<double> Value = new List<double>();
            Value = Stats.Select(i => i.Mark).ToList();

            List<DateTimeOffset> Category = new List<DateTimeOffset>();
            Category = Stats.Select(j => j.Date).ToList();

            List<MainChartModel> listdata = new List<MainChartModel>();

            int Count = Category.Count;
            int index = 0;
            while (Count - index >= 10)
            {
                Value.RemoveAt(0);
                Category.RemoveAt(0);
                index++;
                Debug.WriteLine("Dátum: " + Category[0]);
                Debug.WriteLine("Index: " + index);
                Debug.WriteLine("Počet položiek: " + Count);
            }

            int LoopCount = Count - index;
            for (int i = 0; i < LoopCount; i++)
            {
                var newValue = (double)Value[i];
                var newCategory = Category[i].ToString("dd/MM HH:mm");

                listdata.Add(new MainChartModel() { CategoryMark = newCategory, ValueMark = newValue });
            }

            (this.chartmarkseries.Series[0] as LineSeries).ItemsSource = listdata;
        }

        private void LoadPercentageStats()
        {
            List<double> Value = new List<double>();
            Value = Stats.Select(i => i.Percentage).ToList();

            List<DateTimeOffset> Category = new List<DateTimeOffset>();
            Category = Stats.Select(j => j.Date).ToList();

            List<MainChartModel> listdata = new List<MainChartModel>();

            int Count = Category.Count;
            int index = 0;
            while (Count - index >= 10)
            {
                Value.RemoveAt(0);
                Category.RemoveAt(0);
                index++;
                Debug.WriteLine("Dátum: " + Category[0]);
                Debug.WriteLine("Index: " + index);
                Debug.WriteLine("Počet položiek: " + Count);
            }

            int LoopCount = Count - index;
            for (int i = 0; i < LoopCount; i++)
            {
                var newValue = (double)Value[i];
                var newCategory = Category[i].ToString("dd/MM HH:mm");

                listdata.Add(new MainChartModel() { CategoryPercentage = newCategory, ValuePercentage = newValue });
            }

            (this.chartpercentageseries.Series[0] as LineSeries).ItemsSource = listdata;
        }
    }
}
