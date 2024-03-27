using Prism.Mvvm;
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
    internal class GetProcessModel : BindableBase
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

        public G

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private ObservableCollection<ProcessTime> WorksProcess;
        
    }
}
