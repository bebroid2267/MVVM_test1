using Microsoft.Xaml.Behaviors.Media;
using MVVM_test1.DataBase;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_test1.Model
{
    public class UsingPcTimeModel : BindableBase, INotifyPropertyChanged
    {

        public UsingPcTimeModel() 
        {
            Statistic = _Statistic;
            FirstStart = _FirstStart;
        }
        public ObservableCollection<string> _FirstStart
        {
            get { return FirstStart; }
            set
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    FirstStart = value;
                    OnPropertyChanged(nameof(FirstStart));
                });
            }
        }
        public ObservableCollection<string> _Statistic
        {

            get { return Statistic; }
            set
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    Statistic = value;
                    OnPropertyChanged(nameof(Statistic));
                });
            }
        }
        public string CalculateSumTime(DateTime startTimeProcess, string sumTime)
        {
            string totalTimeSpend = string.Empty;

            string date = DateTime.Now.ToString("d");
            
            if (sumTime == null)
                sumTime = "00:00:00";

            DateTime firstTimePrcs = new DateTime();

            if (DateTime.TryParseExact(sumTime, "HH:mm:ss", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out firstTimePrcs))
            {
                TimeSpan time = DateTime.Now - startTimeProcess;

                firstTimePrcs = firstTimePrcs.Add(time);
                totalTimeSpend = firstTimePrcs.ToString("HH:mm:ss");
            }
            else
            {
                TimeSpan timePassed = DateTime.Now - startTimeProcess;
                DateTime time = DateTime.Today.Add(timePassed);
                DateTime timePassedPrcs = new DateTime();

                DateTime.TryParseExact(time.ToString("HH':'mm':'ss"), "HH:mm:ss", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out timePassedPrcs);
                totalTimeSpend = timePassedPrcs.ToString("HH:mm:ss");
            }
            return totalTimeSpend;

        }
        public void GetStartTimeUsingPcToday()
        {
            string startTime = DateBase.GetFirstStartsUsingPcToday(DateTime.Now.ToString("d"));
            if (startTime == string.Empty)
            {
                startTime = DateTime.Now.ToString("t");
            }
            else
                startTime = startTime.Substring(11,5);
            if (_FirstStart.Count > 0)
                _FirstStart[0] = startTime;
            else
                _FirstStart.Add(startTime);

        }
        public void GetTimeUsingPcToday()
        {
            string usingTime = DateBase.GetSumTimeUsingPcToday(DateTime.Now.ToString("d"));

            if (usingTime == null || usingTime == string.Empty)
                usingTime = "00:00:00";

            string hours = usingTime.Substring(0, 2);
            string minute = usingTime.Substring(3, 2);
            if (hours[0] == '0' && hours[1] != '0')
                hours = hours.Remove(0,1);
            

            if (minute[0] == '0')
                minute = minute.Remove(0,1);

            int hoursNumbers = int.Parse(hours);
            int minuteNumbers = int.Parse(minute);

            if (hours == "00")
                usingTime = minute + " " + DeclensionGenerator.Generate(minuteNumbers, "минуту", "минуты", "минут");
            else
                usingTime = hours + " " + DeclensionGenerator.Generate(hoursNumbers, "час", "часа", "часов") + ", " + minute + " " + DeclensionGenerator.Generate(minuteNumbers, "минута", "минуты", "минут"); ;
            
            if (_Statistic.Count > 0)
                _Statistic[0] = usingTime;
            else
                _Statistic.Add(usingTime);

        }
        public  void CheckUsingPc()
        {
            DateBase.StartTodayUsingPc(DateTime.Now.ToString("d"));
            DateTime startTimeProcess = DateTime.Now;
            while (true)
            {
                string sumTime = DateBase.GetSumTimeUsingPcToday(DateTime.Now.ToString("d"));
                string totalTimeSpend = CalculateSumTime(startTimeProcess, sumTime);

                startTimeProcess = DateTime.Now;
                DateBase.UpdateSumTimeUsingPc(DateTime.Now.ToString("d"), totalTimeSpend);
                Thread.Sleep(10000);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private ObservableCollection<string> Statistic = new ObservableCollection<string>();
        private ObservableCollection<string> FirstStart = new ObservableCollection<string>();
    }
    public class DeclensionGenerator
    {
        /// <summary>
        /// Возвращает слова в падеже, зависимом от заданного числа
        /// </summary>
        /// <param name="number">Число от которого зависит выбранное слово</param>
        /// <param name="nominativ">Именительный падеж слова. Например "день"</param>
        /// <param name="genetiv">Родительный падеж слова. Например "дня"</param>
        /// <param name="plural">Множественное число слова. Например "дней"</param>
        /// <returns></returns>
        public static string Generate(int number, string nominativ, string genetiv, string plural)
        {
            var titles = new[] { nominativ, genetiv, plural };
            var cases = new[] { 2, 0, 1, 1, 1, 2 };
            return titles[number % 100 > 4 && number % 100 < 20 ? 2 : cases[(number % 10 < 5) ? number % 10 : 5]];
        }
    }
}
