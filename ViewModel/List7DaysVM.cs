using GalaSoft.MvvmLight;
using MVVM_test1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_test1.ViewModel
{
    public class List7DaysVM : Utilities.ViewModelBase
    {
        public ObservableCollection<ProcessTime> _Apps
        {
            get { return Apps._WorkProcess; }
            set
            {
                Apps._WorkProcess = value;
                OnPropertyChanged(nameof(_Apps));
            }
        }
        public List7DaysVM(string date)
        {
            Apps = new Last7DaysModel();

            Apps.GetProcess(date);
        }
        
        private Last7DaysModel Apps;
    }
}
