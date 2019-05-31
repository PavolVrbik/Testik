using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using LTF.Core.Models;
using LTF.Core.Services;
using LTF.Services;

using Microsoft.Toolkit.Uwp.UI.Animations;

namespace LTF.ViewModels
{
    public class TestingViewModel : ViewModelBase
    {
        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private ICommand _itemClickCommand;

        public ICommand ItemClickCommand => _itemClickCommand ?? (_itemClickCommand = new RelayCommand<MainRegister>(OnItemClick));

        public ObservableCollection<MainRegister> Source
        {
            get
            {
                return TestsRegister.GetContentGridData();
            }
        }

        public TestingViewModel()
        {
        }

        private void OnItemClick(MainRegister clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnnimation(clickedItem);
                NavigationService.Navigate(typeof(TestingDetailViewModel).FullName, clickedItem.TestId);
            }
        }
    }
}
