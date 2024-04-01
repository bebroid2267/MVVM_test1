using Microsoft.Scripting.Utils;
using MVVM_test1.DataBase;
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

namespace MVVM_test1.Model
{
    public class GetProcessModel : BindableBase
    {
        public GetProcessModel() 
        {
            WorksProcess = _WorkProcess;
        }
        public ObservableCollection<ProcessTime> _WorkProcess
        {
            get { return WorksProcess; }
            set
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    
                    WorksProcess = value;
                    OnPropertyChanged(nameof(WorksProcess));
                });
            }
        }

        private void DeleteProcess()
        {
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                List<ProcessTime> processes = DateBase.GetInfoProcess("works");
                foreach (ProcessTime process in processes)
                {
                    int index = WorksProcess.FindIndex(p => p.NameProcess == process.NameProcess); // Найти индекс элемента по условию

                    if (index == -1)
                    {
                        Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            _WorkProcess.Remove(process);
                        });
                    }
                    
                }

                OnPropertyChanged(nameof(WorksProcess)); // Уведомить об изменении коллекции
            });
        }
        private void AddOrChangeProcess()
        {
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                List<ProcessTime> processes = DateBase.GetInfoProcess("works");
                foreach (ProcessTime process in processes)
                {
                    int index = WorksProcess.FindIndex(p => p.NameProcess == process.NameProcess); // Найти индекс элемента по условию

                    if (index != -1)
                    {
                        Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            _WorkProcess[index] = process; // Обновить элемент по индексу
                        });
                    }
                    else
                    {
                        _WorkProcess.Add(process);
                    }
                }

                foreach (var item in _WorkProcess)
                {
                    int index = processes.FindIndex(p => p.NameProcess == item.NameProcess);
                    if (index == -1)
                    {
                        Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            WorksProcess.Remove(item);

                        });
                    }
                }

                OnPropertyChanged(nameof(WorksProcess)); // Уведомить об изменении коллекции
            });
        }


        public void GetProcess(object sender, EventArgs e)
        {
            List<ProcessTime> procesess = DateBase.GetInfoProcess("works");
               
                //Application.Current.Dispatcher.InvokeAsync(() =>
                //{
                //    Task.Run(() => DeleteProcess());
                //});
                if (procesess.Count > 0)
                {
                    Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        Task.Run(() => AddOrChangeProcess());
                    });
                }
                
            
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public ObservableCollection<ProcessTime> WorksProcess = new ObservableCollection<ProcessTime>();
        
    }
}
