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
    public class FastStatLongTimeHaventAppModel : BindableBase, INotifyPropertyChanged
    {
        public FastStatLongTimeHaventAppModel()
        {
            App = _App;
        }
        public ObservableCollection<ProcessTime> _App
        {
            get { return App; }
            set
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    App = value;
                    OnPropertyChanged(nameof(App));
                });
            }
        
        }
            

        public void GetLongTimeHaventLauchApp()
        {
            List<ProcessTime> apps = DateBase.GetInfoProcess("stoped");
            ProcessTime process = new();
            if (apps.Count > 0)
            {
                process = apps.OrderBy(x => DateTime.Parse(x.EndSession)).First();

                process.EndSession = process.EndSession.Substring(0, 16);

                GetProcessModel getProcessModel = new GetProcessModel();
                string icoPath = getProcessModel.GetDirectoryIco(process);
                if (icoPath != null)
                    process.IcoPath = icoPath;
            }
            else
                process.NameProcess = "собираем статистику";
                
            if (_App.Count > 0)
                _App[0] = process;
            else
                _App.Add(process);

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public ObservableCollection<ProcessTime> App = new ObservableCollection<ProcessTime>();
    }
}
