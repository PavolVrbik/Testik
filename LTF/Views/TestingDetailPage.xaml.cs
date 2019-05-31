using System;

using LTF.Services;
using LTF.ViewModels;

using Microsoft.Toolkit.Uwp.UI.Animations;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;

namespace LTF.Views
{
    public sealed partial class TestingDetailPage : Page
    {
        private long id { get; set; }

        private TestingDetailViewModel ViewModel
        {
            get { return ViewModelLocator.Current.TestingDetailViewModel; }
        }

        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        public TestingDetailPage()
        {
            InitializeComponent();        
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is long testId)
            {
                ViewModel.Initialize(testId);
                id = testId;
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnnimation(ViewModel.Item);
            }
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TesterPage),id);
            Debug.WriteLine("Sending " + id);
        }

        private void Button_Click_Stats(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FullStatsPage), id);
        }
    }
}
