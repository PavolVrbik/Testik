using System;

namespace LTF.Core.Models
{
    public class MainRegister
    {
        public long TestId { get; set; }

        public DateTime CreateDate { get; set; }

        public string TestName { get; set; }

        public string Subject { get; set; }

        public string ShortName { get; set; }

        public string Status { get; set; }

        public char Symbol { get; set; }

        public int Timer { get; set; }

        public int NumQ { get; set; }

        public override string ToString()
        {
            return $"{TestName} {Status}";
        }
    }
}
