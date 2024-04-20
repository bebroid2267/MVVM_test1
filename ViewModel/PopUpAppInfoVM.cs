using MVVM_test1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_test1.ViewModel
{
    public class PopUpAppInfoVM : Utilities.ViewModelBase
    {
        public ObservableCollection<ProcessTime> _AppMiniPage
        {
            get { return AppMiniPage._App; }
            set 
            {
                AppMiniPage._App = value;
                OnPropertyChanged(nameof(_AppMiniPage));
            }
        }
        public PopUpAppInfoVM(string nameApp) 
        {
            AppMiniPage = new MiniPageInfoAppModel();
            AppMiniPage.GetInfoApp(nameApp);
        }
        
        public MiniPageInfoAppModel AppMiniPage;
    }
}
