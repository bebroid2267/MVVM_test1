﻿using Microsoft.Scripting.Utils;
using MVVM_test1.DataBase;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
        private string GetDirectoryIco(ProcessTime process)
        {
            string projectDirectory = Directory.GetCurrentDirectory();
            
            string filePath = Path.Combine(projectDirectory, process.NameProcess + ".ico");
            if (File.Exists(filePath))
                return filePath;
            else
                return null;
        }
        
        private void AddOrChangeProcess()
        {
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                List<ProcessTime> processes = DateBase.GetInfoProcess("works");

                foreach (ProcessTime process in processes)
                {
                    string icoPath = GetDirectoryIco(process);
                    int index = WorksProcess.FindIndex(p => p.NameProcess == process.NameProcess); // Найти индекс элемента по условию
                    

                    if (index != -1)
                    {
                        Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            if (icoPath != null)
                            {
                                process.IcoPath = icoPath;
                            }
                            _WorkProcess[index] = process; // Обновить элемент по индексу
                        });
                    }
                    else
                    {
                        if (icoPath != null)
                        {
                            process.IcoPath = icoPath;
                        }
                        
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
