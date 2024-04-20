using MVVM_test1.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_test1.Model
{
    public class MiniPageInfoAppModel : Utilities.ViewModelBase
    {
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

        public void GetInfoApp(string nameApp)
        {
            ProcessTime app = DateBase.GetAllInfoApp(nameApp);
            GetProcessModel getProcessModel = new GetProcessModel();
            string icoPath = getProcessModel.GetDirectoryIco(app);
            if (icoPath != null)
                app.IcoPath = icoPath;

            if (_App.Count > 0)
                _App[0] = app;
            else
                _App.Add(app);
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
