using MVVM_test1.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVVM_test1.Utilities
{
    public static class NotifyIconMessage
    {
        private static NotifyIcon notifyicon;

        public static void AddNotifyIcon(NotifyIcon icon)
        {
            if (notifyicon == null)
                notifyicon = icon;
        }
        public static NotifyIcon GetNofifyIcon()
        {
            return notifyicon;
        }

        public static void ShowMessage(DateTime startTimeUsingApp)
        {
            DateTime dateTime;
            

            while (true)
            {
                DateTime nowTime = DateTime.Now;
                TimeSpan time = DateTime.Now - startTimeUsingApp;
                string spendTime = time.ToString();

                string hours = spendTime.Substring(0, 2);
                string minute = spendTime.Substring(3, 2);
                if (hours[0] == '0' && hours[1] != '0')
                    hours = hours.Remove(0, 1);


                if (minute[0] == '0')
                    minute = minute.Remove(0, 1);

                int hoursNumbers = int.Parse(hours);
                int minuteNumbers = int.Parse(minute);

                if (hours == "00")
                    spendTime = minute + " " + DeclensionGenerator.Generate(minuteNumbers, "минуту", "минуты", "минут");
                else
                    spendTime = hours + " " + DeclensionGenerator.Generate(hoursNumbers, "час", "часа", "часов") + ", " + minute + " " + DeclensionGenerator.Generate(minuteNumbers, "минута", "минуты", "минут"); ;

                Thread.Sleep(60 * 1000);
                notifyicon.ShowBalloonTip(10000, "Мы беспокоимся о вас!", $"За устройством просидели уже: {spendTime}\nМожет пора отдохнуть?", ToolTipIcon.Info);
            }
        }
    }
}
