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
using System.Windows.Threading;

namespace MVVM_test1.ViewModel
{
    public class MainVM : BindableBase
    {
        
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
            WorksProcesess = new GetProcessModel();
            processes = new ProcessJobsModel();
            Application.Current.Dispatcher.InvokeAsync( () =>
            {
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
        public GetProcessModel WorksProcesess;
        public ProcessJobsModel processes;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
