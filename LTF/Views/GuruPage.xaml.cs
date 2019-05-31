using System;

using LTF.ViewModels;

using Windows.UI.Xaml.Controls;

namespace LTF.Views
{
    public sealed partial class GuruPage : Page
    {
        private GuruViewModel ViewModel
        {
            get { return ViewModelLocator.Current.GuruViewModel; }
        }

        public GuruPage()
        {
            InitializeComponent();

            webbot.Navigate(new Uri("https://webchat.botframework.com/embed/TestikWeb?s=zCrG-ai1BzY.t52Gh-ijylT_Nc1y8xWcvrdwDWx6dDM2cXfpscHxwOA"));
        }
    }
}
