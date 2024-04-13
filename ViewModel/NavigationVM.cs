using MVVM_test1.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Transactions;
using System.Windows.Input;

namespace MVVM_test1.ViewModel
{
    class NavigationVM : ViewModelBase
    {

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }
        public ICommand MainCommand { get; set; }
        public ICommand AllSoftCommand { get; set; }
        public ICommand FindAppsByNameCommand { get; set; }
        public ICommand AnalytycsByDateCommand { get; set; }

        private void Home(object obj) => CurrentView = new MainVM();
        private void AllApps(object obj) => CurrentView = new AllSoftVM();

        public NavigationVM()
        {
            MainCommand = new RelayCommand(Home);
            AllSoftCommand = new RelayCommand(AllApps);
            CurrentView = new MainVM();
        }
    }
}
