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

        public void GetProcess(object sender, EventArgs e)
        {
            List<ProcessTime> procesess = DateBase.GetInfoProcess("works");
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                List<ProcessTime> workProcessesCopy = new List<ProcessTime>(_WorkProcess);

                // Перебираем все элементы в _WorkProcess
                foreach (ProcessTime workProcess in workProcessesCopy)
                {
                    // Проверяем, есть ли текущий элемент workProcess в коллекции processes
                    if (!procesess.Any(p => p.NameProcess == workProcess.NameProcess))
                    {
                        // Если элемент не найден в processes, удаляем его из _WorkProcess
                        _WorkProcess.Remove(workProcess);
                    }
                }

                if (procesess.Count > 0)
                { 
                    foreach (ProcessTime process in procesess)
                    {
                        ProcessTime existingProcess = _WorkProcess.FirstOrDefault(p => p.NameProcess == process.NameProcess);
                        if (existingProcess != null)
                        {
                            existingProcess.SumTimeProcess = process.SumTimeProcess;
                            existingProcess.GlobalStartTime = process.GlobalStartTime;

                            int index = _WorkProcess.IndexOf(existingProcess);
                            _WorkProcess[index] = process;
                        }
                        
                        else
                        {
                            _WorkProcess.Add(process);
                        }
                    }
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private ObservableCollection<ProcessTime> WorksProcess = new ObservableCollection<ProcessTime>();
        
    }
}
