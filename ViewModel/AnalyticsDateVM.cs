using GalaSoft.MvvmLight.Command;
using MVVM_test1.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_test1.ViewModel
{
    public class AnalyticsDateVM : Utilities.ViewModelBase
    {
        public ViewModelBase CurrentVM
        {
            get { return currentVM; }
            set
            {
                currentVM = value;
                OnPropertyChanged(nameof(currentVM));
            }
        }

        public ICommand ShowAllTimesApp { get; set; }

        public AnalyticsDateVM()
        {
            
            ShowAllTimesApp = new RelayCommand<object>(obj => CurrentVM = new AllTimesDateVM());
        }

        private ViewModelBase currentVM;
    }

}
