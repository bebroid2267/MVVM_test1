using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_test1
{
    public class ProcessTime
    {
        public string NameProcess { get; set; }

        //public string NameProcess
        //{

        //    get { return _NameProcess; }

        //    set
        //    {
        //        if (value.Length > 20)
        //        {
        //            _NameProcess = value.Substring(0, 20) + "\n" + value.Substring(20);   
        //        }
        //        else
        //            _NameProcess = value;
        //    }
        //}
        public string StatusApp { get; set; }
        public string SumTimeProcess { get; set; }
        public string StartTodaySession { get; set; }
        public string GlobalStartTime { get; set;}
        public string EndSession { get; set; }
        public string StartSession { get; set; }
        public string IcoPath { get; set; }
        public int TodayCountStarts { get; set; }
        public int YesterdayCountStarts { get; set; }


        public Category CategoryApp {  get; set; }
        public enum Category
        {
            FavoriteAppToday,
            FavoriteAppAllTime,
            HaventLongTimeLaunched,
            UseTimeForToday,
            UseTimeForTomorrow
        }

        
        
    }
}
