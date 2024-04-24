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
    public class FastStatCountStartsAppModelOne : BindableBase, INotifyPropertyChanged
    {
        public FastStatCountStartsAppModelOne() 
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

        public void GetCountStartsRandomApp(string numberApp, string name)
        {
            ProcessTime randomApp = DateBase.GetRandomApp(numberApp, name, DateTime.Now.ToString("d"));
            GetProcessModel getProcessModel = new GetProcessModel();
            string icoPath = getProcessModel.GetDirectoryIco(randomApp);
            if (icoPath != null)
                randomApp.IcoPath = icoPath;

            if (_App.Count > 0)
                _App[0] = randomApp;
            else
                _App.Add(randomApp);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private ObservableCollection<ProcessTime> App = new ObservableCollection<ProcessTime>();
    }
}
