using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Diagnostics;

using LTF.Core.Models;

namespace LTF.Core.Services
{
    public static class TestsRegister
    {
        private static IEnumerable<MainRegister> MainTestRegister()
        {
            // The following is order summary data
            var data = new ObservableCollection<MainRegister>
            {
                new MainRegister
                {
                    TestId = 70003,
                    CreateDate = new DateTime(2019, 04, 10),
                    TestName = "Násobilka číslo 1",
                    Subject = "Matematika",
                    ShortName = "Matematika",
                    Status = "Dostupné",
                    Symbol = (char)57643, // Symbol.Globe
                    Timer = 3,
                    NumQ = 10
                },
            };
            return data;
        }

        public static ObservableCollection<DataPoint> GetChartData()
        {
            var data = MainTestRegister().Select(o => new DataPoint() { Category = o.TestName, Value = 0}) // o.OrderTotal (to bolo vo Value = )
                                  .OrderBy(dp => dp.Category);

            return new ObservableCollection<DataPoint>(data);
        }
        private static IEnumerable<MainRegister> _allOrders;

        public static int GetTimer(long id)
        {
            IEnumerable<MainRegister> data = MainTestRegister().Where(x => x.TestId == id);
            List<MainRegister> asList = data.ToList();
            

            Debug.WriteLine("Calling GetTimer()");

            int time = 0;

            foreach (var lol in data)
            {
                var Timer = lol.Timer;
                Debug.WriteLine(Timer);

                if (Timer > 0)
                {
                    time = Timer;
                }
            }
            return time;
        }

        public static int GetNumQ(long id)
        {
            IEnumerable<MainRegister> data = MainTestRegister().Where(x => x.TestId == id);
            List<MainRegister> asList = data.ToList();

            int numQ = 0;

            foreach (var num in data)
            {
                numQ = num.NumQ;
            }

            return numQ;
        }

        public static ObservableCollection<MainRegister> GetContentGridData()
        {
            if (_allOrders == null)
            {
                _allOrders = MainTestRegister();
            }

            return new ObservableCollection<MainRegister>(_allOrders);
        }
    }
}
