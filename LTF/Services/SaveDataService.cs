using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

using LTF.Models;

namespace LTF.Services
{
    class SaveDataService
    {
        public int CorrectAnsCount;
        private int IncorrectAnsCount;
        private List<string> IncorrectAns;
        private double AvgTime;
        private string currentDirectory = Directory.GetCurrentDirectory();
        private DateTime date = DateTime.Now;
        private long ID;
        private int Mark;
        private int Percentage;

        public SaveDataService(int CorrectAnsCount, int IncorrectAnsCount, List<string> IncorrectAns, double AvgTime, long ID, int Mark, int Percentage)
        {
            this.CorrectAnsCount = CorrectAnsCount;
            this.IncorrectAnsCount = IncorrectAnsCount;
            this.IncorrectAns = IncorrectAns;
            this.AvgTime = AvgTime;
            this.ID = ID;
            this.Mark = Mark;
            this.Percentage = Percentage;
            Debug.WriteLine("Volám SaveData()");
            SaveData();
        }
        
        private void SaveData()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\LTF" + "\\userdata" + ID +".json";
            var tempPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\LTF";

            Debug.WriteLine(path);
            if (File.Exists(path))
            {

                var list = JsonConvert.DeserializeObject<List<SaveDataModel>>(File.ReadAllText(path));
                list.Add(new SaveDataModel
                {
                    date = this.date,
                    CorrectAnsCount = this.CorrectAnsCount,
                    IncorrectAnsCount = this.IncorrectAnsCount,
                    IncorrectAns = this.IncorrectAns,
                    AvgTime = this.AvgTime,
                    Mark = this.Mark,
                    Percentage = this.Percentage
                });

                Debug.WriteLine("Načítavam súbor");
                var convertedList = JsonConvert.SerializeObject(list, Formatting.Indented);
                File.WriteAllText(path, convertedList);
            }
            else
            {
                Directory.CreateDirectory(tempPath);

                var list = new List<SaveDataModel>();
                list.Add(new SaveDataModel
                {
                    date = this.date,
                    CorrectAnsCount = this.CorrectAnsCount,
                    IncorrectAnsCount = this.IncorrectAnsCount,
                    IncorrectAns = this.IncorrectAns,
                    AvgTime = this.AvgTime,
                    Mark = this.Mark,
                    Percentage = this.Percentage
                });

                Debug.WriteLine("Vytváram nový súbor");
                var convertedList = JsonConvert.SerializeObject(list, Formatting.Indented);
                File.WriteAllText(path, convertedList);
            }
        }
        
    }
}
