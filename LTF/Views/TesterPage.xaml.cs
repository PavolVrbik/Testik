using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using LTF.ViewModels;
using LTF.Services;
using LTF.Helpers;
using LTF.Core.Services;
using LTF.Models;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace LTF.Views
{
    public sealed partial class TesterPage : Page
    {
        private string TestName { get; set; }
        private int randomInt { get; set; }
        private int previousRandom = 0;
        /// <summary>
        /// Number of correct answers
        /// </summary>
        public int CorrectAnsCount { get; set; }
        /// <summary>
        /// Number of incorrect answers
        /// </summary>
        public int IncorrectAnsCount { get; set; }
        private long TestID { get; set; }
        /// <summary>
        /// List of all questions
        /// </summary>
        private List<string> Questions { get; set; }
        /// <summary>
        /// List of all answers option 1 correct option
        /// </summary>
        private List<string> Ans1 { get; set; }
        /// <summary>
        /// List of all answers option 2
        /// </summary>
        private List<string> Ans2 { get; set; }
        /// <summary>
        /// List of all answers option 3
        /// </summary>
        private List<string> Ans3 { get; set; }
        /// <summary>
        /// List of all answers option 4
        /// </summary>
        private List<string> Ans4 { get; set; }
        /// <summary>
        /// List of all answers option 5
        /// </summary>
        private List<string> Ans5 { get; set; }
        /// <summary>
        /// List of incorrect questions
        /// </summary>
        private List<string> FullIncAns { get; set; }
        /// <summary>
        /// List of all incorrect answers
        /// </summary>
        List<string> IncorrectAns = new List<string>();
        /// <summary>
        /// List of answer time
        /// </summary>
        private List<int> AnsTime = new List<int>();
        /// <summary>
        /// No answer count
        /// </summary>
        public int NoAnsCount { get; set; }
        /// <summary>
        /// Points counter
        /// </summary>
        private int Points { get; set; }
        /// <summary>
        /// Contains all incorrect questions
        /// </summary>
        private List<string> IncQuestions = new List<string>();
        private int Mark { get; set; }
        private string Announcement { get; set; }
        List<string> selectedQuestionList = new List<string>();
        private DispatcherTimer timer;
        private int basetime;
        private int time;
        private int NumOfQ;
        private int Percentage;

        private string currentDirectory = Directory.GetCurrentDirectory();

        private TesterViewModel ViewModel
        {
            get { return ViewModelLocator.Current.TesterViewModel; }
        }

        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TestID = Convert.ToInt64(e.Parameter);

            TimerSetup();
            ProgressBarSetup();
            DeserializeJSON();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (timer.IsEnabled)
            {
                timer.Stop();
            }
        }

        /// <summary>
        /// This method deserialize data from JSON
        /// </summary>
        private void DeserializeJSON()
        {
            var path = currentDirectory + "\\Assets" + "\\Data" + "\\data" + TestID + ".json";

            if (File.Exists(path))
            {
                var ds = DeserializeService.FromJson(path);

                this.Questions = ds.Question;
                this.Ans1 = ds.Ans1;
                this.Ans2 = ds.Ans2;
                this.Ans3 = ds.Ans3;
                this.Ans4 = ds.Ans4;
                this.Ans5 = ds.Ans5;
                BuildTest();
            }
            else
            {
                // TODO WTS: Dorobiť sťahovanie z internetu
                Debug.Write("Data not found");
                Debug.WriteLine(path);
            }
        }

        /// <summary>
        /// Generate random number and prevents before two equal numbers in a row
        /// </summary>
        /// <param name="max">Maximal number</param>
        /// <returns>Random number</returns>
        private int UpdateRandom(int max)
        {
            RandomGenerator random = new RandomGenerator();
            int randomInt = random.Next(0, max);

            if (randomInt != previousRandom)
            {
                previousRandom = randomInt;
                return randomInt;
            }
            else
            {
                return UpdateRandom(max);
            }
        }

        private void BuildTest()
        {
            if (NumOfQ != TestsRegister.GetNumQ(TestID))
            {
                Ring.IsActive = false;

                int listLength = Questions.Count;
                randomInt = UpdateRandom(listLength);

                Debug.WriteLine("Random " + randomInt);

                Question.Text = Questions[randomInt];

                selectedQuestionList.Add(Ans1[randomInt]); // 0
                selectedQuestionList.Add(Ans2[randomInt]); // 1 
                selectedQuestionList.Add(Ans3[randomInt]); // 2
                selectedQuestionList.Add(Ans4[randomInt]); // 3
                selectedQuestionList.Add(Ans5[randomInt]); // 4

                ShuffleHelper.Shuffle<string>(selectedQuestionList);

                Answer1.Content = selectedQuestionList[0];
                Answer2.Content = selectedQuestionList[1];
                Answer3.Content = selectedQuestionList[2];
                Answer4.Content = selectedQuestionList[3];
                Answer5.Content = selectedQuestionList[4];

                basetime = time;
                ProgressBarTime.Value = time;
                TimerEngine();
                timer.Start();

                NumOfQ++;
            }
            else
            {
                List<int> finalTime = new List<int>();
                Debug.WriteLine("Proces bol ukončený!");

                foreach (var time_ in AnsTime)
                {
                    finalTime.Add(time - time_);
                }

                foreach(var nullcount in IncorrectAns)
                {
                    if (nullcount == null)
                    {
                        NoAnsCount++;
                    }
                }

                double AvgTime = finalTime.Average();
                

                var pnts = (CorrectAnsCount / 0.50) / AvgTime;
                Points = (int)pnts;

                var totalAns = CorrectAnsCount + IncorrectAnsCount;
                Percentage = (100 * CorrectAnsCount) / totalAns;

                if (Percentage >= 90)
                {
                    Mark = 1;
                }
                if (Percentage >= 75 && Percentage < 90)
                {
                    Mark = 2;
                }
                if (Percentage >= 65 && Percentage < 75)
                {
                    Mark = 3;
                }
                if (Percentage >= 40 && Percentage < 65)
                {
                    Mark = 4;
                }
                if (Percentage < 40)
                {
                    Mark = 5;
                }

                SaveDataService sdc = new SaveDataService(CorrectAnsCount, IncorrectAnsCount, IncorrectAns, AvgTime, TestID, Mark, Percentage);

                var parameters = new PassDataModel();
                parameters.CorrectAnsCount = this.CorrectAnsCount;
                parameters.IncorrectAnsCount = this.IncorrectAnsCount;
                parameters.IncorrectAns = this.IncorrectAns;
                parameters.AvgTime = AvgTime;
                parameters.Date = DateTime.Now;
                parameters.NoAnsCount = NoAnsCount;
                parameters.Points = Points;
                parameters.Mark = Mark;
                parameters.Announcement = Annouc();
                parameters.Percentage = Percentage;
                parameters.IncQuestions = IncQuestions;
                parameters.ID = TestID;

                this.Frame.Navigate(typeof(SummaryPage), parameters);
            }
        }

        private void ChangeTitle()
        {
            var viewTitle = ApplicationView.GetForCurrentView();
            viewTitle.Title = "Spustené testovanie";
        }

        public TesterPage()
        {
            InitializeComponent();
            ChangeTitle();
            Ring.IsActive = true;
            TimerSetup();
        }

        private void ProgressBarSetup()
        {
            ProgressBarTime.Maximum = time;
            ProgressBarTime.Value = time;
        }

        private void TimerSetup()
        {
            time = TestsRegister.GetTimer(TestID);
            //basetime = time;
            Debug.WriteLine(time);
        }

        private void TimerEngine()
        {
            this.NavigationCacheMode = NavigationCacheMode.Required;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            timer.Tick += timer_Tick;
            Timer.Text = basetime.ToString();
        }

        private void timer_Tick(object sender, object e)
        {
            basetime = basetime - 1;
            ProgressBarTime.Value = basetime;
            Timer.Text = basetime.ToString();
            if (basetime == 0)
            {
                timer.Stop();
                CheckAnswer(null);
            }
        }

        private string Annouc()
        {
            if(Percentage == 100)
            {
                return "V teste si si viedol veľmi dobre! Nemal si ani jednu chybu! Si veľmi šikovný riešiteľ. Gratulujeme!";
            }
            if(Percentage >= 90)
            {
                return "V teste si si viedol skutočne dobre! Našli sa však nejaké chybičky, na ktorých by si mal popracovať!";
            }
            if (Mark == 2)
            {
                return "V teste si si viedol chválitebne! Našlo sa však zopár chýb. Pozri si ich a popracuj na nich!";
            }
            if (Mark == 3)
            {
                return "V teste si si viedol dobre! Urobil si však niekoľko chýb! Určite si ich pozri a poriadne na nich popracuj!";
            }
            if (Mark == 4)
            {
                return "Tentokrát sa ti moc nedarilo! Určite však nevešaj hlavu! Pozri si všetky chyby a poriadne na nich popracuj!";
            }
            if (Mark == 5)
            {
                return "Tentokrát sa to vôbec nevydarilo! Pozri si všetky svoje chyby, porozmýšľaj nad tým a uvidíš, že ďalší test už dopadne dobre!";
            }
            return "Niekde nastala chyba, skús to znova!";
        }

        private void CheckAnswer(string userAns)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                AnsTime.Add(basetime);
            }

            if (userAns == Ans1[randomInt])
            {
                CorrectAnsCount++;
                Debug.WriteLine("Počet správnych odpovedí " + CorrectAnsCount);
                selectedQuestionList.Clear();
                BuildTest();
            }
            else
            {
                IncorrectAnsCount++;
                IncorrectAns.Add(userAns);
                IncQuestions.Add(Questions[randomInt]);
                Debug.WriteLine("Počet nesprávnych odpovedí " + IncorrectAnsCount);
                selectedQuestionList.Clear();
                BuildTest();
            }
        }

        private void Answer1_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CheckAnswer(Answer1.Content.ToString());
        }

        private void Answer2_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CheckAnswer(Answer2.Content.ToString());
        }

        private void Answer3_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CheckAnswer(Answer3.Content.ToString());
        }

        private void Answer4_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CheckAnswer(Answer4.Content.ToString());
        }

        private void Answer5_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CheckAnswer(Answer5.Content.ToString());
        } 
    }
}
