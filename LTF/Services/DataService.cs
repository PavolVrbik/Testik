using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LTF.Services
{
    class DataService
    {
        public string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public DataService()
        {
            //CheckAppFolder();
        }

        public void CheckAppFolder()
        {
            string path = appdata + "\\LTF";
            Directory.CreateDirectory(path);

            // creating folder with tests
            string exercisesfolder = path + "\\source";
        }

        private void UpdateData()
        {
            
        }
    }
}
