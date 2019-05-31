using System;

using LTF.ViewModels;

using Windows.UI.Xaml.Controls;

namespace LTF.Views
{
    public sealed partial class StatisticsPage : Page
    {
        private StatisticsViewModel ViewModel
        {
            get { return ViewModelLocator.Current.StatisticsViewModel; }
        }

        // TODO WTS: Change the chart as appropriate to your app.
        // For help see http://docs.telerik.com/windows-universal/controls/radchart/getting-started
        public StatisticsPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TestingPage));
        }
    }
}
