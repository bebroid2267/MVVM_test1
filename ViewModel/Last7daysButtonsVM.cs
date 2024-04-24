using GalaSoft.MvvmLight.Command;
using MVVM_test1.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_test1.ViewModel
{
    public class Last7daysButtonsVM : ViewModelBase
    {
        public Last7daysButtonsVM()
        {
            YesterdayCommandParameter = DateTime.Now.AddDays(-1).ToString("d");
            TwoDaysLeftCommandParameter = DateTime.Now.AddDays(-2).ToString("d");
            ThreeDaysLeftCommandParameter = DateTime.Now.AddDays(-3).ToString("d");
            FourDaysLeftCommandParameter = DateTime.Now.AddDays(-4).ToString("d");
            FiveDaysLeftCommandParameter = DateTime.Now.AddDays(-5).ToString("d");
            SixDaysLeftCommandParameter = DateTime.Now.AddDays(-6).ToString("d");
            SevenDaysLeftCommandParameter = DateTime.Now.AddDays(-7).ToString("d");

            ShowLast7DaysCommand = new RelayCommand<object>(ShowApps);
        }
        public void ShowApps(object parameter)
        {
            if (parameter is string currentDate)
            {
                bool existsDays = CheckPages.IfExistsSevenDaysAppsInfo(currentDate);
                if (existsDays)
                    baseView = CheckPages.GetLast7Days(currentDate);
                else if (!existsDays)
                {
                    CheckPages.AddSevenDaysInfo(new List7DaysVM(currentDate), currentDate);
                    baseView = new List7DaysVM(currentDate);
                }
                OnBaseViewChanged(baseView);
            }
        }

        public event EventHandler<ViewModelBase> BaseViewChanged;

        protected void OnBaseViewChanged(ViewModelBase baseView)
        {
            BaseViewChanged?.Invoke(this, baseView);
        }

        public ViewModelBase baseView;
        public List7DaysVM List7DaysVM;
        public ICommand ShowLast7DaysCommand { get; set; }
        public string YesterdayCommandParameter { get; set; }
        public string TwoDaysLeftCommandParameter { get; set; }
        public string ThreeDaysLeftCommandParameter { get; set; }
        public string FourDaysLeftCommandParameter { get; set; }
        public string FiveDaysLeftCommandParameter { get; set; }
        public string SixDaysLeftCommandParameter { get; set; }
        public string SevenDaysLeftCommandParameter { get; set; }

        
    }
}
