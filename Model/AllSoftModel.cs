using Microsoft.Scripting.Utils;
using MVVM_test1.DataBase;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_test1.Model
{
    public class AllSoftModel : BindableBase
    {
        public ObservableCollection<ProcessTime> _Apps
        {
            get { return Apps; }
            set
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    Apps = value;
                    OnPropertyChanged(nameof(Apps));
                });
            }
        }
        public string GetDirectoryIco(ProcessTime process)
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
                List<ProcessTime> processes = DateBase.GetInfoProcess("all");

                foreach (ProcessTime process in processes)
                {
                    string icoPath = GetDirectoryIco(process);
                    int index = Apps.FindIndex(p => p.NameProcess == process.NameProcess); // Найти индекс элемента по условию


                    if (index != -1)
                    {
                        Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            if (icoPath != null)
                            {
                                process.IcoPath = icoPath;
                            }
                            _Apps[index] = process; // Обновить элемент по индексу
                        });
                    }
                    else
                    {
                        if (icoPath != null)
                        {
                            process.IcoPath = icoPath;
                        }

                        _Apps.Add(process);
                    }
                }

                foreach (var item in _Apps)
                {
                    int index = processes.FindIndex(p => p.NameProcess == item.NameProcess);
                    if (index == -1)
                    {
                        Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            Apps.Remove(item);

                        });
                    }
                }

                OnPropertyChanged(nameof(Apps)); // Уведомить об изменении коллекции
            });
        }


        public void GetApps(object sender, EventArgs e)
        {
            List<ProcessTime> procesess = DateBase.GetInfoProcess("all");

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
        private ObservableCollection<ProcessTime> Apps = new ObservableCollection<ProcessTime>();
    }
}
