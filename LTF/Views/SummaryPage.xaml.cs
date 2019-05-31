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
using CrossPieCharts.UWP.PieCharts;
using Windows.UI;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

namespace LTF.Views
{
    public sealed partial class SummaryPage : Page
    {
        private int CorrectAnsCount { get; set; }
        public int IncorrectAnsCount { get; set; }
        public List<string> IncorrectAns { get; set; }
        public double AvgTime { get; set; }
        public DateTime Date { get; set; }
        public List<PieChartArgs> CrossPie { get; set; }
        private int PercentageC { get; set; }
        private int PercentageI { get; set; }
        public int NoAnsCount { get; set; }
        public int Points { get; set; }
        public int Mark { get; set; }
        public string Announcement { get; set; }
        public int Percentage { get; set; }
        public List<string> IncQuestions { get; set; }
        public long ID { get; set; }
        //public List<int> Value = new List<int>();
        //public List<string> Category = new List<string>();
        //List<long> Value = new List<long>();
        //List<DateTimeOffset> Category = new List<DateTimeOffset>();
        List<MainChartModel> listdata = new List<MainChartModel>();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (PassDataModel)e.Parameter;
            this.CorrectAnsCount = parameters.CorrectAnsCount;
            this.IncorrectAnsCount = parameters.IncorrectAnsCount;
            this.IncorrectAns = parameters.IncorrectAns;
            this.AvgTime = parameters.AvgTime;
            this.Date = parameters.Date;
            this.NoAnsCount = parameters.NoAnsCount;
            this.Points = parameters.Points;
            this.Mark = parameters.Mark;
            this.Announcement = parameters.Announcement;
            this.Percentage = parameters.Percentage;
            this.IncQuestions = parameters.IncQuestions;
            this.ID = parameters.ID;

            CountPercentage();
            ChartSetup();
            StatsBuilder();
            LoadSumStats();
        }

        private SummaryViewModel ViewModel
        {
            get { return ViewModelLocator.Current.SummaryViewModel; }
        }

        private void CountPercentage()
        {
            var totalAns = CorrectAnsCount + IncorrectAnsCount;

            this.PercentageC = (100 * CorrectAnsCount) / totalAns;
            this.PercentageI = (100 * IncorrectAnsCount) / totalAns;
        }

        public SummaryPage()
        {
            InitializeComponent();
            Debug.WriteLine(CorrectAnsCount);
        }

        private void StatsBuilder()
        {
            var _points = (CorrectAnsCount + IncorrectAnsCount) * 2;

            tx1.Text = "Počet správnych odpovedí: " + CorrectAnsCount;
            tx2.Text = "Počet nesprávnych odpovedí: " + IncorrectAnsCount;
            tx3.Text = "Počet nezodpovedaných odpovedí: " + NoAnsCount;
            tx4.Text = "Tvoja známka: " + Mark;
            tx5.Text = "Získané body: " + Points + " z možných " + _points;
            tx6.Text = Percentage + "%";
            status.Text = Announcement;

            var IncorrectQ = IncQuestions.Distinct().ToList();
            foreach (var item in IncorrectQ)
            {
                AnsBox.Items.Add(item);
            }
        }

        private void LoadSumStats()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\LTF" + "\\userdata" + ID + ".json";

            var stats = StatsService.FromJson((path));

            List<long> Value = new List<long>();
            Value = stats.Select(i => i.CorrectAnsCount).ToList();

            List<DateTimeOffset> Category = new List<DateTimeOffset>();
            Category = stats.Select(j => j.Date).ToList();

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

        private void ChartSetup()
        {
            CrossPie = new List<PieChartArgs>
            {
                new PieChartArgs
                {
                    Percentage = PercentageC,
                    ColorBrush = new SolidColorBrush(Colors.GreenYellow)
                },
                new PieChartArgs
                {
                    Percentage = PercentageI,
                    ColorBrush = new SolidColorBrush(Colors.Crimson)
                }
            };
            chart.DataContext = CrossPie;
        }

        private void AnsBox_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            var index = AnsBox.SelectedIndex;
            if (IncorrectAns[index] == null)
            {
                var dialogNULL = new MessageDialog("Na túto otázku si neodpovedal v časovom limite!", "Tvoja odpoveď na otázku");

                //dialog.Commands.Add(new UICommand("Zavrieť", delegate (IUICommand command)
                //{
                //    dialog
                //}));

                dialogNULL.ShowAsync();
            } 
            var dialog = new MessageDialog("Na túto otázku si odpovedal: " + IncorrectAns[index] + ", čo je nesprávna odpoveď!", "Tvoja odpoveď na otázku");
            dialog.ShowAsync();
        }
    }
}
