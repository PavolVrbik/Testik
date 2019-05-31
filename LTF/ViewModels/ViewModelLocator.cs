using System;

using GalaSoft.MvvmLight.Ioc;

using LTF.Services;
using LTF.Views;

namespace LTF.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class ViewModelLocator
    {
        private static ViewModelLocator _current;

        public static ViewModelLocator Current => _current ?? (_current = new ViewModelLocator());

        private ViewModelLocator()
        {
            SimpleIoc.Default.Register(() => new NavigationServiceEx());
            SimpleIoc.Default.Register<ShellViewModel>();
            Register<MainViewModel, MainPage>();
            Register<TestingViewModel, TestingPage>();
            Register<TestingDetailViewModel, TestingDetailPage>();
            Register<StatisticsViewModel, StatisticsPage>();
            Register<SettingsViewModel, SettingsPage>();
            Register<TesterViewModel, TesterPage>();
            Register<GuruViewModel, GuruPage>();
            Register<SummaryViewModel, SummaryPage>();
            Register<FullStatsViewModel, FullStatsPage>();
        }

        public FullStatsViewModel FullStatsViewModel => SimpleIoc.Default.GetInstance<FullStatsViewModel>();

        public SummaryViewModel SummaryViewModel => SimpleIoc.Default.GetInstance<SummaryViewModel>();

        public GuruViewModel GuruViewModel => SimpleIoc.Default.GetInstance<GuruViewModel>();

        public TesterViewModel TesterViewModel => SimpleIoc.Default.GetInstance<TesterViewModel>();

        public SettingsViewModel SettingsViewModel => SimpleIoc.Default.GetInstance<SettingsViewModel>();

        public StatisticsViewModel StatisticsViewModel => SimpleIoc.Default.GetInstance<StatisticsViewModel>();

        public TestingDetailViewModel TestingDetailViewModel => SimpleIoc.Default.GetInstance<TestingDetailViewModel>();

        public TestingViewModel TestingViewModel => SimpleIoc.Default.GetInstance<TestingViewModel>();

        public MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();

        public ShellViewModel ShellViewModel => SimpleIoc.Default.GetInstance<ShellViewModel>();

        public NavigationServiceEx NavigationService => SimpleIoc.Default.GetInstance<NavigationServiceEx>();

        public void Register<VM, V>()
            where VM : class
        {
            SimpleIoc.Default.Register<VM>();

            NavigationService.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}
