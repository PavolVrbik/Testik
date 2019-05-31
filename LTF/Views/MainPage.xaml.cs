 using System;

using LTF.ViewModels;
using LTF.Services;

using Windows.UI.Xaml.Controls;

namespace LTF.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel
        {
            get { return ViewModelLocator.Current.MainViewModel; }
        }

        public MainPage()
        {
            InitializeComponent();

            //Temp deactivated
            //DataService ds = new DataService();
            //ds.CheckAppFolder();
        }
    }
}
