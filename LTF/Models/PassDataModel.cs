using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTF.Models
{
    class PassDataModel
    {
        public int CorrectAnsCount { get; set; }
        public int IncorrectAnsCount { get; set; }
        public List<string> IncorrectAns { get; set; }
        public List<string> FullIncAns { get; set; }
        public double AvgTime { get; set; }
        public DateTime Date { get; set; }
        public int NoAnsCount { get; set; }
        public int Points { get; set; }
        public int Mark { get; set; }
        public string Announcement { get; set; }
        public int Percentage { get; set; }
        public List<string> IncQuestions { get; set; }
        public long ID { get; set; }
    }
}
