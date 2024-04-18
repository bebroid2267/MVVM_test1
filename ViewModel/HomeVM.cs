using MVVM_test1.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_test1.ViewModel
{
    public class HomeVM : BindableBase, INotifyPropertyChanged
    {
        public ObservableCollection<ProcessTime> _RandomAppTwo
        {
            get { return RandomAppTwo._App; }
            set
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    RandomAppTwo._App = value;
                    OnPropertyChanged(nameof(_RandomAppTwo));
                });
            }

        }

        public ObservableCollection<ProcessTime> _RandomAppOne
        {
            get { return RandomAppOne._App; }
            set
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    RandomAppOne._App = value;
                    OnPropertyChanged(nameof(_RandomAppOne));
                });
            }

        }
        public ObservableCollection<ProcessTime> _AppHaventLaucnTime
        {
            get { return AppHaventLaucnTime._App; }
            set
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    AppHaventLaucnTime._App = value;
                    OnPropertyChanged(nameof(_AppHaventLaucnTime));
                });
            }
        }
        public ObservableCollection<ProcessTime> _AppEver
        {
            get { return AppEver._MoreUsingApp; }
            set
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    AppEver._MoreUsingApp = value;
                    OnPropertyChanged(nameof(_AppEver));
                });
            }
        }
        public ObservableCollection<string> _StartUsingTime
        {
            get { return UsingTime._FirstStart; }
            set
            {
                UsingTime._FirstStart = value;
                OnPropertyChanged(nameof(_StartUsingTime));
            }
        }
        public ObservableCollection<string> _UsingTime
        {
            get { return UsingTime._Statistic; }
            set
            {
                UsingTime._Statistic = value;
                OnPropertyChanged(nameof(_UsingTime));
            }
        }
        public ObservableCollection<ProcessTime> _AppToday
        {
            get { return AppToday._FavAppToday; }
            set
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    AppToday._FavAppToday = value;
                    OnPropertyChanged(nameof(_AppToday));
                });
            }
        }
        public ObservableCollection<ProcessTime> CheckProcess
        {
            get { return WorksProcesess._WorkProcess; }
            set
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    WorksProcesess._WorkProcess = value;
                    OnPropertyChanged(nameof(CheckProcess));
                });
            }
        }
        
        public HomeVM()
        {
            UsingTime = new UsingPcTimeModel();
            UsingTime.GetStartTimeUsingPcToday();
            UsingTime.GetTimeUsingPcToday();

            RandomAppOne = new FastStatCountStartsAppModelOne();
            RandomAppTwo = new FastStatCountStartsAppModelOne();
            AppHaventLaucnTime = new FastStatLongTimeHaventAppModel();
            AppEver = new FastStatMoreUsingAppEver();
            WorksProcesess = new GetProcessModel();
            AppToday = new FastStatFavoriteAppTodayModel();

            AppHaventLaucnTime.GetLongTimeHaventLauchApp();
            AppEver.GetMoreUsingApp();
            AppToday.GetFavoriteApp();
            RandomAppOne.GetCountStartsRandomApp("one", "name");
            RandomAppTwo.GetCountStartsRandomApp("two", _RandomAppOne[0].NameProcess);

            System.Timers.Timer timerCheckProcesess = new System.Timers.Timer(3000);
            timerCheckProcesess.Elapsed += WorksProcesess.GetProcess;
            timerCheckProcesess.Start();
            CheckProcess = WorksProcesess._WorkProcess;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public GetProcessModel WorksProcesess;
        public FastStatFavoriteAppTodayModel AppToday;
        public FastStatMoreUsingAppEver AppEver;
        public FastStatLongTimeHaventAppModel AppHaventLaucnTime;
        public FastStatCountStartsAppModelOne RandomAppOne;
        public FastStatCountStartsAppModelOne RandomAppTwo;
        public UsingPcTimeModel UsingTime;
    }
}
