using GalaSoft.MvvmLight;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts;
using MVVM_test1.DataBase;
using MVVM_test1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_test1.ViewModel
{
    public class List7DaysVM : Utilities.ViewModelBase
    {
        public ObservableCollection<ProcessTime> _Apps
        {
            get { return Apps._WorkProcess; }
            set
            {
                Apps._WorkProcess = value;
                OnPropertyChanged(nameof(_Apps));
            }
        }

        //public SeriesCollection SeriesCollection { get; private set; }

        public List7DaysVM(string date)
        {
            Apps = new Last7DaysModel();

            Apps.GetProcess(date);
            //SeriesCollection = new SeriesCollection();

            //// Примерный цикл, в котором вы добавляете PieSeries в коллекцию
            //foreach (ProcessTime process in DateBase.GetAppUsingForDay(DateTime.Now.ToString("d")))
            //{
            //    SeriesCollection.Add(new PieSeries
            //    {
            //        Title = process.NameProcess,
            //        Values = new ChartValues<ObservableValue> { new ObservableValue(ParseSumTime(process.SumTimeProcess)) }
            //    });
            //}
        }
        //public static double ParseSumTime(string sumTime)
        //{
        //    if (sumTime.Length == 8)
        //    {
        //        string hours = sumTime.Substring(0, 2);
        //        string minutes = sumTime.Substring(3, 2);
        //        return double.Parse(hours + "," + minutes);

        //    }
        //    return 0;
        //}


        private Last7DaysModel Apps;
    }
}
