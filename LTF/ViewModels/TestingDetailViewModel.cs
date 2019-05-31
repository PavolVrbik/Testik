using System;
using System.Linq;

using GalaSoft.MvvmLight;

using LTF.Core.Models;
using LTF.Core.Services;

namespace LTF.ViewModels
{
    public class TestingDetailViewModel : ViewModelBase
    {
        private MainRegister _item;

        public MainRegister Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public TestingDetailViewModel()
        {
        }

        public void Initialize(long orderId)
        {
            var data = TestsRegister.GetContentGridData();
            Item = data.First(i => i.TestId == orderId);
        }
    }
}
