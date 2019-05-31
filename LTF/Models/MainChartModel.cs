using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTF.Models
{
    class MainChartModel
    {
        public string Category { get; set; } 
        public double Value { get; set; }

        public string CategoryInc { get; set; }
        public double ValueInc { get; set; }

        public string CategoryAvg { get; set; }
        public double ValueAvg { get; set; }

        public string CategoryMark { get; set; }
        public double ValueMark { get; set; }

        public string CategoryPercentage { get; set; }
        public double ValuePercentage { get; set; }
    }
}
