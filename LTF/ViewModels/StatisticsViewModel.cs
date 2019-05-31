using System;
using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;

using LTF.Core.Models;
using LTF.Core.Services;

namespace LTF.ViewModels
{
    public class StatisticsViewModel : ViewModelBase
    {
        public StatisticsViewModel()
        {
        }

        public ObservableCollection<DataPoint> Source
        {
            get
            {
                return null;
            }
        }
    }
}
