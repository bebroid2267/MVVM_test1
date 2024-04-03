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

        public MainVM()
        {
            AppHaventLaucnTime = new FastStatLongTimeHaventAppModel();
            AppEver = new FastStatMoreUsingAppEver();
            WorksProcesess = new GetProcessModel();
            processes = new ProcessJobsModel();
            AppToday = new FastStatFavoriteAppTodayModel();
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                AppHaventLaucnTime.GetLongTimeHaventLauchApp();
                AppEver.GetMoreUsingApp();
                AppToday.GetFavoriteApp();

                System.Timers.Timer timer = new System.Timers.Timer(5000);
                timer.Elapsed += processes.MonitorProcesess;
                timer.Start();
                Process = processes._RunningProcesess;

                System.Timers.Timer timerCheckProcess = new System.Timers.Timer(3000);
                timerCheckProcess.Elapsed += WorksProcesess.GetProcess;
                timerCheckProcess.Start();
                CheckProcess = WorksProcesess._WorkProcess;
            });
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
    }
}
