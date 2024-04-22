using MVVM_test1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_test1.ViewModel
{
    public class AllTimesDateVM : Utilities.ViewModelBase
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
        public AllTimesDateVM()
        {
            Apps = new AllTimesDateModel();

            System.Timers.Timer timerCheckProcess = new System.Timers.Timer(2000);
            timerCheckProcess.Elapsed += Apps.GetApps;
            timerCheckProcess.Start();
        }

        private AllTimesDateModel Apps;
    }
}
