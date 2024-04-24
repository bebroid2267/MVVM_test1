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
    public class Last7DaysModel : BindableBase
    {
        public ObservableCollection<ProcessGroup> _Apps
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

        private void AddOrChangeProcess(Dictionary<string, List<ProcessTime>> data)
        {
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                foreach (var key in data.Keys)
                {
                    bool groupExists = Apps.Any(g => g.Key == key);
                    if (!groupExists)
                    {
                        Apps.Add(new ProcessGroup { Key = key, Processes = data[key] });
                    }
                    else
                    {
                        var group = Apps.First(g => g.Key == key);
                        group.Processes = data[key];
                    }
                }

                // Удаление группы, если ее нет в новых данных
                foreach (var group in Apps.ToList())
                {
                    if (!data.ContainsKey(group.Key))
                    {
                        Apps.Remove(group);
                    }
                }

                OnPropertyChanged(nameof(Apps)); // Уведомить об изменении коллекции
            });
        }

        public void GetApps(object sender, EventArgs e)
        {
            Dictionary<string, List<ProcessTime>> data = DateBase.GetLast7daysUsingApps();

            if (data.Count > 0)
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    Task.Run(() => AddOrChangeProcess(data));
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private ObservableCollection<ProcessGroup> Apps = new ObservableCollection<ProcessGroup>();
    }


    public class ProcessGroup : BindableBase
    {
        private string key;
        public string Key
        {
            get { return key; }
            set { SetProperty(ref key, value); }
        }

        private List<ProcessTime> processes;
        public List<ProcessTime> Processes
        {
            get { return processes; }
            set { SetProperty(ref processes, value); }
        }
    }

}
