using MVVM_test1.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace MVVM_test1.ViewModel
{
    public class AllSoftVM : Utilities.ViewModelBase
    {
        public ObservableCollection<ProcessTime> _Apps
        {
            get { return Apps._Apps; }
            set
            {
                Apps._Apps = value;
                OnPropertyChanged(nameof(_Apps));
            }
        }
        public AllSoftVM() 
        {
            Apps = new AllSoftModel();

            System.Timers.Timer timerCheckProcess = new System.Timers.Timer(3000);
            timerCheckProcess.Elapsed += Apps.GetApps;
            timerCheckProcess.Start();
        }
        public AllSoftModel Apps;
    }
}
