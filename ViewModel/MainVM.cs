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

        

        public MainVM()
        {

            UsingTime = new UsingPcTimeModel();
            processes = new ProcessJobsModel();
            
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                Task.Run(() => UsingTime.CheckUsingPc());

                System.Timers.Timer timer = new System.Timers.Timer(5000);
                timer.Elapsed += processes.MonitorProcesess;
                timer.Start();
                Process = processes._RunningProcesess;

            });
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public UsingPcTimeModel UsingTime;
        public ProcessJobsModel processes;
        
    }
}
