using System;

using LTF.ViewModels;

using Windows.UI.Xaml.Controls;

namespace LTF.Views
{
    public sealed partial class TestingPage : Page
    {
        private TestingViewModel ViewModel
        {
            get { return ViewModelLocator.Current.TestingViewModel; }
        }

        public TestingPage()
        {
            InitializeComponent();
        }
    }
}
