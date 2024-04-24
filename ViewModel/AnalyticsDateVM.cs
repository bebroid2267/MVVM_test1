using GalaSoft.MvvmLight.Command;
using MVVM_test1.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_test1.ViewModel
{
    public class AnalyticsDateVM : Utilities.ViewModelBase
    {
        public ViewModelBase CurrentVM
        {
            get { return currentVM; }
            set
            {
                currentVM = value;
                OnPropertyChanged(nameof(currentVM));
            }
        }
        public ViewModelBase CurrentVMFromButtons
        {
            get { return currentVM; }
            set
            {
                currentVM = value;
                OnPropertyChanged(nameof(currentVM));
            }
        }
        public ICommand ShowYesterdayAppsButton { get; set; }
        public ICommand ShowTodayAppsButton { get; set; }
        public ICommand ShowLast7DaysButton { get; set; }
        public ICommand ShowAllTimesApp { get; set; }

        public AnalyticsDateVM()
        {
            ShowYesterdayAppsButton = new RelayCommand<object>(obj =>
            {
                bool existsDays = CheckPages.IfExistsSevenDaysAppsInfo(DateTime.Now.AddDays(-1).ToString("d"));
                if (existsDays)
                    CurrentVM = CheckPages.GetLast7Days(DateTime.Now.AddDays(-1).ToString("d"));
                else if (!existsDays)
                {
                    List7DaysVM vm = new List7DaysVM(DateTime.Now.AddDays(-1).ToString("d"));
                    CheckPages.AddSevenDaysInfo(vm, DateTime.Now.AddDays(-1).ToString("d"));
                    CurrentVM = vm;
                }
            });

            ShowTodayAppsButton = new RelayCommand<object>(obj =>
            {
                bool existsDays = CheckPages.IfExistsSevenDaysAppsInfo(DateTime.Now.ToString("d"));
                if (existsDays)
                    CurrentVM = CheckPages.GetLast7Days(DateTime.Now.ToString("d"));
                else if (!existsDays)
                {
                    List7DaysVM vm = new List7DaysVM(DateTime.Now.ToString("d"));
                    CheckPages.AddSevenDaysInfo(vm, DateTime.Now.ToString("d"));
                    CurrentVM = vm;
                }
            });
            ShowLast7DaysButton = new RelayCommand<object>(obj => CurrentVM = new Last7daysButtonsVM());

            ShowAllTimesApp = new RelayCommand<object>(obj => {

                bool existsAllTimes = CheckPages.IfExistsAllTimesPage();
                if (existsAllTimes)
                    CurrentVM = CheckPages.GetAllTimesPage();
                else if (!existsAllTimes)
                {
                    AllTimesDateVM vm = new AllTimesDateVM();
                    CheckPages.AddAllTimesPage(vm);
                    CurrentVM = vm;
                }
            });

            PropertyChanged += AnalyticsDateVM_PropertyChanged;
        }
        private void AnalyticsDateVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(currentVM) && CurrentVM is Last7daysButtonsVM)
            {
                (CurrentVM as Last7daysButtonsVM).BaseViewChanged += OnBaseViewChanged;
            }
        }
        private void OnBaseViewChanged(object sender, ViewModelBase baseView)
        {
            CurrentVM = baseView;
        }
        private ViewModelBase currentVM;
    }

}
