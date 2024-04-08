using MVVM_test1.DataBase;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_test1.Model
{
    public static class UsingPcTimeModel
    {
        public static void CheckUsingPc()
        {
            DateBase.StartTodayUsingPc(DateTime.UtcNow.ToString("d"));
            DateTime startTimeProcess = DateTime.UtcNow.AddHours(3);
            while (true)
            {
                string totalTimeSpend = string.Empty;
                
                string date = DateTime.UtcNow.ToString("d");
                string sumTime = DateBase.GetSumTimeUsingPcToday(date);

                if (sumTime == null)
                    sumTime = "00:00:00";

                DateTime firstTimePrcs = new DateTime();

                if (DateTime.TryParseExact(sumTime, "HH:mm:ss", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out firstTimePrcs))
                {
                    TimeSpan time = DateTime.UtcNow.AddHours(3) - startTimeProcess;

                    firstTimePrcs = firstTimePrcs.Add(time);
                    totalTimeSpend = firstTimePrcs.ToString("HH:mm:ss");
                }
                else
                {
                    TimeSpan timePassed = DateTime.UtcNow.AddHours(3) - startTimeProcess;
                    DateTime time = DateTime.Today.Add(timePassed);
                    DateTime timePassedPrcs = new DateTime();

                    DateTime.TryParseExact(time.ToString("HH':'mm':'ss"), "HH:mm:ss", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out timePassedPrcs);
                    totalTimeSpend = timePassedPrcs.ToString("HH:mm:ss");
                }

                startTimeProcess = DateTime.UtcNow.AddHours(3);
                DateBase.UpdateSumTimeUsingPc(DateTime.UtcNow.ToString("d"), totalTimeSpend);
                Thread.Sleep(10000);
            }
        }
    }
}
