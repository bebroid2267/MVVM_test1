using MVVM_test1.DataBase;
using MVVM_test1.Model;
using Prism.Commands;
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
using System.Windows.Input;
using System.Windows.Threading;

namespace MVVM_test1.ViewModel
{
    public class MainVM : BindableBase, INotifyPropertyChanged
    {
        public void ClosingWorksProcesess(object sender, EventArgs e)
        {
            DateBase.StopWorksProcesess();
            DateBase.EndTodayUsingPc(DateTime.Now.ToString("d"));
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
        public ObservableCollection<string> Process
        {

            get { return processes._RunningProcesess; }
            set 
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    processes._RunningProcesess = value;
                    OnPropertyChanged(nameof(Process));
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
        public object _CurrentView
        {
            get { return CurrentView; }
            set
            {
                CurrentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public MainVM()
        {
            UsingTime = new UsingPcTimeModel();
            Task.Run(() => UsingTime.CheckUsingPc());

            RandomAppOne = new FastStatCountStartsAppModelOne();
            RandomAppTwo = new FastStatCountStartsAppModelOne();
            AppHaventLaucnTime = new FastStatLongTimeHaventAppModel();
            AppEver = new FastStatMoreUsingAppEver();
            WorksProcesess = new GetProcessModel();
            processes = new ProcessJobsModel();
            AppToday = new FastStatFavoriteAppTodayModel();
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                DateBase.StartNewDay();
                UsingTime.GetStartTimeUsingPcToday();
                UsingTime.GetTimeUsingPcToday();
                AppHaventLaucnTime.GetLongTimeHaventLauchApp();
                AppEver.GetMoreUsingApp();
                AppToday.GetFavoriteApp();
                RandomAppOne.GetCountStartsRandomApp("one","name");
                RandomAppTwo.GetCountStartsRandomApp("two", _RandomAppOne[0].NameProcess);

                System.Timers.Timer timer = new System.Timers.Timer(5000);
                timer.Elapsed += processes.MonitorProcesess;
                timer.Start();
                Process = processes._RunningProcesess;

                System.Timers.Timer timerCheckProcess = new System.Timers.Timer(3000);
                timerCheckProcess.Elapsed += WorksProcesess.GetProcess;
                timerCheckProcess.Start();
                CheckProcess = WorksProcesess._WorkProcess;
            });
            MainCommand = new Utilities.RelayCommand(Home);
            AllSoftCommand = new Utilities.RelayCommand(AllApps);
            CurrentView = this;
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public GetProcessModel WorksProcesess;
        public ProcessJobsModel processes;
        public FastStatFavoriteAppTodayModel AppToday;
        public FastStatMoreUsingAppEver AppEver;
        public FastStatLongTimeHaventAppModel AppHaventLaucnTime;
        public FastStatCountStartsAppModelOne RandomAppOne;
        public FastStatCountStartsAppModelOne RandomAppTwo;
        public UsingPcTimeModel UsingTime;

        private object CurrentView;
        public ICommand MainCommand {  get; set; }
        public ICommand AllSoftCommand {  get; set; }
        public ICommand FindAppsByNameCommand { get; set; }
        public ICommand AnalytycsByDateCommand { get; set; }

        private void Home(object obj) => CurrentView = new MainVM();
        private void AllApps(object obj) => CurrentView = new AllSoftVM();
        //private void FindApps(object obj) => CurrentView = new Find();
        //private void Home(object obj) => CurrentView = new MainVM();


    }
}
