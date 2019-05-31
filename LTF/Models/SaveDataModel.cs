using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTF.Models
{
    public class SaveDataModel
    {
        public int CorrectAnsCount { get; set; }
        public int IncorrectAnsCount { get; set; }
        public List<string> IncorrectAns { get; set; }
        public double AvgTime { get; set; }
        //public string currentDirectory { get; set; }
        public int Mark { get; set; }
        public int Percentage { get; set; }
        public DateTime date { get; set; }
    }
}
