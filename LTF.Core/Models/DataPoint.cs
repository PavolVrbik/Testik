using System;
using System.Collections.ObjectModel;

namespace LTF.Core.Models
{
    public class DataPoint
    {
        public double Value { get; set; }

        public string Category { get; set; }

        public string Name { get; set; }

        public long Id { get; set; }

        public int TestTime { get; set; }

    }
}
